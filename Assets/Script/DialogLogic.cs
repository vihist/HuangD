using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WDT;

using XLua;

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



    public delegate object DelegateProcess (string op, out string nextParam);
    public delegate string DelegateHistory ();

    public static GameObject newDialogInstace(string title, object content, KeyValuePair<string, string>[] options, DelegateProcess delegateProcess, DelegateHistory delegateHistory)
	{
		GameObject UIRoot = GameObject.Find("Canvas").gameObject;
        GameObject dialog = null;
        if (content.GetType() == typeof(string))
        {
            dialog = Instantiate(Resources.Load(string.Format("Prefabs/Dialog/Dialog_{0}Btn", options.Length)), UIRoot.transform) as GameObject;

            Text txTitle = dialog.transform.Find("title").GetComponent<Text>();
            Text txContent = dialog.transform.Find("content").GetComponent<Text>();

            txTitle.text = title;
            txContent.text = (string)content;
        }
        else
        {
            dialog = Instantiate(Resources.Load("Prefabs/Dialog/DataTablesSimple"), UIRoot.transform) as GameObject;

            WDataTable wdataTable = dialog.GetComponent<WDataTable>();
            List<List<string>> raw = (List<List<string>>)content;

            IList<IList<object>> data = new List<IList<object>>();
            for (int i = 1; i < raw.Count; i++)
            {
                List<object> elem = new List<object>();
                foreach(string str in raw[i])
                {
                    elem.Add(str);
                }

                data.Add(elem);
            }

            wdataTable.InitDataTable(data, raw[0]);
        }

		dialog.transform.SetAsFirstSibling();

        for(int i=0; i<options.Length; i++)
        {
			Button optionTran = dialog.transform.Find (string.Format ("option{0}", i + 1)).GetComponent<Button> ();
           
			Text txop = optionTran.transform.Find("Text").GetComponent<Text>();
			txop.text = options[i].Value;

            dialog.GetComponent<DialogLogic> ().optionName2KeyDict.Add(optionTran.name, options[i].Key);
        }

		dialog.GetComponent<DialogLogic> ().delegateProcess = delegateProcess;
        dialog.GetComponent<DialogLogic> ().delegateHistory = delegateHistory;
        dialog.GetComponent<DialogLogic> ().title = title;
        return dialog;
	}

    public void OnEventButton(Button btn)
    {
        Debug.Log("Event:" + title + ", OnButton:" + btn.name);
       
        object ret = delegateProcess (optionName2KeyDict[btn.name], out _nexparam);
        _result = "";

        if (ret == null)
        {
            return;
        }

        if (ret.GetType () == typeof(string)) 
        {
            _result = (string)ret;
        } 
        else if(ret.GetType () == typeof(LuaTable))
        {
            _table = new List<List<string>>();

            List<object> list = ((XLua.LuaTable)ret).Cast<List<object>>();
            foreach(object o in list)
            {
                _table.Add(((XLua.LuaTable)o).Cast<List<string>>());
            }
        }
                    
        _historyrecord = delegateHistory();
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

    public string historyrecord
    {
        get
        {
            return _historyrecord;
        }
    }

    public List<List<string>> table
    {
        get
        {
            return _table;
        }
    }

	private DelegateProcess delegateProcess;
    private DelegateHistory delegateHistory;

    private string title;
	private string _result;
	private string _nexparam;
    private string _historyrecord;
    private Dictionary<string, string> optionName2KeyDict = new Dictionary<string, string>();
    private List<List<string>> _table;
}
