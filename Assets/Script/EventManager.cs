using System.Collections;
using System.Collections.Generic;
using System;

public class GMEvent
{
	public GMEvent(ItfEvent itf)
	{
		_isChecked = false;
		this.itf = itf;

		optionList = new List<string> ();

		if (itf.option.op1 != null)
		{
			optionList.Add (itf.option.op1);
		}
		if (itf.option.op2 != null)
		{
			optionList.Add (itf.option.op2);
		}
		if (itf.option.op3 != null)
		{
			optionList.Add (itf.option.op3);
		}
		if (itf.option.op4 != null)
		{
			optionList.Add (itf.option.op4);
		}
		if (itf.option.op5 != null)
		{
			optionList.Add (itf.option.op5);
		}
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
			return optionList.ToArray ();
        }
    }
    
	public bool isChecked
	{
		get 
		{
			return _isChecked;
		}
	}

	public string SelectOption(string op)
	{
		_isChecked = true;
		return itf.option.process (op);
	}

	private ItfEvent itf;
	private List<string> optionList;
	private bool _isChecked;
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

	public void Insert(string key)
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

		eventList.Insert (index, new GMEvent(StreamManager.eventDictionary[key]));
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
				eventList.Add (new GMEvent(ie));
			}
		}
	}

	private List<GMEvent> eventList;
}
