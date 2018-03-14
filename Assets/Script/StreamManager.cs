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
			int i = Tools.Probability.GetRandomNum(0, streamManager.periodNames.Count - 1);
			return streamManager.periodNames[i];
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
			int i = Tools.Probability.GetRandomNum(0, streamManager.yearNameFirst.Count-1);
			int j = Tools.Probability.GetRandomNum(0, streamManager.yearNameSecond.Count-1);

			return streamManager.yearNameFirst[i] + streamManager.yearNameSecond[j];
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
			int i = Tools.Probability.GetRandomNum(0, streamManager.personNameFamily.Count - 1);
			int j = Tools.Probability.GetRandomNum(0, streamManager.personNameGiven.Count - 1);

			return streamManager.personNameFamily [i] + streamManager.personNameGiven [j];
		}

		private StreamManager streamManager;
	}

	private StreamManager ()
	{
        luaenv = new LuaEnv();
		luaenv.DoString(script);
        //LoadLua("/native/", "loader");

        LoadName ();
		LoadEvent ();
	}

	private void LoadName()
	{

		ItfYearName yearName = luaenv.Global.Get<ItfYearName>("year_name");
		ItfPersonName personName = luaenv.Global.Get<ItfPersonName>("person_name");
		string speriodName = luaenv.Global.Get<string>("period_name");

		yearNameFirst = new List<string>(yearName.first.Split (','));
		yearNameSecond = new List<string>(yearName.second.Split (','));
		personNameFamily = new List<string>(personName.family.Split (','));
		personNameGiven = new List<string>(personName.given.Split (','));
		periodNames = new List<string>(speriodName.Split (','));
	}

	private void LoadEvent()
	{
        eventDictionary = new Dictionary<string, ItfEvent>();

        Action<string, ItfEvent> action = AnaylizeEvent;
        luaenv.Global.ForEach(action);
    }

	private void AnaylizeEvent(string name, ItfEvent value)
	{
		if (name.StartsWith ("EVENT_")) 
		{
			Debug.Log (name);
			eventDictionary.Add (name, value);
		}
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

	public  static PeriodName periodName = new PeriodName(Inst);
	public  static YearName yearName = new YearName(Inst);
	public  static PersonName personName = new PersonName(Inst);
	public  static Dictionary<string, ItfEvent> eventDictionary;

	private LuaEnv luaenv;

	private List<string> periodNames;

	private List<string> yearNameFirst;
	private List<string> yearNameSecond;
	private List<string> personNameFamily;
	private List<string> personNameGiven;

	string script = @"
		function listToTable(clrlist)
		    local t = {}
		    local it = clrlist:GetEnumerator()
		    while it:MoveNext() do
		      t[#t+1] = it.Current
		    end
		    return t
		end

		function requirefile(name)
				print(""require ""..name)
				require(name)
		end

		function requiredir(name)
			array = listToTable(CS.Tools.StreamDir.GetLuaFileName(name))
			for  i=1,#array do
				requirefile(name..array[i])
			end
		end

		function loadmod(name)
			print(""********** LOAD START ""..name)
			requirefile(name..""/info"")
			if(on) then
				print(name.."" is on"")
			else
				print(name.."" is not on"")
			end
			requiredir(name..""/static"")
			requiredir(name..""/event"")
			print(""********** LOAD END ""..name)
		end

		loadmod(""native"")

		local array = listToTable(CS.Tools.StreamDir.GetSubDirName(""mod""))
		for  i=1,#array do
			loadmod(""mod""..array[i])
		end

    ";
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

[CSharpCallLua]
public interface ItfOption
{
	string op1 { get; }
	string op2 { get; }
	string op3 { get; }
	string op4 { get; }
	string op5 { get; }
	string process(string op);
}

[CSharpCallLua]
public interface ItfEvent
{
	string title { get; }
	string desc { get; }
	bool percondition () ;
	ItfOption option { get;}
}