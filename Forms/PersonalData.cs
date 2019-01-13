using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;
using Microsoft.Win32;
using System.Collections;
using System.Globalization;

using BioCaras.CollectionFaces;

using Neurotec.Biometrics;
using Neurotec.Cameras;
using Neurotec.Images;



namespace BioHuellas
{
    public partial class PersonalData : Form
    {
        #region Private Members
        private static PersonalData _PersonalDataform;
        private delegate void GetFrameEventHandler(NImage image);

        string connectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\DataBase\\Biometrics.mdf;Integrated Security=True;User Instance=True";

		private struct StartData
		{
			public VLMatcher matcher;
			public byte[] template;
		}
		public struct ReturnData: IComparable
		{
			public float similarity;
			public string faceID;
			public int ID;

			public int CompareTo(object x)
            {
				ReturnData tmp = (ReturnData)x;

				if (tmp.similarity < this.similarity)  
                    return -1;
				if (tmp.similarity == this.similarity)
                    return  0;

				return 1;
			}
		}
		public enum Job
		{
			Empty = 0,
			Enroll = 1,
			EnrollWithGen = 2,
			Match = 3,
			Working = 4,
            Enrolling = 5,
            Matching = 6,
            MatchingEnded = 7,
            EnrollingEnded = 8
		}

		public Job currentJob;
        public Job currentTask;
        ResultsOptionsForm resultsOptionsForm;
        private static readonly string settingsFileName = "BioCaras.settings.xml";
        private DirectoryInfo current;
        private string startDirectory;
        private FaceCollection faceCollection;

        private CameraMan cameraMan;
        private Camera activeDevice;
        private VLExtractor extractor;
        private VLMatcher matcher;

        private VleDetectionDetails detectionDetails;
        private VleFace[] faces;
        private int imageCountGeneralization = 0;
        private byte[][] featuresArr;
        private byte[][] generalizationTemplates;

        private Thread getFrameThread = null;
        private bool working = true;

        private BackgroundWorker backgroundWorker;
        private MenuItem menuResultsOptions;

        private ArrayList matchedImagesArrayList = new ArrayList();
#endregion


        #region  SettingsForm data
       
            private short attemptsWhileEnrolling;
            private int attemptsWhileEnrollingCount = 0;
            private short attemptsWhileMatching;
            private short attemptsWhileMatchingCount = 0;
            private short minimalIOD;
            private short maximalIOD;
            private float generalizationThreshold;
            private short imageCount;
            private float matchingThreshold;
            private short matchingAttempts;
            private int matchingAttemptsCount = 0;
            private short maxMatchingResults = 10;
            private bool flipImage;
            private bool fileNameAsRecordID;
            private bool saveEnrolledFaceImages;

        #endregion

        public PersonalData()
        {
            InitializeComponent();

            try
            {
                cameraMan = new CameraMan(this);
            }
            catch
            {
                cameraMan = null;
                menuDevice.Checked = false;
                menuFile.Checked = true;
            }

            resultsOptionsForm = new ResultsOptionsForm();

            if (this.Size.Height < 200 || this.Size.Width < 200 || this.Location.X < -10 || this.Location.Y < -10)
            {
                this.WindowState = FormWindowState.Maximized;
                this.SetBounds(200, 200, 200, 200);
            }

            current = new DirectoryInfo(Directory.GetCurrentDirectory());
            startDirectory = current.FullName;

            LoadSettings();

            currentJob = Job.Empty;

            videoControl.SetBounds(0, 0, 0, 0);

            activeDevice = null;

            detectionDetails = new VleDetectionDetails();

            try
            {
                extractor = new VLExtractor();
                matcher = new VLMatcher();
            }
            catch (Neurotec.NeurotecException ex)
            {
                MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
            }
            //faceCollection = new FaceCollection(VLAccessConnection, this);

            SetParameters();

        }

        public static PersonalData GetChildInstance()
        {

            if (_PersonalDataform == null) //if not created yet, Create an instance
                _PersonalDataform = new PersonalData();

            return _PersonalDataform;  //just created or created earlier.Return it
        }

        private void fACEBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
        }

        private void PersonalData_Load(object sender, EventArgs e)
        {
      
            if (cameraMan != null && cameraMan.Cameras.Count > 0)
            {
                menuDevice_Click(this, null);
            }
            else
            {
                menuDevice.Checked = false;
                menuFile.Checked = true;
            }
        }

