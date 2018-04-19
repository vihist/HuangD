using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using XLua;

public partial class MyGame
{
    [Serializable, LuaCallCSharp]
    public class Person
    {
        public Person(Boolean isMale) : this(isMale, Tools.Probability.GetRandomNum(10, 90))
        {
            
        }

        public Person(Boolean isMale, int score)
        {
            if (isMale)
            {
                _name = StreamManager.personName.GetRandomMale();
            }
            else
            {
                _name = StreamManager.personName.GetRandomFemale();
            }

            _score = score;
        }

        public Person(PER_CREATE_PERSON p)
        {
            _name = p.personName;
            _score = p.score;
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
                listener(this, "DIE");
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
            while (lstPerson.Count < count)
            {
                Person p = new Person(isMale);
                if (lstPerson.Find(x => x.name.Equals(p.name)) != null)
                {
                    continue;
                }

                lstPerson.Add(p);
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

            return lstPerson.GetRange(start, end - start).ToArray();
        }

        public List<Person> GetPersonBySelector(SelectElem Selector)
        {
            List<Person> lstResult = lstPerson.Where(Selector.Complie<Person>()).ToList();

            return lstResult;
        }

        public Person GetByName(string name)
        {
            return lstPerson.Find(x => x.name == name);
        }

        public Person[] GetByName(string[] names)
        {
            List<Person> lstResult = new List<Person>();
            foreach (string name in names)
            {
                Person o = GetByName(name);
                if (o == null)
                {
                    continue;
                }

                lstResult.Add(o);
            }

            return lstResult.ToArray();
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
            lstPerson.Sort(comparison);
        }

        public void Listen(object obj, string cmd)
        {
            switch (cmd)
            {
                case "DIE":
                    lstPerson.Remove((Person)obj);
                    break;
                default:
                    break;
            }
        }

        public void Add(Person p)
        {
            lstPerson.Add(p);
        }

        [SerializeField]
        private List<Person> lstPerson = new List<Person>();
    }
}
