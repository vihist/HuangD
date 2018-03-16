using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ChaoTScene : MonoBehaviour 
{
	void Awake()
	{
		AddOfficeToDict ("Canvas/Panel/SanG");
		AddOfficeToDict ("Canvas/Panel/JiuQ");
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        RefreshOffice();
    }

	private void AddOfficeToDict(string path)
	{
        Transform currTransform = GameObject.Find(path).transform;
        for(int i=0; i< currTransform.childCount; i++)
        {
			lstChaoc.Add (new ChaoChenUI (currTransform.GetChild(i)));
        }
	}

	void RefreshOffice()
	{
		foreach(ChaoChenUI obj in lstChaoc)
		{
			obj.Refresh ();
		}
	}

	private Dictionary<string, Transform> UIDict = new Dictionary<string, Transform>();
	private List<ChaoChenUI> lstChaoc = new List<ChaoChenUI>();
}

class ChaoChenUI
{
	public ChaoChenUI(Transform tran)
	{
		personName = tran.Find ("value").GetComponent<Text> ();
		personScore = tran.Find("score/value").GetComponent<Text> ();

		key = tran.name;
	}

	public void Refresh()
	{
		personName.text = MyGame.Inst.relPersonAndOffice.GetBySecond(key);
	}

	string key;
	Text personName;
	Text personScore;
}