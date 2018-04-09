using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartScene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

        /*		string json = File.ReadAllText (Application.persistentDataPath + "/game.env");
                if (json == null) 
                {
                    Global.SetEnv (new GameEnv());
                }
                else
                {
                    Global.SetEnv (JsonUtility.FromJson<GameEnv>(json));
                }
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonNew()
    {
		SceneManager.LoadSceneAsync("InitScene");

        // GameFrame.GetInstance().OnNew();

        //GameObject UIRoot = GameObject.Find("Canvas");
        //GameObject dialog = Instantiate(Resources.Load("EasyMenu/_Prefabs/Dialog_Init"), UIRoot.transform) as GameObject;

        //Button btnSave = dialog.transform.Find("Random").GetComponent<Button>();
        //btnSave.onClick.AddListener(delegate ()
        //{


        //    InputField inputCountryName = dialog.transform.Find("CountryName").Find("InputField").GetComponent<InputField>();
        //    inputCountryName.text = GetRandomCounryName();

        //    InputField inputYearName = dialog.transform.Find("YearName").Find("InputField").GetComponent<InputField>();
        //    inputYearName.text = GetRandomYearName();

        //    InputField inputFamilyName = dialog.transform.Find("FamilyName").Find("InputField").GetComponent<InputField>();
        //    inputFamilyName.text = Persion.GetFamilyName();

        //    InputField inputSelfName = dialog.transform.Find("SelfName").Find("InputField").GetComponent<InputField>();
        //    inputSelfName.text = Persion.GetSelfName();

        //});

        //Button btnQuit = dialog.transform.Find("Confirm").GetComponent<Button>();
        //btnQuit.onClick.AddListener(delegate ()
        //{
        //    String countryName = dialog.transform.Find("CountryName").Find("InputField").GetComponent<InputField>().text;
        //    String yearName = dialog.transform.Find("YearName").Find("InputField").GetComponent<InputField>().text;
        //    String familyName = dialog.transform.Find("FamilyName").Find("InputField").GetComponent<InputField>().text;
        //    String selfName = dialog.transform.Find("SelfName").Find("InputField").GetComponent<InputField>().text;

        //    Destroy(dialog);

        //    GameFrame.GetInstance().OnNew(countryName, yearName, familyName, selfName);
        //});
    }

    public void OnButtonQuit()
    {
        GameFrame.GetInstance().OnQuit();
    }

	public void OnButtonSave()
	{
		GameFrame.GetInstance().OnSave();
	}

    public void OnButtonLoad()
    {
        GameFrame.GetInstance().OnLoad();
    }

    //private String GetRandomCounryName()
    //{
    //    int rowCount = Tools.Probability.GetRandomNum(1, Cvs.Guohao.RowLength() - 1);
    //    return Cvs.Guohao.Get(rowCount.ToString());
    //}

    //private String GetRandomYearName()
    //{
    //    int rowCount1 = Tools.Probability.GetRandomNum(1, Cvs.Nianhao1.RowLength() - 1);
    //    int rowCount2 = Tools.Probability.GetRandomNum(1, Cvs.Nianhao2.RowLength() - 1);
    //    return Cvs.Nianhao1.Get(rowCount1.ToString()) + Cvs.Nianhao2.Get(rowCount2.ToString());
    //}

}
