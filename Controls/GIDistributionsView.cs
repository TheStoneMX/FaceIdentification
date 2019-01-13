using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Drawing.Drawing2D;

namespace BioHuellas
{
	public partial class GIDistributionsView : Panel
	{
		#region Public types

		public class GIDistributionViewItemCollection : Collection<GIDistributionsViewItem>
		{
			#region Private fields

			private GIDistributionsView owner;

			#endregion

			#region Internal constructor

			public GIDistributionViewItemCollection(GIDistributionsView owner)
			{
				this.owner = owner;
			}

			#endregion

			#region Protected methods

			protected override void InsertItem(int index, GIDistributionsViewItem item)
			{
				if (item.owner != null) throw new ArgumentException("Item already has an owner");
				base.InsertItem(index, item);
				item.owner = owner;
				item.index = index;
				int count = Items.Count;
				for (int i = index + 1; i != count; i++)
				{
					Items[i].index++;
				}
				owner.OnItemsChanged();
			}

			protected override void RemoveItem(int index)
			{
				GIDistributionsViewItem item = Items[index];
				base.RemoveItem(index);
				item.owner = null;
				item.index = -1;
				int count = Items.Count;
				for (int i = index; i != count; i++)
				{
					Items[i].index--;
				}
				owner.OnItemsChanged();
			}

			protected override void SetItem(int index, GIDistributionsViewItem item)
			{
				if (item.owner != null) throw new ArgumentException("Item already has an owner");
				GIDistributionsViewItem oldItem = Items[index];
				base.SetItem(index, item);
				oldItem.owner = null;
				oldItem.index = -1;
				item.owner = owner;
				item.index = index;
				owner.OnItemsChanged();
			}

			protected override void ClearItems()
			{
				foreach (GIDistributionsViewItem item in Items)
				{
					item.owner = null;
					item.index = -1;
				}
				base.ClearItems();
				owner.OnItemsChanged();
			}

			#endregion
		}

		#endregion

		#region Private fields

		private GIDistributionViewItemCollection items;
		private Brush backBrush;
		private Pen axisPen;
		private Pen subAxisPen;
		private Pen eerPen;
		private Brush fontBrush;
		private bool showXSubAxes = true;
		private bool showYSubAxes = false;
		private bool showEer = true;
		private Font labelFont;

		#endregion

		#region Public constructor

		public GIDistributionsView()
		{
			InitializeComponent();
			labelFont = new Font(Font, FontStyle.Bold);
			UpdateBackBrush();
			UpdateAxisPen();
			UpdateSubAxisPen();
			UpdateEerPen();
			UpdateFontBrush();
			items = new GIDistributionViewItemCollection(this);
			UpdateGraphArea();
			OnItemsChanged();
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.OptimizedDoubleBuffer
				| ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
		}

		#endregion

		#region Private methods

		private void OnItemsChanged()
		{
			Invalidate();
		}

		private void UpdateBackBrush()
		{
			if (backBrush != null) backBrush.Dispose();
			backBrush = new SolidBrush(BackColor);
		}

		private void UpdateAxisPen()
		{
			if (axisPen != null) axisPen.Dispose();
			axisPen = new Pen(ForeColor, 1);
		}

		private void UpdateSubAxisPen()
		{
			if (subAxisPen != null) subAxisPen.Dispose();
			subAxisPen = new Pen(ForeColor, 1);
		}

		private void UpdateEerPen()
		{
			if (eerPen != null) eerPen.Dispose();
			eerPen = new Pen(Color.Gray, 1);
		}

		private void UpdateFontBrush()
		{
			if (fontBrush != null) fontBrush.Dispose();
			fontBrush = new SolidBrush(ForeColor);
		}

		#endregion

		#region Internal methods

		internal void OnItemVisibleChanged(GIDistributionsViewItem item)
		{
			Invalidate();
			if (ItemVisibleChanged != null) ItemVisibleChanged(this, new GIDistributionsViewItemEventArgs(item.Index));
		}

