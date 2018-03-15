using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XLua;

[LuaCallCSharp]
public class MyGame
{
    public static MyGame Inst = new MyGame();

	public void Initialize(string strEmpName, string strYearName, string strDynastyName)
    {
        empName  = strEmpName;
        empAge   = Tools.Probability.GetRandomNum (16, 40);
		empHeath = Tools.Probability.GetRandomNum (50, 90);

        Stability = Tools.Probability.GetRandomNum(60, 90);
        Economy   = Tools.Probability.GetRandomNum(60, 90);
        Military  = Tools.Probability.GetRandomNum(60, 90);

        yearName = strYearName;
		dynastyName = strDynastyName;
        date = new GameDateTime();

		officeManager = new OfficeManager ();
		personManager = new PersonManager (officeManager.Count);
		relPersonAndOffice = new RelationManager ();

		InitRelationPerson2Office ();


    }

    private MyGame()
    {
    }

    private string GetText(string str)
    {

#if UNITY_EDITOR_OSX
		if(Tools.StringT.isChinese(str))
		{
			byte[] bytes = Encoding.Unicode.GetBytes(str);
			return BitConverter.ToString (bytes, 0).Replace ("-", string.Empty);
		}
#endif
		return str;
    }

	private void InitRelationPerson2Office ()
	{
		int iCount = officeManager.Count;
		Person[] persons = personManager.GetRange (0, iCount);
		Office[] offices = officeManager.GetRange (0, iCount);

		for (int i = 0; i < iCount; i++) 
		{
			relPersonAndOffice.Set (persons[i].name, offices[i].name);
		}
	}

    public string time
    {
        get
        {
			return dynastyName + " " + yearName + date.ToString();
        }
    }


	public string empName;
	public int    empAge;
	public int    empHeath;


	public string dynastyName;
    public int    Stability;
    public int    Economy;
    public int    Military;

	public PersonManager personManager;
	public OfficeManager officeManager;
	public RelationManager relPersonAndOffice;
	public RelationManager relPersonAndFaction;

    private string yearName;
    private GameDateTime date;
}

public class OfficeManager
{
	public enum ENUM_OFFICE
	{
		SG1=10,
		SG2=8,
		SG3=7,
		JQ1=5,
		JQ2=5,
		JQ3=5,
		JQ4=5,
		JQ5=5,
		JQ6=5,
		JQ7=5,
		JQ8=5,
		JQ9=5,
		CS1=3,
		CS2=3,
		CS3=3,
		CS4=3,
		CS5=3,
		CS6=3,
		CS7=3,
		CS8=3,
		CS9=3,
	}

	public OfficeManager()
	{
		foreach (ENUM_OFFICE eOffice in Enum.GetValues(typeof(ENUM_OFFICE)))
		{
			lstOffice.Add (new Office(eOffice.ToString(), (int)eOffice));
		}
	}

	public Office[] GetRange(int start, int end)
	{
		if (start > lstOffice.Count || start >= end) 
		{
			Office[] ps = { };
			return ps;
		}

		if (end > lstOffice.Count) 
		{
			end = lstOffice.Count;
		}

		return lstOffice.GetRange (start, end - start).ToArray ();
	}

	public int Count
	{
		get 
		{
			return lstOffice.Count;
		}
	}

	private List<Office> lstOffice = new List<Office> ();
}

public class RelationManager
{
	public void Set(string first, string second)
	{
		dictionary.Add (first, second);
	}

	public string GetByFirst(string first)
	{
		if (!dictionary.ContainsKey (first)) 
		{
			return null;
		}

		return dictionary [first];
	}

	public string GetBySecond(string second)
	{
		foreach (KeyValuePair<string, string> kvp in dictionary)
		{
			if (kvp.Value.Equals(second))
			{ 
				return kvp.Key;
			}
		}

		return null;
	}

	private Dictionary<string, string> dictionary = new Dictionary<string, string>();
}

[Serializable]
public class Office
{
	public Office(string name, int power)
	{
		_name = name;
		_power = power;
	}

	public string name
	{
		get 
		{
			return _name;
		}
	}

	public int power
	{
		get 
		{
			return _power;
		}
	}

	string _name;
	int _power;
}

public class PersonManager
{
	public PersonManager(int count)
	{
		while(lstMalePerson.Count < count)
		{
			Person p = new Person ();
			if (lstMalePerson.Find(x => x.name == p.name) != null)
			{
				continue;
			}

			lstMalePerson.Add (new Person ());
		}

		lstMalePerson.Sort ((p1,p2)=> -(p1.score.CompareTo(p2.score)));
	}

	public Person[] GetRange(int start, int end)
	{
		if (start > lstMalePerson.Count || start >= end) 
		{
			Person[] ps = { };
			return ps;
		}

		if (end > lstMalePerson.Count) 
		{
			end = lstMalePerson.Count;
		}

		return lstMalePerson.GetRange (start, end - start).ToArray ();
	}
		

	private List<Person> lstMalePerson = new List<Person>();
	private List<Person> lstFemalePerson;
}

[Serializable]
public class Person
{
	public Person()
	{
		_name = StreamManager.personName.GetRandom ();
		_score = Tools.Probability.GetRandomNum (10, 90);
	}

	public string name
	{
		get 
		{
			return _name;
		}
	}

	public int score
	{
		get 
		{
			return _score;
		}
	}

	string _name;
	int _score;
}


[Serializable]
public class GameDateTime
{
    public GameDateTime()
    {
        _year = 1;
        _month = 1;
        _day = 1;
    }

    public void Increase()
    {
        if (_day == 30)
        {
            if (_month == 12)
            {
                _year++;
                _month = 1;
            }
            else
            {
                _month++;
            }
            _day = 1;
        }
        else
        {
            _day++;
        }
    }

    public int year
    {
        get
        {
            return _year;
        }
    }

    public int month
    {
        get
        {
            return _month;
        }
    }

    public int day
    {
        get
        {
            return _day;
        }
    }

    public override string ToString()
    {
        return _year.ToString() + "年" + _month + "月" + _day + "日";
    }

    public bool Is(string str)
    {
        string[] arr = str.Split('/');
        if (arr.Length < 3)
        {
            throw new Exception();
        }

        if (arr[0] != "*")
        {
            if (Convert.ToInt16(arr[0]) != _year)
            {
                return false;
            }
        }

        if (arr[1] != "*")
        {
            if (Convert.ToInt16(arr[1]) != _month)
            {
                return false;
            }
        }

        if (arr[2] != "*")
        {
            if (Convert.ToInt16(arr[2]) != _day)
            {
                return false;
            }
        }

        return true;
    }

    [SerializeField]
    private int _year;
    [SerializeField]
    private int _month;
    [SerializeField]
    private int _day;
}
