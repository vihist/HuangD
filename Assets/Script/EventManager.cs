using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GMEvent
{
	public GMEvent(ItfEvent itf, string param)
	{
		_isChecked = false;
		this.itf = itf;
		this.param = param;

		optionList = new List<ItfOption> ();

        Debug.Log("Event Start:" + itf.title);
	}

	public string title
	{
		get 
		{
			return itf.title;
		}
	}

	public string content
	{
		get 
		{
			return itf.desc;
		}
	}

	public KeyValuePair<bool, string>[] options
    {
        get
        {
			List<KeyValuePair<bool, string>> result = new List<KeyValuePair<bool, string>> ();
			foreach (ItfOption option in optionList) 
			{
				object rslt = option.desc ();

				if (rslt.GetType () == typeof(string)) 
				{
					result.Add (new KeyValuePair<bool, string> (true, (string)rslt));
				} 
				else 
				{
					List<object> list = ((XLua.LuaTable)rslt).Cast<List<object>>();
					if (list.Count == 1) 
					{
						result.Add (new KeyValuePair<bool, string> (true, (string)list [0]));
					} 
					else if (list.Count == 2) 
					{
						result.Add (new KeyValuePair<bool, string> ((bool)list [1], (string)list [0]));
					} 
					else 
					{
						throw new ArgumentOutOfRangeException ();
					}
				}
			}

			return result.ToArray ();
        }
    }
    
	public bool isChecked
	{
		get 
		{
			return _isChecked;
		}
	}

	public void Initlize()
	{
			itf.initialize (param);

			if (itf.option1 != null)
			{
				optionList.Add (itf.option1);
			}
			if (itf.option2 != null)
			{
				optionList.Add (itf.option2);
			}
			if (itf.option3 != null)
			{
				optionList.Add (itf.option3);
			}
			if (itf.option4 != null)
			{
				optionList.Add (itf.option4);
			}
			if (itf.option5 != null)
			{
				optionList.Add (itf.option5);
			}
	}

	public string SelectOption(string op, out string ret)
	{
		_isChecked = true;
		switch (op) 
		{
		case "op1":
			return optionList [0].process (out ret);
		case "op2":
			return optionList [1].process (out ret);
		case "op3":
			return optionList [2].process (out ret);
		case "op4":
			return optionList [3].process (out ret);
		case "op5":
			return optionList [4].process (out ret);
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

    public string Historyrecord()
    {
        return itf.historyrecord();
    }

	private ItfEvent itf;
	private List<ItfOption> optionList;
	private bool _isChecked;
	private string param;
}

public class EventManager
{
	public IEnumerable<GMEvent> GetEvent()
	{  
		foreach (ItfEvent ie in StreamManager.eventDictionary.Values) 
		{
            Debug.Log("percondition event"+ie.title);
			if (!ie.percondition())
			{
				continue;
			}

			GMEvent eventobj = new GMEvent (ie, null);
			eventobj.Initlize ();
			yield return eventobj;

			if (nextEvent == null)
			{
				continue;
			}

			nextEvent.Initlize ();
			yield return nextEvent;

			nextEvent = null;
		}

		yield break;
	}  

	public void Insert(string key, string param)
	{
		if (key.Length == 0) 
		{
			return;
		}
            
		nextEvent = new GMEvent (StreamManager.eventDictionary [key], param);
	}

	private GMEvent nextEvent = null;
}
