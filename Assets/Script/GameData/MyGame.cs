using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XLua;

using Tools;

[LuaCallCSharp]
public class MyGame
{
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

		officeManager = new OfficeManager ();
		factionManager = new FactionManager ();
		personManager = new PersonManager (officeManager.Count);

		relOffice2Person = new RelationOffice2Person (officeManager, personManager);
		relFaction2Person = new RelationFaction2Person (factionManager, personManager);

		InitRelationOffice2Person ();
		InitRelationFaction2Person ();
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
		Faction[] Factions = factionManager.GetRange (0, factionManager.Count);

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
	public FactionManager factionManager;

	public RelationOffice2Person relOffice2Person;
	public RelationFaction2Person relFaction2Person;

    private string yearName;
    private GameDateTime date;
}

