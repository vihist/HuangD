using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using XLua;


public partial class MyGame
{
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
                lstFaction.Add(new Faction(efaction.ToString()));
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
            List<Faction> lstResult = new List<Faction>();
            foreach (string name in names)
            {
                Faction o = lstFaction.Find(x => x.name == name);
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

            return lstFaction.GetRange(start, end - start).ToArray();
        }

        internal List<Faction> factionBySelector(SelectElem Selector)
        {
            List<Faction> lstResult = lstFaction.Where(Selector.Complie<Faction>()).ToList();

            return lstResult;
        }

        [SerializeField]
        private List<Faction> lstFaction = new List<Faction>();
    }
}
