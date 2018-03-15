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
		
	}

	private void AddOfficeToDict(string path)
	{
		GameObject[] gmArray = GameObject.Find (path).GetComponentsInChildren<GameObject> ();
		foreach(GameObject obj in gmArray)
		{
			UIDict.Add (obj.name, obj);
		}
	}

	void RefreshOffice()
	{
		GameObject obj = UIDict ["SG1"];
		obj.transform.Find ("value").GetComponent<Text> ().text = MyGame.Inst.relPersonAndOffice.GetBySecond ("SG1");
	}

	private Dictionary<string, GameObject> UIDict = new Dictionary<string, GameObject>();
}
