using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.Events;

public class InputManager : MonoBehaviour 
{
	public GameObject															m_Player;
	public IInputInterface														m_InputSource;

	private PlayerController 													m_PlayerController;
	private IAnimator															m_Animator;

	private static InputManager 												m_Instance = null;
	public static InputManager Instance 
	{
		get 
		{
			if (null == m_Instance)
			{
				GameObject go = GameObject.FindObjectOfType (typeof(InputManager)) as GameObject;
				
				m_Instance = go.GetComponent<InputManager> ();
			}
			return m_Instance;
		}
	}

	void Awake ()
	{
		m_Animator = m_Player.GetComponent<IAnimator> ();
		m_PlayerController = m_Player.GetComponent<PlayerController> ();

		m_InputSource = GetComponent<InputJoyStick> ();
		m_InputSource.SetController (m_Animator, m_PlayerController);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (null != m_InputSource) 
		{
			m_InputSource.InputListener ();
		}
	}
}
