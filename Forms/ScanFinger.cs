using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BioAntecedentes
{
    public partial class ScanFinger : Form
    {
        private USB4XX scanner;
        private Bitmap _FingerPrintImage;
        private bool _LiveMode = false;

        #region Prperties
        public Bitmap FingerPrintImage
        {
            get
            {
                return _FingerPrintImage;
            }
            set
            {
                if (value != null)
                    _FingerPrintImage = value;
                else
                    return;
            }
        }
        #endregion

        public ScanFinger()
        {
            InitializeComponent();
            scanner = new USB4XX();
            //scanner.IniScanner(512, 512, pictureBox1.Handle);
        }

        private void ScanFinger_Load(object sender, EventArgs e)
        {
            _GoLive.Text = "Capturar";
            scanner.GoLiveMode(true, pictureBox1.Handle);
        }

        private void ScanFinger_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void GoLive_Click(object sender, EventArgs e)
        {
            _FingerPrintImage = scanner.GetLiveFingerPrint();
        }

    }
}
