using System.Collections;
using System.Collections.Generic;
using System;

public class GMEvent
{
	public GMEvent(ItfEvent itf)
	{
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
    
	private ItfEvent itf;
	private List<string> optionList;
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

	private void makeEvent ()
	{
		foreach (ItfEvent ie in StreamManager.eventList) 
		{
			if (ie.percondition ()) 
			{
				eventList.Add (new GMEvent(ie));
			}
		}
	}

	private List<GMEvent> eventList;
}
