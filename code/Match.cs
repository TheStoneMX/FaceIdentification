using System;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Data;
using Neurotec.Biometrics;
using System.Collections;
using System.IO;
using System.ComponentModel;

namespace Neurotec.Biometrics.VLSample
{
	public class Match
	{
		public VeriLook VLN;
		private byte[] features;
		private double matchingThreshold;
		private FaceCollection faceCollection;

		private int featuresSize;

		public Match(VeriLook VLN, byte[] features, float matchingThreshold, FaceCollection faceCollection)
		{
			this.matchingThreshold = (double)matchingThreshold;
			this.faceCollection = faceCollection;
			this.VLN = VLN;
			this.features = features;
		}

		public event ReturnResultsDelegate ReturnResults;

		public void MatchFace()
		{
			string faceID = String.Empty;
			double resultSimilarity = 0;

			try
			{
				double similarity = 0;
				featuresSize = (int)VLN.GetParameter(VeriLook.FeaturesSize);
				byte[] featuresFromDB = new byte[featuresSize];

				foreach(Face face in faceCollection)
				{				
					featuresFromDB = face.Features;
					
					similarity = VLN.Verify(features, featuresFromDB);

					if(similarity >= matchingThreshold)
					{
						if ( similarity > resultSimilarity )
						{
							faceID = ((Face)face).FaceID;
							resultSimilarity = similarity;
						}						
					}
				}

				if(resultSimilarity == 0)
				{
					ReturnResults(string.Empty, 0, false);
				}
				else
				{
					ReturnResults(faceID, resultSimilarity, true);
				}
			}
			catch(OleDbException ex)
			{
				MessageBox.Show(string.Format("Error: {0}",ex));
			}
		}


	}
}
