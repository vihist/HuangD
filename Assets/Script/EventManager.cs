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

        optionDic = new Dictionary<string, ItfOption>();

        Debug.Log("Event Start:" + itf.title());
	}
        
	public string title
	{
		get 
		{
            return itf.title();
		}
	}

	public string content
	{
		get 
		{
            return itf.desc();
		}
	}

	public KeyValuePair<string, string>[] options
    {
        get
        {
			List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>> ();
            foreach (ItfOption option in optionDic.Values) 
			{
                if (!option.percondition())
                {
                    continue;
                }

                result.Add (new KeyValuePair<string, string> (option.KEY, option.desc ()));
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
            optionDic.Add (itf.option1.KEY, itf.option1);
		}
		if (itf.option2 != null)
		{
            optionDic.Add (itf.option2.KEY, itf.option2);
		}
		if (itf.option3 != null)
		{
            optionDic.Add (itf.option3.KEY, itf.option3);
		}
		if (itf.option4 != null)
		{
            optionDic.Add (itf.option4.KEY, itf.option4);
		}
		if (itf.option5 != null)
		{
            optionDic.Add (itf.option5.KEY, itf.option5);
		}
	}

    public object SelectOption(string opKey, out string ret)
	{
        _isChecked = true;

        return optionDic[opKey].process(out ret);
       
    }

    public string Historyrecord()
    {
        return itf.historyrecord();
    }

	private ItfEvent itf;
	private bool _isChecked;
    private Dictionary<string, ItfOption> optionDic;

	private string param;
}

public class EventManager
{
	public IEnumerable<GMEvent> GetEvent()
	{  
		foreach (ItfEvent ie in StreamManager.eventDictionary.Values) 
		{
            Debug.Log("percondition event"+ie.KEY);
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

    public void Insert(List<List<string>> table)
    {
        if (table.Count == 0) 
        {
            return;
        }

        //nextEvent = new GMEvent ("TABLE_DIALOG", table);
    }

	private GMEvent nextEvent = null;
}
