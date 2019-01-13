namespace BioTechSys.Face.Enroll.Forms
{
	partial class EnrollmentOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnrollmentOptionsForm));
            this.minimalMinutiaCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.generalizationTemplatesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.generalizationTemplatesLabel = new System.Windows.Forms.Label();
            this.vfeTemplateSizeLabel = new System.Windows.Forms.Label();
            this.vfeTemplateSizeComboBox = new System.Windows.Forms.ComboBox();
            this.vfeGeneralizationFarLabel = new System.Windows.Forms.Label();
            this.vfeGeneralizationFarComboBox = new System.Windows.Forms.ComboBox();
            this.vfeGeneralizationMaximalRotationLabel = new System.Windows.Forms.Label();
            this.vfeGeneralizationMaximalRotationDegreeLabel = new System.Windows.Forms.Label();
            this.defaultButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.vfeGeneralizationMaximalRotationNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.vfeUseQualityCheckBox = new System.Windows.Forms.CheckBox();
            this.vfeQualityThresholdLabel = new System.Windows.Forms.Label();
            this.vfeQualityThresholdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.look4DuplicatesCheckBox = new System.Windows.Forms.CheckBox();
            this.useGeneralizationCheckBox = new System.Windows.Forms.CheckBox();
            this.minimalMinutiaCountCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.minimalMinutiaCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.generalizationTemplatesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vfeGeneralizationMaximalRotationNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vfeQualityThresholdNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // minimalMinutiaCountNumericUpDown
            // 
            this.minimalMinutiaCountNumericUpDown.Location = new System.Drawing.Point(175, 32);
            this.minimalMinutiaCountNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.minimalMinutiaCountNumericUpDown.Name = "minimalMinutiaCountNumericUpDown";
            this.minimalMinutiaCountNumericUpDown.Size = new System.Drawing.Size(58, 20);
            this.minimalMinutiaCountNumericUpDown.TabIndex = 2;
            this.minimalMinutiaCountNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // generalizationTemplatesNumericUpDown
            // 
            this.generalizationTemplatesNumericUpDown.Location = new System.Drawing.Point(175, 58);
            this.generalizationTemplatesNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.generalizationTemplatesNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.generalizationTemplatesNumericUpDown.Name = "generalizationTemplatesNumericUpDown";
            this.generalizationTemplatesNumericUpDown.Size = new System.Drawing.Size(58, 20);
            this.generalizationTemplatesNumericUpDown.TabIndex = 4;
            this.generalizationTemplatesNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // generalizationTemplatesLabel
            // 
            this.generalizationTemplatesLabel.AutoSize = true;
            this.generalizationTemplatesLabel.Location = new System.Drawing.Point(241, 61);
            this.generalizationTemplatesLabel.Name = "generalizationTemplatesLabel";
            this.generalizationTemplatesLabel.Size = new System.Drawing.Size(77, 13);
            this.generalizationTemplatesLabel.TabIndex = 5;
            this.generalizationTemplatesLabel.Text = "Generalización";
            // 
            // vfeTemplateSizeLabel
            // 
            this.vfeTemplateSizeLabel.AutoSize = true;
            this.vfeTemplateSizeLabel.Location = new System.Drawing.Point(16, 164);
            this.vfeTemplateSizeLabel.Name = "vfeTemplateSizeLabel";
            this.vfeTemplateSizeLabel.Size = new System.Drawing.Size(103, 13);
            this.vfeTemplateSizeLabel.TabIndex = 12;
            this.vfeTemplateSizeLabel.Text = "Tamaño de Plantilla:";
            // 
            // vfeTemplateSizeComboBox
            // 
            this.vfeTemplateSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vfeTemplateSizeComboBox.FormattingEnabled = true;
            this.vfeTemplateSizeComboBox.Location = new System.Drawing.Point(19, 180);
            this.vfeTemplateSizeComboBox.Name = "vfeTemplateSizeComboBox";
            this.vfeTemplateSizeComboBox.Size = new System.Drawing.Size(121, 21);
            this.vfeTemplateSizeComboBox.TabIndex = 13;
            // 
            // vfeGeneralizationFarLabel
            // 
            this.vfeGeneralizationFarLabel.AutoSize = true;
            this.vfeGeneralizationFarLabel.Location = new System.Drawing.Point(152, 164);
            this.vfeGeneralizationFarLabel.Name = "vfeGeneralizationFarLabel";
            this.vfeGeneralizationFarLabel.Size = new System.Drawing.Size(104, 13);
            this.vfeGeneralizationFarLabel.TabIndex = 14;
            this.vfeGeneralizationFarLabel.Text = "Generalización &FAR:";
            // 
            // vfeGeneralizationFarComboBox
            // 
            this.vfeGeneralizationFarComboBox.FormattingEnabled = true;
            this.vfeGeneralizationFarComboBox.Location = new System.Drawing.Point(175, 180);
            this.vfeGeneralizationFarComboBox.Name = "vfeGeneralizationFarComboBox";
            this.vfeGeneralizationFarComboBox.Size = new System.Drawing.Size(73, 21);
            this.vfeGeneralizationFarComboBox.TabIndex = 15;
            // 
            // vfeGeneralizationMaximalRotationLabel
            // 
            this.vfeGeneralizationMaximalRotationLabel.AutoSize = true;
            this.vfeGeneralizationMaximalRotationLabel.Location = new System.Drawing.Point(39, 131);
            this.vfeGeneralizationMaximalRotationLabel.Name = "vfeGeneralizationMaximalRotationLabel";
            this.vfeGeneralizationMaximalRotationLabel.Size = new System.Drawing.Size(92, 13);
            this.vfeGeneralizationMaximalRotationLabel.TabIndex = 18;
            this.vfeGeneralizationMaximalRotationLabel.Text = "Rotación Maxima:";
            // 
            // vfeGeneralizationMaximalRotationDegreeLabel
            // 
            this.vfeGeneralizationMaximalRotationDegreeLabel.AutoSize = true;
            this.vfeGeneralizationMaximalRotationDegreeLabel.Location = new System.Drawing.Point(202, 223);
            this.vfeGeneralizationMaximalRotationDegreeLabel.Name = "vfeGeneralizationMaximalRotationDegreeLabel";
            this.vfeGeneralizationMaximalRotationDegreeLabel.Size = new System.Drawing.Size(11, 13);
            this.vfeGeneralizationMaximalRotationDegreeLabel.TabIndex = 20;
            this.vfeGeneralizationMaximalRotationDegreeLabel.Text = "°";
            // 
            // defaultButton
            // 
            this.defaultButton.Location = new System.Drawing.Point(11, 223);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(75, 23);
            this.defaultButton.TabIndex = 17;
            this.defaultButton.Text = "&Default";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(92, 223);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 18;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(173, 223);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "Cancelar";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // vfeGeneralizationMaximalRotationNumericUpDown
            // 
            this.vfeGeneralizationMaximalRotationNumericUpDown.Location = new System.Drawing.Point(175, 131);
            this.vfeGeneralizationMaximalRotationNumericUpDown.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.vfeGeneralizationMaximalRotationNumericUpDown.Name = "vfeGeneralizationMaximalRotationNumericUpDown";
            this.vfeGeneralizationMaximalRotationNumericUpDown.Size = new System.Drawing.Size(58, 20);
            this.vfeGeneralizationMaximalRotationNumericUpDown.TabIndex = 19;
            this.vfeGeneralizationMaximalRotationNumericUpDown.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // vfeUseQualityCheckBox
            // 
            this.vfeUseQualityCheckBox.AutoSize = true;
            this.vfeUseQualityCheckBox.Location = new System.Drawing.Point(12, 80);
            this.vfeUseQualityCheckBox.Name = "vfeUseQualityCheckBox";
            this.vfeUseQualityCheckBox.Size = new System.Drawing.Size(131, 17);
            this.vfeUseQualityCheckBox.TabIndex = 6;
            this.vfeUseQualityCheckBox.Text = "Utilizar Calidad de \"G\"";
            this.vfeUseQualityCheckBox.UseVisualStyleBackColor = true;
            // 
            // vfeQualityThresholdLabel
            // 
            this.vfeQualityThresholdLabel.AutoSize = true;
            this.vfeQualityThresholdLabel.Location = new System.Drawing.Point(12, 104);
            this.vfeQualityThresholdLabel.Name = "vfeQualityThresholdLabel";
            this.vfeQualityThresholdLabel.Size = new System.Drawing.Size(119, 13);
            this.vfeQualityThresholdLabel.TabIndex = 7;
            this.vfeQualityThresholdLabel.Text = "Porcentage de Umbral :";
            // 
            // vfeQualityThresholdNumericUpDown
            // 
            this.vfeQualityThresholdNumericUpDown.Location = new System.Drawing.Point(182, 101);
            this.vfeQualityThresholdNumericUpDown.Name = "vfeQualityThresholdNumericUpDown";
            this.vfeQualityThresholdNumericUpDown.Size = new System.Drawing.Size(51, 20);
            this.vfeQualityThresholdNumericUpDown.TabIndex = 8;
            // 
            // look4DuplicatesCheckBox
            // 
            this.look4DuplicatesCheckBox.AutoSize = true;
            this.look4DuplicatesCheckBox.Location = new System.Drawing.Point(11, 11);
            this.look4DuplicatesCheckBox.Name = "look4DuplicatesCheckBox";
            this.look4DuplicatesCheckBox.Size = new System.Drawing.Size(134, 17);
            this.look4DuplicatesCheckBox.TabIndex = 6;
            this.look4DuplicatesCheckBox.Text = "Buscar Por Duplicados";
            this.look4DuplicatesCheckBox.UseVisualStyleBackColor = true;
            // 
            // useGeneralizationCheckBox
            // 
            this.useGeneralizationCheckBox.AutoSize = true;
            this.useGeneralizationCheckBox.Checked = global::BioTechSys.FingerCapture.Properties.Settings.Default.FPUseGeneralization;
            this.useGeneralizationCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useGeneralizationCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::BioTechSys.FingerCapture.Properties.Settings.Default, "FPUseGeneralization", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.useGeneralizationCheckBox.Location = new System.Drawing.Point(12, 57);
            this.useGeneralizationCheckBox.Name = "useGeneralizationCheckBox";
            this.useGeneralizationCheckBox.Size = new System.Drawing.Size(125, 17);
            this.useGeneralizationCheckBox.TabIndex = 3;
            this.useGeneralizationCheckBox.Text = "Numero de Plantillas ";
            this.useGeneralizationCheckBox.UseVisualStyleBackColor = true;
            // 
            // minimalMinutiaCountCheckBox
            // 
            this.minimalMinutiaCountCheckBox.AutoSize = true;
            this.minimalMinutiaCountCheckBox.Checked = global::BioTechSys.FingerCapture.Properties.Settings.Default.FPUseMinimalMinutiaCount;
            this.minimalMinutiaCountCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.minimalMinutiaCountCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::BioTechSys.FingerCapture.Properties.Settings.Default, "FPUseMinimalMinutiaCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.minimalMinutiaCountCheckBox.Location = new System.Drawing.Point(12, 34);
            this.minimalMinutiaCountCheckBox.Name = "minimalMinutiaCountCheckBox";
            this.minimalMinutiaCountCheckBox.Size = new System.Drawing.Size(154, 17);
            this.minimalMinutiaCountCheckBox.TabIndex = 1;
            this.minimalMinutiaCountCheckBox.Text = "Numero Minimo de Minutia ";
            this.minimalMinutiaCountCheckBox.UseVisualStyleBackColor = true;
            // 
            // EnrollmentOptionsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(331, 265);
            this.Controls.Add(this.vfeQualityThresholdNumericUpDown);
            this.Controls.Add(this.vfeQualityThresholdLabel);
            this.Controls.Add(this.vfeGeneralizationMaximalRotationNumericUpDown);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.defaultButton);
            this.Controls.Add(this.vfeGeneralizationMaximalRotationDegreeLabel);
            this.Controls.Add(this.vfeGeneralizationMaximalRotationLabel);
            this.Controls.Add(this.vfeGeneralizationFarComboBox);
            this.Controls.Add(this.vfeGeneralizationFarLabel);
            this.Controls.Add(this.vfeTemplateSizeComboBox);
            this.Controls.Add(this.vfeTemplateSizeLabel);
            this.Controls.Add(this.generalizationTemplatesLabel);
            this.Controls.Add(this.generalizationTemplatesNumericUpDown);
            this.Controls.Add(this.useGeneralizationCheckBox);
            this.Controls.Add(this.minimalMinutiaCountNumericUpDown);
            this.Controls.Add(this.minimalMinutiaCountCheckBox);
            this.Controls.Add(this.look4DuplicatesCheckBox);
            this.Controls.Add(this.vfeUseQualityCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnrollmentOptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Opciones de Enrrolamiento de Huella ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EnrollmentOptionsForm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnrollmentOptionsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.minimalMinutiaCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.generalizationTemplatesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vfeGeneralizationMaximalRotationNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vfeQualityThresholdNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox vfeUseQualityCheckBox;
		private System.Windows.Forms.CheckBox minimalMinutiaCountCheckBox;
		private System.Windows.Forms.NumericUpDown minimalMinutiaCountNumericUpDown;
		private System.Windows.Forms.NumericUpDown generalizationTemplatesNumericUpDown;
		private System.Windows.Forms.CheckBox useGeneralizationCheckBox;
        private System.Windows.Forms.Label generalizationTemplatesLabel;
		private System.Windows.Forms.Label vfeTemplateSizeLabel;
        private System.Windows.Forms.ComboBox vfeTemplateSizeComboBox;
		private System.Windows.Forms.Label vfeGeneralizationFarLabel;
		private System.Windows.Forms.ComboBox vfeGeneralizationFarComboBox;
		private System.Windows.Forms.Label vfeGeneralizationMaximalRotationLabel;
		private System.Windows.Forms.Label vfeGeneralizationMaximalRotationDegreeLabel;
		private System.Windows.Forms.Button defaultButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.NumericUpDown vfeGeneralizationMaximalRotationNumericUpDown;
		private System.Windows.Forms.Label vfeQualityThresholdLabel;
        private System.Windows.Forms.NumericUpDown vfeQualityThresholdNumericUpDown;
        private System.Windows.Forms.CheckBox look4DuplicatesCheckBox;
	}
}