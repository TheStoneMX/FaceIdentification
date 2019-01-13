using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BioHuellas.Properties;
using BioHuellas.Forms;
using BioTechSys.AnsiNist;
using BioTechSys.Share;

namespace BioTechSys.Face.Enroll.Forms
{
    public partial class snsp : Form
    {
        private static snsp _snspForm = null;
        private Singleton _singleton = null;
        private bool _FormValidation = true;

        public  void EnableButton()
        {
            CreateNist.Enabled = true;
        }
        public snsp()
        {
            InitializeComponent();
            IniBomboBoxes();
            _singleton = Singleton.Instance;
        }
        public static snsp GetChildInstance()
        {
            if (_snspForm == null) //if not created yet, Create an instance
                _snspForm = new snsp();

            return _snspForm;  //just created or created earlier.Return it
        }
        private void IniBomboBoxes()
        {
            #region Type 1 Boxes
            _cbxRecordType.SelectedIndex = 0;
            _cbxCSI.SelectedIndex = 0;
            _cbxPSI.SelectedIndex = 0;
            _cbxHPC.SelectedIndex = 0;
            #endregion

            #region Type 2 Boxes
            _cbxRecordType.SelectedIndex = 0;
            _Estado.SelectedIndex = 23;
            _cbxCSI.SelectedIndex = 3;
            _cbxPSI.SelectedIndex = 0;
            _cbxHPC.SelectedIndex = 0;
            _FingerPresent.SelectedIndex = 1;
            _Sexo.SelectedIndex = 0;

            #endregion
        }
        private void snsp_Load(object sender, EventArgs e)
        {

        }
        private void CreateNist_Click(object sender, EventArgs e)
        {
            // need to fill all combo correctly
            if (ValidateForm())
                return;

            #region  Type 2 field

            Singleton.NCP = _CriminalRef.Text;
            Singleton.ORN = _OtherRef.Text;
            Singleton.Clave1 = _TPID1.Text;
            Singleton.Clave2 = _TPID2.Text;
            Singleton.Clave3 = _TPID3.Text;
            Singleton.RecordType = _cbxRecordType.Value.ToString();
            Singleton.FamilyName = _FamilyName.Text;
            Singleton.FatherLastName = _GivenName.Text;
            Singleton.MothersLastName = _MotherName.Text;
            Singleton.CalleNumero = _Street.Text;
            Singleton.Ciudad = _Ciudad.Text;
            Singleton.Delegacion = _Delegacion.Text;
            Singleton.Estado = _Estado.Value.ToString();
            Singleton.Colonia = _Colonia.Text;
            Singleton.CodigoPostal = _Zip.Text;
            Singleton.FechaNacimiento = DateTime.Parse(_DOB.Text);
            Singleton.Sexo = _Sexo.Value.ToString();
            Singleton.Estatura = Convert.ToInt32(_Estatura.Text);
            Singleton.CodigoDelito = Convert.ToInt32(_CodigoDelito.Text);
            Singleton.Peligrosidad = _cbxHPC.Value.ToString();
            Singleton.FingerPresent = _FingerPresent.Value.ToString();
            Singleton.EstadoCivil = _cbxCSI.Value.ToString();
            Singleton.Comentarios = _Comments.Text;
            Singleton.SituacionPersona = _cbxPSI.Value.ToString();
            Singleton.Peso = Convert.ToInt32(_Peso.Text);
            #endregion

            // here after we process all the images and ready to save it to DB
            // we call _btnEnrollCanditate_Click to save dude's personal info.l
            BioTechSys.Share.Singleton.lastDudeKey = SaveDudesInfo();

            // now select the Huellas Windoes and Pass the Ciudadano ID
            MainForm form = (MainForm)this.MdiParent;
            form.SelectFacesWindow();
        }
        private Int64 SaveDudesInfo()
        {
            Int64 dudesKey = -1;

            CiudadanoManager mgr = new CiudadanoManager();
            dudesKey = mgr.InsertCiudadanInfo();
            return dudesKey;
        }

