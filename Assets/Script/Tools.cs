using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;

namespace Tools
{
	[LuaCallCSharp]
	public class Probability
	{
        public static bool IsProbOccur(double prob)
        {
            int prb = (int)(prob * 10000);

            System.Random ra = new System.Random(GetRandomSeed());
            int result = ra.Next(1, 10000);
            if (result <= prb)
            {
                return true;
            }

            return false;
        }

        public static int GetRandomNum(int min, int max)
        {

            System.Random ra = new System.Random(GetRandomSeed());
            return ra.Next(min, max);

        }

        public static int GetGaussianRandomNum(int min, int max)
        {
            System.Random ra = new System.Random(GetRandomSeed());

            int[] iResult = { ra.Next(min, max), ra.Next(min, max), ra.Next(min, max) };

            return (iResult[0] + iResult[1] + iResult[2]) / 3;
        }

		public static int gaussrand(int E, int V, int L)
		{
			int R;
			do {
				
				double V1, V2, S, X;
				//int phase = 0;

				System.Random ra = new System.Random (GetRandomSeed ());

					do {
						double U1 = (double)ra.NextDouble();
						double U2 = (double)ra.NextDouble();

						V1 = 2 * U1 - 1;
						V2 = 2 * U2 - 1;
						S = V1 * V1 + V2 * V2;
					} while(S >= 1 || S == 0);

					X = V1 * Math.Sqrt (-2 * Math.Log (S) / S);
	


				X = X * V + E;
				R = (int)X;

			} while(R <= E + L && R >= E - L);

			return R;
		}

        public static bool Calc(int iRate)
        {
            System.Random ran = new System.Random(GetRandomSeed());
            int RandKey = ran.Next(1, 100);
            if (RandKey <= iRate)
            {
                return true;
            }

            return false;
        }

		public static List<int> GetRandomNumArrayWithStableSum(int count, int sum)
		{
			List<int> list = new List<int> ();
			while(list.Count != count-1)
			{
				int random = GetRandomNum (0, sum);
				if (list.Contains (random)) 
				{
					continue;
				}

				list.Add (random);
			}

			list.Sort ();

			List<int> resultList = new List<int> ();
			for (int i = 0; i < list.Count+1; i++)
			{
				if (i == 0) 
				{
					resultList.Add (list [i] - 0);
				} 
				else if (i == list.Count)
				{
					resultList.Add (100 - list [i - 1]);
				}
				else
				{
					resultList.Add (list [i] - list [i - 1]);
				}
			}

			return resultList;
		}

        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
    }

	[LuaCallCSharp]
	public class StreamDir
	{
		public static string[] GetLuaFileName(string path)
		{
			string fullPath = Application.streamingAssetsPath + path + "/";
			return Directory.GetFiles(fullPath, "*.lua");
		}
	}

//	public class Cvs
//	{
//		private Cvs(string filename)
//		{
//            this.filename = filename;

//			//读取csv二进制文件  
//			TextAsset binAsset = Resources.Load (filename, typeof(TextAsset)) as TextAsset;         

//			//读取每一行的内容  
//			string [] lineArray = binAsset.text.Split ('\r');  

//			m_colIndex = lineArray [0].Replace("ID,", "").Split (',');

//			m_rowIndex = new string[lineArray.Length-1];
//			m_ArrayData = new string [lineArray.Length-1][]; 

//			for(int i =0; i < lineArray.Length-1; i++)  
//			{  
//				string[] raw = lineArray[i+1].Split (',');  
//				m_rowIndex[i] = raw [0];

//				m_ArrayData [i] = new string[raw.Length - 1];
//				Array.Copy (raw, 1, m_ArrayData[i], 0, raw.Length-1);
//			}  
//		}

//		public string Get(string row, string column)
//		{
//            try
//            {
//#if NITY_EDITOR_OSX
//			    return row+"_"+column;
//#else
//                int iRow = Array.FindIndex(m_rowIndex, s => s == row);
//                int iCol = Array.FindIndex(m_colIndex, s => s == column);

//                return m_ArrayData[iRow][iCol];
//#endif
//            }
//            catch(Exception e)
//            {
//                Debug.Log(filename + ":" + row + "," + column);
//                throw;
//            }
//        }

//		public string Get(string row)
//		{
//			return Get (row, "CHI");
//		}
			
//        public int RowLength()
//        {
//            return m_rowIndex.Length;
//        }

//		private string[] m_rowIndex;
//		private string[] m_colIndex;
//		private string[][] m_ArrayData;
//        private string filename;

//		public static Cvs Xings = new Cvs("text/xingshi");
//		public static Cvs Mingz = new Cvs("text/mingzi");
//		public static Cvs UiDesc = new Tools.Cvs ("text/uidesc");
//		public static Cvs MsgDesc = new Tools.Cvs ("text/msgdef");
//		public static Cvs Guohao = new Tools.Cvs ("text/guohao");
//        public static Cvs Nianhao1 = new Tools.Cvs("text/nianhao1");
//        public static Cvs Nianhao2 = new Tools.Cvs ("text/nianhao2");
//    }
		
	[Serializable]
	public class SerialDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField]
		List<TKey> keys;
		[SerializeField]
		List<TValue> values;

		public void OnBeforeSerialize()
		{
			keys = new List<TKey>(this.Keys);
			values = new List<TValue>(this.Values);
		}

		public void OnAfterDeserialize()
		{
			var count = Math.Min(keys.Count, values.Count);
			for (var i = 0; i < count; ++i)
			{
				this.Add(keys[i], values[i]);
			}
		}
	}

	public class StringT 
	{
		public static bool isChinese(string str)
		{
			if (str == null) 
			{
				return false;
			}

			char[] ch = str.ToCharArray();
			if (str != null)
			{
				for (int i = 0; i < ch.Length; i++)
				{
					if (ch[i] >= 0x4E00 && ch[i]<= 0x9FA5)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
