using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

[CSharpCallLua]
public interface PersionName
{
	string family { get; }
	string given { get; }
}

[CSharpCallLua]
public interface YearName
{
	string first { get; }
	string second { get; }
}

public class InitScene : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
        LoadLua();

    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    public void OnBtnRandom()
    {
        Transform UIRoot = GameObject.Find("Canvas").transform.Find("Panel");

        InputField inPeriodName = UIRoot.transform.Find("PeriodName").Find("InputField").GetComponent<InputField>();
        inPeriodName.text = GetRandomPeriodName();

        InputField inYearName = UIRoot.transform.Find("YearName").Find("InputField").GetComponent<InputField>();
        inYearName.text = GetRandomYearName();

        InputField inPersonName = UIRoot.transform.Find("PersonName").Find("InputField").GetComponent<InputField>();
        inPersonName.text = GetRandomPersonName();
    }

    private void LoadLua()
    {
        LuaEnv luaenv = new LuaEnv();
        luaenv.AddLoader(CustomLoaderMethod);

        luaenv.DoString("require 'name'");

        personName = luaenv.Global.Get<PersionName>("person_name");
        yearName = luaenv.Global.Get<YearName>("year_name");
        periodName = luaenv.Global.Get<string>("period_name");
    }

    private byte[] CustomLoaderMethod(ref string fileName)
    {
        //找到指定文件  
        fileName = Application.streamingAssetsPath + "/native/static/" + fileName.Replace('.', '/') + ".lua";
        Debug.Log(fileName);
        if (File.Exists(fileName))
        {
            return File.ReadAllBytes(fileName);
        }
        else
        {
            return null;
        }
    }

    private string GetRandomPeriodName()
    {
        //int index = Tools.Probability.GetRandomNum(0, periodName.Count-1);
        return periodName;
    }

    private string GetRandomYearName()
    {
        //int index1 = Tools.Probability.GetRandomNum(0, yearName.first.Count - 1);
        //int index2 = Tools.Probability.GetRandomNum(0, yearName.second.Count - 1);
        return yearName.first + yearName.second;
    }

    private string GetRandomPersonName()
    {
        //int index1 = Tools.Probability.GetRandomNum(0, personName.family.Count - 1);
        //int index2 = Tools.Probability.GetRandomNum(0, personName.given.Count - 1);
        return personName.family;
    }

    PersionName personName;
    YearName yearName;
    string periodName;
}
