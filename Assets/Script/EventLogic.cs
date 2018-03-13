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
		if(eventManager.GetEventCout () == 0)
		{
			yield return new WaitForSeconds (m_fWaitTime);
			eventManager.makeEvent ();
		}

		for (int i = 0; i < eventManager.EventList.Count; i++) 
		{
			yield return new WaitForSeconds (m_fWaitTime/eventManager.GetEventCout ());

			GMEvent eventobj = eventManager.EventList [i];
			dialog = DialogLogic.newDialogInstace(eventobj.title, eventobj.content, eventobj.options, eventobj.SelectOption);

			yield return new WaitUntil (isChecked);

			string key = dialog.GetComponent<DialogLogic> ().result;
			eventManager.Insert (key);

			Destroy (dialog);
		}
	}

	private bool isChecked()
	{
		if (dialog.GetComponent<DialogLogic> ().result == null) 
		{
			return false;
		}

		return true;
	}

	private float m_fWaitTime;
	private EventManager eventManager;
	private GameObject dialog;
}