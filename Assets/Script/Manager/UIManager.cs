using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{

	private static UIManager m_Instance = null;
	
	public static UIManager Instance 
	{
		get 
		{
			if (null == m_Instance)
			{
				GameObject go = GameObject.FindObjectOfType (typeof(UIManager)) as GameObject;
				
				m_Instance = go.GetComponent<UIManager> ();
			}
			
			return m_Instance;
		}
	}

	public Button[] GetButtons ()
	{
		return GetComponentsInChildren<Button> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
