﻿using System.Collections;
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

		foreach(GMEvent eventobj in eventManager.GetEvent())
		{
			yield return new WaitForSeconds (m_fWaitTime/eventManager.GetEventCout());

			dialog = DialogLogic.newDialogInstace(eventobj.title, eventobj.content, eventobj.options);

			yield return new WaitUntil (isChecked);
		
			Destroy (dialog);
		}
	}

	private bool isChecked()
	{
		return dialog.GetComponent<DialogLogic> ().isChecked ();
	}

	private float m_fWaitTime;
	private EventManager eventManager;
	private GameObject dialog;
}