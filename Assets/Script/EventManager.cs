using System.Collections;
using System.Collections.Generic;
using System;

public class Event
{
	public Event()
	{
        _title = testname.ToString();
		testname++;

		time = DateTime.Now;
	}

	public bool isChecked()
	{
		if((DateTime.Now.Minute - time.Minute)*60 + DateTime.Now.Second - time.Second > 10)
		{
			return true;
		}

		return false;
	}


	public string title
    {
        get
        {
            return _title;
        }
    }

	static int testname = 1;

	DateTime  time;
    string _title;
}

public class EventManager
{
	Event[] events = { new Event(), new Event()};

	public IEnumerable<Event> GetEvent()
	{  
		foreach (Event e in events) {
			yield return e;
		}

		yield break;
	}  

	public int GetEventCout()
	{
		return events.Length;
	}
}
