using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BioHuellas.Properties;


namespace BioTechSys.Face.Enroll.Forms
{
    public partial class SnspPropForm : Form
    {
        public SnspPropForm()
        {
            InitializeComponent();
        }

        private void SnspPropForm_Load(object sender, EventArgs e)
        {
            Settings settings = Settings.Default;
            type1DestinationBox.Text = settings.snsp_Destination;
            type1OriginatingBox.Text = settings.snsp_Origen;
            _toEmail.Text = settings.snsp_toEmail;
            _userEmail.Text = settings.snsp_userEmail;
            _userPassword.Text = settings.snsp_password;
            _UserName.Text = settings.snsp_userName;
            _HostName.Text = settings.snsp_hostName;
            _fromIPAddress.Text = settings.snsp_fromIPAddress;
            _toIPAddress.Text = settings.snsp_toIPAddress;
            _fromIPAddress.Text = settings.snsp_fromIPAddress;
            _snsp_popPort.Text = settings.snsp_smtpPort;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            Settings settings = Settings.Default;
            settings.snsp_Destination = type1DestinationBox.Text;
            settings.snsp_Origen = type1OriginatingBox.Text;
            settings.snsp_userName = _UserName.Text;
            settings.snsp_toEmail = _toEmail.Text;
            settings.snsp_userEmail = _userEmail.Text;
            settings.snsp_password = _userPassword.Text;
            settings.snsp_hostName = _HostName.Text;
            settings.snsp_fromIPAddress = _fromIPAddress.Text;
            settings.snsp_toIPAddress = _toIPAddress.Text;
            settings.snsp_fromIPAddress = _fromIPAddress.Text;
            settings.snsp_smtpPort = _snsp_popPort.Text;
            
            settings.Save();
        }
    }
}
