using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using XLua;

public class StreamManager
{
	public class DynastyName
	{
		public string GetRandom()
		{
			int i = Probability.GetRandomNum(0, names.Count - 1);
			return names[i];
		}

		internal List<string> names = new List<string> ();
	}


	public class YearName
	{
		public string GetRandom()
		{
			int i = Probability.GetRandomNum(0, first.Count-1);
			int j = Probability.GetRandomNum(0, second.Count-1);

			return first[i] + second[j];
		}

		internal List<string> first = new List<string> ();
		internal List<string> second = new List<string> ();
	}


	public class PersonName
	{
		public string GetRandomMale()
		{
			int i = Probability.GetRandomNum(0, family.Count - 1);
			int j = Probability.GetRandomNum(0, given.Count - 1);

			return family [i] + given [j];
		}

		public string GetRandomFemale()
		{
			int i = Probability.GetRandomNum(0, family.Count - 1);
			int j = Probability.GetRandomNum(0, givenfemale.Count - 1);

			return family [i] + givenfemale [j];
		}

		internal List<string> family = new List<string> ();
		internal List<string> given = new List<string> ();
		internal List<string> givenfemale = new List<string> ();
	}

	private StreamManager ()
	{
        luaenv = new LuaEnv();
		luaenv.DoString(script);

      	LoadName ();
		LoadEvent ();
	}

	private void LoadName()
	{
		
		AnaylizePersonName ();
		AnaylizeYearName ();
		AnaylizeDynastyName ();
	}

	private void LoadEvent()
	{
		foreach (string key in luaenv.Global.GetKeys<string> ())
		{
			if (!key.Contains ("EVENT_")) 
			{
				continue;
			}
			Debug.Log ("anaylize " + key);

			ItfEvent value = luaenv.Global.Get<string, ItfEvent> (key);
			eventDictionary.Add (key, value);
		}
    }

	private void AnaylizePersonName()
	{
		Debug.Log ("anaylize person_name");

		ItfPersonName value = luaenv.Global.Get<string, ItfPersonName> ("person_name");

		personName.family.AddRange (value.family.Split (','));
		personName.given.AddRange (value.given.Split (','));
		personName.givenfemale.AddRange (value.givenfemale.Split (','));
	}

	private void AnaylizeYearName()
	{
		Debug.Log ("anaylize year_name");

		ItfYearName value = luaenv.Global.Get<string, ItfYearName> ("year_name");

		yearName.first.AddRange (value.first.Split (','));
		yearName.second.AddRange (value.second.Split (','));
	}

	private void AnaylizeDynastyName()
	{
		Debug.Log ("anaylize dynasty_name");

		string value = luaenv.Global.Get<string, string> ("dynasty_name");
		dynastyName.names.AddRange (value.Split (','));
	}

	public  static DynastyName dynastyName = new DynastyName();
	public  static YearName yearName = new YearName();
	public  static PersonName personName = new PersonName();
	public  static Dictionary<string, ItfEvent> eventDictionary = new Dictionary<string, ItfEvent>();

#pragma warning disable 414  
	private static StreamManager wInst = new StreamManager();
#pragma warning restore

	private LuaEnv luaenv;

	string script = @"
		function listToTable(clrlist)
		    local t = {}
		    local it = clrlist:GetEnumerator()
		    while it:MoveNext() do
		      t[#t+1] = it.Current
		    end
		    return t
		end

	    function GetPerson(name)
            listToTable(CS.MyGame.Inst.GetPerson(name))
	    end

	    function GetFaction(name)
		    listToTable(CS.MyGame.Inst.GetFaction(name))
	    end

		function requirefile(name)
				print(""require ""..name)
				require(name)
		end

		function requirefile(name)
				print(""require ""..name)
				require(name)
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

        Selector = CS.MyGame.Selector
        GMData={
	        GetPerson = function(name)
		        return listToTable(CS.MyGame.Inst:GetPerson(name))
	        end,

	        GetFaction = function(name)
		        return listToTable(CS.MyGame.Inst:GetFaction(name))
	        end
        }
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
	string givenfemale { get; }
}

[CSharpCallLua]
public interface ItfOption
{
	string desc ();
	string process(out string result);
}

[CSharpCallLua]
public interface ItfEvent
{
	string title { get; }
	string desc { get; }
	bool percondition () ;
	void Initlize(string param);
	ItfOption option1 { get;}
	ItfOption option2 { get;}
	ItfOption option3 { get;}
	ItfOption option4 { get;}
	ItfOption option5 { get; }
}