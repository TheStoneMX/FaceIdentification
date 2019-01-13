using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

using BioTechSys.Biometrics;
using BioTechSys.Code;
using BioTechSys.Biometrics.Faces;

namespace BioTechSys.Share
{
    public class CiudadanoManager //: MarshalByRefObject , ICiudadanoManager
    {
        private SqlDataReader _facesDataReader;
        private SqlDataReader _HuellasDataReader;
        private SqlConnection _HuellasConnection = null;
        private SqlConnection _CarasConnection = null;
        private const int IDColumnNr = 2;
        private const int FaceIDColumnNr = 1;
        private string _strConnect;

        public CiudadanoManager()
		{
            BioTechSys.Biometrics.Faces.Settings settings = BioTechSys.Biometrics.Faces.Settings.Load("BioTechSys.Multi.Faces.settings.xml");
            _strConnect = settings.connectionString;
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
            catch (Exception)
            {

                int x = 0;
                throw;
            }
            finally
            {
                conn.Close();
            }
            return dude; // rough estimation
        }
        public DataSet getCiudadanoDS(Int64 ciudadano_id)
        {
            DataSet ds = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(_strConnect);
                conn.Open();

                string strSQL = string.Format("SELECT * FROM CIUDADANO WHERE SUBJECT_ID = {0}", ciudadano_id);

                SqlDataAdapter sda = new SqlDataAdapter(strSQL, conn);

                ds = new DataSet();

                sda.Fill(ds, "Customers");
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public Int64 InsertCiudadanInfo()
        {
            Int64 maxID = -1;
            SqlConnection conn = null;
            SqlDataAdapter adapter = null;
            SqlCommand insert = null;
            string cmdstr = string.Empty;

            try
            {

                if (Singleton.FingerPresent != "")
                {
                    cmdstr = "INSERT INTO CIUDADANO (NCP, ORN, CLAVE1, CLAVE2, CLAVE3, RECORD_TYPE, " +
                                "FAMILY_NAME, FATHER_LAST_NAME, MOTHER_LAST_NAME, CALLE_NUMERO, COLONIA, DELEGACION, CODIGO_POSTAL, " +
                                "CIUDAD, ESTADO, DIA_REGISTRO, FECHA_NACIMIENTO, SEXO, ESTATURA, PESO, ESTADO_CIVIL, SITUACION_PERSONA, " +
                                "CODIGO_DELITO, PELIGROSIDAD, FINGER_PRESENT, COMENTARIOS )" +
                                "values (@NCP, @ORN, @CLAVE1, @CLAVE2, @CLAVE3, @RECORD_TYPE, @FAMILY_NAME, @FATHER_LAST_NAME, @MOTHER_LAST_NAME, @CALLE_NUMERO," +
                                "@COLONIA, @DELEGACION, @CODIGO_POSTAL, @CIUDAD, @ESTADO, @DIA_REGISTRO, @FECHA_NACIMIENTO, @SEXO, @ESTATURA," + // @PAIS," +
                                "@PESO, @ESTADO_CIVIL, @SITUACION_PERSONA, @CODIGO_DELITO, @PELIGROSIDAD, @FINGER_PRESENT, @COMENTARIOS )";
                }
                else
                {
                    cmdstr = "INSERT INTO CIUDADANO (NCP, ORN, CLAVE1, CLAVE2, CLAVE3, RECORD_TYPE, " +
                                "FAMILY_NAME, FATHER_LAST_NAME, MOTHER_LAST_NAME, CALLE_NUMERO, COLONIA, DELEGACION, CODIGO_POSTAL, " +
                                "CIUDAD, ESTADO, DIA_REGISTRO, FECHA_NACIMIENTO, SEXO, ESTATURA, PESO, ESTADO_CIVIL, SITUACION_PERSONA, " +
                                "CODIGO_DELITO, PELIGROSIDAD, COMENTARIOS )" +
                                "values (@NCP, @ORN, @CLAVE1, @CLAVE2, @CLAVE3, @RECORD_TYPE, @FAMILY_NAME, @FATHER_LAST_NAME, @MOTHER_LAST_NAME, @CALLE_NUMERO," +
                                "@COLONIA, @DELEGACION, @CODIGO_POSTAL, @CIUDAD, @ESTADO, @DIA_REGISTRO, @FECHA_NACIMIENTO, @SEXO, @ESTATURA," + // @PAIS," +
                                "@PESO, @ESTADO_CIVIL, @SITUACION_PERSONA, @CODIGO_DELITO, @PELIGROSIDAD, @COMENTARIOS )";
                }

                adapter = new SqlDataAdapter();
                conn = new SqlConnection(_strConnect);
                conn.Open();
                insert = new SqlCommand(cmdstr, conn);

                adapter.SelectCommand = insert;

                insert.Parameters.AddWithValue("@NCP", Singleton.NCP );
                insert.Parameters.AddWithValue("@ORN", Singleton.ORN );
                insert.Parameters.AddWithValue("@CLAVE1", Singleton.Clave1 );
                insert.Parameters.AddWithValue("@CLAVE2", Singleton.Clave2 );
                insert.Parameters.AddWithValue("@CLAVE3", Singleton.Clave3);
                insert.Parameters.AddWithValue("@RECORD_TYPE", Singleton.RecordType );
                insert.Parameters.AddWithValue("@FAMILY_NAME", Singleton.FamilyName );
                insert.Parameters.AddWithValue("@FATHER_LAST_NAME", Singleton.FatherLastName );
                insert.Parameters.AddWithValue("@MOTHER_LAST_NAME", Singleton.MothersLastName );
                insert.Parameters.AddWithValue("@CALLE_NUMERO", Singleton.CalleNumero );
                insert.Parameters.AddWithValue("@COLONIA", Singleton.Colonia );
                insert.Parameters.AddWithValue("@DELEGACION", Singleton.Delegacion );
                insert.Parameters.AddWithValue("@CODIGO_POSTAL", Singleton.CodigoPostal );
                insert.Parameters.AddWithValue("@CIUDAD", Singleton.Ciudad);
                insert.Parameters.AddWithValue("@ESTADO", Singleton.Estado );
                insert.Parameters.AddWithValue("@DIA_REGISTRO", DateTime.Now);
                insert.Parameters.AddWithValue("@FECHA_NACIMIENTO", Singleton.FechaNacimiento );
                insert.Parameters.AddWithValue("@SEXO", Singleton.Sexo );
                insert.Parameters.AddWithValue("@ESTATURA", Singleton.Estatura );
                insert.Parameters.AddWithValue("@PESO", Singleton.Peso );
                insert.Parameters.AddWithValue("@ESTADO_CIVIL", Singleton.EstadoCivil );
                insert.Parameters.AddWithValue("@SITUACION_PERSONA", Singleton.SituacionPersona );
                insert.Parameters.AddWithValue("@CODIGO_DELITO", Singleton.CodigoDelito );
                insert.Parameters.AddWithValue("@PELIGROSIDAD", Singleton.Peligrosidad );
                
                if (Singleton.FingerPresent != "")
                    insert.Parameters.AddWithValue("@FINGER_PRESENT", Singleton.FingerPresent);

                insert.Parameters.AddWithValue("@COMENTARIOS", Singleton.Comentarios );

                ///////////////////////////////////////////
                insert.ExecuteNonQuery();
                SqlCommand select = new SqlCommand("SELECT @@IDENTITY", conn);
                SqlCommand com = new SqlCommand();
                com.Connection = conn;
                com.CommandText = "SELECT @@Identity";
                Object x = com.ExecuteScalar();
                maxID = System.Convert.ToInt64(x);
            }
            catch (InvalidOperationException ex)
            {
                string x = ex.Message;
            }
            catch (System.Exception e)
            {
                string x = e.Message;
            }
            finally
            {
                conn.Close();
            }
            return maxID;
        }
        public Bitmap getHuellaPicture(Int64 Huella_id)
        {
            Bitmap HuellaPicture = null;
            byte[] image;
            int offset = 0;

            SqlConnection conn = new SqlConnection(_strConnect);

            try
            {
                conn.Open();
                string strSQL = string.Format("SELECT  FINGER_PICTURE FROM FINGERS WHERE CIUDADANO_ID = {0}", Huella_id);
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    long fieldSize = reader.GetBytes(0, 0, null, 0, 0);
                    image = new byte[fieldSize];
                    reader.GetBytes(0, offset, image, 0, (int)fieldSize);
                    MemoryStream stream = new MemoryStream(image);
                    HuellaPicture = new Bitmap(stream);
                    stream.Close();
                }
                reader.Close();

                return HuellaPicture;

            }
            catch (SqlException ex)
            {
                string x = ex.Message;;
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        public bool AddHuella(string id, byte[] template)
        {
            int g = NFRecord.GetG(template);
            //Record record = new Record(id, template);

            string insertStatement = "INSERT INTO Fingerprints (Features, FingerID) VALUES (@Features, @FingerID)";

            //SqlCommand insertCommand = new SqlCommand(insertStatement, owner.connection);
            //insertCommand.Parameters.Add("@Features", SqlDbType.VarBinary).Value = template;
            //insertCommand.Parameters.Add("@FingerID", SqlDbType.VarChar).Value = id;

            //int cnt = insertCommand.ExecuteNonQuery();
            //insertCommand.Dispose();

            //Items.Add(record);
            //itemsByG[g].Add(record);
            return false;
        }

        /// <summary>
        /// This Method gets called when the app is starting
        /// to query for all the fingers templates in DB
        /// </summary>
        /// <returns></returns>
        public bool getCiudadanoHuellas()
        {
            _HuellasConnection = new SqlConnection(_strConnect);
            bool ret = false;
            try
            {
                _HuellasConnection.Open();
                string strSQL = "SELECT  FINGER_TEMPLATE, FINGER_NAME, CIUDADANO_ID FROM FINGERS";
                SqlCommand myCommand = new SqlCommand(strSQL, _HuellasConnection);
                _HuellasDataReader = myCommand.ExecuteReader();
                ret = true;
            }
            catch (SqlException e)
            {
                return ret;
                throw;
            }

            return ret;
        }

        public Int64 writeFaceToDB(byte[] features, Int64 dudeKey, string faceRFC, Bitmap nImage, DateTime enrollTime)
        {
            Int64 maxID = -1;
            SqlConnection Sqlconn =  new SqlConnection(_strConnect);

            try
            {
                Sqlconn.Open();
                byte[] image;
                using (Bitmap image2 = (Bitmap)nImage.Clone())
                {
                    MemoryStream stream = new MemoryStream();
                    image2.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    image = new byte[stream.ToArray().Length];
                    image = stream.ToArray();
                }

                string cmdstr = "INSERT INTO FACES (CIUDADANO_ID, FACE_TEMPLATE, NCP, FACE_PICTURE ) values ( @CIUDADANO_ID, @FACE_TEMPLATE, @NCP, @FACE_PICTURE )";

                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand insert = new SqlCommand(cmdstr, Sqlconn);

                adapter.SelectCommand = insert;

                insert.Parameters.AddWithValue("@CIUDADANO_ID", dudeKey);
                insert.Parameters.AddWithValue("@FACE_TEMPLATE", features);
                insert.Parameters.AddWithValue("@NCP", faceRFC);
                insert.Parameters.AddWithValue("@FACE_PICTURE", image);

                insert.ExecuteNonQuery();

                SqlCommand select = new SqlCommand("SELECT @@IDENTITY", Sqlconn);
                SqlCommand com = new SqlCommand();
                com.Connection = Sqlconn;
                com.CommandText = "SELECT @@Identity";
                Object x = com.ExecuteScalar();
                maxID = System.Convert.ToInt64(x);

            }
            catch (Exception ex)
            {
                string x = ex.Message; //MessageBox.Show(string.Format("Failed to insert record into DB:\n{0}", ex.Message));
            }
            finally
            {
                Sqlconn.Close();
            }
            return maxID;
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
            catch (SqlException e)
            {
                _CarasConnection.Close();
                return ret;
                throw;
            }

            return ret;
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

                throw;
            }
            finally
            {
                conn.Close();
            }

            return CiudadanoFace;
        }
        public FaceRecord GetFaceRecord()
        {
            //
            if (_CarasConnection.State == ConnectionState.Closed)
                return null ;

            FaceRecord faceRecord;
            try
            {
                if (_facesDataReader.Read())
                {
                    byte[] template = (byte[])_facesDataReader["FACE_TEMPLATE"];
                    string NCP = _facesDataReader["NCP"].ToString();
                    string ID = _facesDataReader["CIUDADANO_ID"].ToString();
                    faceRecord = new FaceRecord(NCP, template, System.Convert.ToInt64(ID));
                    return faceRecord;
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
                
                throw;
            }
        }
    }
}
