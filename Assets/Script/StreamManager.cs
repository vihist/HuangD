using System;
using System.Reflection; 
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
        LoadUIDesc();
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
            if (key.StartsWith ("EVENT_")) 
			{
                Debug.Log ("anaylize " + key);

                ItfLuaEvent value = luaenv.Global.Get<string, ItfLuaEvent> (key);
                eventDictionary.Add (key, value);
			}
		}

        Debug.Log("Load event cout:" + eventDictionary.Count.ToString());
    }

    private void LoadUIDesc()
    {
        ItfZhoujun value = luaenv.Global.Get<string, ItfZhoujun> ("ZHOUJ");

        Type ty = value.GetType();
        PropertyInfo[] pros = ty.GetProperties();
        foreach (PropertyInfo p in pros)
        {
            UIDictionary.Add(p.Name, (string)p.GetValue(value, null));
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

    public static string GetVarName(System.Linq.Expressions.Expression<Func<string, string>> exp)  
    {  
        return ((System.Linq.Expressions.MemberExpression)exp.Body).Member.Name;  
    }  

	public  static DynastyName dynastyName = new DynastyName();
	public  static YearName yearName = new YearName();
	public  static PersonName personName = new PersonName();
	public  static Dictionary<string, ItfLuaEvent> eventDictionary = new Dictionary<string, ItfLuaEvent>();
    public  static Dictionary<string, string> UIDictionary = new Dictionary<string, string>();

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
            requiredir(name..""/event"")
			requiredir(name..""/static"")	
			print(""********** LOAD END ""..name)
		end

		loadmod(""native"")

		local array = listToTable(CS.Tools.StreamDir.GetSubDirName(""mod""))
		for  i=1,#array do
			loadmod(""mod""..array[i])
		end

        event_metatable = {        
            initialize = function(self, param)
            end,
            historyrecord = function(self)
            end
        }

        for k,v in pairs(_G) do
            if type(v) == ""table"" and string.find(k, ""EVENT_"") == 1 then
                setmetatable(v, { __index = event_metatable })
                v.KEY = k
                if(v.desc == nil) then
                    v.desc = function(self)
                        print(self.DESC)
                        return self.DESC
                    end
                end
                if(v.title == nil) then
                    v.title = function(self)
                        return self.TITLE
                    end
                end
                
                for k1,v1 in pairs(v) do
                    if type(v1) == ""table"" and string.find(k1, ""option"") == 1 then
                        if(v1.desc == nil) then
                            v1.desc = function(self)
                                print(self.DESC)
                                return self.DESC
                            end
                        end
                        if(v1.percondition == nil) then
                            v1.percondition = function(self)
                                return true
                            end
                        end
                        v1.KEY = k1
                        v1.parent = v
                    end
                end
            end
        end

        table.removeElem = function(dataTabel, elem)
            for i =1, #dataTabel do
                if dataTabel[i] == elem then  
                    table.remove(dataTabel, i)  
                end  
            end 
        end

        table.insertRange = function(destTable, srcTable, len)
            for i =1, math.min(len, #srcTable) do
                table.insert(destTable, srcTable[i])  
            end 
        end

        Probability = CS.Tools.Probability
        Selector = CS.MyGame.Selector
        
        
        GMData={

	        GetPersonArray = function(name)
		        return listToTable(CS.MyGame.Inst:GetPerson(name))
	        end,

	        GetFactionArray = function(name)
		        return listToTable(CS.MyGame.Inst:GetFaction(name))
	        end,
            
            GetOfficeArray = function(name)
                return listToTable(CS.MyGame.Inst:GetOffice(name))
            end,

            GetPerson = function(name)
                local persons = listToTable(CS.MyGame.Inst:GetPerson(name))
                if(#persons == 0) then
                    return nil
                end
                if(#persons == 1) then
                    return persons[1]
                end
                
                local i = Probability.GetRandomNum(1, #persons)
                return persons[i]
            end,

            GetFaction = function(name)
                local factions = listToTable(CS.MyGame.Inst:GetFaction(name))
                if(#factions == 0) then
                    return nil
                end
                if(#factions == 1) then
                    return factions[1]
                end
                
                local i = Probability.GetRandomNum(1, #factions)
                return factions[i]
            end,
            
            GetOffice = function(name)
                local offices = listToTable(CS.MyGame.Inst:GetOffice(name))
                if(#offices == 0) then
                    return nil
                end
                if(#offices == 1) then
                    return offices[1]
                end
                
                local i = Probability.GetRandomNum(1, #offices)
                return offices[i]
            end,

            GetProvinceArray = function()
                return listToTable(CS.MyGame.Inst.zhoujManager:Array())
            end,

            Appoint = function(person, office)
                CS.MyGame.Inst:Appoint(person, office)
            end,

            Flag = {
                Get = function(key)
                    return CS.MyGame.Inst:GetFlag(key)
                end,
                
                Set = function(key, value)
                    CS.MyGame.Inst:SetFlag(key, value)
                end,
                
                Clear = function(key)
                     CS.MyGame.Inst:ClearFlag(key)
                end
            },
            
            Emp = {
                Heath = {
                    Dec = function(value)
                        if(value == nil) then
                            value = 1
                        end

                        CS.MyGame.Inst.empHeath = CS.MyGame.Inst.empHeath - value
                        return CS.MyGame.Inst.empHeath
                    end,

                    Inc = function(value)
                      if(value == nil) then
                            value = 1
                        end

                        CS.MyGame.Inst.empHeath = CS.MyGame.Inst.empHeath + value
                        return CS.MyGame.Inst.empHeath
                    end,

                    Value = function()
                        return CS.MyGame.Inst.empHeath
                    end
                },

                Die = function()
                    CS.MyGame.Inst.empDieFlag = true
                end
            },

            Stability = {
                Dec = function(value)
                    if(value == nil) then
                        value = 1
                    end

                    CS.MyGame.Inst.Stability = CS.MyGame.Inst.Stability - value
                    return CS.MyGame.Inst.Stability
                end,

                Inc = function(value)
                  if(value == nil) then
                        value = 1
                    end

                    CS.MyGame.Inst.Stability = CS.MyGame.Inst.Stability + value
                    return CS.MyGame.Inst.Stability
                end,

                Value = function()
                    return CS.MyGame.Inst.GetStability()
                end
            },

            Economy = {
                Dec = function(value)
                    if(value == nil) then
                        value = 1
                    end

                    CS.MyGame.Inst.Economy = CS.MyGame.Inst.Economy - value
                    return CS.MyGame.Inst.Economy
                end,

                Inc = function(value)
                  if(value == nil) then
                        value = 1
                    end

                    CS.MyGame.Inst.Economy = CS.MyGame.Inst.Economy + value
                    return CS.MyGame.Inst.Economy
                end,

                Value = function()
                    return CS.MyGame.Inst.Economy
                end
            },

            Person = {
                PreCreate = function(faction, score)
                    return CS.MyGame.Inst:PreCreatePerson(faction, score)
                end,
                
                Create = function(pInfo, office)
                    return CS.MyGame.Inst:CreatePerson(pInfo, office)
                end,

                Die = function(value)
                    local person = GMData.GetPerson(Selector.ByPerson(value))
                    person:Die()
                end,

                ScoreAdd = function(name, value)
                    local person = GMData.GetPerson(Selector.ByPerson(name))
                    person:ScoreAdd(value)
                end
            },
            
            Date = {
                month = function()
                    return CS.MyGame.Inst.date.month
                end,
                day = function()
                    return CS.MyGame.Inst.date.day
                end
            },
            
            Province = {
                HasDebuff = function(prov, debuff)
                    return prov:HasDebuff(debuff)
                end,

                HasBuff = function(prov, buff)
                    return prov:HasBuff(buff)
                end,

                SetBuff = function(prov, buff)
                    prov:SetBuff(buff)
                end,

                ClearBuff = function(prov, buff)
                    prov:ClearBuff(buff)
                end,
                
                GetDebuffArray = function(prov)
                    if(prov == nil) then
                        return listToTable(CS.MyGame.Inst:GetProvinceDebuff())
                    end
                    return listToTable(prov:GetDebuffArray())
                end,   

                GetDebuff = function(prov)
                    local status = nil

                    if(prov == nil) then
                        status = listToTable(CS.MyGame.Inst:GetProvinceDebuff())
                    else
                        status = listToTable(prov:GetDebuffArray())
                    end
                    
                    if(#status == 0) then
                        return nil
                    end
                    if(#status == 1) then
                        return status[1]
                    end
                    
                    local i = Probability.GetRandomNum(1, #status)
                    return status[i]
                end, 
            }
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
    string KEY{ get;}
    bool   percondition();
	string desc ();
    object process(out string result);
}

[CSharpCallLua]
public interface ItfLuaEvent
{
    string KEY { get;}
    string title();
    string desc();
    bool percondition (out object obj) ;
	void initialize(string param);
    string historyrecord();
	ItfOption option1 { get;}
	ItfOption option2 { get;}
	ItfOption option3 { get;}
	ItfOption option4 { get;}
	ItfOption option5 { get; }
}

[CSharpCallLua]
public interface ItfZhoujun
{
    string Zhouj1 { get;}
    string Zhouj2 { get;}
    string Zhouj3 { get;}
    string Zhouj4 { get;}
    string Zhouj5 { get;}
    string Zhouj6 { get;}
    string Zhouj7 { get;}
    string Zhouj8 { get;}
    string Zhouj9 { get;}
}