        #region Validation Methods for SNSP Form
        private bool ValidateForm()
        {
           _cbxRecordType_Validating(this, null);
            //_Estado_Validating(this, null);
            _Sexo_Validating(this, null);
            //_cbxCSI_Validating(this, null);
            //_cbxPSI_Validating(this, null);
            //_cbxHPC_Validating(this, null);
            
            if (_FormValidation)
            {
                MessageBox.Show("Por favor corrija los errores, antes de proceder a la captura de huellas");
                errorProvider.Clear();

                return true;
            }
            else
                return false;
                
        }
        private void _cbxRecordType_Validating(object sender, CancelEventArgs e)
        {
            if ( _cbxRecordType.SelectedIndex == 0 )
            {
                errorProvider.SetError(_cbxRecordType, "Porfavor Seleccione...");
                _FormValidation = true;
            }
            else
                _FormValidation = false;

        }
        private void _Estado_Validating(object sender, CancelEventArgs e)
        {
            if (_Estado.SelectedIndex == 23)
            {
                errorProvider.SetError(_Estado, "Porfavor Seleccione...");
                _FormValidation = true;
            }
            else
                _FormValidation = false;
        }
        private void _Sexo_Validating(object sender, CancelEventArgs e)
        {
            if (_Sexo.SelectedIndex == 0)
            {
                errorProvider.SetError(_Sexo, "Porfavor Seleccione...");
                _FormValidation = true;
            }
            else
                _FormValidation = false;
        }
        private void _cbxCSI_Validating(object sender, CancelEventArgs e)
        {
            if (_cbxCSI.SelectedIndex == 3)
            {
                errorProvider.SetError(_cbxCSI, "Porfavor Seleccione...");
                _FormValidation = true;
            }
            else
                _FormValidation = false;
        }
        private void _cbxPSI_Validating(object sender, CancelEventArgs e)
        {
            if (_cbxPSI.SelectedIndex == 0)
            {
                errorProvider.SetError(_cbxPSI, "Porfavor Seleccione...");
                _FormValidation = true;
            }
            else
                _FormValidation = false;
        }
        private void _cbxHPC_Validating(object sender, CancelEventArgs e)
        {
            if (_cbxHPC.SelectedIndex == 0)
            {
                errorProvider.SetError(_cbxHPC, "Porfavor Seleccione...");
                _FormValidation = true;
            }
            else
                _FormValidation = false;
        }
        private void _DOB_Validating(object sender, CancelEventArgs e)
        {
            if (_DOB.Text == "")
            {
                errorProvider.SetError(_DOB, "Porfavor Introdusca Fecha...");
                _FormValidation = true;
            }
            else
                _FormValidation = false;
        }
        #endregion

        public void resetAllTextFields()
        {
            _CriminalRef.Text = "";
            _OtherRef.Text = "";
            _TPID1.Text = "";
            _TPID2.Text = "";
            _TPID3.Text = "";
            _cbxRecordType.SelectedIndex = 0;
            _FamilyName.Text = "";
            _GivenName.Text = "";
            _MotherName.Text = "";
            _Street.Text = "";
            _Ciudad.Text = "";
            _Delegacion.Text = "";
            _Estado.SelectedIndex = 0;
            _Colonia.Text = "";
            _Zip.Text = "";
            _DOB.Text = "";
            _Sexo.SelectedIndex = 0;
            _Estatura.Text = "";
            _CodigoDelito.Text = "";
            _cbxHPC.Text = "";
            _FingerPresent.SelectedIndex = 0;
            _cbxCSI.SelectedIndex = 0;
            _Comments.Text = "";
            _cbxPSI.SelectedIndex = 0;
            _Peso.Text = "";

        }

        private void type1TranslNumberBox_Leave(object sender, EventArgs e)
        {
            _OtherRef.Text = type1TranslNumberBox.Text;
        }


    }
}
