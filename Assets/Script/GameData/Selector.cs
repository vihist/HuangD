using System;
using System.Collections;
using System.Collections.Generic;

using XLua;
using UnityEngine;

public partial class MyGame
{
	[LuaCallCSharp]
	public class Selector
	{
		public static BySelector ByName(params string[] key)
		{
			return new BySelector ().ByName(key);
		}

		public static BySelector ByOffice(params string[] key)
		{
			return new BySelector ().ByOffice(key);
		}

		public static BySelector ByFaction(params string[] key)
		{
			return new BySelector ().ByFaction(key);
		}

		public static BySelector ByNameNOT(params string[] key)
		{
			return new BySelector ().ByNameNOT(key);
		}

		public static BySelector ByOfficeNOT(params string[] key)
		{
			return new BySelector ().ByOfficeNOT(key);
		}

		public static BySelector ByFactionNOT(params string[] key)
		{
			return new BySelector ().ByFactionNOT(key);
		}
	}

	[LuaCallCSharp]
	public class BySelector
	{
		public BySelector ByName(params string[] key)
		{
			foreach (string s in key) 
			{
				ByPersonElem.EqualList.Add (key);
			}
			return this;
		}

		public BySelector ByOffice(params string[] key)
		{
			foreach (string s in key) 
			{
				ByOfficeElem.EqualList.Add (key);
			}
			return this;
		}

		public BySelector ByFaction(params string[] key)
		{
			foreach (string s in key) 
			{
				ByFactionElem.EqualList.Add (key);
			}
			return this;
		}

		public BySelector ByNameNOT(params string[] key)
		{
			foreach (string s in key) 
			{
				ByPersonElem.UnequalList.Add (key);
			}
			return this;
		}

		public BySelector ByOfficeNOT(params string[] key)
		{
			foreach (string s in key) 
			{
				ByOfficeElem.UnequalList.Add (key);
			}
			return this;
		}

		public BySelector ByFactionNOT(params string[] key)
		{
			foreach (string s in key) 
			{
				ByFactionElem.UnequalList.Add (key);
			}
				
			return this;
		}

		public bool empty
		{
			get 
			{
				return (ByPersonElem.empty && ByOfficeElem.empty && ByFactionElem.empty);
			}
		}

		public SelectElem persons
		{
			get 
			{
				return ByPersonElem;
			}
		}

		public SelectElem offices
		{
			get 
			{
				return ByOfficeElem;
			}
		}

		public SelectElem factions
		{
			get 
			{
				return ByFactionElem;
			}
		}

		private SelectElem ByPersonElem = new SelectElem();
		private SelectElem ByOfficeElem = new SelectElem();
		private SelectElem ByFactionElem = new SelectElem();
	}

	public class SelectElem
	{
		public bool empty
		{
			get 
			{
				return (EqualList.Count == 0 && UnequalList.Count == 0);
			}
		}

		public List<string> EqualList = new List<string>();
		public List<string> UnequalList = new List<string>();
	}
}


