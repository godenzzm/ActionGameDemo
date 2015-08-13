using UnityEngine;
using System.Collections;

public class TriggerEventObject : MonoBehaviour 
{
	public string DelegateKey;
	ITriggerEventInterface m_Owner = null;

	//public delegate void OnTriggerEnterDelegate(Collider other);
	//public OnTriggerEnterDelegate OnEnter;

	void Awake ()
	{
		TriggerEventListener.GetInstance ().AddToTriggerEventListener (this);
	}

	void OnTriggerEnter (Collider other) 
	{
		TriggerEventListener.GetInstance ().NotifyEnterTriggerEventListener (this, other);
	}

	void OnTriggerStay (Collider other)
	{

		TriggerEventListener.GetInstance ().NotifyStayTriggerEventListener (this, other);
	}

	void OnTriggerExit (Collider other)
	{
		TriggerEventListener.GetInstance ().NotifyExitTriggerEventListener (this, other);
	}

	public ITriggerEventInterface Owner
	{
		get
		{
			return m_Owner;
		}

		set
		{
			m_Owner = value;
		}
	}
}
