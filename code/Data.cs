using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.ComponentModel;
using System.Globalization;
using System.Collections.ObjectModel;
using System.IO;
using BioTechSys.Images;
using System.Data;
using System.Data.OleDb;

namespace BioTechSys.Biometrics.Faces
{
	static class Data
	{
		public static string MatchingThresholdToString(int value)
		{
			double p = -value / 12.0;
			return string.Format(string.Format("{{0:P{0}}}", Math.Max(0, (int)Math.Ceiling(-p) - 2)), Math.Pow(10, p));
		}

		public static int MatchingThresholdFromString(string value)
		{
			double p = Math.Log10(Math.Max(double.Epsilon, Math.Min(1,
				double.Parse(value.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "")) / 100)));
			return Math.Max(0, (int)Math.Round(-12 * p));
		}

		public static int MaximalRotationToDegrees(byte value)
		{
			return (2 * value * 360 + 256) / (2 * 256);
		}

		public static byte MaximalRotationFromDegrees(int value)
		{
			return (byte)((2 * value * 256 + 360) / (2 * 360));
		}

		public static int QualityToPercent(byte value)
		{
			return (2 * value * 100 + 255) / (2 * 255);
		}

		public static byte QualityFromPercent(int value)
		{
			return (byte)((2 * value * 255 + 100) / (2 * 100));
		}

		public static string SizeToString(int size)
		{
			string value;
			int rem;
			size = Math.DivRem(size, 1024, out rem);
			value = rem + " byte(s)";
			if (size != 0)
			{
				size = Math.DivRem(size, 1024, out rem);
				value = rem + " KB " + value;
				if (size != 0)
				{
					size = Math.DivRem(size, 1024, out rem);
					value = rem + " MB " + value;
					if (size != 0)
					{
						size = Math.DivRem(size, 1024, out rem);
						value = rem + " GB " + value;
						if (size != 0)
						{
							value = size + " TB " + value;
						}
					}
				}
			}
			return value;
		}

		public static string TimeToString(TimeSpan time)
		{
			long t = time.Ticks / 10;
			string value;
			long rem;
			t = Math.DivRem(t, 1000, out rem);
			value = rem + " mks";
			if (t != 0)
			{
				t = Math.DivRem(t, 1000, out rem);
				value = rem + " ms " + value;
				if (t != 0)
				{
					t = Math.DivRem(t, 60, out rem);
					value = rem + " s " + value;
					if (t != 0)
					{
						t = Math.DivRem(t, 60, out rem);
						value = rem + " m " + value;
						if (t != 0)
						{
							t = Math.DivRem(t, 24, out rem);
							value = rem + " h " + value;
							if (t != 0)
							{
								value = t + " d " + value;
							}
						}
					}
				}
			}
			return value;
		}

		public static void GetImageFileFilters(out string openFilter, out string saveFilter)
		{
			StringBuilder openFileFilterBuilder = new StringBuilder();
			StringBuilder openAllFormatsFilterBuilder = new StringBuilder();
			StringBuilder saveFileFilterBuilder = new StringBuilder();
			foreach (NImageFormat imageFormat in NImageFormat.Formats)
			{
				if (imageFormat.CanRead)
				{
					openFileFilterBuilder.Append('|');
					openFileFilterBuilder.AppendFormat("{0} files ({1})|{1}",
						imageFormat.Name, imageFormat.FileFilter);
					if (openAllFormatsFilterBuilder.Length != 0) openAllFormatsFilterBuilder.Append(';');
					openAllFormatsFilterBuilder.Append(imageFormat.FileFilter);
				}
				if (imageFormat.CanWrite)
				{
					if (saveFileFilterBuilder.Length != 0) saveFileFilterBuilder.Append('|');
					saveFileFilterBuilder.AppendFormat("{0} files ({1})|{1}",
						imageFormat.Name, imageFormat.FileFilter);
				}
			}
			openFileFilterBuilder.Insert(0,
				string.Format("All supported images ({0})|{0}", openAllFormatsFilterBuilder.ToString()));
			openFileFilterBuilder.Append("|All files (*.*)|*.*");
			openFilter = openFileFilterBuilder.ToString();
			saveFilter = saveFileFilterBuilder.ToString();
		}

		public static string GetOpenImageFileFilter()
		{
			string openFilter, saveFilter;
			GetImageFileFilters(out openFilter, out saveFilter);
			return openFilter;
		}

		public static string GetSaveImageFileFilter()
		{
			string openFilter, saveFilter;
			GetImageFileFilters(out openFilter, out saveFilter);
			return saveFilter;
		}
	}
}
