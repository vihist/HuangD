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



	public delegate string DelegateProcess (string op, out string nextParam);

	public static GameObject newDialogInstace(string title, string content, KeyValuePair<bool, string>[] options, DelegateProcess delegateProcess)
	{
		GameObject UIRoot = GameObject.Find("Canvas").gameObject;
		GameObject dialog = Instantiate(Resources.Load(string.Format("Prefabs/Dialog/Dialog_{0}Btn", options.Length)), UIRoot.transform) as GameObject;
		dialog.transform.SetAsFirstSibling();

        Text txTitle = dialog.transform.Find("title").GetComponent<Text>();
        Text txContent = dialog.transform.Find("content").GetComponent<Text>();

        txTitle.text = title;
		txContent.text = content;

        for(int i=0; i<options.Length; i++)
        {
			Button optionTran = dialog.transform.Find (string.Format ("option{0}", i + 1)).GetComponent<Button> ();
			if (!options [i].Key) 
			{
				optionTran.enabled = false;
			}

			Text txop = optionTran.transform.Find("Text").GetComponent<Text>();
			txop.text = options[i].Value;
        }

		dialog.GetComponent<DialogLogic> ().delegateProcess = delegateProcess;
        dialog.GetComponent<DialogLogic> ().title = title;
        return dialog;
	}

    public void OnEventButton(Button btn)
    {
        Debug.Log("Event:" + title + ", OnButton:" + btn.name);
        
		string name = btn.name.Replace ("option", "op");

		_result = delegateProcess (name, out _nexparam);
		if (_result == null) 
		{
			_result = "";
		}
    }

	public string result
	{
		get
		{
			return _result;
		}
	}

	public string nexparam
	{
		get
		{
			return _nexparam;
		}
	}

	private DelegateProcess delegateProcess;
    private string title;
	private string _result;
	private string _nexparam;
}
