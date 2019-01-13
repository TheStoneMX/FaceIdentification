using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
//using BioTechSys.Share;

namespace BioTechSys.FingerCapture
{
	public enum MatchListType { Full, Half, Minimal };

	public enum MatchPair { None = 0, Genuine = 1, Impostor = 2, Any = Genuine | Impostor };

	public class MatchList
	{
		#region Private static methods

		private static int CalculateRecordAndIDDistribution(Database database,
			out IList<string> ids, out IList<int> idCounts,
			out bool[] recordIsIDFirsts, out int[] recordIDIndexes)
		{
			ids = new List<string>();
			idCounts = new List<int>();
			int recordCount = database.Records.Count;
			recordIsIDFirsts = new bool[recordCount];
			recordIDIndexes = new int[recordCount];

			for(int i = 0; i != recordCount; i++)
			{
                HuellaRecord record = database.Records[i];
				string id = record.dudeID.ToString();
				int idIndex = ids.IndexOf(id);
				if(idIndex == -1)
				{
					idIndex = ids.Count;
					ids.Add(id);
					idCounts.Add(1);
					recordIsIDFirsts[i] = true;
				}
				else
				{
					idCounts[idIndex]++;
					recordIsIDFirsts[i] = false;
				}
				recordIDIndexes[i] = idIndex;
			}

			return recordCount;
		}

		private static int GetNumberOfMatches(int index1, int[] recordIDIndexes1, bool[] recordIsIDFirsts1,
			int[] id1To2Correspondences, IList<int> idCounts2, MatchListType type, bool twoSets, bool twoWay)
		{
			int i = recordIDIndexes1[index1];
			int j = id1To2Correspondences[i];
			int impostorCount = 0;
			for (int jj = 0; jj < idCounts2.Count; jj++)
			{
				if (jj != j)
				{
					switch (type)
					{
						case MatchListType.Full: impostorCount += idCounts2[jj]; break;
						case MatchListType.Half: impostorCount += recordIsIDFirsts1[index1] ? idCounts2[jj] : 1; break;
						case MatchListType.Minimal: impostorCount++; break;
					}
				}
			}
			int genuineCount = idCounts2[j] - (twoSets ? 0 : 1);
			if (twoWay)
			{
				impostorCount *= 2;
				genuineCount *= 2;
			}
			return genuineCount + impostorCount;
		}

		#endregion

		#region Private fields

		private Database database1;
		private Database database2;
		private MatchListType type;
		private MatchPair pairs;
		private bool twoWay;
		bool twoSets;
		int count1;
		int count2;
		IList<string> ids1;
		IList<string> ids2;
		IList<int> idCounts1;
		IList<int> idCounts2;
		bool[] recordIsIDFirsts1;
		bool[] recordIsIDFirsts2;
		int[] recordIDIndexes1;
		int[] recordIDIndexes2;
		int[] id1To2Correspondences;
		int[] id2To1Correspondences;
		private bool noGenuines;
		private bool noImpostors;
		private int genuineCount;
		private int impostorCount;

		#endregion

		#region Public constructors

		public MatchList(Database database)
			: this(database, MatchListType.Full, MatchPair.Any, false)
		{
		}

		public MatchList(Database database, MatchListType type)
			: this(database, type, MatchPair.Any, false)
		{
		}

		public MatchList(Database database, MatchPair pairs)
			: this(database, MatchListType.Full, pairs, false)
		{
		}

		public MatchList(Database database, MatchListType type, MatchPair pairs)
			: this(database, type, pairs, false)
		{
		}

		public MatchList(Database database, MatchListType type, MatchPair pairs, bool twoWay)
			: this(database, null, type, pairs, twoWay)
		{
		}

		public MatchList(Database database1, Database database2, MatchListType type)
			: this(database1, database2, type, MatchPair.Any)
		{
		}

		public MatchList(Database database1, Database database2, MatchPair pairs)
			: this(database1, database2, MatchListType.Full, pairs)
		{
		}

		public MatchList(Database database1, Database database2, MatchListType type, MatchPair pairs)
			: this(database1, database2, type, pairs, false)
		{
		}

