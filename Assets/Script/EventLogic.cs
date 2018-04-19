using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WDT;

public class EventLogic : MonoBehaviour 
{
	void Awake()
	{
		eventManager = new EventManager ();

		m_fWaitTime = 1.0F;
		StartCoroutine(OnTimer());  


	}

	// Use this for initialization
	void Start () 
	{
        IList<IList<object>> aaa = new List<IList<object>>();
        aaa.Add(new List<object>{  "1", "2", "3" } );



        testWDataTable.InitDataTable(aaa, new List<string>{"a", "b", "c"});
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
    private IEnumerator OnTimer()
	{
		float costtime = 0.0f;
		foreach(GMEvent eventobj in eventManager.GetEvent ())
        {
			yield return new WaitForSeconds(0.5f);

            dialog = DialogLogic.newDialogInstace(eventobj.title, eventobj.content, eventobj.options, eventobj.SelectOption, eventobj.Historyrecord);

			yield return new WaitUntil (isChecked);

            string history = dialog.GetComponent<DialogLogic>().historyrecord;
            MyGame.Inst.HistoryRecord(history);

			string key = dialog.GetComponent<DialogLogic> ().result;
			string param = dialog.GetComponent<DialogLogic> ().nexparam;
            List<List<string>> showTable = dialog.GetComponent<DialogLogic>().table;
            			
			Destroy (dialog);

            eventManager.Insert (showTable);
            eventManager.Insert (key, param);

            if (MyGame.Inst.gameEnd)
            {

                yield break;
            }

			costtime += 0.1f;
		}


        yield return new WaitForSeconds(m_fWaitTime - costtime);

        MyGame.Inst.date.Increase();
        StartCoroutine(OnTimer());

	}

	private bool isChecked()
	{
		if (dialog.GetComponent<DialogLogic> ().result == null) 
		{
			return false;
		}

		return true;
	}

    public WDataTable testWDataTable;

    public static bool isEventLogicRun = true;
	private float m_fWaitTime;
	private EventManager eventManager;
    private  GameObject dialog;

}