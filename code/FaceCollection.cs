using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Collections.Generic;
using BioTechSys.Code;
using BioTechSys.Images;
using BioTechSys.Share;

namespace BioTechSys.Biometrics.Faces
{

    [Serializable]
    public class FaceRecord
    {
        #region Private fields

        private string _faceNCP;
        byte[] _faceTemplate;
        private Int64 _faceID;

        #endregion

        #region Internal constructor

        public FaceRecord(string NCP, byte[] template, Int64 ID)
        {
            if (NCP == null)
                throw new ArgumentNullException("RFC");
            if (template == null)
                throw new ArgumentNullException("template");

            this._faceID = ID;
            //this._faceID = System.Convert.ToInt64(_faceID);
            this._faceNCP = NCP;
            this._faceTemplate = template;
        }

        public FaceRecord(string NCP, byte[] template)
        {
            if (NCP == null)
                throw new ArgumentNullException("RFC");
            if (template == null)
                throw new ArgumentNullException("template");

            this._faceNCP = NCP;
            this._faceTemplate = template;
        }
        #endregion

        #region Public properties

        public Int64 FaceID
        {
            get
            {
                return _faceID;
            }
            set
            {
                _faceID = value;
            }
        }

        public string FaceRFC
        {
            get
            {
                return _faceNCP;
            }
        }

        public byte[] FaceTemplate
        {
            get
            {
                return _faceTemplate;
            }
        }

        #endregion
    }

    public class FaceCollection : CollectionBase
    {
        private const string TableName = "Faces";
        private const int IDColumnNr = 2;
        private const int FaceIDColumnNr = 1;
        private BioTechSys.Face.Enroll.Forms.MainForm parent;
        private string _strConnect;
        private SqlDataReader _facesDataReader;
        private SqlConnection _CarasConnection = null;

        public FaceRecord this[int index]
        {
            get
            {
                //we must cast our return object as PhoneNumber 
                return (FaceRecord)this.InnerList[index];
            }
            set
            {
                //warning, this is not for adding, but for reassigning 
                //this will throw an exception if the index does not already 
                //exist. Use Add(phoneNumber) to add to collection 
                this.List[index] = value;
            }
        }

        public uint Add(FaceRecord face, Int64 dudeKey, NImage image, DateTime enrollTime)
        {
            CiudadanoManager Individuo = new CiudadanoManager();
            uint ID = (uint)Individuo.writeFaceToDB(face.FaceTemplate, dudeKey, face.FaceRFC, image.ToBitmap(), enrollTime);

            if (ID >= 0)
            {
                InnerList.Add(face);
            }
            return ID;
        }

        public FaceCollection(BioTechSys.Face.Enroll.Forms.MainForm parent)
        {
            this.parent = parent;
            BioTechSys.Biometrics.Faces.Settings settings = BioTechSys.Biometrics.Faces.Settings.Load("BioTechSys.Multi.Faces.settings.xml");
            _strConnect = settings.connectionString;

            //Thread huellasReader = new Thread(this.LoadFacesThread);
            //huellasReader.Start();
        }

