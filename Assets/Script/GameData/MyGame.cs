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
		empHeath = Probability.GetRandomNum (50, 90);

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

		InitRelationOffice2Person ();
		InitRelationFaction2Person ();

		InitRelationFemaleOffice2Person ();
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

