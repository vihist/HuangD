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
    public class Province
    {
        public class ProvinceAttribute : Attribute
        {
            public ENUM_ECONOMY economy;
        }

        public enum ENUM_PROV
        {
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ1,
            [ProvinceAttribute(economy = ENUM_ECONOMY.LOW)]
            ZHOUJ2,
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ3,
            [ProvinceAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ4,
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ5,
            [ProvinceAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ6,
            [ProvinceAttribute(economy = ENUM_ECONOMY.MID)]
            ZHOUJ7,
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ8,
            [ProvinceAttribute(economy = ENUM_ECONOMY.HIGH)]
            ZHOUJ9
        }

        public enum ENUM_BUFF_TYPE
        {
            NORMAL,
            BUFF,
            DEBUFF
        }

        public class ProvStatusAttribute : Attribute
        {
            public ENUM_BUFF_TYPE buffType;
        }

        public enum ENUM_PROV_STATUS
        {
            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.NORMAL)]
            NORMAL,
            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.DEBUFF)]
            HONG,
            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.DEBUFF)]
            HAN,
            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.DEBUFF)]
            HUANG,
            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.DEBUFF)]
            WEN,
            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.DEBUFF)]
            ZHEN,
            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.DEBUFF)]
            KOU,
            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.DEBUFF)]
            FAN,

            [ProvStatusAttribute(buffType = ENUM_BUFF_TYPE.BUFF)]
            FENG
        }

        public enum ENUM_ECONOMY
        {
            HIGH,
            MID,
            LOW,

        }
        public Province(string name, ENUM_ECONOMY economy)
        {
            _name = name;
            _economy = economy;
            listStatus = new List<ENUM_PROV_STATUS>();
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
                foreach (ENUM_PROV_STATUS status in listStatus)
                {
                    result.Add(status.ToString());
                }

                return result.ToArray();
            }
        }

        public void SetBuff(string buff)
        {
            ENUM_PROV_STATUS e = (ENUM_PROV_STATUS)Enum.Parse(typeof(ENUM_PROV_STATUS), buff);
            if(!listStatus.Contains(e))
            {
                listStatus.Add(e);
            }
        }

        public void ClearBuff(string buff)
        {
            ENUM_PROV_STATUS e = (ENUM_PROV_STATUS)Enum.Parse(typeof(ENUM_PROV_STATUS), buff);
            listStatus.Remove(e);
        }

        public bool HasBuff(string buff)
        {
            if(buff == null)
            {
                foreach (ENUM_PROV_STATUS status in listStatus)
                {
                    FieldInfo field = status.GetType().GetField(status.ToString());
                    ProvStatusAttribute attribute = Attribute.GetCustomAttribute(field, typeof(ProvStatusAttribute)) as ProvStatusAttribute;
                    if(attribute.buffType == ENUM_BUFF_TYPE.BUFF)
                    {
                        return true;
                    }
                }

                return false;
            }

            ENUM_PROV_STATUS e = (ENUM_PROV_STATUS)Enum.Parse(typeof(ENUM_PROV_STATUS), buff);
            return listStatus.Contains(e);
        }

        public bool HasDebuff(string buff)
        {
            if (buff == null)
            {
                foreach (ENUM_PROV_STATUS status in listStatus)
                {
                    FieldInfo field = status.GetType().GetField(status.ToString());
                    ProvStatusAttribute attribute = Attribute.GetCustomAttribute(field, typeof(ProvStatusAttribute)) as ProvStatusAttribute;
                    if (attribute.buffType == ENUM_BUFF_TYPE.DEBUFF)
                    {
                        return true;
                    }
                }

                return false;
            }

            ENUM_PROV_STATUS e = (ENUM_PROV_STATUS)Enum.Parse(typeof(ENUM_PROV_STATUS), buff);
            return listStatus.Contains(e);
        }

        public string[] GetDebuffArray()
        {
            List<string> result = new List<string>();
            foreach (ENUM_PROV_STATUS status in listStatus)
            {
                FieldInfo field = status.GetType().GetField(status.ToString());
                ProvStatusAttribute attribute = Attribute.GetCustomAttribute(field, typeof(ProvStatusAttribute)) as ProvStatusAttribute;
                if (attribute.buffType == ENUM_BUFF_TYPE.DEBUFF)
                {
                    result.Add(status.ToString());
                }
            }

            return result.ToArray();
        }

        private string _name;
        private List<ENUM_PROV_STATUS> listStatus;
        private ENUM_ECONOMY _economy;

    }

    [Serializable, LuaCallCSharp]
    public class ZhoujManager : IEnumerable
    {
        public ZhoujManager()
        {
            foreach (var zj in Enum.GetValues(typeof(Province.ENUM_PROV)))
            {

                FieldInfo field = zj.GetType().GetField(zj.ToString());
                Province.ProvinceAttribute attribute = Attribute.GetCustomAttribute(field, typeof(Province.ProvinceAttribute)) as Province.ProvinceAttribute;

                Province ZhoujObj = new Province(zj.ToString(), attribute.economy);
                lstZhouj.Add(ZhoujObj);

            }
        }

        public Province GetByName(string name)
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

        public Province[] Array()
        {
             return lstZhouj.ToArray();
        }

        private List<Province> lstZhouj = new List<Province>();
    }

}

