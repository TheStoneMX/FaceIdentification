namespace BioTechSys.Face.Enroll.Forms
{
    partial class scannerOptions
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
            this.VideoSettings = new System.Windows.Forms.GroupBox();
            this._SaveChanges = new Infragistics.Win.Misc.UltraButton();
            this.gainLabel = new Infragistics.Win.Misc.UltraLabel();
            this.contrassLabel = new Infragistics.Win.Misc.UltraLabel();
            this.brightLabel = new Infragistics.Win.Misc.UltraLabel();
            this.ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.BrilloTrackBar = new EConTech.Windows.MACUI.BioTechSysTrackBar();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ContrastTrackBar = new EConTech.Windows.MACUI.BioTechSysTrackBar();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.GainTrackBar = new EConTech.Windows.MACUI.BioTechSysTrackBar();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.VideoSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).BeginInit();
            this.ultraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // VideoSettings
            // 
            this.VideoSettings.Controls.Add(this._SaveChanges);
            this.VideoSettings.Controls.Add(this.gainLabel);
            this.VideoSettings.Controls.Add(this.contrassLabel);
            this.VideoSettings.Controls.Add(this.brightLabel);
            this.VideoSettings.Controls.Add(this.ultraGroupBox3);
            this.VideoSettings.Controls.Add(this.ultraGroupBox2);
            this.VideoSettings.Controls.Add(this.ultraGroupBox1);
            this.VideoSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.VideoSettings.Location = new System.Drawing.Point(467, 0);
            this.VideoSettings.Name = "VideoSettings";
            this.VideoSettings.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VideoSettings.Size = new System.Drawing.Size(211, 467);
            this.VideoSettings.TabIndex = 10;
            this.VideoSettings.TabStop = false;
            // 
            // _SaveChanges
            // 
            this._SaveChanges.Location = new System.Drawing.Point(40, 367);
            this._SaveChanges.Name = "_SaveChanges";
            this._SaveChanges.Size = new System.Drawing.Size(120, 23);
            this._SaveChanges.TabIndex = 15;
            this._SaveChanges.Text = "Guardar Cambios";
            this._SaveChanges.Click += new System.EventHandler(this._SaveChanges_Click);
            // 
            // gainLabel
            // 
            this.gainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gainLabel.Location = new System.Drawing.Point(165, 341);
            this.gainLabel.Name = "gainLabel";
            this.gainLabel.Size = new System.Drawing.Size(33, 20);
            this.gainLabel.TabIndex = 14;
            this.gainLabel.Text = "0.0";
            // 
            // contrassLabel
            // 
            this.contrassLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contrassLabel.Location = new System.Drawing.Point(90, 341);
            this.contrassLabel.Name = "contrassLabel";
            this.contrassLabel.Size = new System.Drawing.Size(33, 20);
            this.contrassLabel.TabIndex = 14;
            this.contrassLabel.Text = "0.0";
            // 
            // brightLabel
            // 
            this.brightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brightLabel.Location = new System.Drawing.Point(19, 341);
            this.brightLabel.Name = "brightLabel";
            this.brightLabel.Size = new System.Drawing.Size(33, 20);
            this.brightLabel.TabIndex = 14;
            this.brightLabel.Text = "0.0";
            // 
            // ultraGroupBox3
            // 
            this.ultraGroupBox3.Controls.Add(this.BrilloTrackBar);
            this.ultraGroupBox3.Location = new System.Drawing.Point(7, 19);
            this.ultraGroupBox3.Name = "ultraGroupBox3";
            this.ultraGroupBox3.Size = new System.Drawing.Size(60, 318);
            this.ultraGroupBox3.TabIndex = 13;
            this.ultraGroupBox3.Text = "Brillo";
            this.ultraGroupBox3.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            // 
            // BrilloTrackBar
            // 
            this.BrilloTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.BrilloTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.BrilloTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrilloTrackBar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrilloTrackBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.BrilloTrackBar.IndentHeight = 6;
            this.BrilloTrackBar.Location = new System.Drawing.Point(2, 18);
            this.BrilloTrackBar.Maximum = 255;
            this.BrilloTrackBar.Minimum = 0;
            this.BrilloTrackBar.Name = "BrilloTrackBar";
            this.BrilloTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.BrilloTrackBar.Size = new System.Drawing.Size(56, 298);
            this.BrilloTrackBar.TabIndex = 8;
            this.BrilloTrackBar.TextTickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.BrilloTrackBar.TickColor = System.Drawing.Color.Green;
            this.BrilloTrackBar.TickFrequency = 20;
            this.BrilloTrackBar.TickHeight = 4;
            this.BrilloTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.BrilloTrackBar.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.BrilloTrackBar.TrackerSize = new System.Drawing.Size(16, 16);
            this.BrilloTrackBar.TrackLineColor = System.Drawing.Color.Red;
            this.BrilloTrackBar.TrackLineHeight = 3;
            this.BrilloTrackBar.Value = 0;
            this.BrilloTrackBar.ValueChanged += new EConTech.Windows.MACUI.ValueChangedHandler(this.BrilloTrackBar_ValueChanged);
            this.BrilloTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BrilloTrackBar_MouseUp);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.ContrastTrackBar);
            this.ultraGroupBox2.Location = new System.Drawing.Point(73, 20);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(68, 317);
            this.ultraGroupBox2.TabIndex = 12;
            this.ultraGroupBox2.Text = "Contraste";
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            // 
            // ContrastTrackBar
            // 
            this.ContrastTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.ContrastTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.ContrastTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContrastTrackBar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContrastTrackBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.ContrastTrackBar.IndentHeight = 6;
            this.ContrastTrackBar.Location = new System.Drawing.Point(2, 18);
            this.ContrastTrackBar.Maximum = 255;
            this.ContrastTrackBar.Minimum = 0;
            this.ContrastTrackBar.Name = "ContrastTrackBar";
            this.ContrastTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ContrastTrackBar.Size = new System.Drawing.Size(64, 297);
            this.ContrastTrackBar.TabIndex = 8;
            this.ContrastTrackBar.TextTickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.ContrastTrackBar.TickColor = System.Drawing.Color.Green;
            this.ContrastTrackBar.TickFrequency = 20;
            this.ContrastTrackBar.TickHeight = 4;
            this.ContrastTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.ContrastTrackBar.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.ContrastTrackBar.TrackerSize = new System.Drawing.Size(16, 16);
            this.ContrastTrackBar.TrackLineColor = System.Drawing.Color.Red;
            this.ContrastTrackBar.TrackLineHeight = 3;
            this.ContrastTrackBar.Value = 0;
            this.ContrastTrackBar.ValueChanged += new EConTech.Windows.MACUI.ValueChangedHandler(this.ContrastTrackBar_ValueChanged);
            this.ContrastTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContrastTrackBar_MouseUp);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.GainTrackBar);
            this.ultraGroupBox1.Location = new System.Drawing.Point(147, 19);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(63, 318);
            this.ultraGroupBox1.TabIndex = 11;
            this.ultraGroupBox1.Text = "Intencidad";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            // 
            // GainTrackBar
            // 
            this.GainTrackBar.BackColor = System.Drawing.Color.Transparent;
            this.GainTrackBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.GainTrackBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GainTrackBar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GainTrackBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.GainTrackBar.IndentHeight = 6;
            this.GainTrackBar.Location = new System.Drawing.Point(2, 18);
            this.GainTrackBar.Maximum = 255;
            this.GainTrackBar.Minimum = 0;
            this.GainTrackBar.Name = "GainTrackBar";
            this.GainTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.GainTrackBar.Size = new System.Drawing.Size(59, 298);
            this.GainTrackBar.TabIndex = 8;
            this.GainTrackBar.TextTickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.GainTrackBar.TickColor = System.Drawing.Color.Green;
            this.GainTrackBar.TickFrequency = 20;
            this.GainTrackBar.TickHeight = 4;
            this.GainTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.GainTrackBar.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.GainTrackBar.TrackerSize = new System.Drawing.Size(16, 16);
            this.GainTrackBar.TrackLineColor = System.Drawing.Color.Red;
            this.GainTrackBar.TrackLineHeight = 3;
            this.GainTrackBar.Value = 0;
            this.GainTrackBar.ValueChanged += new EConTech.Windows.MACUI.ValueChangedHandler(this.GainTrackBar_ValueChanged);
            this.GainTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GainTrackBar_MouseUp);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(-4, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(469, 467);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 11;
            this.pictureBox.TabStop = false;
            // 
            // scannerOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 467);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.VideoSettings);
            this.Name = "scannerOptions";
            this.Text = "Opciones del Escaner";
            this.Load += new System.EventHandler(this.scannerOptions_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.scannerOptions_FormClosing);
            this.VideoSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).EndInit();
            this.ultraGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox VideoSettings;
        private Infragistics.Win.Misc.UltraButton _SaveChanges;
        private Infragistics.Win.Misc.UltraLabel gainLabel;
        private Infragistics.Win.Misc.UltraLabel contrassLabel;
        private Infragistics.Win.Misc.UltraLabel brightLabel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox3;
        private EConTech.Windows.MACUI.BioTechSysTrackBar BrilloTrackBar;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private EConTech.Windows.MACUI.BioTechSysTrackBar ContrastTrackBar;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private EConTech.Windows.MACUI.BioTechSysTrackBar GainTrackBar;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}