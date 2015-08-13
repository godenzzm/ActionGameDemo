using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ITriggerEventInterface
{
	void TriggerStayOther (GameObject selfObject, Collider collider);
	void TriggerEnterOther (GameObject selfObject, Collider collider);
	void TriggerExitOther (GameObject selfObject, Collider collider);
}

public class TriggerEventListener 
{
	List<TriggerEventObject> m_EventList;


	static TriggerEventListener m_Instance;
	public static TriggerEventListener GetInstance()
	{
		if (null == m_Instance) 
		{
			//m_Instance = GameObject.FindObjectOfType (typeof (TriggerEventListener)) as TriggerEventListener;		
			m_Instance = new TriggerEventListener ();
		}
		
		return m_Instance;
	}

	TriggerEventListener ()
	{
		m_EventList = new List<TriggerEventObject> ();
	}

	public bool AddToTriggerEventListener (TriggerEventObject sender)
	{
		bool ret = true;

		if (m_EventList.Contains (sender)) return false;

		m_EventList.Add (sender);

		return ret;
	}

	public void NotifyEnterTriggerEventListener (TriggerEventObject sender, Collider other)
	{
		if (m_EventList.Contains (sender)) 
		{
			//
			TriggerEventDispatch.GetIntance ().DispatchTriggerEnterEvent (sender, other);
		}
	}

	public void NotifyStayTriggerEventListener (TriggerEventObject sender, Collider other)
	{
		if (m_EventList.Contains (sender)) 
		{
			//
			TriggerEventDispatch.GetIntance ().DispatchTriggerStayEvent (sender, other);
		}
	}

	public void NotifyExitTriggerEventListener (TriggerEventObject sender, Collider other)
	{
		if (m_EventList.Contains (sender)) 
		{
			//
			TriggerEventDispatch.GetIntance ().DispatchTriggerExitEvent (sender, other);
		}
	}
}
