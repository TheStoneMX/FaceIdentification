using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using BioTechSys.Biometrics.Faces;

namespace BioTechSys.FacesEnroll
{
	public class SettingsForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;

		private System.Windows.Forms.GroupBox enrollWithGenGroupBox;
		private System.Windows.Forms.GroupBox matchingGroupBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox faceDetectionGroupBox;
        public System.Windows.Forms.CheckBox flipImage;

		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.Button setDefaul;
		private System.Windows.Forms.GroupBox miscGroupBox;
		private System.Windows.Forms.TextBox faceConfidenceThreshold;
		private ToolTip toolTip;
		private Label label10;

		private TextBox tbLivenessThreshold;
		public CheckBox cbLivenessDetection;
		private Label label11;
		private Label label12;
		private Label label6;
		private NumericUpDown nudMaxRecordsPerTemplate;
		private NumericUpDown nudEnrollStreamLength;
		private NumericUpDown nudGeneralizationImageCount;
		private ComboBox cbFAR;
		private NumericUpDown nudMatchingAttempts;
		private NumericUpDown nudMatchingStreamLength;
		private NumericUpDown nudFaceQualityThreshold;
		private NumericUpDown nudMaxIOD;
		private NumericUpDown nudMinIOD;
		private GroupBox groupBox1;
		
		private IContainer components;

