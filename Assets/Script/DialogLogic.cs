using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLogic : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		Debug.Log("DialogLogic start");
		_isChecked = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
		

	public static GameObject newDialogInstace()
	{
		GameObject UIRoot = GameObject.Find("Canvas").gameObject;
		GameObject dialog = Instantiate(Resources.Load("Prefabs/Dialog/Dialog_1Btn"), UIRoot.transform) as GameObject;
		dialog.transform.SetAsFirstSibling();

		//_isChecked = false;
		return dialog;
	}

    public void OnEventButton(Button btn)
    {
        Debug.Log("OnEventButton:" + btn.name);
        _isChecked = true;
    }

	public bool isChecked()
	{
		return _isChecked;
	}

	bool _isChecked = false;
}
