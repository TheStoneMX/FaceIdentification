using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BioTechSys.AnsiNist;
using BioTechSys.Images;

namespace BioTechSys.Share
{
    public class FingersData
    {
        public string FingerName = string.Empty;
        public NImage FingerImage = null;
        public byte[] FingerTemplate = null;

        public FingersData(NImage Image, byte[] template, string name)
        {
            FingerName = name;
            FingerImage = Image;
            FingerTemplate = template;
        }
    }

   public class Singleton
    {
        private static Singleton instance;
        private Singleton() { }

        #region Ciudadano Varibles
        private static Int64 _LastDudeKey;
        public static Int64 lastDudeKey
        {
            get { return _LastDudeKey; }
            set { _LastDudeKey = value; }
        }
       private  static string _NCP;
        public  static string NCP
        {
            get { return _NCP; }
            set
            {
                _NCP = value;
            }
        }
       // Numero de Transaccion
        private static string _Type1TranslNumber;
        public static string type1TranslNumber
        {
            get { return _Type1TranslNumber; }
            set { _Type1TranslNumber = value; }
        }
      // Other Refence Number
       private static string _ORN;
        public static string ORN
        {
            get { return _ORN; }
            set
            {
                _ORN = value;
            }
        }
        private  static string _Clave1;
        public  static string Clave1
        {
            get { return _Clave1; }
            set
            {
                _Clave1 = value;
            }
        }
        private  static string _Clave2;
        public  static string Clave2
        {
            get { return _Clave2; }
            set
            {
                _Clave2 = value;
            }
        }
        private  static string _Clave3;
        public  static string Clave3
        {
            get { return _Clave3; }
            set
            {
                _Clave3 = value;
            }
        }
        private  static string _RecordType;
        public  static string RecordType
        {
            get { return _RecordType; }
            set
            {
                _RecordType = value;
            }
        }
        private  static string _FamilyName;
        public  static string FamilyName
        {
            get { return _FamilyName; }
            set
            {
                _FamilyName = value;
            }
        }
        private  static string _FatherLastName;
        public  static string FatherLastName
        {
            get { return _FatherLastName; }
            set
            {
                _FatherLastName = value;
            }
        }
        private  static string _MothersLastName;
        public  static string MothersLastName
        {
            get { return _MothersLastName; }
            set
            {
                _MothersLastName = value;
            }
        }
        private  static string _Estado;
        public  static string Estado
        {
            get { return _Estado; }
            set
            {
                _Estado = value;
            }
        }
        private  static string _CalleNumero;
        public  static string CalleNumero
        {
            get { return _CalleNumero; }
            set
            {
                _CalleNumero = value;
            }
        }
        private  static DateTime _FechaNacimiento;
        public  static DateTime FechaNacimiento
        {
            get { return _FechaNacimiento; }
            set
            {
                _FechaNacimiento = value;
            }
        }
        private  static string _Sexo;
        public  static string Sexo
        {
            get { return _Sexo; }
            set
            {
                _Sexo = value;
            }
        }
        private  static int _Estatura;
        public  static int Estatura
        {
            get { return _Estatura; }
            set
            {
                _Estatura = value;
            }
        }
        private  static int _Peso;
        public  static int Peso
        {
            get { return _Peso; }
            set
            {
                _Peso = value;
            }
        }
        private  static string _EstadoCivil;
        public  static string EstadoCivil
        {
            get { return _EstadoCivil; }
            set
            {
                _EstadoCivil = value;
            }
        }
        private  static string _SituacionPersona;
        public  static string SituacionPersona
        {
            get { return _SituacionPersona; }
            set
            {
                _SituacionPersona = value;
            }
        }
        private  static int _CodigoDelito;
        public  static int CodigoDelito
        {
            get { return _CodigoDelito; }
            set
            {
                _CodigoDelito = value;
            }
        }
        private  static string _Peligrosidad;
        public  static string Peligrosidad
        {
            get { return _Peligrosidad; }
            set
            {
                _Peligrosidad = value;
            }
        }
        private  static string _FingerPresent;
        public  static string FingerPresent
        {
            get { return _FingerPresent; }
            set
            {
                _FingerPresent = value;
            }
        }
        private  static string _Delegacion;
        public  static string Delegacion
        {
            get { return _Delegacion; }
            set
            {
                _Delegacion = value;
            }
        }
        private  static string _Ciudad;
        public  static string Ciudad
        {
            get { return _Ciudad; }
            set
            {
                _Ciudad = value;
            }
        }
        private  static string _Colonia;
        public  static string Colonia
        {
            get { return _Colonia; }
            set
            {
                _Colonia = value;
            }
        }
        private  static string _CodigoPostal;
        public  static string CodigoPostal
        {
            get { return _CodigoPostal; }
            set
            {
                _CodigoPostal = value;
            }
        }
        private  static string _Comentarios;
        public  static string Comentarios
        {
            get { return _Comentarios; }
            set
            {
                _Comentarios = value;
            }
        }
        
        #endregion

        public static void ResetSingleton()
        {
            _NCP = string.Empty; _ORN = string.Empty; _Clave1 = string.Empty; _Clave2 = string.Empty; _Clave3 = string.Empty; 
            _RecordType = string.Empty; _FamilyName = string.Empty; _FatherLastName = string.Empty; _MothersLastName = string.Empty;
            _Estado = string.Empty; _CalleNumero = string.Empty; _FechaNacimiento = DateTime.Today; _Sexo = string.Empty; 
            _Estatura = 0; _Peso = 0; _EstadoCivil = string.Empty; _SituacionPersona = string.Empty; 
            _CodigoDelito = 0; _Peligrosidad = string.Empty; _FingerPresent = string.Empty; _Delegacion = string.Empty;
            _Ciudad = string.Empty; _Colonia = string.Empty; _CodigoPostal = string.Empty; _Comentarios = string.Empty; 
        }

        #region Ciudadano Finger Images 
        private static FingersData[] _FingerImages = new FingersData[10];
        public static FingersData[] FingerData
        {
            get { return _FingerImages; }
            set
            {
                _FingerImages = value;
            }
        }
        
        #endregion
        private static Bitmap _FaceImage;
        public static Bitmap FaceImage
        {
            get { return _FaceImage; }
            set
            {
                _FaceImage = value;
            }
        }
       
        #region SNSP COnnection Info
        //in application properties Settings
        #endregion

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }

    }
}
