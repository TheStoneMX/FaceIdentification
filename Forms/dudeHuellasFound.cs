using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting;
using System.IO;

using BioTechSys.Controls;
using BioTechSys.Share;


namespace BioTechSys.Face.Enroll.Forms
{
    public partial class dudeHuellasFound : Form
    {
        #region Private Fields
        private List<MatchingResult> _IncomingDudes;
        private ArrayList matchedImagesArrayList = new ArrayList();
        private Hashtable _ciudadanosHash = new Hashtable();


        #endregion

        public dudeHuellasFound(List<MatchingResult> dudesFound)
       {
            InitializeComponent();
            _IncomingDudes = dudesFound;
       }

        private void dude_Load(object sender, EventArgs e)
        {
            try
            {
                // here we will get all the info of the Individuo
                // get remoting configuration
                //string _ConfigFilename = "BioHuellas.config";
                //RemotingConfiguration.Configure(_ConfigFilename, false);
                CiudadanoManager mgr = new CiudadanoManager();

                ////  heres we will only get the basic info
                for (int i = 0; i < _IncomingDudes.Count; i++)
                {
                     HuellaRecord retData = (HuellaRecord)_IncomingDudes[i]._record;
                    ListBoxImage.CData data = new ListBoxImage.CData();
                    data.Similarity = "Similiridad - " + _IncomingDudes[i]._score.ToString();
                    data.FaceRFC = retData.FingerName;
                    data.KEY = retData.dudeID;
                    data.Image = mgr.getHuellaPicture(retData.dudeID);
                    matchedImagesArrayList.Add(data);
                }
                lbxFoundDudes.Images = matchedImagesArrayList;

            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }

        }

        private void _btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbxFoundDudes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int Index = lbxFoundDudes.SelectedIndex;

            try
            {
                ListBoxImage.CData data = (ListBoxImage.CData)matchedImagesArrayList[Index];
                CiudadanoManager mgr = new CiudadanoManager();

                if (_ciudadanosHash.Count == 0) // store in catche
                {
                    Ciudadano Dude = mgr.getCiudadano(data.KEY);
                    Dude.FACE = (Bitmap)data.Image.Clone();
                    // add to map
                    LoadFormFields(Dude);
                    _ciudadanosHash.Add(Index.ToString(), Dude);
                    
                }
                else // get from catche
                {
                    Ciudadano dude = (Ciudadano)_ciudadanosHash[Index.ToString()];
                    if( dude == null)
                    {
                        dude = mgr.getCiudadano(data.KEY);
                        dude.FACE = (Bitmap)data.Image.Clone();
                        // add to map
                        LoadFormFields(dude);
                        _ciudadanosHash.Add(Index.ToString(), dude);
                    }
                    else
                        LoadFormFields(dude);

                }

            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            //
            // Set the Title of the Main Window
            this.Text = "Persona(s) Encontrada(s)";
        }

        public void LoadFormFields(Ciudadano dude)
        {
            this.Text = dude.NOMBRES + " " + dude.PRIMER_APELLIDO + dude.SEGUNDO_APELLIDO;
            this._txtNCP.Text = dude.NCP;
            this._txtNames.Text = dude.NOMBRES;
            this._txtFirstLastName.Text = dude.PRIMER_APELLIDO;
            this._txtSecondLastName.Text = dude.SEGUNDO_APELLIDO;
            this._fechaNacimiento.Text = dude.FECHA_NACIMIENTO.ToString();
            this._DateRegistered.Text = dude.DIA_REGISTRO.ToString();
        }
    }
}