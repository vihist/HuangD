using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HouGScene : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		AddOfficeToDict ("Canvas/Panel");
	}
	
	// Update is called once per frame
	void Update () 
	{
		RefreshOffice ();
	}

	private void AddOfficeToDict(string path)
	{
		Transform currTransform = GameObject.Find(path).transform;
		for(int i=0; i< currTransform.childCount; i++)
		{
			lstChaoc.Add (new HouFeiUI (currTransform.GetChild(i)));
		}
	}

	void RefreshOffice()
	{
		foreach(HouFeiUI obj in lstChaoc)
		{
			obj.Refresh ();
		}
	}

	private List<HouFeiUI> lstChaoc = new List<HouFeiUI>();
}


class HouFeiUI
{
	public HouFeiUI(Transform tran)
	{
		UIKey = tran.name;

		officeName = tran.Find ("Text").GetComponent<Text> ();
		personName = tran.Find ("value").GetComponent<Text> ();
		personScore = tran.Find("score/value").GetComponent<Text> ();
		//factionName = tran.Find("faction/value").GetComponent<Text> ();

		Debug.Log ("Key:" + UIKey);
		office = MyGame.Inst.officeManager.GetByName (UIKey);
		officeName.text = office.name;
		Debug.Log ("Text:" + officeName.text);

		tran.Find ("reserve1").gameObject.SetActive (false);
		tran.Find ("reserve2").gameObject.SetActive (false); 
		tran.Find ("reserve3").gameObject.SetActive (false); 
	}

	public void Refresh()
	{
		MyGame.Person p  = MyGame.Inst.relOffice2Person.GetPerson (office);
		//Faction f = MyGame.Inst.relFaction2Person. GetFaction(p);

		personName.text = p.name;
		personScore.text = p.score.ToString();
		//factionName.text = f.name;
	}

	string UIKey;

	Text officeName;
	Text personName;
	Text personScore;
	//Text factionName;

	MyGame.Office office;
}