        #region Settings Locad Save Set Parameters

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(settingsFileName))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(settingsFileName);

                    XmlElement settingsElement = xmlDocument.DocumentElement;
                    if (settingsElement.Name != "Settings") throw new FormatException("Settings element not found");

                    //Face detection
                    XmlNode XMLfaceDetection = settingsElement.ChildNodes[0];
                    if (XMLfaceDetection.Name != "FaceDetection") throw new FormatException("FaceDetection element not found");

                    XmlNode XMLAttemtsWhileEnrolling = XMLfaceDetection.ChildNodes[0];
                    if (XMLAttemtsWhileEnrolling.Name != "AttemptsWhileEnrolling") throw new FormatException("AttemptsWhileEnrolling element not found");
                    attemptsWhileEnrolling = short.Parse(XMLAttemtsWhileEnrolling.InnerText);

                    XmlNode XMLAttemtsWhileMatching = XMLfaceDetection.ChildNodes[1];
                    if (XMLAttemtsWhileMatching.Name != "AttemptsWhileMatching") throw new FormatException("AttemptsWhileMatching element not found");
                    attemptsWhileMatching = short.Parse(XMLAttemtsWhileMatching.InnerText);

                    XmlNode XMLMinimalIOD = XMLfaceDetection.ChildNodes[2];
                    if (XMLMinimalIOD.Name != "MinimalIOD") throw new FormatException("XMLMinimalIOD element not found");
                    minimalIOD = short.Parse(XMLMinimalIOD.InnerText);

                    XmlNode XMLMaximalIOD = XMLfaceDetection.ChildNodes[3];
                    if (XMLMaximalIOD.Name != "MaximalIOD") throw new FormatException("XMLMaximalIOD element not found");
                    maximalIOD = short.Parse(XMLMaximalIOD.InnerText);

                    // Enrollment with generalization
                    XmlNode XMLEnrollmentWithGeneralization = settingsElement.ChildNodes[1];
                    if (XMLEnrollmentWithGeneralization.Name != "EnrollmentWithGeneralization") throw new FormatException("EnrollmentWithGeneralization element not found");

                    XmlNode XMLGeneralizationThreshold = XMLEnrollmentWithGeneralization.ChildNodes[0];
                    if (XMLGeneralizationThreshold.Name != "GeneralizationThreshold") throw new FormatException("GeneralizationThreshold element not found");
                    generalizationThreshold = float.Parse(XMLGeneralizationThreshold.InnerText.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

                    XmlNode XMLImageCount = XMLEnrollmentWithGeneralization.ChildNodes[1];
                    if (XMLImageCount.Name != "ImageCount") throw new FormatException("ImageCount element not found");
                    imageCount = short.Parse(XMLImageCount.InnerText);

                    // Matching
                    XmlNode XMLMatching = settingsElement.ChildNodes[2];
                    if (XMLMatching.Name != "Matching") throw new FormatException("Matching element not found");

                    XmlNode XMLMatchingThreshold = XMLMatching.ChildNodes[0];
                    if (XMLMatchingThreshold.Name != "MatchingThreshold") throw new FormatException("MatchingThreshold element not found");
                    matchingThreshold = float.Parse(XMLMatchingThreshold.InnerText.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

                    XmlNode XMLMatchingAttempts = XMLMatching.ChildNodes[1];
                    if (XMLMatchingAttempts.Name != "MatchingAttempts") throw new FormatException("MatchingAttempts element not found");
                    matchingAttempts = short.Parse(XMLMatchingAttempts.InnerText);

                    XmlNode XMLMatchingResults = XMLMatching.ChildNodes[2];
                    if (XMLMatchingResults.Name != "MatchingResults") throw new FormatException("MatchingResults element not found");
                    maxMatchingResults = short.Parse(XMLMatchingResults.InnerText);

                    //Misc.
                    XmlNode XMLMisc = settingsElement.ChildNodes[3];
                    if (XMLMisc.Name != "Misc") throw new FormatException("Misc element not found");

                    XmlNode XMLFlipImage = XMLMisc.ChildNodes[0];
                    if (XMLFlipImage.Name != "FlipImage") throw new FormatException("FlipImage element not found");
                    flipImage = bool.Parse(XMLFlipImage.InnerText);

                    XmlNode XMLFileNameAsRecordID = XMLMisc.ChildNodes[1];
                    if (XMLFileNameAsRecordID.Name != "FileNameAsRecordID") throw new FormatException("FileNameAsRecordID element not found");
                    fileNameAsRecordID = bool.Parse(XMLFileNameAsRecordID.InnerText);

                    XmlNode XMLSaveEnrolledFaceImages = XMLMisc.ChildNodes[2];
                    if (XMLSaveEnrolledFaceImages.Name != "SaveEnrolledFaceImages") throw new FormatException("SaveEnrolledFaceImages element not found");
                    saveEnrolledFaceImages = bool.Parse(XMLSaveEnrolledFaceImages.InnerText);
                }
                else
                {
                    MessageBox.Show("File " + settingsFileName + " does not found. Using default settings.");
                    SetDefaultSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error loading settings information:\n {0} \n Settings set by default", ex.Message));
                SetDefaultSettings();
            }
        }

        private void SaveSettings()
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlElement settingsElement = xmlDocument.CreateElement("Settings");

            //Face detection
            XmlElement XMLFaceDetection = xmlDocument.CreateElement("FaceDetection");

            XmlElement XMLAttemptsWhileEnrolling = xmlDocument.CreateElement("AttemptsWhileEnrolling");
            XMLAttemptsWhileEnrolling.InnerText = attemptsWhileEnrolling.ToString();
            XMLFaceDetection.AppendChild(XMLAttemptsWhileEnrolling);

            XmlElement XMLAttemptsWhileMatching = xmlDocument.CreateElement("AttemptsWhileMatching");
            XMLAttemptsWhileMatching.InnerText = attemptsWhileMatching.ToString();
            XMLFaceDetection.AppendChild(XMLAttemptsWhileMatching);

            XmlElement XMLMinimalIOD = xmlDocument.CreateElement("MinimalIOD");
            XMLMinimalIOD.InnerText = minimalIOD.ToString();
            XMLFaceDetection.AppendChild(XMLMinimalIOD);

            XmlElement XMLMaximalIOD = xmlDocument.CreateElement("MaximalIOD");
            XMLMaximalIOD.InnerText = maximalIOD.ToString();
            XMLFaceDetection.AppendChild(XMLMaximalIOD);

            settingsElement.AppendChild(XMLFaceDetection);

            //Enrollment with generalization
            XmlElement XMLEnrollmentWithGeneralization = xmlDocument.CreateElement("EnrollmentWithGeneralization");

            XmlElement XMLGeneralizationThreshold = xmlDocument.CreateElement("GeneralizationThreshold");
            XMLGeneralizationThreshold.InnerText = generalizationThreshold.ToString(CultureInfo.InvariantCulture);
            XMLEnrollmentWithGeneralization.AppendChild(XMLGeneralizationThreshold);

            XmlElement XMLImageCount = xmlDocument.CreateElement("ImageCount");
            XMLImageCount.InnerText = imageCount.ToString();
            XMLEnrollmentWithGeneralization.AppendChild(XMLImageCount);

            settingsElement.AppendChild(XMLEnrollmentWithGeneralization);

            //Matching
            XmlElement XMLMatching = xmlDocument.CreateElement("Matching");

            XmlElement XMLMatchingThreshold = xmlDocument.CreateElement("MatchingThreshold");
            XMLMatchingThreshold.InnerText = matchingThreshold.ToString(CultureInfo.InvariantCulture);
            XMLMatching.AppendChild(XMLMatchingThreshold);

            XmlElement XMLMatchingAttempts = xmlDocument.CreateElement("MatchingAttempts");
            XMLMatchingAttempts.InnerText = matchingAttempts.ToString();
            XMLMatching.AppendChild(XMLMatchingAttempts);

            XmlElement XMLMatchingResults = xmlDocument.CreateElement("MatchingResults");
            XMLMatchingResults.InnerText = maxMatchingResults.ToString();
            XMLMatching.AppendChild(XMLMatchingResults);

            settingsElement.AppendChild(XMLMatching);

            //Misc.
            XmlElement XMLMisc = xmlDocument.CreateElement("Misc");

            XmlElement XMLFlipImage = xmlDocument.CreateElement("FlipImage");
            XMLFlipImage.InnerText = flipImage.ToString();
            XMLMisc.AppendChild(XMLFlipImage);

            XmlElement XMLFileNameAsRecordID = xmlDocument.CreateElement("FileNameAsRecordID");
            XMLFileNameAsRecordID.InnerText = fileNameAsRecordID.ToString();
            XMLMisc.AppendChild(XMLFileNameAsRecordID);

            XmlElement XMLSaveEnrolledFaceImages = xmlDocument.CreateElement("SaveEnrolledFaceImages");
            XMLSaveEnrolledFaceImages.InnerText = saveEnrolledFaceImages.ToString();
            XMLMisc.AppendChild(XMLSaveEnrolledFaceImages);

            settingsElement.AppendChild(XMLMisc);

            xmlDocument.AppendChild(settingsElement);

            xmlDocument.Save(startDirectory + "\\" + settingsFileName);
        }

        private void SetDefaultSettings()
        {
            attemptsWhileEnrolling = 10;
            attemptsWhileMatching = 3;
            minimalIOD = 40;
            maximalIOD = 3000;
            generalizationThreshold = 0.625f;
            imageCount = 3;
            matchingThreshold = 0.650f;
            matchingAttempts = 10;
            maxMatchingResults = 10;
            flipImage = false;
            fileNameAsRecordID = true;
            saveEnrolledFaceImages = true;
        }

