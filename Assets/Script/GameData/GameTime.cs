using System;
using UnityEngine;

using XLua;


public partial class MyGame
{
    [Serializable, LuaCallCSharp]
	public class GameTime
	{
		public GameTime()
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