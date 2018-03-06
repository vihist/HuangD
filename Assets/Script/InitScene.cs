using UnityEngine;
using UnityEngine.UI;

public class InitScene : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{

    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    public void OnBtnRandom()
    {
        Transform UIRoot = GameObject.Find("Canvas").transform.Find("Panel");

        InputField inPeriodName = UIRoot.transform.Find("PeriodName").Find("InputField").GetComponent<InputField>();
		inPeriodName.text = StreamManager.periodName.GetRandom ();

        InputField inYearName = UIRoot.transform.Find("YearName").Find("InputField").GetComponent<InputField>();
		inYearName.text = StreamManager.yearName.GetRandom();

        InputField inPersonName = UIRoot.transform.Find("PersonName").Find("InputField").GetComponent<InputField>();
		inPersonName.text = StreamManager.personName.GetRandom();
    }
}
