using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RelationOffice2Person
{
	public RelationOffice2Person(OfficeManager o, PersonManager p)
	{
		personManager = p;
		officeManager = o;
	}

	public void Set(Person p, Office o)
	{
		relation.Add (o.name, p.name);
	}

	public Person GetPerson(Office o)
	{
		return personManager.GetByName (relation[o.name]);
	}

	public Office GetOffice(Person p)
	{
		foreach (KeyValuePair<string, string> kvp in relation)
		{
			if (kvp.Value.Equals(p.name))
			{ 
				return officeManager.GetByName (kvp.Key);
			}
		}

		return null;
	}

	private Dictionary<string, string> relation = new Dictionary<string, string> ();
	private PersonManager personManager;
	private OfficeManager officeManager;
}

public class RelationFaction2Person
{
	public RelationFaction2Person(FactionManager f, PersonManager p)
	{
		personManager = p;
		factionManager = f;
	}

	public void Set(Faction f, Person p)
	{
		if (!relation.ContainsKey (f.name))
		{
			relation.Add (f.name, new List<string>());
		}

		relation [f.name].Add (p.name);
	}

	public Person[] GetPerson(Faction f)
	{
		string[] personNames = relation [f.name].ToArray();

		return personManager.GetByName (personNames);
	}

	public Faction GetFaction(Person p)
	{
		foreach (KeyValuePair<string, List<string>> kvp in relation)
		{
			if (kvp.Value.Contains(p.name))
			{ 
				return factionManager.GetByName (kvp.Key);
			}
		}

		return null;
	}

	private Dictionary<string, List<string>> relation = new Dictionary<string, List<string>> ();
	private PersonManager personManager;
	private FactionManager factionManager;
}
