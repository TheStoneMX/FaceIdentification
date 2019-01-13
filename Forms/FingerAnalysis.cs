using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using IPLab;

using Neurotec.Biometrics.Gui;
using Neurotec.Images;

using AForge;
using AForge.Math;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using rpaulo.toolbar;

namespace BioHuellas
{

    public partial class FingerAnalysis : Form
    {

        private System.Windows.Forms.ToolBarButton toolBarButton4;
        private System.Windows.Forms.ToolBarButton aboutButton;
        private System.Windows.Forms.StatusBarPanel hslPanel;
        private System.Drawing.Bitmap backup = null;
        private System.Drawing.Bitmap _image = null;
        private bool _RememberOnChange = true;
        private string fileName = null;
        private int width;
        private int height;
        private float zoom = 1;
        private bool cropping = false;
        private bool dragging = false;
        private Point start, end, startW, endW;
        public NFView nfView = new NFView();
        private ToolBarManager toolBarManager;

        // Events
        public delegate void SelectionEventHandler(object sender, SelectionEventArgs e);

        public event EventHandler DocumentChanged;
        public event EventHandler ZoomChanged;
        public event SelectionEventHandler MouseImagePosition;
        public event SelectionEventHandler SelectionChanged;


        // Image property
        public Bitmap Image
        {
            get { return _image; }
        }
        // Width property
        public int ImageWidth
        {
            get { return width; }
        }
        // Height property
        public int ImageHeight
        {
            get { return height; }
        }
        // Zoom property
        public float Zoom
        {
            get { return zoom; }
        }      

        public FingerAnalysis( Bitmap Huella )
        {
            _image = Huella;
            InitializeComponent();
            Init();
        }
        // Init the document
        private void Init()
        {
            // form style
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
            // init scroll bars
            this.AutoScroll = true;

            toolBarManager = new ToolBarManager(this, this);

            // add toolbars
            ToolBarDockHolder holder;

            // image toolbar
            imageToolBar.Text = "Image Tool Bar";
            holder = toolBarManager.AddControl(imageToolBar);
            holder.AllowedBorders = AllowedBorders.Top | AllowedBorders.Left | AllowedBorders.Right;
            
            
            /// -------------------------------------------------------------------------- ///
            /// 
            //nfView.Dock = DockStyle.Fill;
            //nfView.ShownImage = ShownImage.Result;
            //nfView.Image = _image;

            //UseWaitCursor = true;
            //NGrayscaleImage resultImage = (NGrayscaleImage)_image.Clone();

            //byte[] template;
            //try
            //{
            //    template = Data.VFExtractor.Extract(resultImage, NFPosition.Unknown, NFImpressionType.LiveScanPlain);
            //}
            //catch (Exception e)
            //{
            //    string text = string.Format("Extractión error: {0}", e.Message);
            //    //LogLine(text);
            //    MessageBox.Show(text, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //UseWaitCursor = false;

            //NFRecord templ = template == null ? null : new NFRecord(template);
            //if (resultImages[availableCount] != null)
            //    resultImages[availableCount].Dispose();

            //resultImages[availableCount] = resultImage;
            //this.resultImage = resultImage;

            //using (NImage ri = NImages.GetGrayscaleColorWrapper(resultImage, resultImageMinColor, resultImageMaxColor))
            //{
            //    if (resultBitmaps[availableCount] != null)
            //        resultBitmaps[availableCount].Dispose();

            //    resultBitmaps[availableCount] = ri.ToBitmap();
            //}

            //templates[availableCount] = template;
            //nfView2.ResultImage = resultBitmaps[availableCount];
            //if (nfView2.Template != null)
            //    nfView2.Template.Dispose();

            //nfView2.Template = templ;
            //OnResultImageChanged();
            
            /// --------------------------------------------------------------------------///
            /// 

            UpdateSize();
        }

        private void imageToolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
                FingerAnalysisCommands[] cmd = new FingerAnalysisCommands[]
				{
						FingerAnalysisCommands.Clone, FingerAnalysisCommands.Crop,
						FingerAnalysisCommands.ZoomIn, FingerAnalysisCommands.ZoomOut,
						FingerAnalysisCommands.ZoomOriginal, FingerAnalysisCommands.FitToSize,
						FingerAnalysisCommands.Levels, FingerAnalysisCommands.Grayscale,
						FingerAnalysisCommands.Threshold, FingerAnalysisCommands.Morphology,
						FingerAnalysisCommands.Convolution, FingerAnalysisCommands.Resize,
						FingerAnalysisCommands.Rotate, FingerAnalysisCommands.Saturation,
						FingerAnalysisCommands.Fourier
				};

