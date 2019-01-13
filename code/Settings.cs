using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace BioTechSys.Biometrics.Faces
{
	[XmlRoot()]
	public struct Settings
	{
		#region Default

		public void SetDefault()
		{
            BioTechSys.Biometrics.NLExtractor extractor = new NLExtractor();

			FaceConfidenceThreshold = extractor.FaceConfidenceThreshold;
			FaceQualityThreshold = extractor.FaceQualityThreshold;
			MinimalIOD = extractor.MinIod;
			MaximalIOD = extractor.MaxIod;

			EnrollStreamLength = 10;
			MaxRecordsPerTemplate = extractor.MaxRecordsPerTemplate;
			GeneralizationImageCount = 4;

			UseLivenessCheck = extractor.UseLivenessCheck;
			LivenessThreshold = extractor.LivenessThreshold;

			extractor.Dispose();

            BioTechSys.Biometrics.NMatcher matcher = new NMatcher();

			FAR = matcher.MatchingThreshold;
			MatchingAttempts = 10;
			MatchingStreamLength = 3;

			matcher.Dispose();

			FlipImage = false;
			FileNameAsRecordID = true;
			SaveEnrolledFaceImages = true;
		}

		public static Settings DefaultSettings
		{
			get
			{
				Settings s = new Settings();
				s.SetDefault();
				return s;
			}
		}

		#endregion

		#region Copy settings to extractor/matcher

		public void CopyToExtractor(NLExtractor extractor)
		{
			extractor.FaceConfidenceThreshold = FaceConfidenceThreshold;
			extractor.FaceQualityThreshold = FaceQualityThreshold;
			extractor.MinIod = MinimalIOD;
			extractor.MaxIod = MaximalIOD;
			extractor.MaxRecordsPerTemplate = MaxRecordsPerTemplate;
			extractor.UseLivenessCheck = UseLivenessCheck;
			extractor.LivenessThreshold = LivenessThreshold;
		}

		public void CopyToMatcher(NMatcher matcher)
		{
			matcher.MatchingThreshold = FAR;
		}

		#endregion Copy settings to extractor/matcher

		#region Load and Save

		public static Settings Load(string settingsFileName)
		{
			try
			{
				if (File.Exists(settingsFileName))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(Settings));
					TextReader reader = null;
					Settings settings;

					try
					{
						reader = new StreamReader(settingsFileName);
						settings = (Settings)serializer.Deserialize(reader);
					}
					finally
					{
						if (reader != null)
						{
							reader.Close();
						}
					}

					return settings;
				}
				else
				{
					// settings file not found
					return DefaultSettings;
				}
			}
			catch // (Exception ex)
			{
				// error while loading
				return DefaultSettings;
			}
		}

		public bool Save(string settingsFileName)
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(this.GetType());
				TextWriter writer = null;

				try
				{
					writer = new StreamWriter(settingsFileName);
					serializer.Serialize(writer, this);
				}
				finally
				{
					if (writer != null)
					{
						writer.Close();
					}
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion Load and save

		#region Setting properties

		#region Features extraction

		private double faceConfidenceThreshold;

		[XmlElement()]
		public double FaceConfidenceThreshold
		{
			get
			{
				return faceConfidenceThreshold;
			}
			set
			{
				if (value < 0.0 || value > 100.0)
				{
					throw new Exception("Face confidence threshold is not in range from 0.0 to 100.0");
				}
				faceConfidenceThreshold = value;
			}
		}

		private byte faceQualityThreshold;

		[XmlElement()]
		public byte FaceQualityThreshold
		{
			get
			{
				return faceQualityThreshold;
			}
			set
			{
				faceQualityThreshold = value;
			}
		}

		private int minimalIOD;

		[XmlElement()]
		public int MinimalIOD
		{
			get
			{
				return minimalIOD;
			}
			set
			{
				if (value < 40 || value > 16384)
				{
					throw new Exception("Minimal IOD is not in range from 40 to 4000");
				}
				minimalIOD = value;
			}
		}

		private int maximalIOD;

		[XmlElement()]
		public int MaximalIOD
		{
			get
			{
				return maximalIOD;
			}
			set
			{
				if (value < 40 || value > 16384)
				{
					throw new Exception("Minimal IOD is not in range from 40 to 4000");
				}
				maximalIOD = value;
			}
		}

		#endregion Features extraction

		#region Enrollment

		private int enrollStreamLength;

		[XmlElement()]
		public int EnrollStreamLength
		{
			get
			{
				return enrollStreamLength;
			}
			set
			{
				if (value < 1 || value > 100)
				{
					throw new Exception("Enroll stream length is not in range from 1 to 100");
				}
				enrollStreamLength = value;
			}
		}

		private int maxRecordsPerTemplate;

		[XmlElement()]
		public int MaxRecordsPerTemplate
		{
			get
			{
				return maxRecordsPerTemplate;
			}
			set
			{
				if (value < 1 || value > 16)
				{
					throw new Exception("Enroll stream length is not in range from 1 to 100");
				}
				maxRecordsPerTemplate = value;
			}
		}

		private int generalizationImageCount;

		[XmlElement()]
		public int GeneralizationImageCount
		{
			get
			{
				return generalizationImageCount;
			}
			set
			{
				if (value < 1)
				{
					throw new Exception("Enroll stream length must be at least 1.");
				}
				generalizationImageCount = value;
			}
		}

		#endregion Enrollment

		#region Matching

		private string far;

		[XmlElement()]
		public int FAR
		{
			get
			{
				return Data.MatchingThresholdFromString(far);
			}
			set
			{
				far = Data.MatchingThresholdToString(value);
			}
		}

		[XmlIgnore()]
		public string FARString
		{
			get
			{
				return far;
			}
			set
			{
				Data.MatchingThresholdFromString(value);
				far = value;
			}
		}

		private int matchingAttempts;

		[XmlElement()]
		public int MatchingAttempts
		{
			get
			{
				return matchingAttempts;
			}
			set
			{
				if (value < 1 || value > 30)
				{
					throw new Exception("Matching attempts not in range from 1 to 30.");
				}
				matchingAttempts = value;
			}
		}

		private bool useLivenessCheck;

		[XmlElement()]
		public bool UseLivenessCheck
		{
			get
			{
				return useLivenessCheck;
			}
			set
			{
				useLivenessCheck = value;
			}
		}

		private double livenessThreshold;

		[XmlElement()]
		public double LivenessThreshold
		{
			get
			{
				return livenessThreshold;
			}
			set
			{
				if ((value < 0) || (value > 100))
				{
					throw new Exception("Invalid value for 'Liveness threshold', you  have to specify a number between 0 and 100!");
				}
				livenessThreshold = value;
			}
		}

		private int matchingStreamLength;

		[XmlElement()]
		public int MatchingStreamLength
		{
			get
			{
				return matchingStreamLength;
			}
			set
			{
				if ((value < 1) || (value > 100))
				{
					throw new Exception("Invalid value for 'Matching stream length', you  have to specify a number between 1 and 100!");
				}
				matchingStreamLength = value;
			}
		}

		#endregion Matching

		#region Misc

		private bool flipImage;

		[XmlElement()]
		public bool FlipImage
		{
			get
			{
				return flipImage;
			}
			set
			{
				flipImage = value;
			}
		}

		private bool fileNameAsRecordID;

		[XmlElement()]
		public bool FileNameAsRecordID
		{
			get
			{
				return fileNameAsRecordID;
			}
			set
			{
				fileNameAsRecordID = value;
			}
		}

		private bool saveEnrolledFaceImages;

		[XmlElement()]
		public bool SaveEnrolledFaceImages
		{
			get
			{
				return saveEnrolledFaceImages;
			}
			set
			{
				saveEnrolledFaceImages = value;
			}
		}

		#endregion Misc

        #region DataBase Connection String
        private string _ConnectionString;
        public string connectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }
        #endregion

		#endregion Setting properties
	}
}
