using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFrame
{
    public static GameFrame GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new GameFrame();
        }

        return m_Instance;
    }

    public void OnNew()//(String countryName, String yearName, String familyName, String selfName)
    {
        //Global.SetMyGame(new MyGame(countryName, yearName, familyName, selfName));
        //Global.GetGameData().Init();

        SceneManager.LoadSceneAsync("MainScene");
    }

    public void OnSave()
    {
        string strSavePath = GetSavePath();
        Debug.Log(strSavePath);
        if (!Directory.Exists(strSavePath))
        {
            Directory.CreateDirectory(strSavePath);
        }

		string json = JsonUtility.ToJson(MyGame.Inst);
        File.WriteAllText(GetSavePath() + "/game.save", json);
    }

    public void OnLoad()
    {
        string strSavePath = GetSavePath();
        Debug.Log(strSavePath);

        string json = File.ReadAllText(GetSavePath() + "/game.save");
		MyGame.Inst = JsonUtility.FromJson<MyGame> (json);

        //Global.SetMyGame(new MyGame(JsonUtility.FromJson<GameData>(json)));
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void OnEnd()
    {
        SceneManager.LoadSceneAsync("EndScene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnReturn()
    {
        SceneManager.LoadSceneAsync("StartScene");
    }

    private string GetSavePath()
    {
        return Application.persistentDataPath + "/save";
    }

    private static GameFrame m_Instance;
}
