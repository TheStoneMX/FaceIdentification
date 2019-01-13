namespace BioTechSys.Face.Enroll.Forms
{
    partial class MainForm
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._StatusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.TabMdiManager = new Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TabMdiManager)).BeginInit();
            this.SuspendLayout();
            // 
            // _StatusBar
            // 
            this._StatusBar.Location = new System.Drawing.Point(0, 727);
            this._StatusBar.Name = "_StatusBar";
            this._StatusBar.Size = new System.Drawing.Size(850, 23);
            this._StatusBar.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // TabMdiManager
            // 
            this.TabMdiManager.AllowMaximize = true;
            this.TabMdiManager.BorderColor = System.Drawing.Color.Black;
            this.TabMdiManager.MdiParent = this;
            this.TabMdiManager.SaveSettings = true;
            this.TabMdiManager.SettingsKey = "MainForm.TabMdiManager";
            this.TabMdiManager.SplitterWidth = 10;
            this.TabMdiManager.TabSettings.AllowClose = Infragistics.Win.DefaultableBoolean.False;
            appearance1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance1.BorderColor = System.Drawing.Color.Black;
            this.TabMdiManager.TabSettings.TabAppearance = appearance1;
            this.TabMdiManager.TabSettings.TabCloseAction = Infragistics.Win.UltraWinTabbedMdi.MdiTabCloseAction.None;
            this.TabMdiManager.UseFlatMode = Infragistics.Win.DefaultableBoolean.False;
            this.TabMdiManager.ViewStyle = Infragistics.Win.UltraWinTabbedMdi.ViewStyle.Office2007;
            this.TabMdiManager.TabSelected += new Infragistics.Win.UltraWinTabbedMdi.MdiTabEventHandler(this.TabMdiManager_TabSelected);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(850, 750);
            this.Controls.Add(this._StatusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BioTechnology Systems Mexico - Estación de Enrolamiento";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.Configuration.IPersistComponentSettings)(this.TabMdiManager)).LoadComponentSettings();
            ((System.ComponentModel.ISupportInitialize)(this.TabMdiManager)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Infragistics.Win.UltraWinStatusBar.UltraStatusBar _StatusBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager TabMdiManager;
    }
}

