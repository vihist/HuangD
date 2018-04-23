using System;
using System.Collections;
using System.Collections.Generic;

using XLua;
using UnityEngine;
using System.Linq.Expressions;

public partial class MyGame
{
	[LuaCallCSharp]
	public class Selector
	{
        private Selector(){ }

        public  static BySelector ByPerson(params string[] key)
		{
			return new BySelector ().ByPerson(key);
		}

		public  static BySelector ByOffice(params string[] key)
		{
			return new BySelector ().ByOffice(key);
		}

		public  static BySelector ByFaction(params string[] key)
		{
			return new BySelector ().ByFaction(key);
		}

		public  static BySelector ByPersonNOT(params string[] key)
		{
			return new BySelector ().ByPersonNOT(key);
		}

		public  static BySelector ByOfficeNOT(params string[] key)
		{
			return new BySelector ().ByOfficeNOT(key);
		}

		public  static BySelector ByFactionNOT(params string[] key)
		{
			return new BySelector ().ByFactionNOT(key);
		}
    }

	[LuaCallCSharp]
	public class BySelector
	{
        public BySelector()
        {
            ByPerson = this._ByPerson;
            ByOffice = this._ByOffice;
            ByFaction = this._ByFaction;

            ByPersonNOT = this._ByPersonNOT;
            ByOfficeNOT = this._ByOfficeNOT;
            ByFactionNOT = this._ByFactionNOT;
        }

        public delegate BySelector DelegateBySelector(params string[] y);

        public DelegateBySelector ByPerson;
        public DelegateBySelector ByOffice;
        public DelegateBySelector ByFaction;

        public DelegateBySelector ByPersonNOT;
        public DelegateBySelector ByOfficeNOT;
        public DelegateBySelector ByFactionNOT;

        public BySelector _ByPerson(params string[] key)
		{
            if(ByPersonElem.EqualList.Count != 0)
            {
                throw new ArgumentException("by person seletor already have value!");
            }

			ByPersonElem.EqualList.AddRange (key);

			return this;
		}

		public BySelector _ByOffice(params string[] key)
		{
            if (ByOfficeElem.EqualList.Count != 0)
            {
                throw new ArgumentException("by office seletor already have value!");
            }

            switch (key[0])
            {
                case "JQX":
                    ByOfficeElem.EqualList.AddRange(new List<string>{"JQ1", "JQ2", "JQ3", "JQ4", "JQ5", "JQ6", "JQ7", "JQ8", "JQ9"});
                    break;

                case "CSX":
                    foreach(var eZhouj in Enum.GetValues(typeof(Zhouj.ENUM_ZHOUJ)))
                    {
                        ByOfficeElem.EqualList.Add(eZhouj.ToString() + "CS");
                    }
                    break;

                case "SGX":
                    ByOfficeElem.EqualList.AddRange(new List<string>{"SG1", "SG2", "SG3"});
                    break;

                default:
                    ByOfficeElem.EqualList.AddRange(key);
                    break;
            }

            return this;
		}

		public BySelector _ByFaction(params string[] key)
		{
            if (ByFactionElem.EqualList.Count != 0)
            {
                throw new ArgumentException("by office factor already have value!");
            }

			ByFactionElem.EqualList.AddRange(key);
			return this;
		}

		public BySelector _ByPersonNOT(params string[] key)
		{
            if (ByPersonElem.UnequalList.Count != 0)
            {
                throw new ArgumentException("by NOT person factor already have value!");
            }


			ByPersonElem.UnequalList.AddRange(key);
			return this;
		}

		public BySelector _ByOfficeNOT(params string[] key)
		{
            if (ByOfficeElem.UnequalList.Count != 0)
            {
                throw new ArgumentException("by NOT office factor already have value!");
            }


			ByOfficeElem.UnequalList.AddRange(key);
			return this;
		}

		public BySelector _ByFactionNOT(params string[] key)
		{
            if (ByFactionElem.UnequalList.Count != 0)
            {
                throw new ArgumentException("by NOT faction factor already have value!");
            }


            ByFactionElem.UnequalList.AddRange(key);	
			return this;
		}

        public override string ToString()
        {
            return String.Format("ByPersonElem:[{0}], ByOfficeElem;[{1}], ByFactionElem:[{2}]", ByPersonElem.ToString(), ByOfficeElem.ToString(), ByFactionElem.ToString()); 
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

        public bool needNull
        {
            get 
            {
                return EqualList.Contains("");
            }
        }

        public Func<T, bool>  Complie<T>()
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var memberName = Expression.PropertyOrField(parameter, "name");

            BinaryExpression exprEqual = null;
            foreach (string name in EqualList)
            {
                var exprnew = Expression.Equal(memberName, Expression.Constant(name));
                if (exprEqual == null)
                {
                    exprEqual = exprnew;
                }
                else
                {
                    exprEqual = Expression.Or(exprEqual, exprnew);
                }
            }

            BinaryExpression exprUnequal = null;
            foreach (string name in UnequalList)
            {
                var exprnew = Expression.NotEqual(memberName, Expression.Constant(name));
                if(exprUnequal == null)
                {
                    exprUnequal = exprnew;
                }
                else
                {
                    exprUnequal = Expression.Or(exprUnequal, exprnew);
                }
            }

            Expression<Func<T, Boolean>> lamda = null;
            if (exprEqual != null && exprUnequal != null)
            {
                lamda = Expression.Lambda<Func<T, Boolean>>(Expression.And(exprEqual, exprUnequal), parameter);
            }
            else if(exprEqual != null)
            {
                lamda = Expression.Lambda<Func<T, Boolean>>(exprEqual, parameter);
            }
            else if(exprUnequal != null)
            {
                lamda = Expression.Lambda<Func<T, Boolean>>(exprUnequal, parameter);
            }
            else
            {
                throw new ArgumentException("exprEqual and exprUnequal is null!");
            }

            Debug.Log("selector:" + this.ToString());
            Debug.Log("lamda:" + lamda.ToString());

            return lamda.Compile();
        }

        public override string ToString()
        {
            string result = "";
            string strEqual = "";
            foreach (string name in EqualList)
            {
                strEqual += name + ",";
            }

            strEqual.TrimEnd(',');

            result = String.Format("equal:({0})", strEqual);

            string strUnequal = "";
            foreach (string name in UnequalList)
            {
                strUnequal += name + ",";
            }

            strUnequal.TrimEnd(',');

            return result + String.Format("unequal:({0})", strUnequal);
        }


        public List<string> EqualList = new List<string>();
		public List<string> UnequalList = new List<string>();
	}
}


