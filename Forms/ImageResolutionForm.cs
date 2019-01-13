using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BioTechSys.FacesEnroll
{
	public partial class ImageResolutionForm : System.Windows.Forms.Form
	{
		#region Public constructor

		public ImageResolutionForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Public properties

		public float HorzResolution
		{
			get
			{
				return float.Parse(horzResolutionTextBox.Text);
			}
			set
			{
				horzResolutionTextBox.Text = value.ToString();
			}
		}

		public float VertResolution
		{
			get
			{
				return float.Parse(vertResolutionTextBox.Text);
			}
			set
			{
				vertResolutionTextBox.Text = value.ToString();
			}
		}

		#endregion

		private void ImageResolutionForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				string error = null;
				float horzResolution, vertResolution;
				if (!float.TryParse(horzResolutionTextBox.Text, out horzResolution))
				{
					horzResolutionTextBox.Select();
					error = "Horz resolution is not valid";
				}
				else if (!float.TryParse(vertResolutionTextBox.Text, out vertResolution))
				{
					vertResolutionTextBox.Select();
					error = "Vert resolution is not valid";
				}
				if (error != null)
				{
					MessageBox.Show(error, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
				}
			}
		}
	}
}
