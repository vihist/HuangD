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

    public class Zhouj
    {
        public class ZhoujAttribute : Attribute
        {
            public ENUM_ECONOMY economy;
        }

        public enum ENUM_ZHOUJ
        {
            [ZhoujAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ1,
            [ZhoujAttribute(economy = ENUM_ECONOMY.LOW)]
            ZHOUJ2,
            [ZhoujAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ3,
            [ZhoujAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ4,
            [ZhoujAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ5,
            [ZhoujAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ6,
            [ZhoujAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ7,
            [ZhoujAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ8,
            [ZhoujAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ9
        }

        public enum ENUM_STATUS
        {
            NORMAL,
            SHUI,
            HAN,
            HUANG,
            WEN,
            ZHEN,
            FENG,
            KOU,
            FAN,
        }

        public enum ENUM_ECONOMY
        {
            HIGH,
            MID,
            LOW,

        }
        public Zhouj(string name, ENUM_ECONOMY economy)
        {
            _name = name;
            _economy = economy;
            listStatus = new List<ENUM_STATUS>();
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public string economy
        {
            get
            {
                return _economy.ToString();
            }
        }

        public string[] status
        {
            get
            {
                List<string> result = new List<string>();
                foreach (ENUM_STATUS status in listStatus)
                {
                    result.Add(status.ToString());
                }

                return result.ToArray();
            }
        }


        private string _name;
        private List<ENUM_STATUS> listStatus;
        private ENUM_ECONOMY _economy;

    }

    public class ZhoujManager : IEnumerable
    {
        public ZhoujManager()
        {
            foreach (var zj in Enum.GetValues(typeof(Zhouj.ENUM_ZHOUJ)))
            {

                FieldInfo field = zj.GetType().GetField(zj.ToString());
                Zhouj.ZhoujAttribute attribute = Attribute.GetCustomAttribute(field, typeof(Zhouj.ZhoujAttribute)) as Zhouj.ZhoujAttribute;

                Zhouj ZhoujObj = new Zhouj(zj.ToString(), attribute.economy);
                lstZhouj.Add(ZhoujObj);

            }
        }

        public Zhouj GetByName(string name)
        {
            return lstZhouj.Find(x => x.name == name);
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < lstZhouj.Count; i++)
            {
                yield return lstZhouj[i];
            }
        }

        private List<Zhouj> lstZhouj = new List<Zhouj>();
    }

}

