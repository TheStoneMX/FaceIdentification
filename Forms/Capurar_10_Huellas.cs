using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BioTechSys.Biometrics;
using BioHuellas.Properties;
using System.Diagnostics;
using System.Threading;
using BioHuellas.Forms.BioCaras;
using BioAntecedentes;
using BiometricsBLCommon;

using BioTechSys.Biometrics.Gui;
using BioTechSys.Images;
using BioHuellas.Forms;
using BioHuellas.Code;
using BioTechSys.CrossMatch;
using BioTechSys.AnsiNist;
using BiometricsBLServer;
//using BioTechSys.Full.Station;

using BioTechSysNistAlert.HelperCode;


namespace BioHuellas
{
    public partial class Capurar_10_Huellas : Form
    {
        #region Class Private Variables

        public enum MatchListType { Full, Half, Minimal };
        public enum MatchPair { None = 0, Genuine = 1, Impostor = 2, Any = Genuine | Impostor };
        public Int64 _lastDudeKey = -1;
        private USB4XX _scanner;
        private static Capurar_10_Huellas _CapturarHuellaform;
        private const int resWith = 512;
        private const int resHeight = 512;
        private byte[] _template;
        private bool[] _fingersCapture;
        private NMatcher _matcher = new NMatcher();

        #endregion
          private bool mTryAgianOnFailure;
          private int mTryAgainDelayTime;

        public bool TryAgianOnFailure 
        {
          set { mTryAgianOnFailure = value; }
          get { return mTryAgianOnFailure; } 
        }

        public int TryAgainDelayTime
        {
            set { mTryAgainDelayTime = value; }
            get { return mTryAgainDelayTime; }
        }

        public Capurar_10_Huellas()
        {
            InitializeComponent();
            InitBioHuellas();
            InitScanner();
        }
       
        #region BioHuellas Init
         private void InitScanner()
        {
            _scanner = new USB4XX();
            _scanner.IniScanner(resWith, resHeight, IntPtr.Zero);
        }
         void InitBioHuellas()
        {
            Settings settings = Settings.Default;

            Data.NFExtractor = new NFExtractor();
            Data.UpdateNfe();
            Data.UpdateNfeSettings();

            Data.NMatcher = new NMatcher();
            Data.UpdateNfm();
            Data.UpdateNMSettings();

            settings.Save();

            Data.Database = new Database();

            _fingersCapture = new bool[10];
             //
            _bntNew.Enabled = false;

        }

        #endregion

        public static Capurar_10_Huellas GetChildInstance()
        {

            if (_CapturarHuellaform == null) //if not created yet, Create an instance
                _CapturarHuellaform = new Capurar_10_Huellas();

            return _CapturarHuellaform;  //just created or created earlier.Return it
        }
 
        public bool ProcessFinger()
        {
            NFRecord record;

            if (_template == null)
                return false;

            record = new NFRecord(_template);
            Settings settings = Settings.Default;
            List<MatchingResult> results;
            TimeSpan matchTime, seTime;
            Int64 matchedCount, countThreshold;

            if (settings.SearchForDuplicates == true)
            {
                /*MatchingResult?*/
                results = Identify(_template, false, false, out matchedCount, out countThreshold,
                                    out results, out matchTime, out seTime);

                // here we found a candidate in the DB
                if (results.Count > 0)
                {
                    dudeHuellasFound foundDude = new dudeHuellasFound(results);
                    foundDude.Text = "Persona(s) Encontrada(s)";
                    foundDude.MdiParent = MdiParent;
                    foundDude.Show();
                    return false;
                }
            }
                return true;
        }

