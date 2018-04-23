using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using WDT;

public class TianXScene: MonoBehaviour 
{
    void Awake()
    {
        //wdataTable = GameObject.Find("Canvas/Panel/DataTablesSimple").GetComponent<WDataTable>();


    }

    // Use this for initialization
    void Start () 
    {
        List<string> colums = new List<string>{ "aaa", "bbb", "ccc"};
        List<IList<object>> data = new List<IList<object>>();
        data.Add(new List<object>{ 1, 2, 3 });
        wdataTable.InitDataTable(data, colums);

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void OnButton(Button btn)
    {

    }

    public void OnDetailButton()
    {

    }

    public void OnCsButton()
    {

    }

    public WDataTable wdataTable;
    //private List<Text> _listBtnText;
}

