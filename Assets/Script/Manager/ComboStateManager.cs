using UnityEngine;
using System.Collections;

public class ComboStateManager 
{
	static ComboStateManager 							m_Instance = null;

	float												m_enterTime;
	float												m_currentTime;
	float												m_exitTime;
	float												m_Length;

	AnimatorStateInfo?									m_animatorState;

	public static ComboStateManager Instance
	{
		get
		{
			if (null == m_Instance) 
			{
				m_Instance = new ComboStateManager ();
			}
			return m_Instance;
		}
	}

	private ComboStateManager ()
	{
		m_enterTime = 0;
		m_currentTime = 0;
		m_exitTime = 0;
		m_animatorState = null;
	}

	public float EnterTime 
	{
		get 
		{
			return m_enterTime;
		}
		set
		{
			m_enterTime = value;
		}
	}

	public float CurrentTime 
	{
		get 
		{
			return m_currentTime;
		}
		set
		{
			m_currentTime = value;
		}
	}

	public float ExitTime 
	{
		get 
		{
			return m_exitTime;
		}
		set
		{
			m_exitTime = value;
		}
	}

	public float Length 
	{
		get 
		{
			return m_Length;
		}
		set
		{
			m_Length = value;
		}
	}

	public AnimatorStateInfo? AnimatorState 
	{
		get 
		{
			return m_animatorState;
		}
		set
		{
			m_animatorState = value;
		}
	}
}
