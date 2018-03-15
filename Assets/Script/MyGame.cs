using System;
using System.Text;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class MyGame
{
    public static MyGame Inst = new MyGame();

	public void Initialize(string strEmpName, string strYearName, string strDynastyName)
    {
        empName  = strEmpName;
        empAge   = Tools.Probability.GetRandomNum (16, 40);
		empHeath = Tools.Probability.GetRandomNum (50, 90);

        Stability = Tools.Probability.GetRandomNum(60, 90);
        Economy   = Tools.Probability.GetRandomNum(60, 90);
        Military  = Tools.Probability.GetRandomNum(60, 90);

        yearName = strYearName;
		dynastyName = strDynastyName;
        date = new GameDateTime();
    }

    private MyGame()
    {
    }

    private string GetText(string str)
    {

#if UNITY_EDITOR_OSX
		if(Tools.StringT.isChinese(str))
		{
			byte[] bytes = Encoding.Unicode.GetBytes(str);
			return BitConverter.ToString (bytes, 0).Replace ("-", string.Empty);
		}
#endif
		return str;
    }

    public string time
    {
        get
        {
			return dynastyName + " " + yearName + date.ToString();
        }
    }

	public string empName;
	public int    empAge;
	public int    empHeath;


	public string dynastyName;
    public int    Stability;
    public int    Economy;
    public int    Military;

    private string yearName;
    private GameDateTime date;
}



[Serializable]
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
