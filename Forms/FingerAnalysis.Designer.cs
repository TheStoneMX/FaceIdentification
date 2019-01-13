namespace BioCarasEnroll.Forms
{
    partial class FingerAnalysis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FingerAnalysis));
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageToolBar = new System.Windows.Forms.ToolBar();
            this.zoomInButton = new System.Windows.Forms.ToolBarButton();
            this.zoomOutButton = new System.Windows.Forms.ToolBarButton();
            this.fitToScreenButton = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton5 = new System.Windows.Forms.ToolBarButton();
            this.resizeButton = new System.Windows.Forms.ToolBarButton();
            this.rotateButton = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton7 = new System.Windows.Forms.ToolBarButton();
            this.levelsButton = new System.Windows.Forms.ToolBarButton();
            this.thresholdButton = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton6 = new System.Windows.Forms.ToolBarButton();
            this.convolutionButton = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton8 = new System.Windows.Forms.ToolBarButton();
            this.saturationButton = new System.Windows.Forms.ToolBarButton();
            this.ultraToolTipManager = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            this.imageList2.Images.SetKeyName(5, "");
            this.imageList2.Images.SetKeyName(6, "");
            this.imageList2.Images.SetKeyName(7, "");
            this.imageList2.Images.SetKeyName(8, "");
            this.imageList2.Images.SetKeyName(9, "");
            this.imageList2.Images.SetKeyName(10, "");
            this.imageList2.Images.SetKeyName(11, "");
            this.imageList2.Images.SetKeyName(12, "");
            this.imageList2.Images.SetKeyName(13, "");
            this.imageList2.Images.SetKeyName(14, "");
            // 
            // imageToolBar
            // 
            this.imageToolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.imageToolBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.zoomInButton,
            this.zoomOutButton,
            this.fitToScreenButton,
            this.toolBarButton5,
            this.resizeButton,
            this.rotateButton,
            this.toolBarButton7,
            this.levelsButton,
            this.thresholdButton,
            this.toolBarButton6,
            this.convolutionButton,
            this.toolBarButton8,
            this.saturationButton});
            this.imageToolBar.Divider = false;
            this.imageToolBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageToolBar.DropDownArrows = true;
            this.imageToolBar.ImageList = this.imageList2;
            this.imageToolBar.Location = new System.Drawing.Point(0, 0);
            this.imageToolBar.Name = "imageToolBar";
            this.imageToolBar.ShowToolTips = true;
            this.imageToolBar.Size = new System.Drawing.Size(31, 698);
            this.imageToolBar.TabIndex = 4;
            this.imageToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.imageToolBar_ButtonClick);
            // 
            // zoomInButton
            // 
            this.zoomInButton.ImageIndex = 2;
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.ToolTipText = "Zoom In";
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.ImageIndex = 3;
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.ToolTipText = "Zoom out";
            // 
            // fitToScreenButton
            // 
            this.fitToScreenButton.ImageIndex = 4;
            this.fitToScreenButton.Name = "fitToScreenButton";
            this.fitToScreenButton.ToolTipText = "Fit to window size";
            // 
            // toolBarButton5
            // 
            this.toolBarButton5.Name = "toolBarButton5";
            this.toolBarButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // resizeButton
            // 
            this.resizeButton.ImageIndex = 5;
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.ToolTipText = "Resize the image";
            // 
            // rotateButton
            // 
            this.rotateButton.ImageIndex = 12;
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.ToolTipText = "Rotate the image";
            // 
            // toolBarButton7
            // 
            this.toolBarButton7.Name = "toolBarButton7";
            this.toolBarButton7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // levelsButton
            // 
            this.levelsButton.ImageIndex = 6;
            this.levelsButton.Name = "levelsButton";
            this.levelsButton.ToolTipText = "Levels correction";
            // 
            // thresholdButton
            // 
            this.thresholdButton.ImageIndex = 8;
            this.thresholdButton.Name = "thresholdButton";
            this.thresholdButton.ToolTipText = "Threshold";
            // 
            // toolBarButton6
            // 
            this.toolBarButton6.Name = "toolBarButton6";
            this.toolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // convolutionButton
            // 
            this.convolutionButton.ImageIndex = 10;
            this.convolutionButton.Name = "convolutionButton";
            this.convolutionButton.ToolTipText = "Custom convolution operator";
            // 
            // toolBarButton8
            // 
            this.toolBarButton8.Name = "toolBarButton8";
            this.toolBarButton8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // saturationButton
            // 
            this.saturationButton.ImageIndex = 13;
            this.saturationButton.Name = "saturationButton";
            this.saturationButton.ToolTipText = "Saturation (HSL)";
            // 
            // ultraToolTipManager
            // 
            this.ultraToolTipManager.ContainingControl = this;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(762, 15);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(843, 15);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(31, 646);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(930, 52);
            this.panel1.TabIndex = 6;
            // 
            // FingerAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(961, 698);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.imageToolBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FingerAnalysis";
            this.Text = "Analisis de Huella";
            this.Load += new System.EventHandler(this.FingerAnalysis_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolBar imageToolBar;
        private System.Windows.Forms.ToolBarButton zoomInButton;
        private System.Windows.Forms.ToolBarButton zoomOutButton;
        private System.Windows.Forms.ToolBarButton fitToScreenButton;
        private System.Windows.Forms.ToolBarButton toolBarButton5;
        private System.Windows.Forms.ToolBarButton resizeButton;
        private System.Windows.Forms.ToolBarButton rotateButton;
        private System.Windows.Forms.ToolBarButton toolBarButton7;
        private System.Windows.Forms.ToolBarButton levelsButton;
        private System.Windows.Forms.ToolBarButton thresholdButton;
        private System.Windows.Forms.ToolBarButton toolBarButton6;
        private System.Windows.Forms.ToolBarButton convolutionButton;
        private System.Windows.Forms.ToolBarButton toolBarButton8;
        private System.Windows.Forms.ToolBarButton saturationButton;
        private Infragistics.Win.UltraWinToolTip.UltraToolTipManager ultraToolTipManager;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;

    }
}