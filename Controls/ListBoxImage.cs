using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BioMetrics.ListBoxImg
{
	public partial class ListBoxImage : System.Windows.Forms.ListBox
	{
		public class CData
		{
			private Bitmap image;
			private string similarity;
			private string faceRFC;
            private Int64 key;

			public Bitmap Image
			{
				get
				{
					return image;
				}
				set
				{
					image = (Bitmap)value.Clone();
				}
			}

			public string Similarity
			{
				get
				{
					return similarity;
				}
				set
				{
					similarity = value;
				}
			}

			public string FaceRFC
			{
				get
				{
                    return faceRFC;
				}
				set
				{
                    faceRFC = value;
				}
			}
            public Int64 KEY
            {
                get
                {
                    return key;
                }
                set
                {
                    key = value;
                }
            }
		}

		private ArrayList images;
		private int startSecondLine = 15;
		private int distanceBetweenImages = 10;

		[DefaultValue(15)]
		public int StartSecondLine
		{
			get
			{
				return startSecondLine;
			}
			set
			{
				startSecondLine = value;
			}
		}

		[DefaultValue(10)]
		public int DistanceBetweenImages
		{
			get
			{
				return distanceBetweenImages;
			}
			set
			{
				distanceBetweenImages = value;
			}
		}

		public ListBoxImage()
		{
			InitializeComponent();
			this.DrawMode = DrawMode.OwnerDrawVariable;
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			// TODO: Add custom paint code here

			// Calling the base class OnPaint
			base.OnPaint(pe);
		}

		public ArrayList Images
		{
			get 
			{
				return images;
			}
			set
			{
				this.Items.Clear();
				images = value;
				if (value != null) 
				{
					for (int i = 0; i < value.Count; i++)
					{
						this.Items.Add(0);
					}
				}			
			}
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (images != null)
			{
				if (images.Count > e.Index)
				{
					Graphics g = e.Graphics;
					CData data = (CData)images[e.Index];
					if (data.Image != null)
					{
						// Image holder
						int holderBoundX = e.Bounds.X + distanceBetweenImages,
							holderBoundY = e.Bounds.Y + distanceBetweenImages,
							holderHeight = e.Bounds.Height - distanceBetweenImages,
							holderWidth  = this.ColumnWidth;
						// Image itself
						int leftConerX = holderBoundX,
							leftConerY = holderBoundY,
							w = data.Image.Width,
							h = data.Image.Height,
							x = holderWidth,
							y = holderHeight;


						double imageRatio, itemRatio;
						imageRatio = (double)w / (double)h;
						itemRatio = (double)x / (double)y;

						if (imageRatio < itemRatio)
						{
							w = (int)((double)(w * y) / (double)h);
							leftConerX = (int)((double)(x - w) / (double)2) + distanceBetweenImages;
							h = holderHeight;
						}
						if (imageRatio > itemRatio)
						{
							h = (int)((double)(h * x) / (double)w);
							leftConerY = (int)(holderBoundY + ((double)(holderHeight - h) / (double)2));
							w = holderWidth;
						}
						g.DrawImage(data.Image, new Rectangle(leftConerX, leftConerY, w, h));

						g.DrawRectangle(new Pen(SystemBrushes.WindowText), new Rectangle(holderBoundX, holderBoundY,
							holderWidth, holderHeight));

						g.DrawString(data.FaceRFC.ToString(), e.Font,
							Brushes.Black, new PointF(holderWidth + distanceBetweenImages, holderBoundY));
						g.DrawString(data.Similarity, e.Font,
							Brushes.Black, new PointF(holderWidth + distanceBetweenImages, holderBoundY + startSecondLine));
					}
				}
			}

			base.OnDrawItem(e);
		}

		protected override void OnMeasureItem(MeasureItemEventArgs e)
		{
			if (images != null)
			{
				if (images.Count > e.Index)
				{
					e.ItemHeight += 2 * distanceBetweenImages;
					e.ItemWidth   = this.ColumnWidth;
				}
			}
		}	
	}
}