		internal void OnItemColorChanged(GIDistributionsViewItem item)
		{
			Invalidate();
			if (ItemColorChanged != null) ItemColorChanged(this, new GIDistributionsViewItemEventArgs(item.Index));
		}

		#endregion

		#region Protected methods

		protected override void OnBackColorChanged(EventArgs e)
		{
			UpdateBackBrush();
			Invalidate();
			base.OnBackColorChanged(e);
		}

		protected override void OnForeColorChanged(EventArgs e)
		{
			UpdateAxisPen();
			UpdateSubAxisPen();
			UpdateFontBrush();
			Invalidate();
			base.OnForeColorChanged(e);
		}

		protected override void OnFontChanged(EventArgs e)
		{
			UpdateGraphArea();
			Invalidate();
			base.OnFontChanged(e);
		}

		private const int margin = 8;
		private const float pointSize = 5;
		private const float pointSizeHalf = pointSize / 2;
		private const float maxX = 1f;
		private const float minX = 0.0001f;
		private const float maxY = 1f;
		private const float minY = 0.001f;
		private const string xLabel = "FAR";
		private const string yLabel = "FRR";

		float maxXDivLog;
		float minXDivLog;
		float maxYDivLog;
		float minYDivLog;
		float maxXLog;
		float minXLog;
		float maxYLog;
		float minYLog;
		bool minXIsDiv;
		bool maxXIsDiv;
		bool minYIsDiv;
		bool maxYIsDiv;
		RectangleF graphRect;

		private static string ValueToString(float value)
		{
			return (value * 100).ToString("G4") + '%';
		}

		private static string ValueLogToString(float valueLog)
		{
			return ValueToString((float)Math.Pow(10, valueLog));
		}

		private float ValueLogToX(float valueLog)
		{
			valueLog = Math.Max(minXLog, Math.Min(maxXLog, valueLog));
			return graphRect.Left + (valueLog - minXLog) * (graphRect.Width - 1) / (maxXLog - minXLog);
		}

		private float ValueToX(float value)
		{
			return ValueLogToX(value < float.Epsilon ? minXLog : (float)Math.Log10(value));
		}

		private float ValueLogToY(float valueLog)
		{
			valueLog = Math.Max(minYLog, Math.Min(maxYLog, valueLog));
			return graphRect.Bottom - 1 - (valueLog - minYLog) * (graphRect.Height - 1) / (maxYLog - minYLog);
		}

		private float ValueToY(float value)
		{
			return ValueLogToY(value < float.Epsilon ? minYLog : (float)Math.Log10(value));
		}

		private void UpdateGraphArea()
		{
			maxXLog = (float)Math.Log10(maxX);
			minXLog = (float)Math.Log10(minX);
			maxYLog = (float)Math.Log10(maxY);
			minYLog = (float)Math.Log10(minY);
			maxXDivLog = (float)Math.Floor(maxXLog);
			minXDivLog = (float)Math.Ceiling(minXLog);
			maxYDivLog = (float)Math.Floor(maxYLog);
			minYDivLog = (float)Math.Ceiling(minYLog);
			minXIsDiv = Math.Abs(minXDivLog - minXLog) < float.Epsilon;
			maxXIsDiv = Math.Abs(maxXDivLog - maxXLog) < float.Epsilon;
			minYIsDiv = Math.Abs(minYDivLog - minYLog) < float.Epsilon;
			maxYIsDiv = Math.Abs(maxYDivLog - maxYLog) < float.Epsilon;
			float yAxisLabelWidth, xAxisLabelHeight, maxXLabelWidthHalf, maxYLabelHeightHalf;
			float labelHeight;
			using (Graphics g = CreateGraphics())
			{
				SizeF maxYLabelSize = g.MeasureString(ValueToString(maxY), Font);
				yAxisLabelWidth = Math.Max(g.MeasureString(ValueToString(minY), Font).Width, maxYLabelSize.Width);
				xAxisLabelHeight = Font.GetHeight(g);
				maxXLabelWidthHalf = g.MeasureString(ValueToString(maxX), Font).Width / 2;
				maxYLabelHeightHalf = maxYLabelSize.Height / 2;
				labelHeight = labelFont.GetHeight(g);
			}
			Rectangle cr = ClientRectangle;
			float left = cr.Left + margin + labelHeight + margin + yAxisLabelWidth + margin;
			float top = cr.Top + margin + maxYLabelHeightHalf;
			float right = Math.Max(left, cr.Right - margin - maxXLabelWidthHalf);
			float bottom = Math.Max(top, cr.Bottom - margin - labelHeight - margin - xAxisLabelHeight - margin);
			graphRect = RectangleF.FromLTRB(left, top, right, bottom);
		}

