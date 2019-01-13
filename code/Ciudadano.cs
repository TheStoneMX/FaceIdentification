using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace BioTechSys.Code
{
    [Serializable]
    public class Ciudadano
    {
        private int pid;
        private int tid;
        private string machine;

        private string m_ncp;
        private string m_familyName;
        private string m_FirstLastName;
        private string m_SecondLastName;
        private string m_Address;
        private string m_City;
        private string m_State;
        private string m_Pais;
        private DateTime m_BirthDate;
        private DateTime m_RegisterDate;
        private Bitmap m_face;

        public string NCP
        {
            get
            {
                return m_ncp;
            }
            set
            {
                m_ncp = value;
            }
        }
        public string FamilyName
        {
            get
            {
                return m_familyName;
            }
            set
            {
                m_familyName = value;
            }

        }
        public string PRIMER_APELLIDO
        {
            get
            {
                return m_FirstLastName;
            }
            set
            {
                m_FirstLastName = value;
            }
        }
        public string SEGUNDO_APELLIDO
        {
            get
            {
                return m_SecondLastName;
            }
            set
            {
                m_SecondLastName = value;
            }
        }
        public string DIRECCION
        {
            set
            {
                m_Address = value;
            }
            get
            {
                return m_Address;
            }
        }
        public string CIUDAD
        {
            get
            {
                return m_City;
            }
            set
            {
                m_City = value;
            }
        }
        public string ESTADO
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }
        public string PAIS
        {
            get
            {
                return m_Pais;
            }
            set
            {
                m_Pais = value;
            }
        }
        public DateTime FECHA_NACIMIENTO
        {
            get
            {
                return m_BirthDate;
            }
            set
            {
                m_BirthDate = value;
            }
        }
        public Bitmap FACE
        {
           get
            {
                return (Bitmap)m_face.Clone();
            }
            set
            {
                m_face = value;
            }

        }
        public DateTime DIA_REGISTRO
        {
            get
            {
                return m_RegisterDate;
            }
            set
            {
                m_RegisterDate = value;
            }
        }
        public int getAge()
        {
            TimeSpan tmp = DateTime.Today.Subtract(m_BirthDate);
            return tmp.Days / 365; // rough estimation
        }
    }
    
//    [Serializable]
//    public class FaceRecord
//    {
//        #region Private fields

//        private string _faceRFC;
//        byte[] _faceTemplate;
//        private Int64 _faceID;
        
//        #endregion

//        #region Internal constructor

//        public FaceRecord( string RFC, byte[] template, Int64 ID )
//        {
//            if (RFC == null)
//                throw new ArgumentNullException("RFC");
//            if (template == null)
//                throw new ArgumentNullException("template");

/////// TO BE FIX ////////////
//            this._faceID = ID;
//            //this._faceID = System.Convert.ToUInt32(_faceID);
//            this._faceRFC = RFC;
//            this._faceTemplate = template;
//        }

//        public FaceRecord(string RFC, byte[] template)
//        {
//            if (RFC == null)
//                throw new ArgumentNullException("RFC");
//            if (template == null)
//                throw new ArgumentNullException("template");

//            this._faceRFC = RFC;
//            this._faceTemplate = template;
//        }
//        #endregion

//        #region Public properties

//        public Int64 FaceID
//        {
//            get
//            {
//                return _faceID;
//            }
//            set
//            {
//                _faceID = value;
//            }
//        }

//        public string FaceRFC
//        {
//            get
//            {
//                return _faceRFC;
//            }
//        }

//        public byte[] FaceTemplate
//        {
//            get
//            {
//                return _faceTemplate;
//            }
//        }

//        #endregion
//    }

    // The first thing to do when building a CollectionBase class is
    // to inherit from System.Collections.CollectionBase
    [Serializable]
   public class Ciudadanos : System.Collections.CollectionBase
    {
        // After you inherit the CollectionBase class, 
        // you can access an intrinsic object
        // called InnerList that represents your 
        // collection. InnerList is of type ArrayList
        public Ciudadano Add(Ciudadano Value)
        {
            this.InnerList.Add(Value);
            return Value;
        }

        // To retrieve an item from the InnerList, pass
        // the index of that item to the .Item property.
       public Ciudadano Item(int Index)
        {
            return (Ciudadano)this.InnerList[Index];
        }

        public void Remove(Ciudadano dude)
        {
            // To remove an item from the collection, you must
            // pass in a reference to that item (in this case, the
            // Customer object we want to remove).
            // However, it is often more convenient to create a
            // Remove method that allows the calling code to pass in 
            // only the index of the item instead of an object reference.
            // So we've overloaded the Remove method to use either one.

            this.InnerList.Remove(dude);
        }

        public void Remove(int Index)
        {
            // This is the second Remove method. Instead of passing
            // in an object reference, this Remove expects an index.
            // The calling code can decide which one to call.
            // if the calling code passes an index, you can simply
            // look up that item by using the InnerList.Item method,
            // then remove the item.

            Ciudadano dude;
            dude = (Ciudadano)this.Item(Index);
            //cust = (Customer) this.InnerList.Item(Index);

            if (dude != null)
            {
                this.InnerList.Remove(dude);
            }
        }

    }

}