        private List<MatchingResult> Identify(byte[] template, bool matchAll, bool showAllResults,
                                              out Int64 matchedCount,
                                              out Int64 countThreshold,
                                              out List<MatchingResult> results,
                                              out TimeSpan matchTime, out TimeSpan seTime)
        {
            Settings settings = Settings.Default;
            bool useG = settings.FPUseG;
            Int64 recordCount = Data.Database.Records.Count;

            countThreshold = useG ? (recordCount * settings.FPGPercent + 99) / 100 : recordCount;

            IEnumerable<HuellaRecord> recordEnumerable = useG ? Data.Database.Records.GetEnumerator(template) : Data.Database.Records;

            results = new List<MatchingResult>();
            int bestScore = 0;
            MatchingResult? bestResult = null;
            matchedCount = 0;
            Stopwatch sw = new Stopwatch();
            Stopwatch sesw = Stopwatch.StartNew();
            NMMatchDetails matchDetails;

            try
            {
                Data.NMatcher.IdentifyStart(template, out matchDetails);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Error starting identification: {0}", ex.Message));
            }

            sesw.Stop();

            foreach (HuellaRecord record in recordEnumerable)
            {
                if (matchedCount >= countThreshold)
                    break;

                int score;
                sw.Start();

                try
                {
                    score = Data.NMatcher.IdentifyNext(record.Template, matchDetails);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(string.Format("Error identifying: {0}", ex.Message));
                }

                sw.Stop();

                matchedCount++;
                MatchingResult? result = null;

                if (score != 0 || showAllResults)
                {
                    result = new MatchingResult(record, score, matchDetails);
                    results.Add(result.Value);
                }

                if (score != 0)
                {
                    if (bestScore < score)
                    {
                        bestScore = score;
                        bestResult = result;
                    }
                }
            }
            sesw.Start();
            try
            {
                Data.NMatcher.IdentifyEnd();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Error ending identification: {0}", ex.Message));
            }
            sesw.Stop();
            matchTime = sw.Elapsed;
            seTime = sesw.Elapsed;
            return results;
        }
        
        private void menuEnrollmentOptions_Click(object sender, EventArgs e)
        {
            EnrollmentOptionsForm form = new EnrollmentOptionsForm();
            form.ShowDialog();
        }