		public SettingsForm()
		{
			InitializeComponent();

			cbFAR.BeginUpdate();
			cbFAR.Items.Add(0.001.ToString("P1"));
			cbFAR.Items.Add(0.0001.ToString("P2"));
			cbFAR.Items.Add(0.00001.ToString("P3"));
			cbFAR.EndUpdate();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.faceDetectionGroupBox = new System.Windows.Forms.GroupBox();
            this.nudMaxIOD = new System.Windows.Forms.NumericUpDown();
            this.nudMinIOD = new System.Windows.Forms.NumericUpDown();
            this.nudFaceQualityThreshold = new System.Windows.Forms.NumericUpDown();
            this.faceConfidenceThreshold = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLivenessDetection = new System.Windows.Forms.CheckBox();
            this.tbLivenessThreshold = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.enrollWithGenGroupBox = new System.Windows.Forms.GroupBox();
            this.nudEnrollStreamLength = new System.Windows.Forms.NumericUpDown();
            this.nudMaxRecordsPerTemplate = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudGeneralizationImageCount = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.matchingGroupBox = new System.Windows.Forms.GroupBox();
            this.nudMatchingStreamLength = new System.Windows.Forms.NumericUpDown();
            this.nudMatchingAttempts = new System.Windows.Forms.NumericUpDown();
            this.cbFAR = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.miscGroupBox = new System.Windows.Forms.GroupBox();
            this.flipImage = new System.Windows.Forms.CheckBox();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.setDefaul = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.faceDetectionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIOD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinIOD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFaceQualityThreshold)).BeginInit();
            this.enrollWithGenGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEnrollStreamLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxRecordsPerTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGeneralizationImageCount)).BeginInit();
            this.matchingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchingStreamLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchingAttempts)).BeginInit();
            this.miscGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // faceDetectionGroupBox
            // 
            this.faceDetectionGroupBox.Controls.Add(this.nudMaxIOD);
            this.faceDetectionGroupBox.Controls.Add(this.nudMinIOD);
            this.faceDetectionGroupBox.Controls.Add(this.nudFaceQualityThreshold);
            this.faceDetectionGroupBox.Controls.Add(this.faceConfidenceThreshold);
            this.faceDetectionGroupBox.Controls.Add(this.label5);
            this.faceDetectionGroupBox.Controls.Add(this.label4);
            this.faceDetectionGroupBox.Controls.Add(this.label3);
            this.faceDetectionGroupBox.Controls.Add(this.label2);
            this.faceDetectionGroupBox.Controls.Add(this.label1);
            this.faceDetectionGroupBox.Location = new System.Drawing.Point(8, 16);
            this.faceDetectionGroupBox.Name = "faceDetectionGroupBox";
            this.faceDetectionGroupBox.Size = new System.Drawing.Size(237, 176);
            this.faceDetectionGroupBox.TabIndex = 0;
            this.faceDetectionGroupBox.TabStop = false;
            this.faceDetectionGroupBox.Text = "Features Extraction";
            // 
            // nudMaxIOD
            // 
            this.nudMaxIOD.Location = new System.Drawing.Point(161, 78);
            this.nudMaxIOD.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.nudMaxIOD.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudMaxIOD.Name = "nudMaxIOD";
            this.nudMaxIOD.Size = new System.Drawing.Size(56, 20);
            this.nudMaxIOD.TabIndex = 5;
            this.nudMaxIOD.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // nudMinIOD
            // 
            this.nudMinIOD.Location = new System.Drawing.Point(161, 54);
            this.nudMinIOD.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.nudMinIOD.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudMinIOD.Name = "nudMinIOD";
            this.nudMinIOD.Size = new System.Drawing.Size(56, 20);
            this.nudMinIOD.TabIndex = 3;
            this.nudMinIOD.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // nudFaceQualityThreshold
            // 
            this.nudFaceQualityThreshold.Location = new System.Drawing.Point(161, 130);
            this.nudFaceQualityThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudFaceQualityThreshold.Name = "nudFaceQualityThreshold";
            this.nudFaceQualityThreshold.Size = new System.Drawing.Size(56, 20);
            this.nudFaceQualityThreshold.TabIndex = 8;
            // 
            // faceConfidenceThreshold
            // 
            this.faceConfidenceThreshold.Location = new System.Drawing.Point(161, 28);
            this.faceConfidenceThreshold.Name = "faceConfidenceThreshold";
            this.faceConfidenceThreshold.Size = new System.Drawing.Size(56, 20);
            this.faceConfidenceThreshold.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "* - Inter ocular distance";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Maximal IOD*:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Minimal IOD*:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Face quality threshold:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Face confidence threshold:";
            // 
            // cbLivenessDetection
            // 
            this.cbLivenessDetection.AutoSize = true;
            this.cbLivenessDetection.Enabled = false;
            this.cbLivenessDetection.Location = new System.Drawing.Point(11, 89);
            this.cbLivenessDetection.Name = "cbLivenessDetection";
            this.cbLivenessDetection.Size = new System.Drawing.Size(133, 17);
            this.cbLivenessDetection.TabIndex = 4;
            this.cbLivenessDetection.Text = "Use liveness detection";
            this.cbLivenessDetection.UseVisualStyleBackColor = true;
            this.cbLivenessDetection.CheckedChanged += new System.EventHandler(this.cbLivenessDetection_CheckedChanged);
            // 
            // tbLivenessThreshold
            // 
            this.tbLivenessThreshold.Enabled = false;
            this.tbLivenessThreshold.Location = new System.Drawing.Point(156, 110);
            this.tbLivenessThreshold.Name = "tbLivenessThreshold";
            this.tbLivenessThreshold.Size = new System.Drawing.Size(56, 20);
            this.tbLivenessThreshold.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(8, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(145, 15);
            this.label10.TabIndex = 5;
            this.label10.Text = "Liveness detection threshold:";
            // 
            // enrollWithGenGroupBox
            // 
            this.enrollWithGenGroupBox.Controls.Add(this.nudEnrollStreamLength);
            this.enrollWithGenGroupBox.Controls.Add(this.nudMaxRecordsPerTemplate);
            this.enrollWithGenGroupBox.Controls.Add(this.label6);
            this.enrollWithGenGroupBox.Controls.Add(this.label7);
            this.enrollWithGenGroupBox.Location = new System.Drawing.Point(251, 198);
            this.enrollWithGenGroupBox.Name = "enrollWithGenGroupBox";
            this.enrollWithGenGroupBox.Size = new System.Drawing.Size(224, 71);
            this.enrollWithGenGroupBox.TabIndex = 3;
            this.enrollWithGenGroupBox.TabStop = false;
            this.enrollWithGenGroupBox.Text = "Enrollment";
            // 
            // nudEnrollStreamLength
            // 
            this.nudEnrollStreamLength.Location = new System.Drawing.Point(156, 19);
            this.nudEnrollStreamLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEnrollStreamLength.Name = "nudEnrollStreamLength";
            this.nudEnrollStreamLength.Size = new System.Drawing.Size(55, 20);
            this.nudEnrollStreamLength.TabIndex = 1;
            this.toolTip.SetToolTip(this.nudEnrollStreamLength, "1-100\r\n(default 10)");
            this.nudEnrollStreamLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMaxRecordsPerTemplate
            // 
            this.nudMaxRecordsPerTemplate.Location = new System.Drawing.Point(156, 45);
            this.nudMaxRecordsPerTemplate.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudMaxRecordsPerTemplate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxRecordsPerTemplate.Name = "nudMaxRecordsPerTemplate";
            this.nudMaxRecordsPerTemplate.Size = new System.Drawing.Size(55, 20);
            this.nudMaxRecordsPerTemplate.TabIndex = 3;
            this.toolTip.SetToolTip(this.nudMaxRecordsPerTemplate, "1-16");
            this.nudMaxRecordsPerTemplate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(9, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "Max records per template:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(9, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "Enroll stream length:";
            // 
            // nudGeneralizationImageCount
            // 
            this.nudGeneralizationImageCount.Location = new System.Drawing.Point(156, 13);
            this.nudGeneralizationImageCount.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudGeneralizationImageCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGeneralizationImageCount.Name = "nudGeneralizationImageCount";
            this.nudGeneralizationImageCount.Size = new System.Drawing.Size(55, 20);
            this.nudGeneralizationImageCount.TabIndex = 10;
            this.nudGeneralizationImageCount.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "Template count:";
            // 
            // matchingGroupBox
            // 
            this.matchingGroupBox.Controls.Add(this.nudMatchingStreamLength);
            this.matchingGroupBox.Controls.Add(this.nudMatchingAttempts);
            this.matchingGroupBox.Controls.Add(this.cbFAR);
            this.matchingGroupBox.Controls.Add(this.label11);
            this.matchingGroupBox.Controls.Add(this.cbLivenessDetection);
            this.matchingGroupBox.Controls.Add(this.tbLivenessThreshold);
            this.matchingGroupBox.Controls.Add(this.label10);
            this.matchingGroupBox.Controls.Add(this.label9);
            this.matchingGroupBox.Controls.Add(this.label8);
            this.matchingGroupBox.Enabled = false;
            this.matchingGroupBox.Location = new System.Drawing.Point(251, 16);
            this.matchingGroupBox.Name = "matchingGroupBox";
            this.matchingGroupBox.Size = new System.Drawing.Size(224, 176);
            this.matchingGroupBox.TabIndex = 1;
            this.matchingGroupBox.TabStop = false;
            this.matchingGroupBox.Text = "Matching";
            // 
            // nudMatchingStreamLength
            // 
            this.nudMatchingStreamLength.Enabled = false;
            this.nudMatchingStreamLength.Location = new System.Drawing.Point(156, 136);
            this.nudMatchingStreamLength.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudMatchingStreamLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMatchingStreamLength.Name = "nudMatchingStreamLength";
            this.nudMatchingStreamLength.Size = new System.Drawing.Size(56, 20);
            this.nudMatchingStreamLength.TabIndex = 8;
            this.toolTip.SetToolTip(this.nudMatchingStreamLength, "1 - 5 (liveness detection disabled)\r\n10-25 (liveness detection enabled)");
            this.nudMatchingStreamLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMatchingAttempts
            // 
            this.nudMatchingAttempts.Enabled = false;
            this.nudMatchingAttempts.Location = new System.Drawing.Point(156, 51);
            this.nudMatchingAttempts.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudMatchingAttempts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMatchingAttempts.Name = "nudMatchingAttempts";
            this.nudMatchingAttempts.Size = new System.Drawing.Size(56, 20);
            this.nudMatchingAttempts.TabIndex = 3;
            this.nudMatchingAttempts.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbFAR
            // 
            this.cbFAR.Enabled = false;
            this.cbFAR.FormattingEnabled = true;
            this.cbFAR.Location = new System.Drawing.Point(123, 25);
            this.cbFAR.Name = "cbFAR";
            this.cbFAR.Size = new System.Drawing.Size(89, 21);
            this.cbFAR.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 138);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Matching stream length:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(9, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 16);
            this.label9.TabIndex = 2;
            this.label9.Text = "Matching attempts:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(9, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "FAR:";
            // 
            // miscGroupBox
            // 
            this.miscGroupBox.Controls.Add(this.flipImage);
            this.miscGroupBox.Location = new System.Drawing.Point(8, 198);
            this.miscGroupBox.Name = "miscGroupBox";
            this.miscGroupBox.Size = new System.Drawing.Size(237, 116);
            this.miscGroupBox.TabIndex = 2;
            this.miscGroupBox.TabStop = false;
            this.miscGroupBox.Text = "Misc.";
            // 
            // flipImage
            // 
            this.flipImage.Location = new System.Drawing.Point(11, 19);
            this.flipImage.Name = "flipImage";
            this.flipImage.Size = new System.Drawing.Size(192, 24);
            this.flipImage.TabIndex = 0;
            this.flipImage.Text = "Flip video image horizontally";
            // 
            // ok
            // 
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Location = new System.Drawing.Point(320, 323);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 6;
            this.ok.Text = "OK";
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(400, 323);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 7;
            this.cancel.Text = "Cancel";
            // 
            // setDefaul
            // 
            this.setDefaul.Location = new System.Drawing.Point(8, 323);
            this.setDefaul.Name = "setDefaul";
            this.setDefaul.Size = new System.Drawing.Size(75, 23);
            this.setDefaul.TabIndex = 5;
            this.setDefaul.Text = "Default";
            this.setDefaul.Click += new System.EventHandler(this.setDefault_Click);
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.nudGeneralizationImageCount);
            this.groupBox1.Location = new System.Drawing.Point(251, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 39);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Generalization";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(481, 358);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.setDefaul);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.miscGroupBox);
            this.Controls.Add(this.enrollWithGenGroupBox);
            this.Controls.Add(this.faceDetectionGroupBox);
            this.Controls.Add(this.matchingGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.faceDetectionGroupBox.ResumeLayout(false);
            this.faceDetectionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIOD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinIOD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFaceQualityThreshold)).EndInit();
            this.enrollWithGenGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudEnrollStreamLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxRecordsPerTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGeneralizationImageCount)).EndInit();
            this.matchingGroupBox.ResumeLayout(false);
            this.matchingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchingStreamLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMatchingAttempts)).EndInit();
            this.miscGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Features extraction

		private double FaceConfidenceThreshold
		{
			get
			{
				double val = double.Parse(faceConfidenceThreshold.Text);
				if(val < 0.0f || val > 100.0f)
				{
					throw new Exception("Invalid value for 'Face confidence threshold', you have to specify a number between 0.0 and 100.0!");
				}
				return val;
			}
			set
			{
				faceConfidenceThreshold.Text = value.ToString();
			}
		}

		private byte FaceQualityThreshold
		{
			get
			{
				return Convert.ToByte(nudFaceQualityThreshold.Value);
			}
			set
			{
				nudFaceQualityThreshold.Value = value;
			}
		}

		private int MinimalIOD
		{
			get
			{
				return Convert.ToInt32(nudMinIOD.Value);
			}
			set
			{
				nudMinIOD.Value = value;
			}
		}

		private int MaximalIOD
		{
			get
			{
				return Convert.ToInt32(nudMaxIOD.Value);
			}
			set
			{
				nudMaxIOD.Value = value;
			}
		}

		#endregion Features extraction

		#region Enrollment

		private int EnrollStreamLength
		{
			get
			{
				return Convert.ToInt32(nudEnrollStreamLength.Value);
			}
			set
			{
				nudEnrollStreamLength.Value = value;
			}
		}

		private int MaxRecordsPerTemplate
		{
			get
			{
				return Convert.ToInt32(nudMaxRecordsPerTemplate.Value);
			}
			set
			{
				nudMaxRecordsPerTemplate.Value = value;
			}
		}

		private int GeneralizationImageCount
		{
			get
			{
				return Convert.ToInt32(nudGeneralizationImageCount.Value);
			}
			set
			{
				nudGeneralizationImageCount.Value = value;
			}
		}

		#endregion Enrollment

		#region Matching

		private string FAR
		{
			get
			{
				return cbFAR.Text;
			}
			set
			{
				cbFAR.Text = value;
			}
		}

		private int MatchingAttempts
		{
			get
			{
				return Convert.ToInt32(nudMatchingAttempts.Value);
			}
			set
			{
				nudMatchingAttempts.Value = value;
			}
		}

		private bool UseLivenessCheck
		{
			get
			{
				return cbLivenessDetection.Checked;
			}
			set
			{
				cbLivenessDetection.Checked = value;
			}
		}

		private double LivenessThreshold
		{
			set
			{
				tbLivenessThreshold.Text = value.ToString();
			}
			get
			{
				double val = Double.Parse(tbLivenessThreshold.Text);
				if ((val > 100) || (val < 0))
				{
					throw new Exception("Invalid value for 'Liveness threshold', you  have to specify a number between 0 and 100!");
				}
				return val;
			}
		}

		private int MatchingStreamLength
		{
			get
			{
				return Convert.ToInt32(nudMatchingStreamLength.Value);
			}
			set
			{
				nudMatchingStreamLength.Value = value;
			}
		}

		#endregion Matching

		#region Misc

		private bool FlipImage
		{
			get
			{
				return flipImage.Checked;
			}
			set
			{
				flipImage.Checked = value;
			}
		}

		#endregion Misc

		#region Data exchange

        private void load(Settings settings)
		{
			FaceConfidenceThreshold = settings.FaceConfidenceThreshold;
			FaceQualityThreshold = settings.FaceQualityThreshold;
			MinimalIOD = settings.MinimalIOD;
			MaximalIOD = settings.MaximalIOD;

			EnrollStreamLength = settings.EnrollStreamLength;
			MaxRecordsPerTemplate = settings.MaxRecordsPerTemplate;
			GeneralizationImageCount = settings.GeneralizationImageCount;

			FAR = settings.FARString;
			MatchingAttempts = settings.MatchingAttempts;
			UseLivenessCheck = settings.UseLivenessCheck;
			LivenessThreshold = settings.LivenessThreshold;
			MatchingStreamLength = settings.MatchingStreamLength;

			FlipImage = settings.FlipImage;
		}

        private Settings save()
		{
            Settings settings = new Settings();

			settings.FaceConfidenceThreshold = FaceConfidenceThreshold;
			settings.FaceQualityThreshold = FaceQualityThreshold;
			settings.MinimalIOD = MinimalIOD;
			settings.MaximalIOD = MaximalIOD;

			settings.EnrollStreamLength = EnrollStreamLength;
			settings.MaxRecordsPerTemplate = MaxRecordsPerTemplate;
			settings.GeneralizationImageCount = GeneralizationImageCount;

			settings.FARString = FAR;
			settings.MatchingAttempts = MatchingAttempts;
			settings.UseLivenessCheck = UseLivenessCheck;
			settings.LivenessThreshold = LivenessThreshold;
			settings.MatchingStreamLength = MatchingStreamLength;

			settings.FlipImage = FlipImage;

			return settings;
		}

		private void setDefault_Click(object sender, System.EventArgs e)
		{
            load(Settings.DefaultSettings);
		}

        public Settings CurrentSettings
        {
            get
            {
                return save();
            }
            set
            {
                load(value);
            }
        }

		#endregion Data exchange

		#region Event handling

		private void cbLivenessDetection_CheckedChanged(object sender, EventArgs e)
		{
			tbLivenessThreshold.Enabled = cbLivenessDetection.Checked;
			if (cbLivenessDetection.Checked)
			{
				nudMatchingStreamLength.Minimum = 10;
				nudMatchingStreamLength.Maximum = 25;
			}
			else
			{
				nudMatchingStreamLength.Minimum = 1;
				nudMatchingStreamLength.Maximum = 5;
			}
		}

		#endregion Event handling
	}
}