#endregion


        private void SetParameters()
        {
            try
            {
                extractor.MinIod = minimalIOD;
                extractor.MaxIod = maximalIOD;

                if (activeDevice != null)
                {
                    activeDevice.MirrorHorizontal = flipImage;
                }
            }
            catch (Neurotec.NeurotecException ex)
            {
                MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
            }
        }


        // 1. Capturing frames. Getting frames from camera
        // getFrameThread thread captures frames from camera(device).
        private void menuDevice_Click(object sender, System.EventArgs e)
        {
            if (cameraMan == null)
            {
                menuDevice.Checked = false;
                menuFile.Checked = true;
                MessageBox.Show("No se Encontraron Camaras para capturar imagenes!");
                return;
            }

            if (getFrameThread != null && getFrameThread.IsAlive)
            {
                working = false;
                while (getFrameThread.IsAlive)
                {
                    Application.DoEvents();
                }
            }
            DeviceForm deviceForm = null;

            try
            {
                deviceForm = new DeviceForm(cameraMan.Cameras, activeDevice);
            }
            catch (Neurotec.NeurotecException ex)
            {
                MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
            }

            if ((deviceForm != null) && (deviceForm.ShowDialog() == DialogResult.OK))
            {
                if (getFrameThread != null && getFrameThread.IsAlive)
                {
                    working = false;
                    while (getFrameThread.IsAlive)
                    {
                        Application.DoEvents();
                    }
                }

                activeDevice = deviceForm.ActiveDevice;

                // 1. Capturing frames. Getting frames from camera
                // getFrameThread thread captures frames from camera(device).
                getFrameThread = new Thread(new ThreadStart(GetFrames));
                getFrameThread.Priority = ThreadPriority.BelowNormal;
                getFrameThread.Start();
                messageBoard.AppendText(string.Format("La Fuente de Imagemes es : {0}\r\n", activeDevice.ToString()));
                menuDevice.Checked = true;
                menuFile.Checked = false;
            }
            else
            {
                if (activeDevice == null)
                {
                    menuDevice.Checked = false;
                    menuFile.Checked = true;
                }
            }
        }

        // 2. Capturing frames. The method of getFrameThread thread.
        private void GetFrames()
        {
            try
            {
                if (activeDevice == null)
                {
                    return;
                }
                working = true;
                activeDevice.StartCapturing();
                activeDevice.MirrorHorizontal = flipImage;

                while (working)
                {
                    NImage image = activeDevice.GetCurrentFrame();
                    SetCurrentFrame(image);
                }
            }
            finally
            {
                if (activeDevice != null)
                {
                    activeDevice.StopCapturing();
                    activeDevice = null;
                }
            }
        }

        // 3. Capturing frames. Calls SetCurrentImage asynchronously.
        //
        // The method menuDevice_Click creates thread getFrameThread. This thread is 
        // getting frames from the camera and shows them in the other thread control VideoControl.
        // The getFrameThread thread can not set frames on the VideoControl directly. 
        // The SetCurrentFrame method creates a GetFrameEventHandler delegate and calls itself
        // asynchronously using the Invoke method.
        private void SetCurrentFrame(NImage image)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    GetFrameEventHandler getFrameEventHandler = new GetFrameEventHandler(SetCurrentFrame);
                    this.Invoke(getFrameEventHandler, new object[] { image });
                }
                else
                {	// This case is accessed only after call this.Invoke from the main thread, 
                    // not from getFrameThread thread.
                    SetCurrentImage(image);
                }
            }
            catch (Neurotec.NeurotecException extrEx)
            {
                MessageBox.Show(extrEx.Message + ". StackTrace: " + extrEx.StackTrace.ToString());
                throw;
            }

        }

        // 4. Capturing frames. Shows the frames from on the VideoControl.
        //
        // This method is invoked from getFrameThread tread indirectly(asynchronously) or from file.
        // getFrameThread thread retrieves frames from the camera(device) through 
        // method GetCurrentFrame indirectly.
        private void SetCurrentImage(NImage image)
        {
            NGrayscaleImage grayImage = (NGrayscaleImage)NImage.FromImage(NPixelFormat.Grayscale, 0, image);
            Bitmap bm = image.ToBitmap();

            faces = null;
            videoControl.Faces = null;
            detectionDetails.EyesAvailable = false;
            detectionDetails.FaceAvailable = false;

            videoControl.SetBounds(0, 0, (int)image.Width, (int)image.Height);
            videoControl.Image = new Bitmap(bm);

            switch (currentJob)
            {
                case Job.Match:
                    Matching(grayImage, bm);
                    break;
                case Job.Enroll:
                    Enrollment(grayImage, image);
                    break;
                case Job.EnrollWithGen:
                    EnrollmentWithGeneralization(grayImage, image);
                    break;
                case Job.Empty:
                    if (menuPreview.Checked)
                    {
                        faces = EmptyJob(grayImage);
                    }
                    break;
                case Job.Working:
                    break;
                default:
                    MessageBox.Show("This type " + currentJob.ToString() + " of job do not exist");
                    break;
            }

            if (menuPreview.Checked)
            {
                videoControl.DetectionDetails = detectionDetails;
                if (faces != null && faces.Length != 0)
                {
                    if (currentJob != Job.Enroll)
                    {
                        videoControl.Faces = faces;
                    }
                }
                else
                {
                    faces = null;
                    videoControl.Faces = null;
                }
            }

            image.Dispose();
            grayImage.Dispose();
            bm.Dispose();
        }

        private VleFace[] EmptyJob(NGrayscaleImage grayImage)
        {
            VleFace[] faces = null;
            if (VLExtractor.IsRegistered)
            {
                try
                {
                    faces = extractor.DetectFaces(grayImage);
                }
                catch (Neurotec.NeurotecException ex)
                {
                    MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
                }
            }

            return faces;
        }


        private void Matching(NGrayscaleImage grayImage, Bitmap bm)
        {

            bool stopped = false;
            try
            {
                byte[] template;

                if (menuFile.Checked)
                {
                    messageBoard.AppendText("Extrayendo Biometricas de Cara...\r\n");
                    template = extractor.Extract(grayImage, out detectionDetails);
                    stopped = true;
                    matchingAttemptsCount = matchingAttempts - 1;
                    //fACE_IMAGEPictureBox.Image = new Bitmap(bm);
                    videoControl.DetectionDetails = detectionDetails;
                }
                else
                {
                    if (attemptsWhileMatchingCount++ == 0)
                    {
                        extractor.ExtractStart(attemptsWhileMatching);
                    }

                    messageBoard.AppendText("Extrayendo Biometricas de Cara...\r\n");
                    stopped = extractor.ExtractNext(grayImage, out detectionDetails, out template);

                    if (attemptsWhileMatching <= attemptsWhileMatchingCount || stopped)
                    {
                        attemptsWhileMatchingCount = 0;
                    }

                    if (detectionDetails.EyesAvailable || detectionDetails.FaceAvailable)//data for showing
                    {
                        //fACE_IMAGEPictureBox.Image = (Bitmap)bm.Clone();
                        videoControl.DetectionDetails = detectionDetails;
                    }
                }

                if (stopped)
                {
                    matchingAttemptsCount++;
                    if (template != null)
                    {
                        messageBoard.AppendText("Biometricas de la cara extraida\r\n");
                        PrintMatchingAttempt();
                        currentJob = Job.Working;
                        attemptsWhileMatchingCount = 0;
                        menuItemJob.Enabled = false;
                        menuDevice.Enabled = false;
                        CreateMatchingThread(matcher, template);
                    }
                    else
                    {
                        messageBoard.AppendText("Extraccion de Biometricas de Cara Falló.\r\n");
                        PrintMatchingAttempt();
                        if (matchingAttemptsCount == matchingAttempts)
                        {
                            currentJob = Job.Empty;
                            matchingAttemptsCount = 0;
                            attemptsWhileMatchingCount = 0;
                            menuItemJob.Enabled = true;
                            menuFile.Enabled = true;
                            menuDevice.Enabled = true;
                        }
                    }
                }
            }
            catch (Neurotec.NeurotecException extrEx)
            {
                MessageBox.Show(extrEx.Message + ". StackTrace: " + extrEx.StackTrace.ToString());
                attemptsWhileMatchingCount = 0;
            }
        }

        private void GeneralizationStopped()
        {
            generalizationTemplates = null;
            currentJob = Job.Empty;
            imageCountGeneralization = 0;
            attemptsWhileEnrollingCount = 0;
            menuItemJob.Enabled = true;
            menuFile.Enabled = true;
            menuDevice.Enabled = true;
        }

        private void menuMatch_Click(object sender, System.EventArgs e)
        {
            matchedImagesArrayList.Clear();

            if (!VLExtractor.IsRegistered || !VLMatcher.IsRegistered)
            {
                MessageBox.Show("Uno de los componentes de BioCaras no esta activado...");
            }
            else
            {
                if (faceCollection.Count == 0)
                {
                    currentJob = Job.Empty;
                    string msg = "La Base de datos esta vacia...";
                    messageBoard.AppendText(string.Format("{0}\r\n", msg));
                }
                else
                {
                    menuItemJob.Enabled = false;
                    currentJob = Job.Match;
                    messageBoard.AppendText("\r\n");
                    messageBoard.AppendText("Buscando Biometrica de caras para empatar...\r\n");
                    matchingAttemptsCount = 0;

                    if (menuFile.Checked)
                    {
                        ImageFromFile(sender);
                    }
                }
            }
        }

        private void CreateMatchingThread(VLMatcher matcher, byte[] template)
        {
            DateTime matchTime = DateTime.Now;
            StartData sd = new StartData();
            // set Data for parmeter
            sd.matcher = matcher;
            sd.template = template;
            // call Thread and pass the StartData Structure
            backgroundWorker.RunWorkerAsync((object)sd);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            DateTime firstTime;
            string concatenateTime;
            string faceID;
            StartData sd = new StartData();

            //cast the incomming argument into the proper type
            sd = (StartData)e.Argument;

            //ReturnData retData = new ReturnData();
            ArrayList retDataArray = new ArrayList();

            try
            {
                double similarity = 0;
                byte[] featuresFromDB;

                //double biggestSimilarity = 0;
                firstTime = DateTime.Now;
                concatenateTime = TimeDiff(firstTime);
                sd.matcher.IdentifyStart(sd.template);
                foreach (Face face in faceCollection)
                {
                    featuresFromDB = face.Features;
                    similarity = sd.matcher.IdentifyNext(featuresFromDB);
                    ReturnData retData = new ReturnData();

                    if (worker.CancellationPending)
                    {
                        retData.similarity = 0;
                        e.Cancel = true;
                        retDataArray.Sort();
                        e.Result = (Object)retDataArray.Add(retData);
                        break;
                    }

                    // If we find a face with the Bio that maches the incoming
                    // add it to the Array
                    if (similarity >= matchingThreshold)
                    {
                        faceID = ((Face)face).FaceID;
                        retData.faceID = faceID;
                        retData.ID = ((Face)face).ID;
                        retData.similarity = (float)similarity * 100f;
                        retDataArray.Add(retData);
                    }
                }
                sd.matcher.IdentifyEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
            }
            // es el Arrray con todas las caras que se encontraron
            retDataArray.Sort();
            e.Result = (Object)retDataArray; // cast it back to an object 
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MatchingEnded();
                MessageBox.Show(e.Error.Message + ". StackTrace: " + e.Error.StackTrace.ToString());
            }
            else
            {
                if (e.Cancelled)
                {
                    messageBoard.AppendText("Empate  Parado\r\n");
                    MatchingEnded();
                }
                else
                {
                    ArrayList rd = (ArrayList)e.Result;

                    if (rd.Count > 0)
                    {
                        attemptsWhileMatchingCount = 0;

                        messageBoard.AppendText(string.Format("Empte de Biometrica de Cara Completado exitosamente\r\n"));
                        //Wintbs ChildTabs = new Wintbs();
                        bool FinedPerson = false;

                        // If we found any bio Matching Create Tabs to display results;
                        for (int i = 0; (i < rd.Count) && (i < maxMatchingResults); i++)
                        {
                            // Call BeginUpdate to prevent the display from 
                            // refreshing as we add individual tabs. 
                            // Note: This MUST be paired with a call to 
                            // EndUpdate below. 
                            //this._TabControl.BeginUpdate();

                            ReturnData resData = (ReturnData)rd[i];
                            ListViewItem listViewData = new ListViewItem();
                            listViewData.Text = resData.faceID;
                            listViewData.SubItems.Add("HelloWorld"); //resData.similarity.ToString());
                            //CreateTabs(this._TabControl, resData);
                            //ChildTabs.CreateTabs(this._TabControl, resData, GetImageFromDB(resData.ID, out enrollTime));
                            FinedPerson = true;

                            //ListBoxImage.CData data = new ListBoxImage.CData();
                            //Bitmap bm = GetImageFromDB(resData.ID, out enrollTime);
                            //if (bm != null)
                            //    data.Image = (Bitmap)bm.Clone();

                            //data.Similarity = "Similarity: " + resData.similarity.ToString();
                            //data.FaceID = "Face ID from DB: " + resData.faceID;

                            //matchedImagesArrayList.Add(data);
                        }
                        //listBoxImage.Images = matchedImagesArrayList;
                        if (FinedPerson == true)
                        {
                            // Call EndUpdate to allow the display to refresh 
                            //this._TabControl.EndUpdate();

                            currentJob = Job.Empty;
                            MessageBox.Show("Se encontraron Registros de la persona en la base de Datos", "BioCaras Enroll");
                        }
                        MatchingEnded();
                    }
                    else
                    {
                        if (matchingAttempts != matchingAttemptsCount)
                        {
                            currentJob = Job.Match;
                        }
                        else
                        {
                            messageBoard.AppendText("Perona No encontrada, continuando Erollment...\r\n");
                            currentJob = Job.Enroll;
                            MatchingEnded();
                        }
                    }
                }
            }
        }

        private void PrintMatchingAttempt()
        {
            if (!menuFile.Checked)
            {
                messageBoard.AppendText("Matching attempt " + matchingAttemptsCount.ToString() + " of " + matchingAttempts.ToString() + ".\r\n");
            }
        }

        private void MatchingEnded()
        {
            //currentJob = Job.Enroll;
            attemptsWhileMatchingCount = 0;
            menuItemJob.Enabled = true;
            menuFile.Enabled = true;
            menuDevice.Enabled = true;
        }

        private void ImageFromFile(object sender)
        {
            try
            {
                openFileDialog.Filter = NImages.GetOpenFileFilterString(true, true);
                openFileDialog.FileName = null;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    NImage image;

                    switch (currentJob)
                    {
                        case Job.Match:
                            image = GetImage(openFileDialog);
                            SetCurrentImage(image);
                            break;
                        case Job.Enroll:
                            EnrollImageFromFile();
                            break;
                        case Job.EnrollWithGen:
                            EnrollImageWithGeneralizationFromFile();
                            break;
                        default:
                            MessageBox.Show("This type " + currentJob.ToString() + " of job do not exist");
                            break;
                    }
                }
                else
                {
                    EnrollmentComplete();
                }
                openFileDialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());

                EnrollmentComplete();
            }
        }

        private void EnrollImageFromFile()
        {
            NImage image = GetImage(openFileDialog);

            NGrayscaleImage grayImage = (NGrayscaleImage)NImage.FromImage(NPixelFormat.Grayscale, 0, image);

            int width = (int)image.Width;
            int height = (int)image.Height;
            videoControl.SetBounds(0, 0, width, height);
            //videoControlRight.SetBounds(0, 0, width, height);
            videoControl.Image = (Bitmap)image.ToBitmap().Clone();
            //fACE_IMAGEPictureBox.Image = (Bitmap)image.ToBitmap().Clone();
            messageBoard.AppendText("Extrayendo Bio de Cara...\r\n");
            byte[] template = extractor.Extract(grayImage, out detectionDetails);
            if (template != null)
            {
                messageBoard.AppendText("Face extracted.\r\n");
                EnrollTemplate(template, image, Path.GetFileName(openFileDialog.FileName), DateTime.Now);
            }
            else
            {
                messageBoard.AppendText("Enrool - Extracción de Cara Fallo.\r\n");
            }

            videoControl.DetectionDetails = detectionDetails;
            //videoControlRight.DetectionDetails = detectionDetails;

            EnrollmentComplete();
        }

        private void EnrollImageWithGeneralizationFromFile()
        {
            NImage image;
            int selectedFilesCount = openFileDialog.FileNames.Length;
            if (selectedFilesCount == imageCount)
            {
                for (int i = 0; i < selectedFilesCount; i++)
                {
                    messageBoard.AppendText(string.Format("Processing file: {0}\r\n", openFileDialog.FileNames[i]));
                    int formatIndex = openFileDialog.FilterIndex - 2;
                    NImageFormat imageFormat =
                        formatIndex == -1 || formatIndex == NImageFormat.Formats.Count
                        ? null : NImageFormat.Formats[formatIndex];
                    string fileName = openFileDialog.FileNames[i];

                    try
                    {
                        image = NImage.FromFile(fileName, imageFormat);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error opening file \"{0}\": {1}", fileName, ex.Message + ". StackTrace: " + ex.StackTrace.ToString()),
                            Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    float horzResolution = image.HorzResolution;
                    float vertResolution = image.VertResolution;

                    NGrayscaleImage grayImage = (NGrayscaleImage)NImage.FromImage(NPixelFormat.Grayscale, 0, horzResolution, vertResolution, image);

                    int width = (int)image.Width;
                    int height = (int)image.Height;
                    videoControl.SetBounds(0, 0, width, height);
                    //videoControlRight.SetBounds(0, 0, width, height);
                    videoControl.Image = (Bitmap)image.ToBitmap().Clone();
                    //fACE_IMAGEPictureBox.Image = (Bitmap)image.ToBitmap().Clone();
                    messageBoard.AppendText("Extrayendo Bio de Cara...\r\n");

                    byte[] template = extractor.Extract(grayImage, out detectionDetails);
                    if (template != null)
                    {
                        messageBoard.AppendText("Features extracted.\r\n");
                        ++imageCountGeneralization;

                        generalizationTemplates[imageCountGeneralization - 1] = template;

                        if (imageCount == imageCountGeneralization)
                        {
                            byte[] generalizedTemplate = extractor.Generalize(generalizationTemplates);
                            if (generalizedTemplate != null && generalizedTemplate.Length > 0)
                            {
                                EnrollTemplate(generalizedTemplate, image, string.Empty, DateTime.Now);
                                messageBoard.AppendText("Generalization succeeded \r\n");
                            }
                            else
                            {
                                messageBoard.AppendText("Generalization failed \r\n");
                            }
                        }
                    }
                    else
                    {
                        EnrollmentIncomplete();
                    }

                    if (imageCount == imageCountGeneralization)
                    {
                        GeneralizationStopped();
                    }
                }
            }
            else
            {
                messageBoard.AppendText("Wrong nuber of files selected!\r\n");
                EnrollmentComplete();
            }
        }

        private void EnrollmentIncomplete()
        {
            messageBoard.AppendText(String.Format("Face extraction failed.\r\n"));

            generalizationTemplates = null;
            currentJob = Job.Empty;
            attemptsWhileEnrollingCount = 0;
            imageCountGeneralization = 0;
            menuItemJob.Enabled = true;
            menuFile.Enabled = true;
            menuDevice.Enabled = true;
        }
        private void EnrollmentComplete()
        {
            currentJob = Job.Empty;
            attemptsWhileEnrollingCount = 0;
            imageCountGeneralization = 0;
            menuItemJob.Enabled = true;
            menuFile.Enabled = true;
            menuDevice.Enabled = true;
        }

        private void EnrollTemplate(byte[] template, NImage image, string filename, DateTime enrollTime)
        {
            string imageID = string.Empty;
            byte[] compressedTemplate;

            if (filename == string.Empty || !fileNameAsRecordID)//(enroll from camera) || (enroll from file and !fileNameAsRecordID)
            {
                //EnrollForm enrollForm = new EnrollForm();
                //if (enrollForm.ShowDialog() == DialogResult.OK)
                //{
                //    imageID = enrollForm.ImageID;

                //    compressedTemplate = extractor.Compress(template);
                //    faceCollection.Add(new Face(imageID, compressedTemplate), image, enrollTime);

                //    if (saveEnrolledFaceImages)
                //    {
                //        SaveImage(image, imageID);
                //    }
                //}
            }
            else//enroll from file and fileNameAsRecordID
            {
                imageID = filename;

                compressedTemplate = extractor.Compress(template);
                faceCollection.Add(new Face(imageID, compressedTemplate), image, enrollTime);

                if (saveEnrolledFaceImages)
                {
                    SaveImage(image, imageID);
                }
            }
            EnrollmentComplete();
        }
        private void SaveImage(NImage image, string imageID)
        {
            if (imageID != string.Empty)
            {
                SaveImageToFile(image, imageID, "Images\\");
            }
            else
            {
                MessageBox.Show("Image not saved to file. File name is empty.\r\n");
            }
        }

        private void SaveImageToFile(NImage image, string fileName, string imagesDirectory)
        {
            string imagesDirectoryPath = startDirectory + "\\" + imagesDirectory;
            if (saveEnrolledFaceImages)
            {
                if (imagesDirectory.Length != 0)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(imagesDirectoryPath);
                    if (!directoryInfo.Exists)
                        directoryInfo.Create();
                }

                if (saveEnrolledFaceImages)
                {
                    if (Path.GetExtension(fileName) == string.Empty)
                    {
                        image.Save(imagesDirectoryPath + fileName + ".png");
                    }
                    else
                    {
                        image.Save(imagesDirectoryPath + fileName);
                    }
                }
            }
        }

        private NImage GetImage(OpenFileDialog openFileDialog)
        {
            messageBoard.AppendText(string.Format("Processing file: {0}\r\n", openFileDialog.FileName));
            int formatIndex = openFileDialog.FilterIndex - 2;
            NImageFormat imageFormat = formatIndex == -1 || formatIndex == NImageFormat.Formats.Count ? null : NImageFormat.Formats[formatIndex];
            string fileName = openFileDialog.FileName;

            NImage image;

            try
            {
                image = NImage.FromFile(fileName, imageFormat);

                return image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error opening file \"{0}\": {1}", fileName, ex.Message + ". StackTrace: " + ex.StackTrace.ToString()),
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void Enrollment(NGrayscaleImage grayImage, NImage image)
        {
            byte[] template;
            if (IsFaceFeaturesExtracted(grayImage, image, out template))
            {
                if (template != null)
                {
                    EnrollTemplate(template, image, string.Empty, DateTime.Now);
                }
                else
                {
                    EnrollmentIncomplete();
                }
            }
        }

        private void EnrollmentWithGeneralization(NGrayscaleImage grayImage, NImage image)
        {
            try
            {
                byte[] template;
                if (IsFaceFeaturesExtracted(grayImage, image, out template))
                {
                    ++imageCountGeneralization;

                    if (template != null)
                    {
                        generalizationTemplates[imageCountGeneralization - 1] = template;

                        if (imageCount == imageCountGeneralization)
                        {
                            byte[] generalizedTemplate = extractor.Generalize(generalizationTemplates);
                            if (generalizedTemplate != null && generalizedTemplate.Length > 0)
                            {
                                EnrollTemplate(generalizedTemplate, image, string.Empty, DateTime.Now);
                                messageBoard.AppendText("Generalization succeeded \r\n");
                            }
                            else
                            {
                                messageBoard.AppendText("Generalization failed \r\n");
                            }
                        }
                    }
                    else
                    {
                        EnrollmentIncomplete();
                    }
                }

                if (imageCount == imageCountGeneralization)
                {
                    GeneralizationStopped();
                }
            }
            catch (Exception ex)
            {
                GeneralizationStopped();
                MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
            }
        }

        private bool IsFaceFeaturesExtracted(NGrayscaleImage grayImage, NImage image, out byte[] template)
        {
            bool stopped = false;
            template = null;
            try
            {
                DateTime firstTime = DateTime.Now;

                // first we need to extract the Bio of face.
                if (attemptsWhileEnrollingCount++ == 0)
                {
                    extractor.ExtractStart(attemptsWhileEnrolling);
                }

                stopped = extractor.ExtractNext(grayImage, out detectionDetails, out template);

                if (detectionDetails.EyesAvailable || detectionDetails.FaceAvailable)//data for showing
                {
                    videoControl.DetectionDetails = detectionDetails;
                    //fACE_IMAGEPictureBox.Image = (Bitmap)image.ToBitmap().Clone();
                    //eNROLL_TIMETextBox.Text = DateTime.Now.ToString();
                }

                string concatenateTime = TimeDiff(firstTime);

                messageBoard.AppendText(string.Format("Frame processed ({0} sec.).\r\n", concatenateTime));

                if (detectionDetails.EyesAvailable)
                {
                    messageBoard.AppendText("Eyes available.\r\n");
                }

                if (stopped)
                {
                    attemptsWhileEnrollingCount = 0;
                    if (template != null)
                    {
                        messageBoard.AppendText("Face extracted.\r\n");
                    }
                    else
                    {
                        if (attemptsWhileEnrollingCount == attemptsWhileEnrolling)
                        {
                            //_StatusBar.Text = "Face extraction failed.\r\n";
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                currentJob = Job.Empty;
                attemptsWhileEnrollingCount = 0;
                imageCountGeneralization = 0;
                menuItemJob.Enabled = true;
                menuFile.Enabled = true;
                menuDevice.Enabled = true;
                MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
            }

            return stopped;
        }
 
        public string TimeDiff(DateTime firstTime)
        {
            System.TimeSpan diffTime = DateTime.Now.Subtract(firstTime);
            string concatenateTime = diffTime.TotalSeconds.ToString();
            return concatenateTime;
        }

        private void menuPreview_Click(object sender, System.EventArgs e)
        {
            if (menuPreview.Checked)
            {
                menuPreview.Checked = false;
            }
            else
            {
                menuPreview.Checked = true;
            }
        }

        private void menuClearLog_Click(object sender, System.EventArgs e)
        {
            messageBoard.Clear();
            matchedImagesArrayList.Clear();
        }
        private void menuSettings_Click(object sender, System.EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.AttemptsWhileEnrolling = attemptsWhileEnrolling;
            settingsForm.AttemptsWhileMatching = attemptsWhileMatching;
            settingsForm.MinimalIOD = minimalIOD;
            settingsForm.MaximalIOD = maximalIOD;
            settingsForm.GeneralizationThreshold = generalizationThreshold;
            settingsForm.ImageCount = imageCount;
            settingsForm.MatchingThreshold = matchingThreshold;
            settingsForm.MatchingAttempts = matchingAttempts;
            settingsForm.MaxMatchingResults = maxMatchingResults;
            settingsForm.flipImage.Checked = flipImage;
            settingsForm.fileNameAsRecordID.Checked = fileNameAsRecordID;
            settingsForm.saveEnrolledFaceImages.Checked = saveEnrolledFaceImages;

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                attemptsWhileEnrolling = settingsForm.AttemptsWhileEnrolling;
                attemptsWhileMatching = settingsForm.AttemptsWhileMatching;
                minimalIOD = settingsForm.MinimalIOD;
                maximalIOD = settingsForm.MaximalIOD;
                generalizationThreshold = settingsForm.GeneralizationThreshold;
                imageCount = settingsForm.ImageCount;
                matchingThreshold = settingsForm.MatchingThreshold;
                matchingAttempts = settingsForm.MatchingAttempts;
                maxMatchingResults = settingsForm.MaxMatchingResults;
                flipImage = settingsForm.flipImage.Checked;
                fileNameAsRecordID = settingsForm.fileNameAsRecordID.Checked;
                saveEnrolledFaceImages = settingsForm.saveEnrolledFaceImages.Checked;
                SetParameters();
            }
        }

        //---------------------- Jobs Menu -----------------

        private void menuEnroll_Click(object sender, System.EventArgs e)
        {
            if (!VLExtractor.IsRegistered || !VLMatcher.IsRegistered)
            {
                MessageBox.Show("Uno de los Componentes de BioMetrics no esta activado");
            }
            else
            {
                menuItemJob.Enabled = false;
                menuFile.Enabled = false;
                menuDevice.Enabled = false;
                currentJob = Job.Enroll;

                messageBoard.AppendText(string.Format("\r\n Inscribiendo persona...\r\n"));
                if (menuFile.Checked)
                {
                    ImageFromFile(sender);
                }
            }
        }

        private void menuEnrollWithGen_Click(object sender, System.EventArgs e)
        {
            if (!VLExtractor.IsRegistered || !VLMatcher.IsRegistered)
            {
                MessageBox.Show("One of VeriLook components is not activated");
            }
            else
            {
                generalizationTemplates = new byte[imageCount][];
                menuItemJob.Enabled = false;
                menuFile.Enabled = false;
                menuDevice.Enabled = false;
                currentJob = Job.EnrollWithGen;
                messageBoard.AppendText("\r\n Registrando persona con Generalización...\r\n");
                featuresArr = new byte[imageCount][];

                if (menuFile.Checked)
                {
                    ImageFromFile(sender);
                }
            }
        }
    }
}