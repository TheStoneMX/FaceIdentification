namespace BioTechSys.Face.Enroll.Forms
{
    partial class Capurar_2_Huellas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Capurar_2_Huellas));
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("Haga doble-click para scanear Dedo", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            this._rightIndice = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this._leftIndice = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScannerOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEnrollmentOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripEnrollmentOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSNSProp = new System.Windows.Forms.ToolStripButton();
            this.toolStripScannerOptions = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._bntNew = new Infragistics.Win.Misc.UltraButton();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStripFingerPrint = new System.Windows.Forms.StatusStrip();
            this.ToolTipManager = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this._captureLeftFinger = new Infragistics.Win.Misc.UltraButton();
            this._captureRightFinger = new Infragistics.Win.Misc.UltraButton();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _rightIndice
            // 
            this._rightIndice.BorderShadowColor = System.Drawing.Color.Empty;
            this._rightIndice.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded4Thick;
            this._rightIndice.Image = ((object)(resources.GetObject("_rightIndice.Image")));
            this._rightIndice.Location = new System.Drawing.Point(476, 147);
            this._rightIndice.Name = "_rightIndice";
            this._rightIndice.Size = new System.Drawing.Size(216, 282);
            this._rightIndice.TabIndex = 13;
            this._rightIndice.DoubleClick += new System.EventHandler(this._rightIndice_DoubleClick);
            // 
            // _leftIndice
            // 
            this._leftIndice.BorderShadowColor = System.Drawing.Color.Empty;
            this._leftIndice.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded4Thick;
            this._leftIndice.Image = ((object)(resources.GetObject("_leftIndice.Image")));
            this._leftIndice.Location = new System.Drawing.Point(126, 147);
            this._leftIndice.Name = "_leftIndice";
            this._leftIndice.Size = new System.Drawing.Size(217, 282);
            this._leftIndice.TabIndex = 15;
            ultraToolTipInfo1.ToolTipText = "Haga doble-click para scanear Dedo";
            this.ToolTipManager.SetUltraToolTip(this._leftIndice, ultraToolTipInfo1);
            this._leftIndice.DoubleClick += new System.EventHandler(this._leftIndice_DoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuScannerOptions,
            this.menuEnrollmentOptions});
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.opcionesToolStripMenuItem.Text = "&Opciones";
            // 
            // menuScannerOptions
            // 
            this.menuScannerOptions.Image = ((System.Drawing.Image)(resources.GetObject("menuScannerOptions.Image")));
            this.menuScannerOptions.Name = "menuScannerOptions";
            this.menuScannerOptions.Size = new System.Drawing.Size(244, 22);
            this.menuScannerOptions.Text = "&Opciones de Escaners";
            // 
            // menuEnrollmentOptions
            // 
            this.menuEnrollmentOptions.Image = ((System.Drawing.Image)(resources.GetObject("menuEnrollmentOptions.Image")));
            this.menuEnrollmentOptions.Name = "menuEnrollmentOptions";
            this.menuEnrollmentOptions.Size = new System.Drawing.Size(244, 22);
            this.menuEnrollmentOptions.Text = "&Opciones de Registro de Huellas";
            this.menuEnrollmentOptions.Click += new System.EventHandler(this.menuEnrollmentOptions_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.toolStripEnrollmentOptions,
            this.toolStripSeparator3,
            this.toolStripSNSProp,
            this.toolStripScannerOptions});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(854, 38);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripEnrollmentOptions
            // 
            this.toolStripEnrollmentOptions.AutoSize = false;
            this.toolStripEnrollmentOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripEnrollmentOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolStripEnrollmentOptions.Image")));
            this.toolStripEnrollmentOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripEnrollmentOptions.Name = "toolStripEnrollmentOptions";
            this.toolStripEnrollmentOptions.Size = new System.Drawing.Size(40, 32);
            this.toolStripEnrollmentOptions.ToolTipText = "Opciones de Captura de Minutae";
            this.toolStripEnrollmentOptions.Click += new System.EventHandler(this.menuEnrollmentOptions_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripSNSProp
            // 
            this.toolStripSNSProp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSNSProp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSNSProp.Image")));
            this.toolStripSNSProp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSNSProp.Name = "toolStripSNSProp";
            this.toolStripSNSProp.Size = new System.Drawing.Size(52, 35);
            this.toolStripSNSProp.Click += new System.EventHandler(this.toolStripSNSProp_Click);
            // 
            // toolStripScannerOptions
            // 
            this.toolStripScannerOptions.AutoSize = false;
            this.toolStripScannerOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripScannerOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolStripScannerOptions.Image")));
            this.toolStripScannerOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripScannerOptions.Name = "toolStripScannerOptions";
            this.toolStripScannerOptions.Size = new System.Drawing.Size(45, 35);
            this.toolStripScannerOptions.ToolTipText = "Opciones de ajuste del Escaner";
            this.toolStripScannerOptions.Click += new System.EventHandler(this.toolStripScannerOptions_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // _bntNew
            // 
            this._bntNew.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2007ScrollbarButton;
            this._bntNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._bntNew.Location = new System.Drawing.Point(549, 533);
            this._bntNew.Name = "_bntNew";
            this._bntNew.Size = new System.Drawing.Size(116, 23);
            this._bntNew.TabIndex = 29;
            this._bntNew.Text = "Registrar Huellas";
            this._bntNew.Click += new System.EventHandler(this._bntNew_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
            // 
            // statusStripFingerPrint
            // 
            this.statusStripFingerPrint.Location = new System.Drawing.Point(0, 607);
            this.statusStripFingerPrint.Name = "statusStripFingerPrint";
            this.statusStripFingerPrint.Size = new System.Drawing.Size(854, 22);
            this.statusStripFingerPrint.TabIndex = 36;
            // 
            // ToolTipManager
            // 
            this.ToolTipManager.ContainingControl = this;
            // 
            // _captureLeftFinger
            // 
            this._captureLeftFinger.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2007ScrollbarButton;
            this._captureLeftFinger.Location = new System.Drawing.Point(167, 447);
            this._captureLeftFinger.Name = "_captureLeftFinger";
            this._captureLeftFinger.Size = new System.Drawing.Size(155, 23);
            this._captureLeftFinger.TabIndex = 37;
            this._captureLeftFinger.Text = "Capturar Dedo Derecho";
            this._captureLeftFinger.Click += new System.EventHandler(this._captureLeftFinger_Click);
            // 
            // _captureRightFinger
            // 
            this._captureRightFinger.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2007ScrollbarButton;
            this._captureRightFinger.Location = new System.Drawing.Point(517, 447);
            this._captureRightFinger.Name = "_captureRightFinger";
            this._captureRightFinger.Size = new System.Drawing.Size(155, 23);
            this._captureRightFinger.TabIndex = 38;
            this._captureRightFinger.Text = "Capturar Dedo Derecho";
            this._captureRightFinger.Click += new System.EventHandler(this._captureRightFinger_Click);
            // 
            // Capurar_2_Huellas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(854, 629);
            this.Controls.Add(this._captureRightFinger);
            this.Controls.Add(this._captureLeftFinger);
            this.Controls.Add(this.statusStripFingerPrint);
            this.Controls.Add(this._rightIndice);
            this.Controls.Add(this._bntNew);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this._leftIndice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Capurar_2_Huellas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capturar Huellas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Capurar_10_Huellas_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraPictureBox _rightIndice;
        private Infragistics.Win.UltraWinEditors.UltraPictureBox _leftIndice;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuScannerOptions;
        private System.Windows.Forms.ToolStripMenuItem menuEnrollmentOptions;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripEnrollmentOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Infragistics.Win.Misc.UltraButton _bntNew;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private BioTechSys.Mail.Smtp.BioTechSysSmtpMail bioTechSysSmtpMail;
        private System.Windows.Forms.ToolStripButton toolStripSNSProp;
        private System.Windows.Forms.StatusStrip statusStripFingerPrint;
        private Infragistics.Win.UltraWinToolTip.UltraToolTipManager ToolTipManager;
        private System.Windows.Forms.ToolStripButton toolStripScannerOptions;
        private Infragistics.Win.Misc.UltraButton _captureRightFinger;
        private Infragistics.Win.Misc.UltraButton _captureLeftFinger;


    }
}