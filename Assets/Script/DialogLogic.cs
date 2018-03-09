using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLogic : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		_isChecked = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
		

	public static GameObject newDialogInstace(string title, string content, string[] options)
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
