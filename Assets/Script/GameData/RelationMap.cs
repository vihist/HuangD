using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Tools;

[Serializable]
class StringSerialDictionary : SerialDictionary<string,string>{};

[Serializable]
class PersonNameList : SerialList<string>{};

[Serializable]
class ListSerialDictionary : SerialDictionary<string, PersonNameList>{};

partial class MyGame
{
	[NonSerialized]
	public RelationOffice2Person relOffice2Person = new RelationOffice2Person();

	[NonSerialized]
	public RelationFaction2Person relFaction2Person = new RelationFaction2Person();

	[SerializeField]
	private StringSerialDictionary DictOffce2Person = new StringSerialDictionary ();

	[SerializeField]
	private  ListSerialDictionary DictFaction2Person = new ListSerialDictionary ();

	public class RelationOffice2Person
	{
		public RelationOffice2Person()
		{
		}

		public void Set(Person p, Office o)
		{
			Inst.DictOffce2Person.Add (o.name, p.name);
		}

		public Person GetPerson(Office o)
		{
			string personName = Inst.DictOffce2Person [o.name];
			Person p = Inst.personManager.GetByName (personName);
			if (p == null) 
			{
				p = Inst.femalePersonManager.GetByName (personName);
			}

			return p;
		}

		public Office GetOffice(Person p)
		{
			string officeName = "";
			foreach (KeyValuePair<string, string> kvp in Inst.DictOffce2Person)
			{
				if (kvp.Value.Equals(p.name))
				{ 
					officeName = kvp.Key;
				}
			}

			if (officeName == "") 
			{
				return null;
			}

			Office  office = Inst.officeManager.GetByName (officeName);
			if (office == null) 
			{
				office = Inst.femaleOfficeManager.GetByName (officeName);
			}
			return office;
		}

		public void Listen(object obj, string cmd)
		{
			switch (cmd) 
			{
			case "DIE":
				{
					Office o = GetOffice ((Person) obj);
					if (o == null)
					{
						break;
					}
					Inst.DictOffce2Person [o.name] = "";
				}
				break;
			default:
				break;
			}
		}
	}

	public class RelationFaction2Person
	{
		public void Set(Faction f, Person p)
		{
			if (!Inst.DictFaction2Person.ContainsKey (f.name))
			{
				Inst.DictFaction2Person.Add (f.name, new PersonNameList());
			}

			Inst.DictFaction2Person [f.name].ToList().Add (p.name);
		}

		public Person[] GetPerson(Faction f)
		{
			string[] personNames = Inst.DictFaction2Person [f.name].ToList().ToArray();

			return Inst.personManager.GetByName (personNames);
		}

		public Faction GetFaction(Person p)
		{
			foreach (KeyValuePair<string, PersonNameList> kvp in Inst.DictFaction2Person)
			{
				if (kvp.Value.ToList().Contains(p.name))
				{ 
					return Inst.factionManager.GetByName (kvp.Key);
				}
			}

			return null;
		}

		public void Listen(object obj, string cmd)
		{
			switch (cmd) 
			{
			case "DIE":
				{
					Person p = (Person)obj;
					Faction o = GetFaction (p);
					if (o == null)
					{
						break;
					}
					Inst.DictFaction2Person [o.name].ToList ().Remove (p.name);
				}
				break;
			default:
				break;
			}
		}
	}
}