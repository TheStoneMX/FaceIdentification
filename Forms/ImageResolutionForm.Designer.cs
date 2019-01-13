namespace BioTechSys.FacesEnroll
{
	partial class ImageResolutionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageResolutionForm));
            this.horzResolutionLabel = new System.Windows.Forms.Label();
            this.horzResolutionTextBox = new System.Windows.Forms.TextBox();
            this.horzResolutionDpiLabel = new System.Windows.Forms.Label();
            this.vertResolutionLabel = new System.Windows.Forms.Label();
            this.vertResolutionTextBox = new System.Windows.Forms.TextBox();
            this.vertResolutionDpiLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // horzResolutionLabel
            // 
            this.horzResolutionLabel.AutoSize = true;
            this.horzResolutionLabel.Location = new System.Drawing.Point(12, 15);
            this.horzResolutionLabel.Name = "horzResolutionLabel";
            this.horzResolutionLabel.Size = new System.Drawing.Size(80, 13);
            this.horzResolutionLabel.TabIndex = 0;
            this.horzResolutionLabel.Text = "Horz resolution:";
            // 
            // horzResolutionTextBox
            // 
            this.horzResolutionTextBox.Location = new System.Drawing.Point(98, 12);
            this.horzResolutionTextBox.Name = "horzResolutionTextBox";
            this.horzResolutionTextBox.Size = new System.Drawing.Size(45, 20);
            this.horzResolutionTextBox.TabIndex = 1;
            // 
            // horzResolutionDpiLabel
            // 
            this.horzResolutionDpiLabel.AutoSize = true;
            this.horzResolutionDpiLabel.Location = new System.Drawing.Point(149, 15);
            this.horzResolutionDpiLabel.Name = "horzResolutionDpiLabel";
            this.horzResolutionDpiLabel.Size = new System.Drawing.Size(21, 13);
            this.horzResolutionDpiLabel.TabIndex = 2;
            this.horzResolutionDpiLabel.Text = "dpi";
            // 
            // vertResolutionLabel
            // 
            this.vertResolutionLabel.AutoSize = true;
            this.vertResolutionLabel.Location = new System.Drawing.Point(12, 41);
            this.vertResolutionLabel.Name = "vertResolutionLabel";
            this.vertResolutionLabel.Size = new System.Drawing.Size(77, 13);
            this.vertResolutionLabel.TabIndex = 3;
            this.vertResolutionLabel.Text = "Vert resolution:";
            // 
            // vertResolutionTextBox
            // 
            this.vertResolutionTextBox.Location = new System.Drawing.Point(99, 38);
            this.vertResolutionTextBox.Name = "vertResolutionTextBox";
            this.vertResolutionTextBox.Size = new System.Drawing.Size(44, 20);
            this.vertResolutionTextBox.TabIndex = 4;
            // 
            // vertResolutionDpiLabel
            // 
            this.vertResolutionDpiLabel.AutoSize = true;
            this.vertResolutionDpiLabel.Location = new System.Drawing.Point(149, 41);
            this.vertResolutionDpiLabel.Name = "vertResolutionDpiLabel";
            this.vertResolutionDpiLabel.Size = new System.Drawing.Size(21, 13);
            this.vertResolutionDpiLabel.TabIndex = 5;
            this.vertResolutionDpiLabel.Text = "dpi";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(11, 73);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(92, 73);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ImageResolutionForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(179, 111);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.vertResolutionDpiLabel);
            this.Controls.Add(this.vertResolutionTextBox);
            this.Controls.Add(this.vertResolutionLabel);
            this.Controls.Add(this.horzResolutionDpiLabel);
            this.Controls.Add(this.horzResolutionTextBox);
            this.Controls.Add(this.horzResolutionLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageResolutionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Image Resolution";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageResolutionForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label horzResolutionLabel;
		private System.Windows.Forms.TextBox horzResolutionTextBox;
		private System.Windows.Forms.Label horzResolutionDpiLabel;
		private System.Windows.Forms.Label vertResolutionLabel;
		private System.Windows.Forms.TextBox vertResolutionTextBox;
		private System.Windows.Forms.Label vertResolutionDpiLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}