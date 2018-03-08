﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
				
			Debug.Log ("do event:" + eventobj.getName());

			yield return new WaitUntil (eventobj.isChecked);
		}
	}
		
	private float m_fWaitTime;
	private EventManager eventManager;
}