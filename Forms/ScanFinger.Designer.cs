namespace BioAntecedentes
{
    partial class ScanFinger
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
            this._GoLive = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _GoLive
            // 
            this._GoLive.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._GoLive.Location = new System.Drawing.Point(136, 442);
            this._GoLive.Name = "_GoLive";
            this._GoLive.Size = new System.Drawing.Size(118, 23);
            this._GoLive.TabIndex = 1;
            this._GoLive.Text = "Go Live";
            this._GoLive.UseVisualStyleBackColor = true;
            this._GoLive.Click += new System.EventHandler(this.GoLive_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(412, 412);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // ScanFinger
            // 
            this.AcceptButton = this._GoLive;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 493);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._GoLive);
            this.Name = "ScanFinger";
            this.Text = "ScanFinger";
            this.Load += new System.EventHandler(this.ScanFinger_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanFinger_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _GoLive;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}