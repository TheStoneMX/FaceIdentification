using System;
using System.Windows.Forms;
using Microsoft.Win32;
using BioTechSys.Face.Enroll.Forms;

namespace BioTechSys.Biometrics.Faces
{
	public class ControlPosition
	{
		private const string VerticalScreenResolution = "verticalScreenResolution";
		private const string HorizontalScreenResolution = "horizontalScreenResolution";

		private const string Top = "Top";
		private const string Left = "Left";
		private const string Width = "Width";
		private const string Height = "Height";

		private const string ItemHeight = "ItemHeight";
		private const string ColumnWidth = "ColumnWidth";

		public static void Load(Control control, string subKey)
		{
			using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(subKey))
			{
				try
				{
					if(Screen.PrimaryScreen.Bounds.Height != (int)registryKey.GetValue(VerticalScreenResolution) || Screen.PrimaryScreen.Bounds.Width  != (int)registryKey.GetValue(HorizontalScreenResolution))
					{//if screen resolutions differs
						return;
					}
				}
				catch(NullReferenceException)
				{
					return;
				}
			
				control.SuspendLayout();
                BioTechSys.Face.Enroll.Forms.MainForm form = control as MainForm;
				if(form != null)
				{
					try
					{
						form.StartPosition = FormStartPosition.Manual;
					}
					catch(NullReferenceException){}
				}

				try
				{
					control.Top = (int)registryKey.GetValue(control.Name + Top);
					control.Left = (int)registryKey.GetValue(control.Name + Left);
					control.Width = (int)registryKey.GetValue(control.Name + Width);
					control.Height = (int)registryKey.GetValue(control.Name + Height);
				}
				catch(NullReferenceException){}

				control.ResumeLayout();
			}
		}

		public static void Save(Control control, string subKey)
		{
			using(RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(subKey))
			{
				MainForm form = control as MainForm;
				if(form != null)
				{
					registryKey.SetValue(VerticalScreenResolution, Screen.PrimaryScreen.Bounds.Height);
					registryKey.SetValue(HorizontalScreenResolution, Screen.PrimaryScreen.Bounds.Width);
				}

				registryKey.SetValue(control.Name + Top, control.Top);
				registryKey.SetValue(control.Name + Left, control.Left);
				registryKey.SetValue(control.Name + Width, control.Width);
				registryKey.SetValue(control.Name + Height, control.Height);
			}
		}

		public static void LoadListBox(Control control, out int itemHeight, out int columnWidth, string subKey)
		{
			using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(subKey))
			{
				try
				{
					itemHeight = (int)registryKey.GetValue(control.Name + ItemHeight);
					columnWidth = (int)registryKey.GetValue(control.Name + ColumnWidth);
				}
				catch (NullReferenceException) 
				{
					itemHeight = 100;
					columnWidth = 150;
				}

			}
		}

		public static void SaveListBox(Control control, string subKey)
		{
			using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(subKey))
			{
				MainForm form = control as MainForm;
				if (form != null)
				{
					registryKey.SetValue(VerticalScreenResolution, Screen.PrimaryScreen.Bounds.Height);
					registryKey.SetValue(HorizontalScreenResolution, Screen.PrimaryScreen.Bounds.Width);
				}

				registryKey.SetValue(control.Name + ItemHeight, ((ListBox)control).ItemHeight);
				registryKey.SetValue(control.Name + ColumnWidth, ((ListBox)control).ColumnWidth);
			}
		}
	}
}
