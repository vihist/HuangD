using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    void Awake()
    {
		txtTime      = GameObject.Find("Canvas/PanelTop/TIME/value").GetComponent<Text>();

        txtEmpName   = GameObject.Find("Canvas/PanelTop/BtnEmp/name/value").GetComponent<Text>();
		txtEmpAge    = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail/age/value").GetComponent<Text>();
		sldEmpHeath  = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail/heath/slider").GetComponent<Slider>();

        btnEmpDetail = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail");
    }

    void Start()
    {
		//txtTime.text = MyGame.Inst.;

        txtEmpName.text = MyGame.Inst.empName;
		txtEmpAge.text = MyGame.Inst.empAge.ToString();
		sldEmpHeath.value = MyGame.Inst.empHeath;

		btnEmpDetail.SetActive(false);

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
	Text txtEmpAge;
	Text txtTime;
	Slider sldEmpHeath;
}
