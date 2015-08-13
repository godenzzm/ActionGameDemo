using UnityEngine;
using System.Collections;

public class TriggerEventDispatch 
{
	static TriggerEventDispatch m_Intance;

	public static TriggerEventDispatch GetIntance ()
	{
		if (m_Intance == null) 
		{
			m_Intance = new TriggerEventDispatch ();
		}

		return m_Intance;
	}

	public void DispatchTriggerEnterEvent (TriggerEventObject sender, Collider other)
	{
		if (null != sender.Owner) 
		{
			sender.Owner.TriggerEnterOther (sender.gameObject, other);
		}
	}

	public void DispatchTriggerStayEvent (TriggerEventObject sender, Collider other)
	{
		if (null != sender.Owner) 
		{
			sender.Owner.TriggerStayOther (sender.gameObject, other);
		}
	}

	public void DispatchTriggerExitEvent (TriggerEventObject sender, Collider other)
	{
		if (null != sender.Owner) 
		{
			sender.Owner.TriggerExitOther (sender.gameObject, other);
		}
	}
}
