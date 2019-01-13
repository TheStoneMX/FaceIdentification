using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BioHuellas.Properties;
using BioTechSys;
using BioTechSys.Biometrics;
using BioTechSys.AnsiNist;
using BioTechSys.Share;

using Infragistics.Win;
using Infragistics.Win.UltraWinStatusBar;
using Infragistics.Win.UltraWinProgressBar;
using BioTechSys.FacesEnroll;


namespace BioTechSys.Face.Enroll.Forms
{
    public partial class MainForm : Form
    {
        #region Private Fields

        private snsp _snspForm = null;
        public FacesMainForm _FacesMainForm;
        private UltraStatusPanel panel;
        private UltraStatusPanel _StatusTextPanel = new UltraStatusPanel();
        private static Singleton _singleton;

        private Int64 _LastInsertedID;
        public Int64 LastInsertedID
        {
            get { return _LastInsertedID; }
            set { _LastInsertedID = value; }
        }
        #endregion


        public MainForm()
        {
            InitializeComponent();
            InitInfragisticsStatusBar();
            _singleton = Singleton.Instance;
        }

        #region InitInfragisticsStatusBar

        private void InitInfragisticsStatusBar()
        {

            // Note: Under windows XP if the ‘SupportThemes‘ property 
            // is left is True (its default setting) then some of 
            // the explicit appearance, border style and button 
            // style properties are ignored. 
            this._StatusBar.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;

            // Set the border style for the status bar control 
            this._StatusBar.BorderStyle = UIElementBorderStyle.InsetSoft;

            // Set the default border style for panels 
            this._StatusBar.BorderStylePanel = UIElementBorderStyle.InsetSoft;

            // Set the style for button type panels 
            this._StatusBar.ButtonStyle = UIElementButtonStyle.PopupSoftBorderless;

            // Set the # of pixels between panels  
            this._StatusBar.InterPanelSpacing = 10;

            // Specify the margins inside the status bar control.             
            this._StatusBar.Padding = new UIElementMargins(2, 1, 1, 2);

            // Set some apperance setting for the control 
            this._StatusBar.Appearance.BackGradientStyle = GradientStyle.VerticalBump;
            this._StatusBar.Appearance.BackColor = Color.Blue;
            this._StatusBar.Appearance.BackColor2 = Color.Aqua;

            // Set the default appearance for panels 
            this._StatusBar.PanelAppearance.BackColor = Color.Transparent;

            // Set some additional properties on the control 
            this._StatusBar.PanelsVisible = true;
            this._StatusBar.ResizeStyle = ResizeStyle.Immediate;
            this._StatusBar.ScaledImageSize = new Size(8, 8);
            this._StatusBar.ScaleImages = ScaleImage.OnlyWhenNeeded;
            this._StatusBar.ShowToolTips = true;
            this._StatusBar.SizeGripVisible = DefaultableBoolean.True;
            this._StatusBar.UseMnemonic = true;
            this._StatusBar.WrapText = true;

            // Add some panels to the collection 
            //UltraStatusPanel panel; 

            // Add a simple text panel  
            panel = _StatusTextPanel = this._StatusBar.Panels.Add("P1", PanelStyle.Text);
            _StatusTextPanel.SizingMode = PanelSizingMode.Spring;
            _StatusTextPanel.Text = "BioTechnology System Mexico";


            // Add a time style panel  
            _StatusTextPanel = this._StatusBar.Panels.Add("P2", PanelStyle.Time);
            _StatusTextPanel.DateTimeFormat = "hh:mm:ss tt";

            // Add a date style panel  
            _StatusTextPanel = this._StatusBar.Panels.Add("P3", PanelStyle.Date);
            _StatusTextPanel.DateTimeFormat = "MMM-dd-yyyy";
        }
        #endregion


        ///<summary>
        /// Update the lable for displaying the staus, and offline/online button.
        ///</summary>
        public void UpdateState(string state)
        {
            if ("Online" == state)
            {
                //_StatusBar.Panels[0].Text = "Biometrics esta conectado a la red";
                panel.Text = "Biometrics esta conectado a la red";
            }
            else
            {
                _StatusBar.Panels[0].Text = "Biometrics no esta conectado a la red";
                //_StatusTextPanel.DisplayText = "Biometrics no esta conectado a la red";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Create SNSP Form
            _snspForm = snsp.GetChildInstance();
            _snspForm.MdiParent = this;
            _snspForm.Show();
            
            //
            _FacesMainForm = FacesMainForm.GetChildInstance(this);
            _FacesMainForm.MdiParent = this;
            _FacesMainForm.Show();
                      
            //
            _snspForm.BringToFront();
                
        }
        public void SelectFacesWindow()
        {
            _FacesMainForm.BringToFront();
        }

        public void SelectSNSPWindow()
        {
            _snspForm.BringToFront();
            _snspForm.resetAllTextFields();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void TabMdiManager_TabSelected(object sender, Infragistics.Win.UltraWinTabbedMdi.MdiTabEventArgs e)
        {
            if(_FacesMainForm != null)
            _FacesMainForm.Refresh();
        }
    }
}