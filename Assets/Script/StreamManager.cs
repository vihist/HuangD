using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using XLua;

public class StreamManager
{
	public class PeriodName
	{
		public PeriodName(StreamManager streamManger)
		{
			this.streamManager = streamManger;
		}

		public string GetRandom()
		{
			streamManager.LoadLua ("/native/static/", "name");

			string periodName = streamManager.luaenv.Global.Get<string>("period_name");

			string[] periodNameList = periodName.Split (',');

			int i = Tools.Probability.GetRandomNum(0, periodNameList.Length - 1);
			return periodNameList[i];
		}

		private StreamManager streamManager;
	}


	public class YearName
	{
		public YearName(StreamManager streamManger)
		{
			this.streamManager = streamManger;
		}

		public string GetRandom()
		{
			streamManager.LoadLua ("/native/static/", "name");

			ItfYearName yearName = streamManager.luaenv.Global.Get<ItfYearName>("year_name");

			string[] first = yearName.first.Split (',');
			string[] second = yearName.second.Split (',');

			int i = Tools.Probability.GetRandomNum(0, first.Length - 1);
			int j = Tools.Probability.GetRandomNum(0, second.Length - 1);

			return first[i] + second[j];

		}

		private StreamManager streamManager;
	}


	public class PersonName
	{
		public PersonName(StreamManager streamManger)
		{
			this.streamManager = streamManger;
		}

		public string GetRandom()
		{
			streamManager.LoadLua ("/native/static/", "name");

			ItfPersonName personName = streamManager.luaenv.Global.Get<ItfPersonName>("person_name");

			string[] family = personName.family.Split (',');
			string[] given = personName.given.Split (',');

			int i = Tools.Probability.GetRandomNum(0, family.Length - 1);
			int j = Tools.Probability.GetRandomNum(0, given.Length - 1);

			return family [i] + given [j];

		}

		private StreamManager streamManager;
	}

	private StreamManager ()
	{
		luaenv = new LuaEnv();
	}

	private void LoadLua(string path, string file)
	{
		LuaEnv.CustomLoader loader = delegate(ref string fileName) {
			return CustomLoaderMethod(ref fileName, path);
		};

		luaenv.AddLoader (loader);

		luaenv.DoString("require '"+file+"'");
	}


	private byte[] CustomLoaderMethod(ref string fileName, string path)
	{
		//找到指定文件  
		fileName = Application.streamingAssetsPath + path + fileName.Replace('.', '/') + ".lua";
		Debug.Log(fileName);
		if (File.Exists(fileName))
		{
			return File.ReadAllBytes(fileName);
		}
		else
		{
			throw new FileNotFoundException ();
		}
	}


	private static StreamManager Inst = new StreamManager();

	public static PeriodName periodName = new PeriodName(Inst);
	public static YearName yearName = new YearName(Inst);
	public static PersonName personName = new PersonName(Inst);



	private LuaEnv luaenv;
}


[CSharpCallLua]
public interface ItfYearName
{
	string first { get; }
	string second { get; }
}

[CSharpCallLua]
public interface ItfPersonName
{
	string family { get; }
	string given { get; }
}