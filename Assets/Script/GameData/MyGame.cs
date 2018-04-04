using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XLua;

using Tools;

[LuaCallCSharp, Serializable]
public partial class MyGame
{
	[NonSerialized]
    public static MyGame Inst = new MyGame();

	public void Initialize(string strEmpName, string strYearName, string strDynastyName)
    {
        empName  = strEmpName;
        empAge   = Probability.GetRandomNum (16, 40);
		empHeath = Probability.GetRandomNum (5, 8);

        Stability = Probability.GetRandomNum(60, 90);
        Economy   = Probability.GetRandomNum(60, 90);
        Military  = Probability.GetRandomNum(60, 90);

        yearName = strYearName;
		dynastyName = strDynastyName;
        date = new GameDateTime();

		officeManager = new OfficeManager (typeof(ENUM_OFFICE));
		femaleOfficeManager = new OfficeManager (typeof(ENUM_OFFICE_FEMALE));

		factionManager = new FactionManager ();

		personManager = new PersonManager (officeManager.Count, true);
		personManager.Sort ((p1,p2)=> -(p1.score.CompareTo(p2.score)));

		femalePersonManager = new PersonManager (femaleOfficeManager.Count, false);

		Person.ListListener.Add (relOffice2Person.Listen);
		Person.ListListener.Add (relFaction2Person.Listen);
		Person.ListListener.Add (personManager.Listen);
		Person.ListListener.Add (femalePersonManager.Listen);

		InitRelationOffice2Person ();
		InitRelationFaction2Person ();

		InitRelationFemaleOffice2Person ();
    }
		
	public Person[] GetPerson(BySelector selecor)
	{
		List<Person> results = null;

		if (selecor.empty)
		{
			throw new ArgumentException ("seletor is empty!");
		}
			
		List<Person> o_persons = null;
		List<Person> f_persons = null;
		List<Person> p_persons = null;

		if(!selecor.offices.empty)
		{
			o_persons = relOffice2Person.GetPerson (selecor.offices);
		}

		if(!selecor.factions.empty)
		{
			f_persons = relFaction2Person.GetPerson (selecor.factions);
		}

		if(!selecor.persons.empty)
		{
			p_persons = personManager.GetByName (selecor.persons);
		}
			

		if(o_persons != null)
		{
			results = o_persons;
		}
		if(f_persons != null)
		{
			if (results == null) 
			{
				results = f_persons;
			} 
			else 
			{
				for (int i = 0; i < results.Count; i++) 
				{
					if (f_persons.Find (x => x.name == results [i].name) == null) 
					{
						results.RemoveAt (i);
					}
				}
			}
		}
		if(p_persons != null)
		{
			if (results == null) 
			{
				results = p_persons;
			} 
			else 
			{
				for (int i = 0; i < results.Count; i++) 
				{
					if (p_persons.Find (x => x.name == results [i].name) == null) 
					{
						results.RemoveAt (i);
					}
				}
			}
		}

		return results.ToArray ();
	}

	public Faction[] GetFaction(BySelector selecor)
	{
		List<Faction> results = null;

//		if (selecor.empty)
//		{
//			throw new ArgumentException ("seletor is empty!");
//		}
//
//		List<Faction> o_factions = null;
//		List<Faction> f_factions = null;
//		List<Faction> p_factions = null;
//
//		if(selecor.offices.Length != 0)
//		{
//			List<Person>  lstPerson = relOffice2Person.GetPerson (selecor.offices);
//			o_factions = new List<Faction> ();
//
//			foreach (Person p in lstPerson) 
//			{
//				o_factions.Add (relFaction2Person.GetFaction (p));
//			}
//		}
//
//		if(selecor.factions.Length != 0)
//		{
//			f_factions = factionManager.GetByName (selecor.factions);
//		}
//
//		if(selecor.persons.Length != 0)
//		{
//
//			p_factions = relFaction2Person.GetFaction (selecor.persons);
//		}
//
//
//		if(o_factions != null)
//		{
//			results = o_factions;
//		}
//		if(f_factions != null)
//		{
//			if (results == null) 
//			{
//				results = f_factions;
//			} 
//			else 
//			{
//				for (int i = 0; i < results.Count; i++) 
//				{
//					if (f_factions.Find (x => x.name == results [i].name) == null) 
//					{
//						results.RemoveAt (i);
//					}
//				}
//			}
//		}
//		if(p_factions != null)
//		{
//			if (results == null) 
//			{
//				results = p_factions;
//			} 
//			else 
//			{
//				for (int i = 0; i < results.Count; i++) 
//				{
//					if (p_factions.Find (x => x.name == results [i].name) == null) 
//					{
//						results.RemoveAt (i);
//					}
//				}
//			}
//		}

		return results.ToArray ();
	}

    private MyGame()
    {
    }


	private void InitRelationOffice2Person ()
	{
		int iCount = officeManager.Count;
		Person[] persons = personManager.GetRange (0, iCount);
		Office[] offices = officeManager.GetRange (0, iCount);

		for (int i = 0; i < iCount; i++) 
		{
			relOffice2Person.Set(persons[i], offices[i]);
		}
	}

	private void InitRelationFaction2Person ()
	{
		Person[] persons = personManager.GetRange (0, personManager.Count);

		for (int i = 0; i < persons.Length; i++) 
		{
			int iRandom = Probability.GetRandomNum (0, 100);
			if(iRandom < 60)
			{
				relFaction2Person.Set (factionManager.GetByName(FactionManager.ENUM_FACTION.SHI.ToString()), persons[i]);
			}
			else if(iRandom < 90)
			{
				relFaction2Person.Set (factionManager.GetByName(FactionManager.ENUM_FACTION.XUN.ToString()), persons[i]);
			}
			else
			{
				relFaction2Person.Set (factionManager.GetByName(FactionManager.ENUM_FACTION.WAI.ToString()), persons[i]);
			}
		}
	}

	private void InitRelationFemaleOffice2Person()
	{
		int iCount = femaleOfficeManager.Count;
		Person[] persons = femalePersonManager.GetRange (0, iCount);
		Office[] offices = femaleOfficeManager.GetRange (0, iCount);

		for (int i = 0; i < iCount; i++) 
		{
			relOffice2Person.Set(persons[i], offices[i]);
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
	public PersonManager femalePersonManager;

	public OfficeManager officeManager;
	public OfficeManager femaleOfficeManager;

	public FactionManager factionManager;

	public string yearName;
	public GameDateTime date;
}

