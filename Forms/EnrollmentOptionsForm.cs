using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BioHuellas.Properties;
using BioTechSys.Biometrics;
using System.Globalization;

namespace BioTechSys.Face.Enroll.Forms
{
	public partial class EnrollmentOptionsForm : Form
	{
		#region Private static readonly fields

		private static readonly KeyValuePair<uint, string>[] vfeModes =
			{
				new KeyValuePair<uint, string>(NFExtractor.ModeGeneral, "General"),
				new KeyValuePair<uint, string>(NFExtractor.ModeDigitalPersonaUareU, "DigitalPersona U.are.U"),
				new KeyValuePair<uint, string>(NFExtractor.ModeBiometrikaFX2000, "BiometriKa FX2000"),
				new KeyValuePair<uint, string>(NFExtractor.ModeBiometrikaFX3000, "BiometriKa FX3000"),
				new KeyValuePair<uint, string>(NFExtractor.ModeKeytronicSecureDesktop, "Keytronic SecureDesktop"),
				new KeyValuePair<uint, string>(NFExtractor.ModeIdentixTouchView, "Identix TouchView"),
				new KeyValuePair<uint, string>(NFExtractor.ModeIdentixDfr2090, "Identix DFR2090"),
				new KeyValuePair<uint, string>(NFExtractor.ModePreciseBiometrics100CS, "PreciseBiometrics 100CS"),
				new KeyValuePair<uint, string>(NFExtractor.ModeUpekTouchChip, "Upek TouchChip"),
				new KeyValuePair<uint, string>(NFExtractor.ModeIdenticatorTechnologyDF90, "IdenticatorTechnology DF90"),
				new KeyValuePair<uint, string>(NFExtractor.ModeAuthentecAFS2, "Authentec AF-S2"),
				new KeyValuePair<uint, string>(NFExtractor.ModeAuthentecAes4000, "Authentec AES4000"),
				new KeyValuePair<uint, string>(NFExtractor.ModeAuthentecAes2501B, "Authentec AES2501B"),
				new KeyValuePair<uint, string>(NFExtractor.ModeBmfBlp100, "BMF BLP100"),
				new KeyValuePair<uint, string>(NFExtractor.ModeSecugenHamster, "Secugen Hamster"),
				new KeyValuePair<uint, string>(NFExtractor.ModeEthentica, "Ethentica"),
				new KeyValuePair<uint, string>(NFExtractor.ModeCrossMatchVerifier300, "CrossMatch Verifier 300"),
				new KeyValuePair<uint, string>(NFExtractor.ModeNitgenFingkeyHamster, "Nitgen Fingkey Hamster"),
				new KeyValuePair<uint, string>(NFExtractor.ModeTestechBioI, "Testech Bio-i"),
				new KeyValuePair<uint, string>(NFExtractor.ModeDigentIzzix, "Digent Izzix"),
				new KeyValuePair<uint, string>(NFExtractor.ModeStartekFM200, "Startek FM200"),
				new KeyValuePair<uint, string>(NFExtractor.ModeFujitsuMbf200, "Fujitsu MBF200"),
				new KeyValuePair<uint, string>(NFExtractor.ModeFutronicFS80, "Futronic FS80"),
				new KeyValuePair<uint, string>(NFExtractor.ModeLighTuningLttC500, "LighTuning LTT-C500"),
				new KeyValuePair<uint, string>(NFExtractor.ModeTacomaCmos, "Tacoma CMOS")
			};
		private static readonly Dictionary<uint, int> vfeModesToIndexes = new Dictionary<uint, int>();
		private static readonly KeyValuePair<NfeTemplateSize, string>[] vfeTemplateSizes =
			{
				new KeyValuePair<NfeTemplateSize, string>(NfeTemplateSize.Small, "Small"),
				new KeyValuePair<NfeTemplateSize, string>(NfeTemplateSize.Large, "Large")
			};
		private static readonly Dictionary<NfeTemplateSize, int> VfeTemplateSizesToIndexes = new Dictionary<NfeTemplateSize, int>();
		private static readonly KeyValuePair<NfeReturnedImage, string>[] VfeReturnedImages =
			{
				new KeyValuePair<NfeReturnedImage, string>(NfeReturnedImage.None, "None"),
				new KeyValuePair<NfeReturnedImage, string>(NfeReturnedImage.Binarized, "Binarized"),
				new KeyValuePair<NfeReturnedImage, string>(NfeReturnedImage.Skeletonized, "Skeletonized")
			};
		private static readonly Dictionary<NfeReturnedImage, int> VfeReturnedImagesToIndexes = new Dictionary<NfeReturnedImage, int>();

		#endregion

		#region Static constructor

		static EnrollmentOptionsForm()
		{
			int i;
			i = 0;
			foreach (KeyValuePair<uint, string> vfeMode in vfeModes)
			{
				vfeModesToIndexes.Add(vfeMode.Key, i++);
			}
			i = 0;
			foreach (KeyValuePair<NfeTemplateSize, string> vfeTemplateSize in vfeTemplateSizes)
			{
				VfeTemplateSizesToIndexes.Add(vfeTemplateSize.Key, i++);
			}
			i = 0;
			foreach (KeyValuePair<NfeReturnedImage, string> vfeReturnedImage in VfeReturnedImages)
			{
				VfeReturnedImagesToIndexes.Add(vfeReturnedImage.Key, i++);
			}
		}