        private void LoadFacesThread()
        {
            bool result = getCiudadanoFaces();

            if (result == false)
                MessageBox.Show("Biometrics", "Error de comunicación con el servidor de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // now we get each Face record...
            FaceRecord record = null;
            while (true)
            {
                record = GetFaceRecord();
                if (record != null)
                    InnerList.Add(record);
                else
                    break;
            }
        }
        public bool getCiudadanoFaces()
        {
            _CarasConnection = new SqlConnection(_strConnect);
            bool ret = false;
            try
            {
                _CarasConnection.Open();
                string strSQL = "SELECT FACE_TEMPLATE, NCP, CIUDADANO_ID FROM FACES";
                SqlCommand myCommand = new SqlCommand(strSQL, _CarasConnection);
                _facesDataReader = myCommand.ExecuteReader();
                ret = true;
            }
            catch (SqlException ex)
            {
                string x = ex.Message;
                return ret;
            }

            return ret;
        }
        public FaceRecord GetFaceRecord()
        {
            //
            if (_CarasConnection.State == ConnectionState.Closed)
                return null;

            FaceRecord faceRecord = null;

            try
            {
                if (_facesDataReader.Read())
                {
                    byte[] template = (byte[])_facesDataReader["FACE_TEMPLATE"];
                    string ncp = _facesDataReader["NCP"].ToString();
                    Int64 ID = (Int64)_facesDataReader["CIUDADANO_ID"];

                    faceRecord = new FaceRecord(ncp, template, ID);
                }
                else
                {
                    _facesDataReader.Close();
                    _CarasConnection.Close();
                    return null;
                }
            }
            catch (SqlException ex)
            {
                string e = ex.Message;
            }
            return faceRecord;
        }

        private int writeToDB(byte[] features, string imageID, NImage nImage, DateTime enrollTime)
        {
            int maxID = -1;
            //try
            //{
            //    byte[] image;
            //    using (Bitmap image2 = nImage.ToBitmap())
            //    {
            //        MemoryStream stream = new MemoryStream();
            //        image2.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            //        image = new byte[stream.ToArray().Length];
            //        image = stream.ToArray();
            //    }

            //    parent.messageBoard.AppendText(string.Format("Subject ID = {0}.\r\n", imageID));
            //    string cmdstr = "INSERT INTO " + TableName + " (Features, FaceID, Picture, EnrollTime) values (@features, @imageID, @image, @currentTime)";

            //    OleDbDataAdapter adapter = new OleDbDataAdapter();

            //    OleDbCommand insert = new OleDbCommand(cmdstr, vlAccessConnection);

            //    adapter.SelectCommand = insert;

            //    insert.Parameters.AddWithValue("@features", features);
            //    insert.Parameters.AddWithValue("@imageID", imageID);
            //    insert.Parameters.AddWithValue("@image", image);
            //    insert.Parameters.AddWithValue("@currentTime", enrollTime.ToString());

            //    parent.messageBoard.AppendText(string.Format("Adding to the database..."));
            //    DateTime firstTime = DateTime.Now;
            //    insert.ExecuteNonQuery();
            //    string concatenatedTime = parent.TimeDiff(firstTime);
            //    parent.messageBoard.AppendText(string.Format("OK ({0} sec.)\r\n", concatenatedTime));

            //    OleDbCommand select = new OleDbCommand(string.Format("SELECT MAX(ID) FROM {0}", TableName), vlAccessConnection);

            //    OleDbDataReader reader = select.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        maxID = reader.GetInt32(0);
            //    }
            //}
            //catch (InvalidOperationException ex)
            //{
            //    MessageBox.Show(string.Format("Failed to insert record into DB:\n{0}", ex.Message));
            //}
            //if (maxID == -1)
            //{
            //    MessageBox.Show("Error. Cant get ID from database! Description: function writeToDB");
            //}
            return maxID;
        }

        public Bitmap getCiudadanoFace(Int64 CiudadanoID)
        {
            Bitmap CiudadanoFace = null;
            SqlConnection conn = new SqlConnection(_strConnect);
            try
            {
                conn.Open();
                SqlCommand select = new SqlCommand(string.Format("Select FACE_PICTURE from FACES where CIUDADANO_ID = {0}", CiudadanoID), conn);

                SqlDataReader reader = select.ExecuteReader();
                byte[] image;
                int offset = 0;

                while (reader.Read())
                {
                    long fieldSize = reader.GetBytes(0, 0, null, 0, 0);
                    image = new byte[fieldSize];
                    reader.GetBytes(0, offset, image, 0, (int)fieldSize);
                    MemoryStream stream = new MemoryStream(image);
                    CiudadanoFace = new Bitmap(stream);
                    stream.Close();
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                string x = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return CiudadanoFace;
        }

        public Ciudadano getCiudadano(Int64 CiudadanoID)
        {
            Ciudadano dude = new Ciudadano();
            SqlConnection conn = new SqlConnection(_strConnect);

            try
            {

                conn.Open();
                SqlCommand select = new SqlCommand(string.Format("SELECT CIUDADANO.* FROM CIUDADANO WHERE (CIUDADANO_ID = {0})", CiudadanoID), conn);

                SqlDataReader reader = select.ExecuteReader();
                if (reader.Read())
                {
                    dude.NCP = reader["NCP"].ToString();
                    dude.FamilyName = reader["FAMILY_NAME"].ToString();
                    dude.PRIMER_APELLIDO = reader["FATHER_LAST_NAME"].ToString();
                    dude.SEGUNDO_APELLIDO = reader["MOTHER_LAST_NAME"].ToString();
                    dude.DIA_REGISTRO = (DateTime)reader["DIA_REGISTRO"];
                    dude.FECHA_NACIMIENTO = (DateTime)reader["FECHA_NACIMIENTO"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return dude; // rough estimation
        }

    }
}