		private void DrawXAxis(float yValueLog, Graphics g)
		{
			float y = ValueLogToY(yValueLog);
			g.DrawLine(axisPen, graphRect.Left, y, graphRect.Right - 1, y);
			string label = ValueLogToString(yValueLog);
			SizeF labelSize = g.MeasureString(label, Font);
			g.DrawString(label, Font, fontBrush, graphRect.Left - margin - labelSize.Width, y - labelSize.Height / 2);
		}

		private void DrawXSubAxes(float yMinLog, float yMaxLog, Graphics g)
		{
			if (showXSubAxes)
			{
				float yMin = (float)Math.Pow(10, yMinLog);
				float yMax = (float)Math.Pow(10, yMaxLog);
				float yDelta = (float)Math.Pow(10, Math.Floor(yMinLog));
				for (float y = yDelta * 2; y < yMax; y += yDelta)
				{
					if (y - yMin > float.Epsilon)
					{
						float yy = ValueToY(y);
						g.DrawLine(subAxisPen, graphRect.Left, yy, graphRect.Right - 1, yy);
					}
				}
			}
		}

		private void DrawYAxis(float xValueLog, Graphics g)
		{
			float x = ValueLogToX(xValueLog);
			g.DrawLine(axisPen, x, graphRect.Top, x, graphRect.Bottom - 1);
			string label = ValueLogToString(xValueLog);
			SizeF labelSize = g.MeasureString(label, Font);
			g.DrawString(label, Font, fontBrush, x - labelSize.Width / 2, graphRect.Bottom + margin);
		}