        private void _bntNew_Click(object sender, EventArgs e)
        {
            _rightPulgar.Image = null;
            _rightIndice.Image = null;
            _rightMedio.Image = null;
            _rightAnular.Image = null;
            _rightMeñique.Image = null;
            //////////////////////////////////
            _leftPulgar.Image = null;
            _leftIndice.Image = null;
            _leftMedio.Image = null;
            _leftAnular.Image = null;
            _leftMeñique.Image = null;

            // here after we process all the images and ready to save it to DB
            // we call _btnEnrollCanditate_Click to save dude's personal info.l
			if (!NLExtractor.IsRegistered || !NMatcher.IsRegistered)
			{
                MessageBox.Show("Uno de los componentes de BioTechSys no esta acticado.", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // here after we process all the images and ready to save it to DB
            // we call _btnEnrollCanditate_Click to save dude's personal info.l
            Singleton.lastDudeKey = SaveDudesInfo();

            //now lets save the Finger Prints...
            SaveFingerPrints(Singleton.lastDudeKey);

            // setup the smtp component
            Settings settings = Settings.Default;

            if (bioTechSysSmtpMail == null)
                bioTechSysSmtpMail = new BioTechSys.Mail.Smtp.BioTechSysSmtpMail();

            bioTechSysSmtpMail.MailTo = settings.snsp_toEmail;
            bioTechSysSmtpMail.MailFrom = settings.snsp_userEmail;
            bioTechSysSmtpMail.MailSubject = Singleton.NCP;
            ////
            bioTechSysSmtpMail.SMTPPassword = settings.snsp_password;
            bioTechSysSmtpMail.SMTPUsername = settings.snsp_userEmail;
            bioTechSysSmtpMail.SMTPServer = settings.snsp_toIPAddress;
            bioTechSysSmtpMail.SMTPPort = Convert.ToInt16(settings.snsp_smtpPort);


            // here we go and create the nist and send it to the SNSP
            NistHelper Nist = new NistHelper();
            Nist.CreateSendNist();
            // allocate the space for the attachment and attach it...
            bioTechSysSmtpMail.MailAttachments = new string[1];
            bioTechSysSmtpMail.MailAttachments[0] = string.Format("..\\Nists\\{0}.nist", Singleton.NCP);
            bioTechSysSmtpMail.Send();
            bioTechSysSmtpMail.Dispose();
            bioTechSysSmtpMail = null;

            // now we go back to the first Tab
            MainForm form = (MainForm)this.MdiParent;
            form.SelectSNSPWindow();
        }
        private Int64 SaveDudesInfo()
        {
            Int64 dudesKey = -1;

            CiudadanoManager mgr = new CiudadanoManager();
            dudesKey = mgr.InsertCiudadanInfo();
            return dudesKey;
        }
        private void SaveFingerPrints(Int64 lastID)
        {
            for (int i = 0; i < Singleton.FingerData.Length -1; i++)
            {
                try
                {
                    if (Singleton.FingerData[i] != null)
                        Data.Database.Records.Add(lastID, Singleton.FingerData[i].FingerName,
                                                          Singleton.FingerData[i].FingerTemplate,
                                                          Singleton.FingerData[i].FingerImage.ToBitmap() );
                }
                catch (Exception ex)
                {
                    string e = ex.Message;
                    continue;
                }
            }
        }
        private void Capurar_10_Huellas_FormClosing(object sender, FormClosingEventArgs e)
        {
            _scanner.UnIniScanner();
            _matcher.Dispose();
        }

        #region Capture Fingersprint Images

        private NImage CaptureFinger(out byte[] Template)
        {
            byte[] tmpTemplate = null;
            NImage tempBmp = null;
            Template = tmpTemplate;

            Settings settings = Settings.Default;
            ScanFinger scann = new ScanFinger(settings.FPMinimalMinutiaCount);

            if ((scann.ShowDialog() == DialogResult.OK) && (scann.FingerPrintImage != null))
            {
                try
                {
                    // read image
                    tempBmp = scann.FingerPrintImage;
                    _template = scann.template;

                    if (_template == null)
                    {
                        MessageBox.Show("Extracción de Minutias Falló", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    else
                        Template = _template;
                }
                catch (Exception ex)
                {
                    string x = ex.Message;
                }

                // here we look for a mathing finger in our DB
                if (ProcessFinger())
                return tempBmp;
                else
                    return null;
            }
            else
                return null;
        }
        private void _leftPulgar_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null && template != null)
            {
                if (Check4ScannedFinger(template, 5) != false)
                {
                    Singleton.FingerData[5] = new FingersData(x, template, "leftThumb");
                    _leftPulgar.Image = x.ToBitmap();
                    _fingersCapture[5] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _leftIndice_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null && template != null)
            {
                if( Check4ScannedFinger(template, 6) != false)
                {
                    Singleton.FingerData[6] = new FingersData(x, template, "leftIndex");
                    _leftIndice.Image = x.ToBitmap();
                    _fingersCapture[6] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _leftMedio_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null && template != null)
            {
                if (Check4ScannedFinger(template, 7) != false)
                {
                    Singleton.FingerData[7] = new FingersData(x, template, "leftMiddle");
                    _leftMedio.Image = x.ToBitmap();
                    _fingersCapture[7] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _leftAnular_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null && template != null)
            {
                if (Check4ScannedFinger(template, 8) != false)
                {
                    Singleton.FingerData[8] = new FingersData(x, template, "leftRing");
                    _leftAnular.Image = x.ToBitmap();
                    _fingersCapture[8] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _leftMeñique_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null && template != null)
            {
                if (Check4ScannedFinger(template, 9) != false)
                {
                    Singleton.FingerData[9] = new FingersData(x, template, "leftLittle");
                    _leftMeñique.Image = x.ToBitmap();
                    _fingersCapture[9] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _rightPulgar_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null && template != null)
            {
                if (Check4ScannedFinger(template, 0) != false)
                {
                    Singleton.FingerData[0] = new FingersData(x, template, "rightThumb");
                    _rightPulgar.Image = x.ToBitmap();
                    _fingersCapture[0] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _rightIndice_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null && template != null)
            {
                if (Check4ScannedFinger(template, 1) != false)
                {
                    Singleton.FingerData[1] = new FingersData(x, template, "rightIndex");
                    _rightIndice.Image = x.ToBitmap();
                    _fingersCapture[1] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _rightMedio_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null || template != null)
            {
                if (Check4ScannedFinger(template, 2) != false)
                {
                    Singleton.FingerData[2] = new FingersData(x, template, "rightMiddle");
                    _rightMedio.Image = x.ToBitmap();
                    _fingersCapture[2] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _rightAnular_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null || template != null)
            {
                if (Check4ScannedFinger(template, 3) != false)
                {
                    Singleton.FingerData[3] = new FingersData(x, template, "rightRing");
                    _rightAnular.Image = x.ToBitmap();
                    _fingersCapture[3] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _rightMeñique_DoubleClick(object sender, EventArgs e)
        {
            byte[] template;
            NImage x = CaptureFinger(out template);
            if (x != null || template != null)
            {
                if (Check4ScannedFinger(template, 4) != false)
                {
                    Singleton.FingerData[4] = new FingersData(x, template, "rightLittle");
                    _rightMeñique.Image = x.ToBitmap();
                    _fingersCapture[4] = true;
                    CheckEnableSendBtn();
                }
            }
            else
                MessageBox.Show("Error Capturando Huella, Intente otra vez", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Finger Previews

        private void _rightMeñique_Click(object sender, EventArgs e)
        {
            rightFingerPriview.Image = _rightMeñique.Image;
        }
        private void _rightAnular_Click(object sender, EventArgs e)
        {
            rightFingerPriview.Image = _rightAnular.Image;
        }
        private void _rightMedio_Click(object sender, EventArgs e)
        {
            rightFingerPriview.Image = _rightMedio.Image;
        }
        private void _rightIndice_Click(object sender, EventArgs e)
        {
            rightFingerPriview.Image = _rightIndice.Image;
        }
        private void _rightPulgar_Click(object sender, EventArgs e)
        {
            rightFingerPriview.Image = _rightPulgar.Image;
        }
        private void _leftPulgar_Click(object sender, EventArgs e)
        {
            leftFingerPreview.Image = _leftPulgar.Image;
        }

        private void _leftIndice_Click(object sender, EventArgs e)
        {
            leftFingerPreview.Image = _leftIndice.Image;
        }

        private void _leftMedio_Click(object sender, EventArgs e)
        {
            leftFingerPreview.Image = _leftMedio.Image;
        }

        private void _leftAnular_Click(object sender, EventArgs e)
        {
            leftFingerPreview.Image = _leftAnular.Image;
        }

        private void _leftMeñique_Click(object sender, EventArgs e)
        {
            leftFingerPreview.Image = _leftMeñique.Image;
        }
        #endregion

        private void toolStripSNSProp_Click(object sender, EventArgs e)
        {
            SnspPropForm form = new SnspPropForm();
            form.ShowDialog();
        }
        private void CheckEnableSendBtn()
        {
            for (int i = 0; i < _fingersCapture.Length; i++)
            {
                if (_fingersCapture[i] == true)
                    continue;
                else
                    return;
            }
            _bntNew.Enabled = true;
        }
        private bool Check4ScannedFinger( byte[] plantilla, int dedo )
        {
            int score;
            _matcher.IdentifyStart(plantilla);

            for (int i = 0; i < Singleton.FingerData.Length; i++)
            {
                if ((Singleton.FingerData[i] != null) && (dedo != i))
                {
                    if ((score = _matcher.IdentifyNext(Singleton.FingerData[i].FingerTemplate)) > 0)
                    {
                        MessageBox.Show(string.Format("Este Dedo {0} ya ha sido escaneado, por favor corrija su error", Singleton.FingerData[i].FingerName),
                                                      "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _matcher.IdentifyEnd();
                        return false;
                    }
                }
            }
            _matcher.IdentifyEnd();
            return true;
        }
    }
}