using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BioTechSys.FacesEnroll
{
	public class EnrollForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label ImageIDlabel;
		private System.Windows.Forms.Button enroll;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.TextBox imID;
		private System.ComponentModel.Container components = null;

		public EnrollForm()
		{
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnrollForm));
            this.imID = new System.Windows.Forms.TextBox();
            this.ImageIDlabel = new System.Windows.Forms.Label();
            this.enroll = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // imID
            // 
            this.imID.Location = new System.Drawing.Point(12, 45);
            this.imID.Name = "imID";
            this.imID.Size = new System.Drawing.Size(238, 20);
            this.imID.TabIndex = 0;
            // 
            // ImageIDlabel
            // 
            this.ImageIDlabel.Location = new System.Drawing.Point(12, 21);
            this.ImageIDlabel.Name = "ImageIDlabel";
            this.ImageIDlabel.Size = new System.Drawing.Size(133, 16);
            this.ImageIDlabel.TabIndex = 1;
            this.ImageIDlabel.Text = "Nombre de la Imagen";
            // 
            // enroll
            // 
            this.enroll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.enroll.Location = new System.Drawing.Point(15, 71);
            this.enroll.Name = "enroll";
            this.enroll.Size = new System.Drawing.Size(75, 23);
            this.enroll.TabIndex = 2;
            this.enroll.Text = "&Enrrolar";
            this.enroll.Click += new System.EventHandler(this.enroll_Click);
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(175, 71);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "&Cancelar";
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // EnrollForm
            // 
            this.AcceptButton = this.enroll;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CancelButton = this.cancel;
            this.ClientSize = new System.Drawing.Size(272, 112);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.enroll);
            this.Controls.Add(this.ImageIDlabel);
            this.Controls.Add(this.imID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnrollForm";
            this.ShowInTaskbar = false;
            this.Text = "Enrrolar";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private string imageID;
		private void enroll_Click(object sender, System.EventArgs e)
		{
			imageID = imID.Text;
			this.Close();
		}

		private void cancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		public string ImageID
		{
			get
			{
				return imageID;
			}
		}
	}
}
