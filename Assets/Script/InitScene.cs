using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour 
{

	void Awake()
	{
		GameObject UIRoot = GameObject.Find("Canvas/Panel");

		inDynastyName = UIRoot.transform.Find("DynastyName/InputField").GetComponent<InputField>();
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
		MyGame.Inst.Initialize(inPersonName.text, inYearName.text, inDynastyName.text);
		SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
	}

	public void OnBtnCannel()
	{
		SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Single);
	}

	private void RefreshRandomData()
	{
		inDynastyName.text = StreamManager.dynastyName.GetRandom ();
		inYearName.text = StreamManager.yearName.GetRandom();
		inPersonName.text = StreamManager.personName.GetRandomMale();
	}

	InputField inDynastyName;
	InputField inYearName;
	InputField inPersonName;
}
