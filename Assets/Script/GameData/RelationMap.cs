﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			return GetPerson (o.name);
		}

		public Person GetPerson(string office)
		{
			string personName = Inst.DictOffce2Person [office];
			Person p = Inst.personManager.GetByName (personName);
			if (p == null) 
			{
				p = Inst.femalePersonManager.GetByName (personName);
			}

			return p;
		}

		public List<Person> GetPerson(Office[] offices)
		{
			List<Person> listResult = new List<Person> ();
			foreach (Office o in offices) 
			{
				Person p = GetPerson (o);
				if (p == null) 
				{
					continue;
				}

				listResult.Add (p);
			}

			return listResult;
		}

		public List<Person> GetPersonBySelector(SelectElem Selector, List<Person> ListPerson)
		{
            Office[] Offices = null;
            if (ListPerson == null)
            {
                Offices = Inst.officeManager.GetByName(Inst.DictOffce2Person.Keys.ToArray());
            }
            else
            {
                Offices = GetOffice(ListPerson.ToArray());
            }

            List<Office> SelectOffices = Offices.Where(Selector.Complie<Office>()).ToList();

            return GetPerson(SelectOffices.ToArray());
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

        public Office[] GetOffice(Person[] Persons)
        {
            List<Office> listResult = new List<Office>();
            foreach(Person p in Persons)
            {
                Office o = GetOffice(p);
                if(o != null)
                {
                    listResult.Add(o);
                }
            }

            return listResult.ToArray();

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
			return GetPerson (f.name);
		}

		public Person[] GetPerson(string f)
		{
			string[] personNames = Inst.DictFaction2Person [f].ToList().ToArray();

			return Inst.personManager.GetByName (personNames).ToArray();
		}

		public Person[] GetPerson(Faction[] factions)
		{
			List<Person> listResult = new List<Person> ();
			foreach (Faction o in factions) 
			{
				Person[] p = GetPerson (o);

				listResult.AddRange (p);
			}

			return listResult.ToArray();
		}

		public Faction GetFaction(Person p)
		{
			return GetFaction(p.name);
		}

		public Faction GetFaction(string pname)
		{
			foreach (KeyValuePair<string, PersonNameList> kvp in Inst.DictFaction2Person)
			{
				if (kvp.Value.ToList().Contains(pname))
				{ 
					return Inst.factionManager.GetByName (kvp.Key);
				}
			}

			return null;
		}

		public Faction[] GetFaction(Person[] Persons)
		{
			List<string> listFactionName = new List<string> ();
			
			foreach (KeyValuePair<string, PersonNameList> kvp in Inst.DictFaction2Person)
			{
				foreach (Person p in Persons)
				{
					if (kvp.Value.ToList().Contains(p.name))
					{ 
						if (!listFactionName.Contains (kvp.Key)) 
						{
							listFactionName.Add (kvp.Key);
						}
					}
				}
			}

            return Inst.factionManager.GetByName(listFactionName.ToArray());
		}

        public List<Person> GetPersonBySelector(SelectElem Selector, List<Person> ListPerson)
        {
            if (ListPerson == null)
            {
                Faction[] Factions = Inst.factionManager.GetByName(Inst.DictFaction2Person.Keys.ToArray());

                Faction[] SelectFactions = Factions.Where(Selector.Complie<Faction>()).ToList().ToArray();
                return GetPerson(SelectFactions).ToList();
            }
            else
            {
                for(int i=0; i< ListPerson.Count; i++)
                {
                    if(!Selector.Complie<Faction>().Invoke(GetFaction(ListPerson[i])))
                    {
                        ListPerson.RemoveAt(i);
                    }
                }

                return ListPerson;
            }
        }

        public List<Faction> GetFactionBySelector(SelectElem Selector, List<Faction> ListFaction)
        {
            if (ListFaction == null)
            {
                List<string> listPerson = new List<string>();
                foreach(PersonNameList list in Inst.DictFaction2Person.Values)
                {
                    listPerson.AddRange(list.ToList());
                }

                Person[] Persons = Inst.personManager.GetByName(listPerson.ToArray()).ToArray();

                Person[] SelectPersons = Persons.Where(Selector.Complie<Person>()).ToList().ToArray();

                return GetFaction(SelectPersons).ToList();
            }
            else
            {
                for (int i = 0; i < ListFaction.Count; i++)
                {
                    Person[] Persons = GetPerson(ListFaction[i]);

                    foreach(Person p in Persons)
                    {
                       if(!Selector.Complie<Person>().Invoke(p))
                        {
                            ListFaction.RemoveAt(i);
                            break;
                        }
                    }
                }

                return ListFaction;
            }
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