using UnityEngine;
using System.Collections;

public class TrackMovement : MonoBehaviour
{

	[SerializeField] protected Vector3 									m_Dir;
	[SerializeField] protected float									m_Dis;
	[SerializeField] protected int										m_Frame;

	[SerializeField] protected GameObject 								m_Target;

	protected string													m_Id;
	protected int														m_FrameCount;
	protected bool														m_Switch;

	protected void Awake ()
	{
		m_Id = "";
		m_Dir = Vector3.zero;
		m_Dis = 1;
		m_Frame = 60;
		
		m_FrameCount = 0;
		m_Switch = false;
	}

	#region get set
	public GameObject Target
	{
		get
		{
			return m_Target;
		}
		set
		{
			m_Target = value;
		}
	}

	public string Id
	{
		get
		{
			return m_Id;
		}
		set
		{
			m_Id = value;
		}
	}
	public bool Switch
	{
		get 
		{
			return m_Switch;
		}
		set 
		{
			m_Switch = value;
		}
	}
	public Vector3 Direction 
	{
		get
		{
			return m_Dir;
		}
		set
		{
			m_Dir = value;
		}
	}
	
	public float Distance
	{
		get
		{
			return m_Dis;
		}
		set
		{
			m_Dis = value;
		}
	}
	#endregion
}
