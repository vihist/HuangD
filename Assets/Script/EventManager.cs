using System.Collections;
using System.Collections.Generic;
using System;

public class GMEvent
{
	public GMEvent(ItfEvent itf, string param)
	{
		_isChecked = false;
		this.itf = itf;
		this.param = param;

		optionList = new List<ItfOption> ();
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

    public string[] options
    {
        get
        {
			List<string> result = new List<string> ();
			foreach (ItfOption option in optionList) 
			{
				result.Add (option.desc());
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
			itf.Initlize (param);

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

	private ItfEvent itf;
	private List<ItfOption> optionList;
	private bool _isChecked;
	private string param;
}

public class EventManager
{
	public EventManager()
	{
		eventList = new List<GMEvent> ();
	}

	public IEnumerable<GMEvent> GetEvent()
	{  
		if(eventList.Count == 0)
		{
			makeEvent ();
		}

		foreach (GMEvent e in eventList)
		{
			yield return e;
		}

		yield break;
	}  

	public int GetEventCout()
	{
		return eventList.Count;
	}

	public void Insert(string key, string param)
	{
		if (key.Length == 0) 
		{
			return;
		}

		int index=eventList.FindIndex(a=>!a.isChecked);
		if (index == -1) 
		{
			index = eventList.Count;
		}

		GMEvent eventobj = new GMEvent (StreamManager.eventDictionary [key], param);
		eventobj.Initlize ();

		eventList.Insert (index, eventobj);
	}

	public List<GMEvent> EventList
	{
		get 
		{
			return eventList;
		}
	}

	public void makeEvent ()
	{
		foreach (ItfEvent ie in StreamManager.eventDictionary.Values) 
		{
			if (ie.percondition ()) 
			{
				GMEvent eventobj = new GMEvent (ie, null);
				eventobj.Initlize ();

				eventList.Add (eventobj);
			}
		}
	}

	public void ClearEvent()
	{
		eventList.Clear ();
	}

	private List<GMEvent> eventList;
}
