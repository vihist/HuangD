using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        title = GameObject.Find("Canvas/Panel/title").GetComponent<Text>();
        history   = GameObject.Find("Canvas/Panel/history/value").GetComponent<Text>();

        title.text = MyGame.Inst.dynastyName + "史";
        history.text = MyGame.Inst.HistoryGet();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void OnConfrim()
    {
        SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);
    }

    private Text title;
    private Text history;
}