                 ExecuteCommand(cmd[e.Button.ImageIndex]);
        }

        // Paint image
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_image != null)
            {
                Graphics g = e.Graphics;
                Rectangle rc = ClientRectangle;
                Pen pen = new Pen(Color.FromArgb(0, 0, 0));

                int width = (int)(this.width * zoom);
                int height = (int)(this.height * zoom);
                int x = (rc.Width < width) ? this.AutoScrollPosition.X : (rc.Width - width) / 2;
                int y = (rc.Height < height) ? this.AutoScrollPosition.Y : (rc.Height - height) / 2;

                // draw rectangle around the image
                g.DrawRectangle(pen, x - 1, y - 1, width + 1, height + 1);

                // set nearest neighbor interpolation to avoid image smoothing
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                // draw image
                g.DrawImage(_image, x, y, width, height);

                pen.Dispose();
            }
        }
        
        
        // Execute command
        public void ExecuteCommand(FingerAnalysisCommands cmd)
        {
            switch (cmd)
            {
                case FingerAnalysisCommands.Clone:		// clone the image
                    break;
                case FingerAnalysisCommands.Crop:			// crop the image
                    break;
                case FingerAnalysisCommands.ZoomIn:		// zoom in
                    ZoomIn();
                    break;
                case FingerAnalysisCommands.ZoomOut:		// zoom out
                    ZoomOut();
                    break;
                case FingerAnalysisCommands.ZoomOriginal:	// original size
                    zoom = 1;
                    UpdateZoom();
                    break;
                case FingerAnalysisCommands.FitToSize:	// fit to screen
                    FitToScreen();
                    break;
                case FingerAnalysisCommands.Levels:		// levels
                    Levels();
                    break;
                case FingerAnalysisCommands.Grayscale:	// grayscale
                    break;
                case FingerAnalysisCommands.Threshold:	// threshold
                    Threshold();
                    break;
                case FingerAnalysisCommands.Morphology:	// morphology
                    break;
                case FingerAnalysisCommands.Convolution:	// convolution
                    Convolution();
                    break;
                case FingerAnalysisCommands.Resize:		// resize the image
                    ResizeImage();
                    break;
                case FingerAnalysisCommands.Rotate:		// rotate the image
                    RotateImage();
                    break;
                case FingerAnalysisCommands.Brightness:	// adjust brightness
                    Brightness();
                    break;
                case FingerAnalysisCommands.Contrast:		// modify contrast
                    Contrast();
                    break;
                case FingerAnalysisCommands.Saturation:	// adjust saturation
                    Saturation();
                    break;
                case FingerAnalysisCommands.Fourier:		// fourier transformation
                    break;
            }
        }
        // Update document and notify client about changes
        private void UpdateNewImage()
        {
            // update size
            UpdateSize();
            // repaint
            Invalidate();
        }
        // Update document size 
        private void UpdateSize()
        {
            // image dimension
            width = _image.Width;
            height = _image.Height;

            // scroll bar size
            this.AutoScrollMinSize = new Size((int)(width * zoom), (int)(height * zoom));
        }
        
        // Adjust saturation using HSL
        private void Saturation()
        {
            if (_image.PixelFormat != PixelFormat.Format24bppRgb)
            {
                MessageBox.Show("Saturation filter using HSL color space is available for color images only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaturationForm form = new SaturationForm();
            form.Image = _image;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter(form.Filter);
            }
        }

        // Modify contrast
        private void Contrast()
        {
            if (_image.PixelFormat != PixelFormat.Format24bppRgb)
            {
                MessageBox.Show("Contrast filter using HSL color space is available for color images only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ContrastForm form = new ContrastForm();
            form.Image = _image;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter(form.Filter);
            }
        }


        private void Brightness()
        {
            if (_image.PixelFormat != PixelFormat.Format24bppRgb)
            {
                MessageBox.Show("Brightness filter using HSL color space is available for color images only", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            BrightnessForm form = new BrightnessForm();
            form.Image = _image;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter(form.Filter);
            }
        }        
        // Rotate the image
        private void RotateImage()
        {
            RotateForm form = new RotateForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter(form.Filter);
            }
        }

        // Resize the image
        private void ResizeImage()
        {
            ResizeForm form = new ResizeForm();

            form.OriginalSize = new Size(width, height);

            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter(form.Filter);
            }
        }
        
        // Custom convolution filter
        private void Convolution()
        {
            ConvolutionForm form = new ConvolutionForm();

            form.Image = _image;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter(form.Filter);
            }
        }
        
        // Threshold binarization
        private void Threshold()
        {
            ThresholdForm form = new ThresholdForm();

            // set image to preview
            form.Image = _image;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter(form.Filter);
            }
        }

        private void Levels()
        {
            LevelsLinearForm form = new LevelsLinearForm(new ImageStatistics(_image));

            form.Image = _image;

            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyFilter(form.Filter);
            }
        }
        // Restore image to previous
        private void backImageItem_Click(object sender, System.EventArgs e)
        {
            if (backup != null)
            {
                // release current image
                _image.Dispose();
                // restore
                _image = backup;
                backup = null;

                // update
                UpdateNewImage();
            }
        }
     
        // Zoom In image
        private void ZoomIn()
        {
            float z = zoom * 1.5f;

            if (z <= 10)
            {
                zoom = z;
                UpdateZoom();
            }
        }
        private void ZoomOut()
        {
            float z = zoom / 1.5f;

            if (z >= 0.05)
            {
                zoom = z;
                UpdateZoom();
            }
        }
        // Update zoom factor
        private void UpdateZoom()
        {
            this.AutoScrollMinSize = new Size((int)(width * zoom), (int)(height * zoom));
            this.Invalidate();

            // notify host
            if (ZoomChanged != null)
                ZoomChanged(this, null);
        }
        // Fit to size
        private void FitToScreen()
        {
            Rectangle rc = ClientRectangle;

            zoom = Math.Min((float)rc.Width / (width + 2), (float)rc.Height / (height + 2));

            UpdateZoom();
        }

        // Zoom image
        private void zoomItem_Click(object sender, System.EventArgs e)
        {
            // get menu item text
            String t = ((MenuItem)sender).Text;
            // parse it`s value
            int i = int.Parse(t.Remove(t.Length - 1, 1));
            // calc zoom factor
            zoom = (float)i / 100;

            UpdateZoom();
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (dockManager.ActiveDocument != null)
            //{
            //    try
            //    {
            //        printDialog.Document = printDocument;
            //        if (printDialog.ShowDialog() == DialogResult.OK)
            //        {
            //            printDocument.Print();
            //        }
            //    }
            //    catch (InvalidPrinterException)
            //    {
            //        MessageBox.Show(this, "Failed accessing printer device", "Error",
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        private void vistaPreviaDeImprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (dockManager.ActiveDocument != null)
            //{
            //    try
            //    {
            //        printPreviewDialog.Document = printDocument;
            //        printPreviewDialog.ShowDialog();
            //    }
            //    catch (InvalidPrinterException)
            //    {
            //        MessageBox.Show(this, "Failed accessing printer device", "Error",
            //            MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }
        // Apply filter on the image
        private void ApplyFilter(IFilter filter)
        {
            try
            {
                // set wait cursor
                this.Cursor = Cursors.WaitCursor;

                // apply filter to the image
                Bitmap newImage = filter.Apply(_image);

                if (_RememberOnChange)
                {
                    // backup current image
                    if (backup != null)
                        backup.Dispose();

                    backup = _image;
                }
                else
                {
                    // release current image
                    _image.Dispose();
                }

                _image = newImage;

                // update
                UpdateNewImage();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Selected filter can not be applied to the image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // restore cursor
                this.Cursor = Cursors.Default;
            }
        }

        private void FingerAnalysis_Load(object sender, EventArgs e)
        {

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    // Selection arguments
    public class SelectionEventArgs : EventArgs
    {
        private Point location;
        private Size size;

        // Constructors
        public SelectionEventArgs(Point location)
        {
            this.location = location;
        }
        public SelectionEventArgs(Point location, Size size)
        {
            this.location = location;
            this.size = size;
        }

        // Location property
        public Point Location
        {
            get { return location; }
        }
        // Size property
        public Size Size
        {
            get { return size; }
        }
    }


    // Commands
    public enum FingerAnalysisCommands
    {
        Clone,
        Crop,
        ZoomIn,
        ZoomOut,
        ZoomOriginal,
        FitToSize,
        Levels,
        Grayscale,
        Threshold,
        Morphology,
        Convolution,
        Resize,
        Rotate,
        Brightness,
        Contrast,
        Saturation,
        Fourier
    }

}