		private void DrawYSubAxes(float xMinLog, float xMaxLog, Graphics g)
		{
			if (showYSubAxes)
			{
				float xMin = (float)Math.Pow(10, xMinLog);
				float xMax = (float)Math.Pow(10, xMaxLog);
				float xDelta = (float)Math.Pow(10, Math.Floor(xMinLog));
				for (float x = xDelta * 2; x < xMax; x += xDelta)
				{
					if (x - xMin > float.Epsilon)
					{
						float xx = ValueToX(x);
						g.DrawLine(subAxisPen, xx, graphRect.Top, xx, graphRect.Bottom - 1);
					}
				}
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;
			PixelOffsetMode lastPom = g.PixelOffsetMode;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;
			Rectangle cr = ClientRectangle;
			g.FillRectangle(backBrush, cr);

			// Draw y axes
			if (!minXIsDiv)
			{
				DrawYAxis(minXLog, g);
				DrawYSubAxes(minXLog, minXDivLog, g);
			}
			for (float xLog = minXDivLog; xLog <= maxXDivLog; xLog += 1)
			{
				DrawYAxis(xLog, g);
				DrawYSubAxes(xLog, xLog + 1, g);
			}
			if (!maxXIsDiv)
			{
				DrawYSubAxes(maxXDivLog, maxXLog, g);
				DrawYAxis(maxXLog, g);
			}

			// Draw X axes
			if (!minYIsDiv)
			{
				DrawXAxis(minYLog, g);
				DrawXSubAxes(minYLog, minYDivLog, g);
			}
			for (float yLog = minYDivLog; yLog <= maxYDivLog; yLog += 1)
			{
				DrawXAxis(yLog, g);
				DrawXSubAxes(yLog, yLog + 1, g);
			}
			if (!maxYIsDiv)
			{
				DrawXSubAxes(maxYDivLog, maxYLog, g);
				DrawXAxis(maxXLog, g);
			}

			// Draw EER line
			if (showEer)
			{
				float minLog = Math.Max(minXLog, minYLog);
				float maxLog = Math.Min(maxXLog, maxYLog);
				float minX = ValueLogToX(minLog);
				float minY = ValueLogToY(minLog);
				float maxX = ValueLogToX(maxLog);
				float maxY = ValueLogToY(maxLog);
				g.DrawLine(eerPen, minX, minY, maxX, maxY);
			}

			// Draw Y label
			SizeF yLabelSize = g.MeasureString(yLabel, labelFont);
			g.TranslateTransform(margin, (graphRect.Top + graphRect.Bottom + yLabelSize.Width) / 2);
			g.RotateTransform(270);
			g.DrawString(yLabel, labelFont, fontBrush, 0, 0);
			g.ResetTransform();


			// Draw X label
			SizeF xLabelSize = g.MeasureString(xLabel, labelFont);
			g.DrawString(xLabel, labelFont, fontBrush, (graphRect.Left + graphRect.Right - xLabelSize.Width) / 2, cr.Bottom - margin - xLabelSize.Height);

			// Draw points
			foreach(GIDistributionsViewItem item in items)
			{
				if (item.Visible)
				{
					using (Pen pen = new Pen(item.Color, 1))
					{
						using (Brush brush = new SolidBrush(item.Color))
						{
							float lastX = float.NaN, lastY = float.NaN;
							foreach (GIProbabilityValueTriple pvp in item.Distribution.GetFarAgainstFrrPoints())
							{
								float y = ValueToY((float)pvp.GenuineProbability);
								float x = ValueToX((float)pvp.ImpostorProbability);
								if (!float.IsNaN(lastX) && !float.IsNaN(lastY))
								{
									g.DrawLine(pen, lastX, lastY, x, y);
								}
								lastX = x;
								lastY = y;
								g.FillRectangle(brush, x - pointSizeHalf, y - pointSizeHalf, pointSize, pointSize);
							}
						}
					}
				}
			}

			g.PixelOffsetMode = lastPom;
			base.OnPaint(pe);
		}

		protected override void OnClientSizeChanged(EventArgs e)
		{
			UpdateGraphArea();
			base.OnClientSizeChanged(e);
		}

		#endregion

		#region Public properties

		public GIDistributionViewItemCollection Items
		{
			get
			{
				return items;
			}
		}

		#endregion

		#region Public events

		public event EventHandler<GIDistributionsViewItemEventArgs> ItemVisibleChanged;
		public event EventHandler<GIDistributionsViewItemEventArgs> ItemColorChanged;

		#endregion
	}

	public class GIDistributionsViewItem
	{
		#region Private fields

		private GIDistribution distribution;
		private bool visible;
		private Color color;

		#endregion

		#region Internal fields

		internal GIDistributionsView owner = null;
		internal int index = -1;

		#endregion

		#region Public constructor

		public GIDistributionsViewItem(GIDistribution distribution)
		{
			if (distribution == null) throw new ArgumentNullException("distribution");
			this.distribution = distribution;
			visible = true;
			color = Color.Blue;
		}

		#endregion

		#region Public properties

		public GIDistributionsView Owner
		{
			get
			{
				return owner;
			}
		}

		public int Index
		{
			get
			{
				return index;
			}
		}

		public GIDistribution Distribution
		{
			get
			{
				return distribution;
			}
		}

		public bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				if (visible != value)
				{
					visible = value;
					if(owner != null) owner.OnItemVisibleChanged(this);
				}
			}
		}

		public Color Color
		{
			get
			{
				return color;
			}
			set
			{
				if (color != value)
				{
					color = value;
					if (owner != null) owner.OnItemColorChanged(this);
				}
			}
		}

		#endregion
	}

	public class GIDistributionsViewItemEventArgs : EventArgs
	{
		#region Private fields

		private int index;

		#endregion

		#region Public constructor

		public GIDistributionsViewItemEventArgs(int index)
		{
			this.index = index;
		}

		#endregion

		#region Public properties

		public int Index
		{
			get
			{
				return index;
			}
		}

		#endregion
	}
}
