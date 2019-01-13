using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BioTechSys.CrossMatch;


namespace BioTechSys.Face.Enroll.Forms
{
    public partial class scannerOptions : Form
    {

        private USB4XX _scanner;

        public scannerOptions()
        {
            InitializeComponent();

            /////////////
            _scanner = new USB4XX();
            /////////////
            // Init TrackBar´s
            BrilloTrackBar.Value = _scanner.Brightness; brightLabel.Text = _scanner.Brightness.ToString();
            ContrastTrackBar.Value = _scanner.Contrass; contrassLabel.Text = _scanner.Contrass.ToString();
            GainTrackBar.Value = _scanner.Gain; gainLabel.Text = _scanner.Gain.ToString();
        }

        private void GainTrackBar_ValueChanged(object sender, decimal value)
        {
            gainLabel.Text = GainTrackBar.Value.ToString();
        }

        private void ContrastTrackBar_ValueChanged(object sender, decimal value)
        {
            contrassLabel.Text = ContrastTrackBar.Value.ToString();
        }

        private void BrilloTrackBar_ValueChanged(object sender, decimal value)
        {
            brightLabel.Text = BrilloTrackBar.Value.ToString();
        }

        private void _SaveChanges_Click(object sender, EventArgs e)
        {
            _scanner.SaveSettings();
        }

        private void _recicleScanner_Click(object sender, EventArgs e)
        {
            _scanner.IniScanner(512, 512, pictureBox.Handle);
        }

        private void BrilloTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            _scanner.Brightness = Convert.ToByte(BrilloTrackBar.Value);
        }

        private void ContrastTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            _scanner.Contrass = Convert.ToByte(ContrastTrackBar.Value);
        }

        private void GainTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            _scanner.Gain = Convert.ToByte(GainTrackBar.Value);
        }

        private void scannerOptions_Load(object sender, EventArgs e)
        {
            if (_scanner.GoLiveMode(true, pictureBox.Handle) == 1)
                this.Close();
        }

        private void scannerOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            _scanner.StopLiveMode();
        }
    }
}
