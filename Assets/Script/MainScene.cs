using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    void Awake()
    {
        txtEmpName = GameObject.Find("Canvas/PanelTop/BtnEmp/name").GetComponent<Text>();
        btnEmpDetail = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail");
    }

    void Start()
    {
        btnEmpDetail.SetActive(false);
        txtEmpName.text = MyGame.Inst.strEmpName;

        SceneManager.LoadSceneAsync("TianXScene", LoadSceneMode.Additive);
    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    public void OnSelectScene(Toggle toggle)
    {
        if(!toggle.isOn)
        {
            return;
        }

        OnSelectScene(toggle.name);
    }

    public void onEmperorButtonClick()
    {
        if (btnEmpDetail.activeSelf)
        {
            btnEmpDetail.SetActive(false);
        }
        else
        {
            btnEmpDetail.SetActive(true);
        }
    }

    private void OnSelectScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    private float m_fWaitTime;
    GameObject btnEmpDetail;
    Text txtEmpName;
}
