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
            Transform sub = currTransform.GetChild(i);

            Debug.Log(sub.name);
            UIDict.Add(sub.name, sub);
        }
	}

	void RefreshOffice()
	{
        Transform obj = UIDict ["SG1"];
		obj.Find ("value").GetComponent<Text> ().text = MyGame.Inst.relPersonAndOffice.GetBySecond ("SG1");
	}

	private Dictionary<string, Transform> UIDict = new Dictionary<string, Transform>();
}
