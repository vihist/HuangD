using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour 
{

	void Awake()
	{
		GameObject UIRoot = GameObject.Find("Canvas/Panel");

		inPeriodName = UIRoot.transform.Find("PeriodName/InputField").GetComponent<InputField>();
		inYearName = UIRoot.transform.Find("YearName/InputField").GetComponent<InputField>();
		inPersonName = UIRoot.transform.Find("PersonName/InputField").GetComponent<InputField>();
	}

	// Use this for initialization
	void Start () 
	{
		RefreshRandomData ();
    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    public void OnBtnRandom()
    {
		RefreshRandomData();
    }

	public void OnBtnConfirm()
	{
		MyGame.Inst.Initialize(inPersonName.text, inYearName.text, inPeriodName.text);
		SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
	}

	public void OnBtnCannel()
	{
		SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);
	}

	private void RefreshRandomData()
	{
		inPeriodName.text = StreamManager.periodName.GetRandom ();
		inYearName.text = StreamManager.yearName.GetRandom();
		inPersonName.text = StreamManager.personName.GetRandom();
	}

	InputField inPeriodName;
	InputField inYearName;
	InputField inPersonName;
}
