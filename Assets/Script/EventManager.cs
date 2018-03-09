using System.Collections;
using System.Collections.Generic;
using System;

public class Event
{
	public Event()
	{
        _title = "title:" + testname.ToString();
        _content = "content:" + testname.ToString();

        testname++;

		time = DateTime.Now;
	}

	public string title
    {
        get
        {
            return _title;
        }
    }

    public string content
    {
        get
        {
            return _content;
        }
    }

    public string[] options
    {
        get
        {
            List<string> list = new List<string>();
            list.Add(_op1);
            return list.ToArray();
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
    string _content;

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
