using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using BioTechSys.Cameras;

namespace BioTechSys.FacesEnroll
{
	public class DeviceForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button ok;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.ColumnHeader Name2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView devicesList;

		private System.ComponentModel.Container components = null;
		private Camera activeDevice;
		private Label lblVideoFormat;
		private ComboBox cmbVideoFormat;
		private bool IsDeviceSelected;

		public DeviceForm(CameraMan.CameraCollection devices, Camera activeD)
		{
			InitializeComponent();
			activeDevice = activeD;
			ListViewItem dev = null;
			foreach (Camera device in devices)
			{
				dev = new ListViewItem(device.ID);
				dev.Tag = device;
				if (activeD != null)
				{
					if (activeD == device)
					{
						dev.Selected = true;
					}
				}
				devicesList.Items.Add(dev);
			}
			if ((activeD == null) && (devicesList.Items.Count > 0))
			{
				devicesList.Items[0].Selected = true;
			}

			if (activeD != null)
			{
				FillVideoFormatCmb(activeD);
			}
			else
			{
				if (devicesList.Items.Count > 0)
				{
					FillVideoFormatCmb((Camera)devicesList.Items[0].Tag);
				}
			}
			devicesList.Focus();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceForm));
            this.devicesList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.Name2 = new System.Windows.Forms.ColumnHeader();
            this.ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.lblVideoFormat = new System.Windows.Forms.Label();
            this.cmbVideoFormat = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // devicesList
            // 
            this.devicesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.devicesList.Location = new System.Drawing.Point(8, 8);
            this.devicesList.Name = "devicesList";
            this.devicesList.Size = new System.Drawing.Size(392, 152);
            this.devicesList.TabIndex = 0;
            this.devicesList.UseCompatibleStateImageBehavior = false;
            this.devicesList.View = System.Windows.Forms.View.Details;
            this.devicesList.DoubleClick += new System.EventHandler(this.devicesList_DoubleClick);
            this.devicesList.Click += new System.EventHandler(this.devicesList_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nombre";
            this.columnHeader1.Width = 351;
            // 
            // Name2
            // 
            this.Name2.Width = 400;
            // 
            // ok
            // 
            this.ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ok.Location = new System.Drawing.Point(244, 169);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 3;
            this.ok.Text = "OK";
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(325, 169);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Cancel";
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // lblVideoFormat
            // 
            this.lblVideoFormat.AutoSize = true;
            this.lblVideoFormat.Location = new System.Drawing.Point(12, 172);
            this.lblVideoFormat.Name = "lblVideoFormat";
            this.lblVideoFormat.Size = new System.Drawing.Size(93, 13);
            this.lblVideoFormat.TabIndex = 1;
            this.lblVideoFormat.Text = "Formato de Video:";
            // 
            // cmbVideoFormat
            // 
            this.cmbVideoFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVideoFormat.FormattingEnabled = true;
            this.cmbVideoFormat.Location = new System.Drawing.Point(111, 170);
            this.cmbVideoFormat.Name = "cmbVideoFormat";
            this.cmbVideoFormat.Size = new System.Drawing.Size(121, 21);
            this.cmbVideoFormat.TabIndex = 2;
            // 
            // DeviceForm
            // 
            this.AcceptButton = this.ok;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(408, 203);
            this.Controls.Add(this.cmbVideoFormat);
            this.Controls.Add(this.lblVideoFormat);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.devicesList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceForm";
            this.ShowInTaskbar = false;
            this.Text = "Elija el dispositivo";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DeviceForm_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void cancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void ok_Click(object sender, System.EventArgs e)
		{
			if (devicesList.SelectedItems.Count != 1)
			{
				MessageBox.Show("Select one device from the list");
				IsDeviceSelected = false;
			}
			else
			{
				ListViewItem selectedItem = devicesList.SelectedItems[0];
				activeDevice = (Camera)selectedItem.Tag;
				activeDevice.StopCapturing();
				activeDevice.VideoFormat = activeDevice.GetAvailableVideoFormats()[cmbVideoFormat.SelectedIndex];
				IsDeviceSelected = true;
				Close();
			}
		}

		public Camera ActiveDevice
		{
			set
			{
				activeDevice = value;
			}
			get
			{
				return activeDevice;
			}
		}

		private void devicesList_DoubleClick(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			ok_Click(sender, null);
		}

		private void DeviceForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (this.DialogResult == DialogResult.OK && !IsDeviceSelected)
			{
				e.Cancel = true;
			}
		}

		private void FillVideoFormatCmb(Camera device)
		{
			int position = 0;
			CameraVideoFormat currentFormat;

			cmbVideoFormat.Items.Clear();
			CameraVideoFormat[] formats = device.GetAvailableVideoFormats();
			currentFormat = device.VideoFormat;
			foreach (CameraVideoFormat format in formats)
			{
				position = cmbVideoFormat.Items.Add(String.Format("{0:d}x{1:d} {2:f2} fps", format.FrameWidth, format.FrameHeight, format.FrameRate));
				if ((format.FrameHeight == currentFormat.FrameHeight) && (format.FrameWidth == currentFormat.FrameWidth) && (format.FrameRate == currentFormat.FrameRate))
				{
					cmbVideoFormat.SelectedIndex = position;
				}
			}
			if (!(cmbVideoFormat.SelectedIndex >= 0))
			{
				cmbVideoFormat.SelectedIndex = cmbVideoFormat.Items.Count - 1;
			}
		}

		private void devicesList_Click(object sender, EventArgs e)
		{
			FillVideoFormatCmb((Camera)devicesList.SelectedItems[0].Tag);
		}
	}
}
