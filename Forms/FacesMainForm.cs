using System;
using System.Globalization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Data.OleDb;
using System.Xml;
using Microsoft.Win32;
using System.IO;

using BioTechSys.Biometrics;
using BioTechSys.Images;
using BioTechSys.Cameras;
using BioTechSys.Biometrics.Faces;
using BioTechSys.Share;


namespace BioTechSys.FacesEnroll
{
	public class FacesMainForm : System.Windows.Forms.Form
    {

        #region Private Variables

        // This delegate enables asynchronous calls for showing the images 
		// in the program.
		private delegate void GetFrameEventHandler(NImage image);

        private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panel1;

		private FaceCollection _faceCollection;

		private CameraMan cameraMan;
		private Camera _activeDevice;
		private NLExtractor extractor;
		private NMatcher matcher;

		private NleDetectionDetails detectionDetails;
		private NleFace[] faces;
		private int attemptsWhileEnrollingCount;

		private NLTemplate[] generalizationTemplates;
		private int generalizationImageCount = 0;
		private byte[][] featuresArr;

		private int matchingAttemptsCount;
		private int attemptsWhileMatchingCount;

        private Settings settings;

		private static readonly string settingsFileName = "BioTechSys.Faces.settings.xml";
		private DirectoryInfo current;
		private string startDirectory;

		private const string subKey = @"SOFTWARE\BioTechSys\FaceEnrollStation\Main.cpp";

		private Thread getFrameThread = null;
		private bool working = true;

		private IContainer components;
		private BackgroundWorker backgroundWorker;
		private ToolStrip toolStrip;
        private ToolStripButton toolStripButtonExtract;

		ResultsOptionsForm resultsOptionsForm;
		ArrayList matchedImagesArrayList = new ArrayList();
        private BioTechSys.Biometrics.Gui.NLView videoControl;
        private ToolStripSeparator toolStripSeparator1;
        private SplitContainer splitContainer1;
		public TextBox messageBoard;
        private BioMetrics.ListBoxImg.ListBoxImage listBoxImage;
        private Infragistics.Win.UltraWinDock.UltraDockManager DockManager;
        private Infragistics.Win.UltraWinDock.AutoHideControl _MainFormAutoHideControl;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MainFormUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MainFormUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MainFormUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MainFormUnpinnedTabAreaRight;//keeps images for showing in listBoxImage
        private static FacesMainForm _FacesMainForm;
        private ToolStripButton toolStripCamera;
        private ToolStripButton toolStripFile;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripGeneralization;
        private ToolStripButton toolStripSettings;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton toolStripPreview;
		public Job currentJob;
        private ToolStripButton tbtnZoomOut;
        private ToolStripButton tbtnZoomIn;
        public BioTechSys.Face.Enroll.Forms.MainForm _parentForm;

        #endregion

        #region Structs 
        private struct StartData
		{
			public NMatcher matcher;
			public byte[] template;
		}

		private struct ReturnData : IComparable
		{
			public int similarity;
			public string faceID;
			public int ID;

			public int CompareTo(object x)
			{
				ReturnData tmp = (ReturnData)x;
				if (tmp.similarity < this.similarity) return -1;
				if (tmp.similarity == this.similarity) return 0;
				return 1;
			}
		}

