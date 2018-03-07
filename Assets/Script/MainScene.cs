using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    void Awake()
    {
        Stability = GameObject.Find("Canvas/PanelTop/Stability/value").GetComponent<Text>();
        Economy   = GameObject.Find("Canvas/PanelTop/Economy/value").GetComponent<Text>();
        Military  = GameObject.Find("Canvas/PanelTop/Military/value").GetComponent<Text>();
        txtTime   = GameObject.Find("Canvas/PanelTop/Time").GetComponent<Text>();

        txtEmpName   = GameObject.Find("Canvas/PanelTop/BtnEmp/name/value").GetComponent<Text>();
		txtEmpAge    = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail/age/value").GetComponent<Text>();
		sldEmpHeath  = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail/heath/slider").GetComponent<Slider>();

        btnEmpDetail = GameObject.Find("Canvas/PanelTop/BtnEmp/BtnEmpDetail");
    }

    void Start()
    {
		btnEmpDetail.SetActive(false);
        SceneManager.LoadSceneAsync("TianXScene", LoadSceneMode.Additive);

        onRefresh();
    }
	
	// Update is called once per frame
	void Update () 
	{
        onRefresh();
    }

    public void OnSelectScene(Toggle toggle)
    {
        if(!toggle.isOn)
        {
            return;
        }

        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1).name);
        SceneManager.LoadSceneAsync(toggle.name, LoadSceneMode.Additive);
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

    private void onRefresh()
    {
        Stability.text = MyGame.Inst.Stability.ToString();
        Economy.text   = MyGame.Inst.Economy.ToString();
        Military.text  = MyGame.Inst.Military.ToString();
        txtTime.text   = MyGame.Inst.time;

        txtEmpName.text = MyGame.Inst.empName;
        txtEmpAge.text = MyGame.Inst.empAge.ToString();
        sldEmpHeath.value = MyGame.Inst.empHeath;
    }

    private float m_fWaitTime;

    GameObject btnEmpDetail;

    Text Stability;
    Text Economy;
    Text Military;

    Text txtEmpName;
	Text txtEmpAge;
	Text txtTime;
	Slider sldEmpHeath;
}
