using UnityEngine;
using System.Collections;

public class ColliderManager 
{
	private string																				m_Path;

	private static ColliderManager 																m_Instance = null;
	public static ColliderManager Instance
	{
		get
		{
			if (null == m_Instance)
			{
				m_Instance = new ColliderManager ();
			}
			return m_Instance;
		}
	}

	public ColliderManager ()
	{
		Init ();	
	}

	private void Init ()
	{
		m_Path = "Collider/";
	}

	public GameObject SpawnCollider (string name)
	{
		GameObject ret = null;

		Object prefab = Resources.Load (m_Path + name) as Object;

		if (null != prefab) 
		{
			ret = GameObject.Instantiate (prefab) as GameObject;
		}

		return ret;
	}
}