		#endregion

		#region Public constructor

		public EnrollmentOptionsForm()
		{
			InitializeComponent();

            //vfeQualityThresholdPercentLabel.Text = CultureInfo.CurrentCulture.NumberFormat.PercentSymbol;

            //vfeModeComboBox.BeginUpdate();
            //foreach (KeyValuePair<uint, string> vfeMode in vfeModes)
            //{
            //    vfeModeComboBox.Items.Add(vfeMode.Value);
            //}
            //vfeModeComboBox.EndUpdate();

			vfeTemplateSizeComboBox.BeginUpdate();
			foreach (KeyValuePair<NfeTemplateSize, string> vfeTemplateSize in vfeTemplateSizes)
			{
				vfeTemplateSizeComboBox.Items.Add(vfeTemplateSize.Value);
			}
			vfeTemplateSizeComboBox.EndUpdate();

            //vfeReturnedImageComboBox.BeginUpdate();
            //foreach (KeyValuePair<NfeReturnedImage, string> vfeReturnedImage in VfeReturnedImages)
            //{
            //    vfeReturnedImageComboBox.Items.Add(vfeReturnedImage.Value);
            //}
            //vfeReturnedImageComboBox.EndUpdate();

			vfeGeneralizationFarComboBox.BeginUpdate();
			vfeGeneralizationFarComboBox.Items.Add(0.001.ToString("P1"));
			vfeGeneralizationFarComboBox.Items.Add(0.0001.ToString("P2"));
			vfeGeneralizationFarComboBox.Items.Add(0.00001.ToString("P3"));
			vfeGeneralizationFarComboBox.EndUpdate();

			UpdateSettings();
		}

		#endregion

		#region Private methods

		private void UpdateSettings()
		{
			Settings settings = Settings.Default;
			minimalMinutiaCountNumericUpDown.Value = settings.FPMinimalMinutiaCount;
			generalizationTemplatesNumericUpDown.Value = settings.FPGeneralizationTemplates;
             look4DuplicatesCheckBox.Checked = settings.SearchForDuplicates;
			vfeUseQualityCheckBox.Checked = Data.NFExtractor.UseQuality;
			vfeQualityThresholdNumericUpDown.Value = Data.QualityToPercent(Data.NFExtractor.QualityThreshold);
            //vfeModeComboBox.SelectedIndex = vfeModesToIndexes[Data.NFExtractor.Mode];
			vfeTemplateSizeComboBox.SelectedIndex = VfeTemplateSizesToIndexes[Data.NFExtractor.TemplateSize];
            //vfeReturnedImageComboBox.SelectedIndex = VfeReturnedImagesToIndexes[Data.NFExtractor.ReturnedImage];
			vfeGeneralizationFarComboBox.Text = Data.MatchingThresholdToString(Data.NFExtractor.GeneralizationThreshold);
			vfeGeneralizationMaximalRotationNumericUpDown.Value = Data.MaximalRotationToDegrees(Data.NFExtractor.GeneralizationMaximalRotation);
		}

		#endregion

		private void EnrollmentOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				string error = null;
				try
				{
					int vfeGeneralizationThreshold = Data.MatchingThresholdFromString(vfeGeneralizationFarComboBox.Text);
				}
				catch
				{
					vfeGeneralizationFarComboBox.Select();
					error = "Generalization FAR is not valid";
				}
				if (error != null)
				{
					MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
				}
			}
		}

		private void EnrollmentOptionsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Settings settings = Settings.Default;
			if (DialogResult == DialogResult.OK)
			{
				settings.FPMinimalMinutiaCount = (int)minimalMinutiaCountNumericUpDown.Value;
				settings.FPGeneralizationTemplates = (int)generalizationTemplatesNumericUpDown.Value;
                settings.SearchForDuplicates = look4DuplicatesCheckBox.Checked;
				Data.NFExtractor.UseQuality = vfeUseQualityCheckBox.Checked;
				Data.NFExtractor.QualityThreshold = Data.QualityFromPercent((int)vfeQualityThresholdNumericUpDown.Value);
                //Data.NFExtractor.Mode = vfeModes[vfeModeComboBox.SelectedIndex].Key;
				Data.NFExtractor.TemplateSize = vfeTemplateSizes[vfeTemplateSizeComboBox.SelectedIndex].Key;
                //Data.NFExtractor.ReturnedImage = VfeReturnedImages[vfeReturnedImageComboBox.SelectedIndex].Key;
				Data.NFExtractor.GeneralizationThreshold = Data.MatchingThresholdFromString(vfeGeneralizationFarComboBox.Text);
				Data.NFExtractor.GeneralizationMaximalRotation = Data.MaximalRotationFromDegrees((int)vfeGeneralizationMaximalRotationNumericUpDown.Value);
			}
			else
			{
				settings.Reload();
				Data.NFExtractor.Reset();
				Data.UpdateNfe();
			}
			Data.UpdateNfeSettings();
			settings.Save();
		}

		private void defaultButton_Click(object sender, EventArgs e)
		{
			Data.ResetSetting("SearchForDuplicates");
			Data.ResetSetting("FPUseMinimalMinutiaCount");
			Data.ResetSetting("FPMinimalMinutiaCount");
			Data.ResetSetting("FPUseGeneralization");
			Data.ResetSetting("FPGeneralizationTemplates");
			Data.NFExtractor.Reset();
			UpdateSettings();
		}
	}
}
