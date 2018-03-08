using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventLogic : MonoBehaviour 
{
	void Awake()
	{
		eventManager = new EventManager ();

		m_fWaitTime = 3.0F;
		StartCoroutine(OnTimer());  
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    private IEnumerator OnTimer()
	{
		int eventCount = eventManager.GetEventCout ();
		if(eventCount == 0)
		{
			yield return new WaitForSeconds (m_fWaitTime);
		}

		foreach(Event eventobj in eventManager.GetEvent())
		{
			yield return new WaitForSeconds (m_fWaitTime/eventManager.GetEventCout());

            GameObject UIRoot = GameObject.Find("Canvas").gameObject;
            GameObject dialog = Instantiate(Resources.Load("Prefabs/Dialog/Dialog_1Btn"), UIRoot.transform) as GameObject;
            dialog.transform.SetAsFirstSibling();

            Text txTitle = dialog.transform.Find("title").GetComponent<Text>();
            txTitle.text = eventobj.title;

            Text txop = dialog.transform.Find("option1/Text").GetComponent<Text>();
            txop.text = eventobj.title;

            Debug.Log ("do event:" + eventobj.title);

			yield return new WaitUntil (eventobj.isChecked);
		}
	}
		
	private float m_fWaitTime;
	private EventManager eventManager;
}