using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BioTechSys.FacesEnroll
{
	public partial class ResultsOptionsForm : Form
	{
		public event EventHandler ApplyButton;

		public ResultsOptionsForm()
		{
			InitializeComponent();
			widthTextBox.Focus();
		}

		void applyButton_Click(object sender, EventArgs e)
		{
			Control currentControl = null;
			try
			{
				currentControl = heightTextBox;
				int AreaHeight_ = AreaHeight;
				if (ApplyButton != null) ApplyButton(sender, e);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				currentControl.Focus();
			}
		}

		public int AreaWidth
		{
			set
			{
				widthTextBox.Text = value.ToString();
			}
			get
			{
				return int.Parse(widthTextBox.Text);
			}
		}

		public int AreaHeight
		{
			set
			{
				heightTextBox.Text = value.ToString();
			}
			get
			{
				int height = int.Parse(heightTextBox.Text);
				if (height > 255)
				{
					throw new Exception("Invalid value for results area. The number should be between 0 and 255.)");
				}

				return height;
			}
		}

		private void ResultsOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				Control currentControl = null;
				try
				{
					currentControl = widthTextBox;
					int AreaWidth_ = AreaWidth;
					currentControl = heightTextBox;
					int AreaHeight_ = AreaHeight;
				}
				catch(Exception ex)
				{
					e.Cancel = true;
					MessageBox.Show(ex.Message);
					currentControl.Focus();
				}
			}
		}

		private void setDefaultButton_Click(object sender, EventArgs e)
		{
			widthTextBox.Text = "150";
			heightTextBox.Text = "100";
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
