using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLogic : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		_result = null;
			
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

	public delegate string DelegateProcess (string op);

	public static GameObject newDialogInstace(string title, string content, string[] options, DelegateProcess delegateProcess)
	{
		GameObject UIRoot = GameObject.Find("Canvas").gameObject;
		GameObject dialog = Instantiate(Resources.Load(string.Format("Prefabs/Dialog/Dialog_{0}Btn", options.Length)), UIRoot.transform) as GameObject;
		dialog.transform.SetAsFirstSibling();

        Text txTitle = dialog.transform.Find("title").GetComponent<Text>();
        Text txContent = dialog.transform.Find("content").GetComponent<Text>();

        txTitle.text = title;
        txContent.text = title;

        for(int i=0; i<options.Length; i++)
        {
            Text txop = dialog.transform.Find(string.Format("option{0}/Text", i+1)).GetComponent<Text>();
            txop.text = options[i];
        }

		dialog.GetComponent<DialogLogic> ().delegateProcess = delegateProcess;

        return dialog;
	}

    public void OnEventButton(Button btn)
    {
        Debug.Log("OnEventButton:" + btn.name);
        
		string name = btn.name.Replace ("option", "op");
		_result = delegateProcess (name);
		if (_result == null) 
		{
			_result = "";
		}

		Debug.Log("result:" + result);
    }

	public string result
	{
		get
		{
			return _result;
		}
	}

	private DelegateProcess delegateProcess;
	private string _result;
}