		public MatchList(Database database1, Database database2, MatchListType type, MatchPair pairs, bool twoWay)
		{
			if(database1 == null) throw new ArgumentNullException("database1");

			this.database1 = database1;
			this.database2 = database2;
			twoSets = database2 != null;
			count1 = CalculateRecordAndIDDistribution(database1,
				out ids1, out idCounts1,
				out recordIsIDFirsts1, out recordIDIndexes1);
			if (twoSets)
			{
				count2 = CalculateRecordAndIDDistribution(database2,
					out ids2, out idCounts2,
					out recordIsIDFirsts2, out recordIDIndexes2);
			}
			else
			{
				count2 = count1;
				ids2 = ids1;
				idCounts2 = idCounts1;
				recordIsIDFirsts2 = recordIsIDFirsts1;
				recordIDIndexes2 = recordIDIndexes1;
			}

			int idCount1 = ids1.Count;
			int idCount2 = ids2.Count;
			id1To2Correspondences = new int[idCount1];
			if (twoSets)
			{
				id2To1Correspondences = new int[idCount2];
				for (int i = 0; i < idCount2; i++)
				{
					id2To1Correspondences[i] = -1;
				}
				for (int i = 0; i < idCount1; i++)
				{
					int j = ids2.IndexOf(ids1[i]);
					id1To2Correspondences[i] = j;
					if (j != -1)
					{
						id2To1Correspondences[j] = i;
					}
				}
			}
			else
			{
				id2To1Correspondences = id1To2Correspondences;
				for (int i = 0; i < idCount1; i++)
				{
					id1To2Correspondences[i] = i;
				}
			}
			genuineCount = 0;
			for (int i = 0; i < idCount1; i++)
			{
				genuineCount += twoSets
					? idCounts1[i] * idCounts2[id1To2Correspondences[i]]
					: idCounts1[i] * (idCounts1[i] - 1) / (twoWay ? 1 : 2);
			}
			impostorCount = 0;
			switch (type)
			{
				case MatchListType.Full:
					impostorCount = twoSets
						? count1 * count2 - genuineCount
						: count1 * (count1 - 1) / (twoWay ? 1 : 2) - genuineCount;
					break;
				case MatchListType.Half:
					for (int i = 0; i < idCount1; i++)
					{
						if (twoSets)
						{
							int j = id1To2Correspondences[i];
							impostorCount += idCounts1[i] * (idCount2 - (j == -1 ? 0 : 1));
						}
						else
						{
							impostorCount += (idCounts1[i] * (idCount1 - 1) - i) * (twoWay ? 2 : 1);
						}
					}
					break;
				case MatchListType.Minimal:
					for (int i = 0; i < idCount1; i++)
					{
						if (twoSets)
						{
							int j = id1To2Correspondences[i];
							impostorCount += idCount2 - (j == -1 ? 0 : 1);
						}
						else
						{
							impostorCount = idCount1 * (idCount1 - 1) / (twoWay ? 1 : 2);
						}
					}
					break;
				default:
					throw new ArgumentException("Invalid type value", "type");
			}
			this.twoWay = twoSets ? false : twoWay;
			this.type = type;
			if (((int)pairs & (int)MatchPair.Any) == 0 || ((int)pairs & ~(int)MatchPair.Any) != 0)
				throw new ArgumentException("Invalid pairs value", "pairs");
			this.pairs = pairs;
			noGenuines = pairs == MatchPair.Impostor;
			noImpostors = pairs == MatchPair.Genuine;
			if (noGenuines) genuineCount = 0;
			if (noImpostors) impostorCount = 0;
		}

		#endregion

		#region Private methods

		private void CheckIndex1(int index1)
		{
			if (index1 < 0 || index1 >= count1) throw new ArgumentOutOfRangeException("index1", index1, "Index1 is less than zero or greater than or equal to Count1");
		}

		private void CheckIndex2(int index2)
		{
			if (index2 < 0 || index2 >= count1) throw new ArgumentOutOfRangeException("index2", index2, "Index2 is less than zero or greater than or equal to Count2");
		}

		#endregion

		#region Public methods

		public bool IsSameID(int index1, int index2)
		{
			CheckIndex1(index1);
			CheckIndex2(index2);
			return id1To2Correspondences[recordIDIndexes1[index1]] == recordIDIndexes2[index2];
		}

		public int GetNumberOfMatches1(int index1)
		{
			CheckIndex1(index1);
			return GetNumberOfMatches(index1, recordIDIndexes1, recordIsIDFirsts1, id1To2Correspondences, idCounts2, type, twoSets, twoWay);
		}

		public int GetNumberOfMatches2(int index2)
		{
			CheckIndex2(index2);
			return GetNumberOfMatches(index2, recordIDIndexes2, recordIsIDFirsts2, id2To1Correspondences, idCounts1, type, twoSets, twoWay);
		}

		public bool IsIDFirst1(int index1)
		{
			CheckIndex1(index1);
			return recordIsIDFirsts1[index1];
		}

		public bool IsIDFirst2(int index2)
		{
			CheckIndex2(index2);
			return recordIsIDFirsts2[index2];
		}

		public bool AreMatched(int index1, int index2)
		{
			return GetMatchPair(index1, index2) != MatchPair.None;
		}

		public MatchPair GetMatchPair(int index1, int index2)
		{
			CheckIndex1(index1);
			CheckIndex2(index2);
			if (!twoWay && !twoSets && index2 < index1 + 1) return MatchPair.None;
			if (!twoSets && index2 == index1) return MatchPair.None;
			bool sameID = id1To2Correspondences[recordIDIndexes1[index1]] == recordIDIndexes2[index2];
			if (noGenuines && sameID) return MatchPair.None;
			if (noImpostors && !sameID) return MatchPair.None;
			if (type != MatchListType.Full && !sameID)
			{
				bool recordIsIDFirst1 = recordIsIDFirsts1[index1];
				bool recordIsIDFirst2 = recordIsIDFirsts2[index2];
				if (type == MatchListType.Half)
				{
					if (!recordIsIDFirst1 && !recordIsIDFirst2) return MatchPair.None;
				}
				else if (type == MatchListType.Minimal)
				{
					if (!recordIsIDFirst1 || !recordIsIDFirst2) return MatchPair.None;
				}
			}
			return sameID ? MatchPair.Genuine : MatchPair.Impostor;
		}

		#endregion

		#region Public properties

		public Database Database1
		{
			get
			{
				return database1;
			}
		}

		public Database Database2
		{
			get
			{
				return database2;
			}
		}

		public MatchListType Type
		{
			get
			{
				return type;
			}
		}

		public MatchPair Pairs
		{
			get
			{
				return pairs;
			}
		}

		public bool TwoWay
		{
			get
			{
				return twoWay;
			}
		}

		public int Count1
		{
			get
			{
				return count1;
			}
		}

		public int Count2
		{
			get
			{
				return count2;
			}
		}

		public int GenuineCount
		{
			get
			{
				return genuineCount;
			}
		}

		public int ImpostorCount
		{
			get
			{
				return impostorCount;
			}
		}

		public int Count
		{
			get
			{
				return genuineCount + impostorCount;
			}
		}

		#endregion
	}
}
