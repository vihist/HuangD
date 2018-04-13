using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;

using XLua;

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

    public class OfficeAttrAttribute : Attribute
    {
        public int Power;
    }

    public enum ENUM_OFFICE
    {
        [OfficeAttr(Power = 10)]
        SG1,

        [OfficeAttr(Power = 8)]
        SG2,

        [OfficeAttr(Power = 7)]
        SG3,

        [OfficeAttr(Power = 10)]
        JQ1,

        [OfficeAttr(Power = 5)]
        JQ2,

        [OfficeAttr(Power = 5)]
        JQ3,

        [OfficeAttr(Power = 5)]
        JQ4,

        [OfficeAttr(Power = 5)]
        JQ5,

        [OfficeAttr(Power = 5)]
        JQ6,

        [OfficeAttr(Power = 5)]
        JQ7,

        [OfficeAttr(Power = 5)]
        JQ8,

        [OfficeAttr(Power = 5)]
        JQ9,

        [OfficeAttr(Power = 3)]
        CS1,

        [OfficeAttr(Power = 3)]
        CS2,

        [OfficeAttr(Power = 3)]
        CS3,

        [OfficeAttr(Power = 3)]
        CS4,

        [OfficeAttr(Power = 3)]
        CS5,

        [OfficeAttr(Power = 3)]
        CS6,

        [OfficeAttr(Power = 3)]
        CS7,

        [OfficeAttr(Power = 3)]
        CS8,

        [OfficeAttr(Power = 3)]
        CS9,
    }

    public enum ENUM_OFFICE_FEMALE
    {
        [OfficeAttr(Power = 0)]
        HOU,

        [OfficeAttr(Power = 0)]
        GUI1,

        [OfficeAttr(Power = 0)]
        GUI2,

        [OfficeAttr(Power = 0)]
        GUI3,

        [OfficeAttr(Power = 0)]
        FEI1,

        [OfficeAttr(Power = 0)]
        FEI2,

        [OfficeAttr(Power = 0)]
        FEI3,

        [OfficeAttr(Power = 0)]
        FEI4,

        [OfficeAttr(Power = 0)]
        FEI5,

        [OfficeAttr(Power = 0)]
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

                lstOffice.Add(new Office(eOffice.ToString(), attribute.Power));
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

            return lstOffice.GetRange(start, end - start).ToArray();
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
            List<Office> lstResult = new List<Office>();
            foreach (string name in names)
            {
                Office o = lstOffice.Find(x => x.name == name);
                if (o == null)
                {
                    continue;
                }

                lstResult.Add(o);
            }

            return lstResult.ToArray();
        }

        public List<Office> GetOfficeBySelector(SelectElem Selector)
        {
            List<Office> lstResult = lstOffice.Where(Selector.Complie<Office>()).ToList();

            return lstResult;
        }

        [SerializeField]
        private List<Office> lstOffice = new List<Office>();
    }
}