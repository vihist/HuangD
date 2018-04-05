using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;

using XLua;

using Mono;
using System.Linq;

public partial class MyGame
{
	[Serializable, LuaCallCSharp]
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
			
		[SerializeField]
		string _name;

		[SerializeField]
		int _power;
	}

	public enum ENUM_OFFICE
	{
		[OfficeAttr(Power=10)] 
		SG1,

		[OfficeAttr(Power=8)]
		SG2,

		[OfficeAttr(Power=7)]
		SG3,

		[OfficeAttr(Power=10)]
		JQ1,

		[OfficeAttr(Power=5)]
		JQ2,

		[OfficeAttr(Power=5)]
		JQ3,

		[OfficeAttr(Power=5)]
		JQ4,

		[OfficeAttr(Power=5)]
		JQ5,

		[OfficeAttr(Power=5)]
		JQ6,

		[OfficeAttr(Power=5)]
		JQ7,

		[OfficeAttr(Power=5)]
		JQ8,

		[OfficeAttr(Power=5)]
		JQ9,

		[OfficeAttr(Power=3)]
		CS1,

		[OfficeAttr(Power=3)]
		CS2,

		[OfficeAttr(Power=3)]
		CS3,

		[OfficeAttr(Power=3)]
		CS4,

		[OfficeAttr(Power=3)]
		CS5,

		[OfficeAttr(Power=3)]
		CS6,

		[OfficeAttr(Power=3)]
		CS7,

		[OfficeAttr(Power=3)]
		CS8,

		[OfficeAttr(Power=3)]
		CS9,
	}

	public enum ENUM_OFFICE_FEMALE
	{
		[OfficeAttr(Power=0)]
		HOU,

		[OfficeAttr(Power=0)]
		GUI1,

		[OfficeAttr(Power=0)]
		GUI2,

		[OfficeAttr(Power=0)]
		GUI3,

		[OfficeAttr(Power=0)]
		FEI1,

		[OfficeAttr(Power=0)]
		FEI2,

		[OfficeAttr(Power=0)]
		FEI3,

		[OfficeAttr(Power=0)]
		FEI4,

		[OfficeAttr(Power=0)]
		FEI5,

		[OfficeAttr(Power=0)]
		FEI6,
	}

	[Serializable, LuaCallCSharp]
	public class OfficeManager
	{
		public OfficeManager(Type t)
		{
			foreach (var eOffice in Enum.GetValues(t))
			{
				
				FieldInfo field = eOffice.GetType().GetField(eOffice.ToString());
				OfficeAttrAttribute attribute = Attribute.GetCustomAttribute(field, typeof(OfficeAttrAttribute)) as OfficeAttrAttribute;

				lstOffice.Add (new Office(eOffice.ToString(), attribute.Power));
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

		public Office GetByName(string name)
		{
			foreach (Office office in lstOffice) 
			{
				if (office.name == name) 
				{
					return office;
				}
			}

			return null;
		}

		public Office[] GetByName(string[] names)
		{
			List<Office> lstResult = new List<Office> ();
			foreach(string name in names)
			{
				Office o = lstOffice.Find (x=>x.name == name);
				if (o == null) 
				{
					continue;
				}

				lstResult.Add (o);
			}

			return lstResult.ToArray ();
		}

		[SerializeField]
		private List<Office> lstOffice = new List<Office> ();
	}

	[Serializable, LuaCallCSharp]
	public class Faction
	{
		public Faction(string name)
		{
			_name = name;
		}

		public string name
		{
			get 
			{
				return _name;
			}
		}

        public override string ToString()
        {
            return name;
        }

		[SerializeField]
		string _name;
	}

	[Serializable, LuaCallCSharp]
	public class FactionManager
	{
		public enum ENUM_FACTION
		{
			SHI,
			XUN,
			WAI,
			HUA,
		}

		public FactionManager()
		{
			foreach (ENUM_FACTION efaction in Enum.GetValues(typeof(ENUM_FACTION)))
			{
				lstFaction.Add (new Faction(efaction.ToString()));
			}
		}

		public Faction GetByName(string name)
		{
			foreach (Faction faction in lstFaction) 
			{
				if (faction.name == name) 
				{
					return faction;
				}
			}

			return null;
		}

		public Faction[] GetByName(string[] names)
		{
			List<Faction> lstResult = new List<Faction> ();
			foreach(string name in names)
			{
				Faction o = lstFaction.Find (x=>x.name == name);
				if (o == null) 
				{
					continue;
				}

				lstResult.Add (o);
			}

			return lstResult.ToArray();
		}

		public int Count
		{
			get 
			{
				return lstFaction.Count;
			}
		}

		public Faction[] GetRange(int start, int end)
		{
			if (start > lstFaction.Count || start >= end) 
			{
				Faction[] ps = { };
				return ps;
			}

			if (end > lstFaction.Count) 
			{
				end = lstFaction.Count;
			}

			return lstFaction.GetRange (start, end - start).ToArray ();
		}

        internal List<Faction> factionBySelector(SelectElem Selector)
        {
            List<Faction> lstResult = lstFaction.Where(Selector.Complie<Faction>()).ToList();

            return lstResult;
        }

        [SerializeField]
		private List<Faction> lstFaction = new List<Faction> ();
	}

	[Serializable, LuaCallCSharp]
	public class Person
	{
		public Person(Boolean isMale)
		{
			if (isMale) 
			{
				_name = StreamManager.personName.GetRandomMale ();
			} 
			else 
			{
				_name = StreamManager.personName.GetRandomFemale ();
			}

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

		public void Die()
		{
			foreach (Action<object, string> listener in ListListener) 
			{
				listener (this, "DIE");
			}
		}

		public static List<Action<object, string>> ListListener = new List<Action<object, string>>();

		[SerializeField]
		string _name;

		[SerializeField]
		int _score;
	}

	[Serializable, LuaCallCSharp]
	public class PersonManager
	{
		public PersonManager(int count, Boolean isMale)
		{
			while(lstPerson.Count < count)
			{
				Person p = new Person (isMale);
				if (lstPerson.Find(x => x.name.Equals(p.name)) != null)
				{
					continue;
				}

				lstPerson.Add (p);
			}

		}

		public Person[] GetRange(int start, int end)
		{
			if (start > lstPerson.Count || start >= end) 
			{
				Person[] ps = { };
				return ps;
			}

			if (end > lstPerson.Count) 
			{
				end = lstPerson.Count;
			}

			return lstPerson.GetRange (start, end - start).ToArray ();
		}

		public List<Person> GetPersonBySelector(SelectElem Selector)
		{
            List<Person> lstResult = lstPerson.Where(Selector.Complie<Person>()).ToList();

            return lstResult;
		}

		public Person GetByName(string name)
		{
			return lstPerson.Find (x=>x.name == name);
		}

		public List<Person> GetByName(string[] names)
		{
			List<Person> lstResult = new List<Person> ();
			foreach(string name in names)
			{
				Person o = GetByName(name);
				if (o == null) 
				{
					continue;
				}

				lstResult.Add (o);
			}

			return lstResult;
		}

		public int Count
		{
			get 
			{
				return lstPerson.Count;
			}
		}

		public void Sort(Comparison<Person> comparison)
		{
			lstPerson.Sort (comparison);
		}

		public void Listen(object obj, string cmd)
		{
			switch (cmd) 
			{
			case "DIE":
				lstPerson.Remove ((Person)obj);
				break;
			default:
				break;
			}
		}

		[SerializeField]
		private List<Person> lstPerson = new List<Person>();
	}

	public class OfficeAttrAttribute : Attribute
	{
		public int Power;
	}  

	[Serializable, LuaCallCSharp]
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
}