		public enum Job
		{
			Empty = 0,
			Enroll = 1,
			EnrollWithGen = 2,
			Match = 3,
			Working = 4
		}
        #endregion

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacesMainForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.videoControl = new BioTechSys.Biometrics.Gui.NLView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.messageBoard = new System.Windows.Forms.TextBox();
            this.DockManager = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._MainFormUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MainFormUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MainFormUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MainFormUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MainFormAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.listBoxImage = new BioMetrics.ListBoxImg.ListBoxImage();
            this.tbtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tbtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripCamera = new System.Windows.Forms.ToolStripButton();
            this.toolStripFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExtract = new System.Windows.Forms.ToolStripButton();
            this.toolStripGeneralization = new System.Windows.Forms.ToolStripButton();
            this.toolStripSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripPreview = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DockManager)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.RestoreDirectory = true;
            // 
            // videoControl
            // 
            this.videoControl.CanPan = true;
            this.videoControl.DetectionDetails = ((BioTechSys.Biometrics.NleDetectionDetails)(resources.GetObject("videoControl.DetectionDetails")));
            this.videoControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoControl.DrawConfidenceForMainFace = true;
            this.videoControl.DrawConfidenceForMainFaceEyes = false;
            this.videoControl.Faces = null;
            this.videoControl.Image = null;
            this.videoControl.Location = new System.Drawing.Point(0, 0);
            this.videoControl.MainFaceColor = System.Drawing.Color.GreenYellow;
            this.videoControl.MainFaceRectangleWidth = 2;
            this.videoControl.MultipleFaceRectangleWidth = 2;
            this.videoControl.MultipleFacesColor = System.Drawing.Color.Yellow;
            this.videoControl.Name = "videoControl";
            this.videoControl.Size = new System.Drawing.Size(900, 485);
            this.videoControl.TabIndex = 0;
            this.videoControl.Zoom = 1F;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 602);
            this.panel1.TabIndex = 16;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCamera,
            this.toolStripFile,
            this.toolStripSeparator3,
            this.toolStripButtonExtract,
            this.toolStripGeneralization,
            this.toolStripSeparator1,
            this.tbtnZoomIn,
            this.tbtnZoomOut,
            this.toolStripSeparator4,
            this.toolStripSettings,
            this.toolStripPreview,
            this.toolStripSeparator2});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(900, 25);
            this.toolStrip.TabIndex = 9;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.videoControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.messageBoard);
            this.splitContainer1.Size = new System.Drawing.Size(900, 602);
            this.splitContainer1.SplitterDistance = 485;
            this.splitContainer1.TabIndex = 17;
            // 
            // messageBoard
            // 
            this.messageBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageBoard.Location = new System.Drawing.Point(0, 0);
            this.messageBoard.Multiline = true;
            this.messageBoard.Name = "messageBoard";
            this.messageBoard.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.messageBoard.Size = new System.Drawing.Size(900, 113);
            this.messageBoard.TabIndex = 9;
            this.messageBoard.WordWrap = false;
            // 
            // DockManager
            // 
            this.DockManager.HostControl = this;
            // 
            // _MainFormUnpinnedTabAreaLeft
            // 
            this._MainFormUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._MainFormUnpinnedTabAreaLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._MainFormUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 25);
            this._MainFormUnpinnedTabAreaLeft.Name = "_MainFormUnpinnedTabAreaLeft";
            this._MainFormUnpinnedTabAreaLeft.Owner = this.DockManager;
            this._MainFormUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 602);
            this._MainFormUnpinnedTabAreaLeft.TabIndex = 18;
            // 
            // _MainFormUnpinnedTabAreaRight
            // 
            this._MainFormUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._MainFormUnpinnedTabAreaRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._MainFormUnpinnedTabAreaRight.Location = new System.Drawing.Point(900, 25);
            this._MainFormUnpinnedTabAreaRight.Name = "_MainFormUnpinnedTabAreaRight";
            this._MainFormUnpinnedTabAreaRight.Owner = this.DockManager;
            this._MainFormUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 602);
            this._MainFormUnpinnedTabAreaRight.TabIndex = 19;
            // 
            // _MainFormUnpinnedTabAreaTop
            // 
            this._MainFormUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._MainFormUnpinnedTabAreaTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._MainFormUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 25);
            this._MainFormUnpinnedTabAreaTop.Name = "_MainFormUnpinnedTabAreaTop";
            this._MainFormUnpinnedTabAreaTop.Owner = this.DockManager;
            this._MainFormUnpinnedTabAreaTop.Size = new System.Drawing.Size(900, 0);
            this._MainFormUnpinnedTabAreaTop.TabIndex = 20;
            // 
            // _MainFormUnpinnedTabAreaBottom
            // 
            this._MainFormUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._MainFormUnpinnedTabAreaBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._MainFormUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 627);
            this._MainFormUnpinnedTabAreaBottom.Name = "_MainFormUnpinnedTabAreaBottom";
            this._MainFormUnpinnedTabAreaBottom.Owner = this.DockManager;
            this._MainFormUnpinnedTabAreaBottom.Size = new System.Drawing.Size(900, 0);
            this._MainFormUnpinnedTabAreaBottom.TabIndex = 21;
            // 
            // _MainFormAutoHideControl
            // 
            this._MainFormAutoHideControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._MainFormAutoHideControl.Location = new System.Drawing.Point(626, 25);
            this._MainFormAutoHideControl.Name = "_MainFormAutoHideControl";
            this._MainFormAutoHideControl.Owner = this.DockManager;
            this._MainFormAutoHideControl.Size = new System.Drawing.Size(253, 602);
            this._MainFormAutoHideControl.TabIndex = 22;
            // 
            // listBoxImage
            // 
            this.listBoxImage.ColumnWidth = 150;
            this.listBoxImage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxImage.FormattingEnabled = true;
            this.listBoxImage.Images = null;
            this.listBoxImage.ItemHeight = 100;
            this.listBoxImage.Location = new System.Drawing.Point(85, 151);
            this.listBoxImage.Name = "listBoxImage";
            this.listBoxImage.Size = new System.Drawing.Size(241, 93);
            this.listBoxImage.TabIndex = 1;
            // 
            // tbtnZoomOut
            // 
            this.tbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnZoomOut.Image = global::BioHuellas.Properties.Resources.ZoomOut;
            this.tbtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnZoomOut.Name = "tbtnZoomOut";
            this.tbtnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tbtnZoomOut.Text = "Zoom Out";
            this.tbtnZoomOut.Click += new System.EventHandler(this.tbtnZoomOut_Click_1);
            // 
            // tbtnZoomIn
            // 
            this.tbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnZoomIn.Image = global::BioHuellas.Properties.Resources.ZoomIn;
            this.tbtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnZoomIn.Name = "tbtnZoomIn";
            this.tbtnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tbtnZoomIn.Text = "Zoom In";
            this.tbtnZoomIn.Click += new System.EventHandler(this.tbtnZoomIn_Click_1);
            // 
            // toolStripCamera
            // 
            this.toolStripCamera.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripCamera.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCamera.Image")));
            this.toolStripCamera.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCamera.Name = "toolStripCamera";
            this.toolStripCamera.Size = new System.Drawing.Size(23, 22);
            this.toolStripCamera.Text = "toolStripButton1";
            this.toolStripCamera.Click += new System.EventHandler(this.toolStripCamera_Click);
            // 
            // toolStripFile
            // 
            this.toolStripFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripFile.Image")));
            this.toolStripFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripFile.Name = "toolStripFile";
            this.toolStripFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripFile.Text = "toolStripButton2";
            this.toolStripFile.Click += new System.EventHandler(this.toolStripFile_Click);
            // 
            // toolStripButtonExtract
            // 
            this.toolStripButtonExtract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonExtract.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.toolStripButtonExtract.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExtract.Image")));
            this.toolStripButtonExtract.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExtract.Name = "toolStripButtonExtract";
            this.toolStripButtonExtract.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonExtract.Text = "Enrrolar";
            this.toolStripButtonExtract.ToolTipText = "Enrrolar";
            this.toolStripButtonExtract.Click += new System.EventHandler(this.menuEnroll_Click);
            // 
            // toolStripGeneralization
            // 
            this.toolStripGeneralization.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripGeneralization.Image = ((System.Drawing.Image)(resources.GetObject("toolStripGeneralization.Image")));
            this.toolStripGeneralization.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripGeneralization.Name = "toolStripGeneralization";
            this.toolStripGeneralization.Size = new System.Drawing.Size(23, 22);
            this.toolStripGeneralization.Text = "toolStripButton1";
            this.toolStripGeneralization.Click += new System.EventHandler(this.toolStripGeneralization_Click);
            // 
            // toolStripSettings
            // 
            this.toolStripSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSettings.Image")));
            this.toolStripSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSettings.Name = "toolStripSettings";
            this.toolStripSettings.Size = new System.Drawing.Size(23, 22);
            this.toolStripSettings.Text = "toolStripButton1";
            this.toolStripSettings.Click += new System.EventHandler(this.toolStripSettings_Click);
            // 
            // toolStripPreview
            // 
            this.toolStripPreview.Checked = true;
            this.toolStripPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripPreview.Image = ((System.Drawing.Image)(resources.GetObject("toolStripPreview.Image")));
            this.toolStripPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripPreview.Name = "toolStripPreview";
            this.toolStripPreview.Size = new System.Drawing.Size(23, 22);
            this.toolStripPreview.Text = "toolStripButton2";
            this.toolStripPreview.Click += new System.EventHandler(this.toolStripPreview_Click);
            // 
            // FacesMainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(900, 627);
            this.Controls.Add(this._MainFormAutoHideControl);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBoxImage);
            this.Controls.Add(this._MainFormUnpinnedTabAreaTop);
            this.Controls.Add(this._MainFormUnpinnedTabAreaBottom);
            this.Controls.Add(this._MainFormUnpinnedTabAreaRight);
            this.Controls.Add(this._MainFormUnpinnedTabAreaLeft);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "FacesMainForm";
            this.Text = "BioTechSys - BioFaces Enroll";
            this.Load += new System.EventHandler(this.Form_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DockManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        public static FacesMainForm GetChildInstance(BioTechSys.Face.Enroll.Forms.MainForm parentForm)
        {

            if (_FacesMainForm == null) //if not created yet, Create an instance
                _FacesMainForm = new FacesMainForm(parentForm);

            return _FacesMainForm;  //just created or created earlier.Return it
        }
        //---------------------- Ohter methods ----------------------
        private void Form_Load(object sender, System.EventArgs e)
        {
            _faceCollection = new FaceCollection(_parentForm);

            Show();

            if (cameraMan != null && cameraMan.Cameras.Count > 0)
            {
                toolStripCamera_Click(this, null);
            }
            else
            {
                toolStripCamera.Checked = false;
                toolStripFile.Checked = true;
            }
        }
		public FacesMainForm( BioTechSys.Face.Enroll.Forms.MainForm parentForm )
		{
			InitializeComponent();

            _parentForm = parentForm;

			try
			{
				cameraMan = new CameraMan(this);
			}
			catch
			{
				cameraMan = null;
                toolStripCamera.Checked = false;
                toolStripFile.Checked = true;
			}

			resultsOptionsForm = new ResultsOptionsForm();

			int height, width;
			ControlPosition.LoadListBox(listBoxImage, out height, out width, subKey);
			listBoxImage.ItemHeight = height;
			listBoxImage.ColumnWidth = width;

			ControlPosition.Load(this, subKey);

			if (this.Size.Height < 200 || this.Size.Width < 200 || this.Location.X < -10 || this.Location.Y < -10)
			{
				this.WindowState = FormWindowState.Maximized;
			}

			current = new DirectoryInfo(Directory.GetCurrentDirectory());

			startDirectory = current.FullName;

            settings = Settings.Load(settingsFileName);

			currentJob = Job.Empty;

			_activeDevice = null;

			detectionDetails = new NleDetectionDetails();

			try
			{
				extractor = new NLExtractor();
				matcher = new NMatcher();
			}
            catch (BioTechSysException ex)
			{
				MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
			}

			SetParameters();
		}

        private void toolStripFile_Click(object sender, EventArgs e)
        {
            if (toolStripFile.Checked)
            {
                return;
            }

            working = false;
            messageBoard.AppendText("Fuente de Imagenes es de Archivos...\r\n");
            while (getFrameThread.IsAlive)
            {
                Application.DoEvents();
            }

            videoControl.Image = null;

            toolStripFile.Checked = true;
            toolStripCamera.Checked = false;
        }

		//---------------------- Source Menu ---------------
        // 1. Capturing frames. Getting frames from camera
        // getFrameThread thread captures frames from camera(device).
        private void toolStripCamera_Click(object sender, EventArgs e)
        {
            if (cameraMan == null)
            {
                toolStripCamera.Checked = false;
                toolStripFile.Checked = true;
                MessageBox.Show("No capture devices found!");
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


            if (_activeDevice == null)
            {

                try
                {
                    deviceForm = new DeviceForm(cameraMan.Cameras, _activeDevice);
                }
                catch (Exception)
                {
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

                    _activeDevice = deviceForm.ActiveDevice;

                    // 1. Capturing frames. Getting frames from camera
                    // getFrameThread thread captures frames from camera(device).
                    getFrameThread = new Thread(new ThreadStart(GetFrames));
                    getFrameThread.Priority = ThreadPriority.Normal;
                    getFrameThread.Start();
                    messageBoard.AppendText(string.Format("Image source is set to: {0}\r\n", _activeDevice.ToString()));
                    toolStripCamera.Checked = true;
                    toolStripFile.Checked = false;
                }
                else
                {
   				    if (_activeDevice == null)
				    {
                        toolStripCamera.Checked = false;
                        toolStripFile.Checked = true;
                    }
                }
            }
        }

		// 2. Capturing frames. The method of getFrameThread thread.
		private void GetFrames()
		{
			try
			{
				if (_activeDevice == null)
				{
					return;
				}
				working = true;
				_activeDevice.StartCapturing();
				_activeDevice.MirrorHorizontal = settings.FlipImage;

				while (working)
				{
					NImage image = _activeDevice.GetCurrentFrame();
					SetCurrentFrame(image);
				}
			}
			finally
			{
				if (_activeDevice != null)
				{
					_activeDevice.StopCapturing();
                    //_activeDevice = null;
				}
			}
		}
		// 3. Capturing frames. Calls SetCurrentImage asynchronously.
		//
		// The method toolStripCamera_Click creates thread getFrameThread. This thread is 
		// getting frames from the camera and shows them in the other thread control VideoControl.
		// The getFrameThread thread can not set frames on the VideoControl directly. 
		// The SetCurrentFrame method creates a GetFrameEventHandler delegate and calls itself
		// asynchronously using the Invoke method.
		private void SetCurrentFrame(NImage image)
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
		// 4. Capturing frames. Shows the frames from on the VideoControl.
		//
		// This method is invoked from getFrameThread tread indirectly(asynchronously) or from file.
		// getFrameThread thread retrieves frames from the camera(device) through 
		// method GetCurrentFrame indirectly.
		private void SetCurrentImage(NImage image)
		{
			NGrayscaleImage grayImage = (NGrayscaleImage)NImage.FromImage(
				NPixelFormat.Grayscale, 0, image);
			Bitmap bm = image.ToBitmap();

			faces = null;
			videoControl.Faces = null;
			detectionDetails.EyesAvailable = false;
			detectionDetails.FaceAvailable = false;

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
					if (toolStripPreview.Checked)
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

            if (toolStripPreview.Checked)
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
		private NleFace[] EmptyJob(NGrayscaleImage grayImage)
		{
			NleFace[] faces = null;
			if (NLExtractor.IsRegistered)
			{
				try
				{
					faces = extractor.DetectFaces(grayImage);
				}
                catch (BioTechSysException ex)
				{
					MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
				}
			}

			return faces;
		}

		//---------------------- Jobs Menu -----------------

		private void menuEnroll_Click(object sender, System.EventArgs e)
		{
			if (!NLExtractor.IsRegistered || !NMatcher.IsRegistered)
			{
                MessageBox.Show("Uno de los componentes de BioTechSys no esta acticado.","BioTechSys",MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				extractor.MaxRecordsPerTemplate = settings.MaxRecordsPerTemplate;
                toolStripFile.Enabled = false;
                toolStripCamera.Enabled = false;
				currentJob = Job.Enroll;

				messageBoard.AppendText(string.Format("\r\nEnrolling...\r\n"));
                if (toolStripFile.Checked)
				{
					ImageFromFile(sender);
				}
			}
		}
        private void toolStripGeneralization_Click(object sender, EventArgs e)
        {
            if (!NLExtractor.IsRegistered || !NMatcher.IsRegistered)
            {
                MessageBox.Show("Uno de los componentes de BioTechSys no esta acticado.", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                extractor.MaxRecordsPerTemplate = settings.MaxRecordsPerTemplate;
                generalizationTemplates = new NLTemplate[settings.GeneralizationImageCount];
                toolStripFile.Enabled = false;
                toolStripCamera.Enabled = false;
                currentJob = Job.EnrollWithGen;
                messageBoard.AppendText("\r\nEnrolando en modo de Generalización...\r\n");
                featuresArr = new byte[settings.GeneralizationImageCount][];

                if (toolStripFile.Checked)
                {
                    ImageFromFile(sender);
                }
            }
        }
		private void Enrollment(NGrayscaleImage grayImage, NImage image)
		{
			NLTemplate template;
			NleExtractionStatus extractionStatus;
			if (IsFaceFeaturesExtracted(grayImage, image, out template, out extractionStatus))
			{
				if (extractionStatus == NleExtractionStatus.TemplateCreated)
				{
					EnrollTemplate(template, image, string.Empty );
				}
				else
				{
					EnrollmentIncomplete(extractionStatus);
				}
			}
		}

		private void EnrollmentComplete()
		{
			currentJob = Job.Empty;
			attemptsWhileEnrollingCount = 0;
			generalizationImageCount = 0;
            toolStripFile.Enabled = true;
            toolStripCamera.Enabled = true;
            messageBoard.AppendText("\r\nEnrolamiento se completo con Exito...\r\n");
		}

        private void EnrollTemplate(NLTemplate faceTemplate, NImage FaceImage, string filename)
		{
			string imageID = string.Empty;
			NLTemplate compressedTemplate;
            
            if (Singleton.lastDudeKey == -1)
            {
                MessageBox.Show("Error Guardando información del Ciudadano", "BioMetrics", MessageBoxButtons.OK, MessageBoxIcon.Error);
                messageBoard.AppendText("El ultimo registro de ciudadano falló, intente otravez.\r\n");
                return;
            }

            compressedTemplate = extractor.Compress(faceTemplate);
            _faceCollection.Add(new FaceRecord(Singleton.NCP,
                                                 compressedTemplate.Save(), Singleton.lastDudeKey), 
                                                 Singleton.lastDudeKey,
                                                 FaceImage,
                                                 DateTime.Now );
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
			if (settings.SaveEnrolledFaceImages)
			{
				if (imagesDirectory.Length != 0)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(imagesDirectoryPath);
					if (!directoryInfo.Exists)
						directoryInfo.Create();
				}

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

		private void EnrollmentWithGeneralization(NGrayscaleImage grayImage, NImage image)
		{
			try
			{
				NLTemplate template;
				NleExtractionStatus extractionStatus;
				if (IsFaceFeaturesExtracted(grayImage, image, out template, out extractionStatus))
				{
					++generalizationImageCount;

					if (extractionStatus == NleExtractionStatus.TemplateCreated)
					{
						generalizationTemplates[generalizationImageCount - 1] = template;

						if (settings.GeneralizationImageCount == generalizationImageCount)
						{
							NLTemplate generalizedTemplate = extractor.Generalize(generalizationTemplates);
							if (generalizedTemplate != null)
							{
								EnrollTemplate(generalizedTemplate, image, string.Empty );
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
						EnrollmentIncomplete(extractionStatus);
					}
				}

				if (settings.GeneralizationImageCount == generalizationImageCount)
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

		private bool IsFaceFeaturesExtracted(NGrayscaleImage grayImage, NImage image, out NLTemplate template, out NleExtractionStatus extractionStatus)
		{
			template = null;
			extractionStatus = NleExtractionStatus.None;

			try
			{
				DateTime firstTime = DateTime.Now;

				if (attemptsWhileEnrollingCount++ == 0)
				{
					extractor.ExtractStart(settings.EnrollStreamLength);
				}

				extractionStatus = extractor.ExtractNext(grayImage, out detectionDetails, out template);

				if (detectionDetails.EyesAvailable || detectionDetails.FaceAvailable)//data for showing
				{
					videoControl.DetectionDetails = detectionDetails;
				}

				string concatenateTime = TimeDiff(firstTime);
				messageBoard.AppendText(string.Format("Frame processed ({0} sec.).\r\n", concatenateTime));

				if (detectionDetails.EyesAvailable)
				{
					messageBoard.AppendText("Eyes available.\r\n");
				}

				if (extractionStatus != NleExtractionStatus.None)
				{
					attemptsWhileEnrollingCount = 0;
					if (extractionStatus == NleExtractionStatus.TemplateCreated)
					{
						messageBoard.AppendText("Face extracted.\r\n");
					}
					else
					{
						if (attemptsWhileEnrollingCount == settings.EnrollStreamLength)
						{
							messageBoard.AppendText("Face extraction failed.\r\n");
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				currentJob = Job.Empty;
				attemptsWhileEnrollingCount = 0;
				generalizationImageCount = 0;
                toolStripFile.Enabled = true;
                toolStripCamera.Enabled = true;
				MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
			}

			return extractionStatus != NleExtractionStatus.None;
		}

		private void GeneralizationStopped()
		{
			generalizationTemplates = null;
			currentJob = Job.Empty;
			generalizationImageCount = 0;
			attemptsWhileEnrollingCount = 0;
            toolStripFile.Enabled = true;
            toolStripCamera.Enabled = true;
		}

		private void menuMatch_Click(object sender, System.EventArgs e)
		{
			listBoxImage.Items.Clear();

			if (!NLExtractor.IsRegistered || !NMatcher.IsRegistered)
			{
                MessageBox.Show("Uno de los componentes de BioTechSys no esta acticado.", "BioTechSys", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			else
			{
				if (_faceCollection.Count == 0)
				{
					currentJob = Job.Empty;
					string msg = "The database is empty!";
					messageBoard.AppendText(string.Format("{0}\r\n", msg));
					MessageBox.Show(msg);
				}
				else
				{
					extractor.MaxRecordsPerTemplate = 1;
					currentJob = Job.Match;
					messageBoard.AppendText("\r\n");
					messageBoard.AppendText("Empatando...\r\n");
					matchingAttemptsCount = 0;
                    if (toolStripFile.Checked)
					{
						ImageFromFile(sender);
					}
				}
			}
		}

		private void Matching(NGrayscaleImage grayImage, Bitmap bm)
		{
			try
			{
				NLTemplate template;

				NleExtractionStatus extractionStatus = NleExtractionStatus.None;

                if (toolStripFile.Checked)
				{
					messageBoard.AppendText("Extrayendo...\r\n");
					template = extractor.Extract(grayImage, out detectionDetails, out extractionStatus);
					matchingAttemptsCount = settings.MatchingAttempts - 1;
					videoControl.DetectionDetails = detectionDetails;
				}
				else
				{
					if (attemptsWhileMatchingCount++ == 0)
					{
						extractor.ExtractStart(settings.MatchingStreamLength);
					}

					messageBoard.AppendText("Extracting...\r\n");
					extractionStatus = extractor.ExtractNext(grayImage, out detectionDetails, out template);

					if (settings.MatchingStreamLength <= attemptsWhileMatchingCount || extractionStatus != NleExtractionStatus.None)
					{
						attemptsWhileMatchingCount = 0;
					}

					if (detectionDetails.EyesAvailable || detectionDetails.FaceAvailable)//data for showing
					{
						videoControl.DetectionDetails = detectionDetails;
					}
				}

				if (extractionStatus != NleExtractionStatus.None)
				{
					matchingAttemptsCount++;
					if (extractionStatus == NleExtractionStatus.TemplateCreated)
					{
						messageBoard.AppendText("Face extracted\r\n");
						PrintMatchingAttempt();
						currentJob = Job.Working;
						attemptsWhileMatchingCount = 0;
                        toolStripCamera.Enabled = false;
						CreateMatchingThread(matcher, template);
					}
					else
					{
						messageBoard.AppendText("Face extraction failed.\r\n");
						PrintMatchingAttempt();
						if (matchingAttemptsCount == settings.MatchingAttempts)
						{
							currentJob = Job.Empty;
							matchingAttemptsCount = 0;
							attemptsWhileMatchingCount = 0;
                            toolStripFile.Enabled = true;
                            toolStripCamera.Enabled = true;
						}
					}
				}
			}
            catch (BioTechSysException extrEx)
			{
				MessageBox.Show(extrEx.Message + ". StackTrace: " + extrEx.StackTrace.ToString());
				attemptsWhileMatchingCount = 0;
			}
		}

		private void CreateMatchingThread(NMatcher matcher, NLTemplate template)
		{
			DateTime matchTime = DateTime.Now;
			StartData sd = new StartData();
			sd.matcher = matcher;
			sd.template = template.Save();
			backgroundWorker.RunWorkerAsync((object)sd);
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			DateTime firstTime;
			string concatenateTime;
			string faceID;
			StartData sd = new StartData();
			sd = (StartData)e.Argument;
			ArrayList retDataArray = new ArrayList();

			try
			{
                //int similarity = 0;
                //byte[] featuresFromDB;

                //firstTime = DateTime.Now;
                //concatenateTime = TimeDiff(firstTime);
                //sd.matcher.IdentifyStart(sd.template);
                //foreach (Face face in _faceCollection)
                //{
                //    featuresFromDB = face.Features;
                //    similarity = sd.matcher.IdentifyNext(featuresFromDB);
                //    ReturnData retData = new ReturnData();
                //    if (worker.CancellationPending)
                //    {
                //        retData.similarity = 0;
                //        e.Cancel = true;
                //        retDataArray.Add(retData);
                //        retDataArray.Sort();
                //        e.Result = (Object)retDataArray;
                //        break;
                //    }

                //    if (similarity >= settings.FAR)
                //    {
                //        faceID = ((Face)face).FaceID;
                //        retData.faceID = faceID;
                //        retData.ID = ((Face)face).ID;
                //        retData.similarity = similarity;
                //        retDataArray.Add(retData);
                //    }
                //}
                //sd.matcher.IdentifyEnd();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
			}
			retDataArray.Sort();
			e.Result = (Object)retDataArray;
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
					messageBoard.AppendText("Trabajo de empate se paro\r\n");
					MatchingEnded();
				}
				else
				{
					ArrayList rd = (ArrayList)e.Result;
					if (rd.Count > 0)
					{
						attemptsWhileMatchingCount = 0;

						messageBoard.AppendText(string.Format("Empate completo exitosamente\r\n"));

                        //for (int i = 0; i < rd.Count; i++)
                        //{
                        //    ReturnData resData = (ReturnData)rd[i];
                        //    ListViewItem listViewData = new ListViewItem();
                        //    listViewData.Text = resData.faceID;
                        //    listViewData.SubItems.Add(resData.similarity.ToString());

                        //    String enrollTime;
                        //    ListBoxImage.CData data = new ListBoxImage.CData();
                        //    Bitmap bm = _faceCollection.GetImageFromDB(resData.ID, out enrollTime);
                        //    if (bm != null)
                        //        data.Image = (Bitmap)bm.Clone();

                        //    //data.Similarity = "Similarity: " + resData.similarity.ToString();
                        //    //data.FaceID = "Face ID from DB: " + resData.faceID;

                        //    matchedImagesArrayList.Add(data);
                        //}
						listBoxImage.Images = new ArrayList(matchedImagesArrayList.ToArray());
						matchedImagesArrayList.Clear();
                        listBoxImage.Refresh();
						MatchingEnded();
					}
					else
					{
						if (settings.MatchingAttempts != matchingAttemptsCount)
						{
							currentJob = Job.Match;
						}
						else
						{
							messageBoard.AppendText("No se encontro al Ciudadano.\r\n");
							MatchingEnded();
						}
					}
				}
			}
		}

		private void PrintMatchingAttempt()
		{
            if (!toolStripFile.Checked)
			{
				messageBoard.AppendText("Matching attempt " + matchingAttemptsCount.ToString() + " of " + settings.MatchingAttempts.ToString() + ".\r\n");
			}
		}

		private void MatchingEnded()
		{
			currentJob = Job.Empty;
			attemptsWhileMatchingCount = 0;
			toolStripFile.Enabled = true;
			toolStripCamera.Enabled = true;
			toolStrip.Enabled = true;
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
			videoControl.Image = (Bitmap)image.ToBitmap().Clone();
			messageBoard.AppendText("Extrallendo...\r\n");

			NleExtractionStatus extractionStatus;
			NLTemplate template = extractor.Extract(grayImage, out detectionDetails, out extractionStatus);
			if (extractionStatus == NleExtractionStatus.TemplateCreated)
			{
				messageBoard.AppendText("Cara Extraida.\r\n");
				EnrollTemplate(template, image, Path.GetFileName(openFileDialog.FileName) );
			}
			else
			{
				messageBoard.AppendText("Extracción de cara falló.\r\n");
			}

			videoControl.DetectionDetails = detectionDetails;

			EnrollmentComplete();
		}

		private void EnrollImageWithGeneralizationFromFile()
		{
			NImage image;
			int selectedFilesCount = openFileDialog.FileNames.Length;
			if (selectedFilesCount == settings.GeneralizationImageCount)
			{
				for (int i = 0; i < selectedFilesCount; i++)
				{
					messageBoard.AppendText(string.Format("Procesando archivo: {0}\r\n", openFileDialog.FileNames[i]));
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
						MessageBox.Show(string.Format("Error abriendo archivo \"{0}\": {1}", fileName, ex.Message + ". StackTrace: " + ex.StackTrace.ToString()),
							Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					float horzResolution = image.HorzResolution;
					float vertResolution = image.VertResolution;

					NGrayscaleImage grayImage = (NGrayscaleImage)NImage.FromImage(NPixelFormat.Grayscale, 0, horzResolution, vertResolution, image);
					int width = (int)image.Width;
					int height = (int)image.Height;
					videoControl.Image = (Bitmap)image.ToBitmap().Clone();
					messageBoard.AppendText("Extrallendo Biometria...\r\n");

					NleExtractionStatus extractionStatus;
					NLTemplate template = extractor.Extract(grayImage, out detectionDetails, out extractionStatus);
					if (extractionStatus == NleExtractionStatus.TemplateCreated)
					{
						messageBoard.AppendText("Biometria Extraidad.\r\n");
						++generalizationImageCount;

						generalizationTemplates[generalizationImageCount - 1] = template;

						if (settings.GeneralizationImageCount == generalizationImageCount)
						{
							NLTemplate generalizedTemplate = extractor.Generalize(generalizationTemplates);
							if (generalizedTemplate != null)
							{
								EnrollTemplate(generalizedTemplate, image, string.Empty );
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
						EnrollmentIncomplete(extractionStatus);
					}

					if (settings.GeneralizationImageCount == generalizationImageCount)
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

		private void EnrollmentIncomplete(NleExtractionStatus extractionStatus)
		{
			messageBoard.AppendText(String.Format("Face extraction failed." + Environment.NewLine));
			switch (extractionStatus)
			{
				case NleExtractionStatus.EyesNotDetected:
					messageBoard.AppendText("Eyes were not detected." + Environment.NewLine);
					break;

				case NleExtractionStatus.FaceNotDetected:
					messageBoard.AppendText("Face was not detected." + Environment.NewLine);
					break;

				case NleExtractionStatus.FaceTooCloseToImageBorder:
					messageBoard.AppendText("Face was too close to image border." + Environment.NewLine);
					break;

				case NleExtractionStatus.LivenessCheckFailed:
					messageBoard.AppendText("Liveness check failed." + Environment.NewLine);
					break;

				case NleExtractionStatus.QualityCheckExposureFailed:
					messageBoard.AppendText("Quality check exposure failure." + Environment.NewLine);
					break;

				case NleExtractionStatus.QualityCheckGrayscaleDensityFailed:
					messageBoard.AppendText("Quality check grayscale density failure." + Environment.NewLine);
					break;

				case NleExtractionStatus.QualityCheckSharpnessFailed:
					messageBoard.AppendText("Quality check sharpness failure." + Environment.NewLine);
					break;
			}

			generalizationTemplates = null;
			currentJob = Job.Empty;
			attemptsWhileEnrollingCount = 0;
			generalizationImageCount = 0;
            toolStripFile.Enabled = true;
            toolStripCamera.Enabled = true;
		}

		private NImage GetImage(OpenFileDialog openFileDialog)
		{
			messageBoard.AppendText(string.Format("Processing file: {0}\r\n", openFileDialog.FileName));
			int formatIndex = openFileDialog.FilterIndex - 2;
			NImageFormat imageFormat =
				formatIndex == -1 || formatIndex == NImageFormat.Formats.Count
				? null : NImageFormat.Formats[formatIndex];
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

		//---------------------- Tools Menu -----------------

		private void menuResultsOptions_Click(object sender, EventArgs e)
		{

			resultsOptionsForm.AreaWidth = listBoxImage.ColumnWidth;
			resultsOptionsForm.AreaHeight = listBoxImage.ItemHeight;

			if (resultsOptionsForm.ShowDialog() == DialogResult.OK)
			{
				listBoxImage.ColumnWidth = resultsOptionsForm.AreaWidth;
				listBoxImage.ItemHeight = resultsOptionsForm.AreaHeight;
				listBoxImage.Refresh();
			}

		}

		//---------------------- Help Menu -----------------

		public string TimeDiff(DateTime firstTime)
		{
			System.TimeSpan diffTime = DateTime.Now.Subtract(firstTime);
			string concatenateTime = diffTime.TotalSeconds.ToString();
			return concatenateTime;
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			settings.Save(settingsFileName);

			ControlPosition.Save(this, subKey);

			ControlPosition.SaveListBox(listBoxImage, subKey);

			currentJob = Job.Empty;

			bool isCancelAsync = false;
			while (backgroundWorker.IsBusy)
			{
				if (!isCancelAsync)
				{
					this.backgroundWorker.CancelAsync();
					isCancelAsync = true;
				}

				Application.DoEvents();
			}

			if (getFrameThread != null && getFrameThread.IsAlive)
			{
				working = false;
				while (getFrameThread.IsAlive)
				{
					Application.DoEvents();
				}
			}
            toolStripFile.Checked = true;

			videoControl.Image = null;

            toolStripPreview.Checked = false;

			if (extractor != null) 
                extractor.Dispose();

			if (matcher != null) 
                matcher.Dispose();

			if (cameraMan != null) 
                cameraMan.Dispose();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		private void SetParameters()
		{
			try
			{
				settings.CopyToExtractor(extractor);
				settings.CopyToMatcher(matcher);

				if (_activeDevice != null)
				{
					_activeDevice.MirrorHorizontal = settings.FlipImage;
				}
			}
            catch (BioTechSysException ex)
			{
				MessageBox.Show(ex.Message + ". StackTrace: " + ex.StackTrace.ToString());
			}
		}

		private void tbtnZoomIn_Click(object sender, EventArgs e)
        {

        }

		private void tbtnZoomOut_Click(object sender, EventArgs e)
        {

        }

        private void toolStripPreview_Click(object sender, EventArgs e)
        {
            if (toolStripPreview.Checked)
            {
                toolStripPreview.Checked = false;
            }
            else
            {
                toolStripPreview.Checked = true;
            }
        }

        private void toolStripStop_Click(object sender, EventArgs e)
        {
            currentJob = Job.Empty;
            attemptsWhileEnrollingCount = 0;
            generalizationImageCount = 0;
            toolStripFile.Enabled = true;
            toolStripCamera.Enabled = true;
        }

        private void toolStripSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.CurrentSettings = settings;
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                settings = settingsForm.CurrentSettings;
                SetParameters();
            }
        }

        private void tbtnZoomIn_Click_1(object sender, EventArgs e)
        {
            videoControl.Zoom *= 2;
        }

        private void tbtnZoomOut_Click_1(object sender, EventArgs e)
        {
            videoControl.Zoom /= 2;
        }
	}
}
