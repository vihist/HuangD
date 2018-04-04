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

	private List<ChaoChenUI> lstChaoc = new List<ChaoChenUI>();
}

class ChaoChenUI
{
	public ChaoChenUI(Transform tran)
	{
		UIKey = tran.name;

		officeName = tran.Find ("Text").GetComponent<Text> ();
		personName = tran.Find ("value").GetComponent<Text> ();
		personScore = tran.Find("score/value").GetComponent<Text> ();
		factionName = tran.Find("faction/value").GetComponent<Text> ();

		office = MyGame.Inst.officeManager.GetByName (UIKey);
		officeName.text = office.name;

		tran.Find ("reserve1").gameObject.SetActive (false);
		tran.Find ("reserve2").gameObject.SetActive (false); 

	}

	public void Refresh()
	{
		MyGame.Person p  = MyGame.Inst.relOffice2Person.GetPerson (office);
		if (p == null) 
		{
			return;
		}

		MyGame.Faction f = MyGame.Inst.relFaction2Person. GetFaction(p);

		personName.text = p.name;
		personScore.text = p.score.ToString();
		factionName.text = f.name;
	}

	string UIKey;

	Text officeName;
	Text personName;
	Text personScore;
	Text factionName;

	MyGame.Office office;
}