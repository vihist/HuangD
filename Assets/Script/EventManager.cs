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
	public string op1
    {
        get
        {
            return _op1;
        }
    }
    public string op2
    {
        get
        {
            return _op2;
        }
    }
    public string op3
    {
        get
        {
            return _op3;
        }
    }
    public string op4
    {
        get
        {
            return _op4;
        }
    }
    public string op5
    {
        get
        {
            return _op5;
        }
    }
    
	static int testname = 1;

	DateTime  time;
    string _title;
    string _op1;
    string _op2;
    string _op3;
    string _op4;
    string _op5;
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
