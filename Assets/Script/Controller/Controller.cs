using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
	protected IAnimator																		m_Animator;
	protected AnimatorStateInfo																m_CurrentAnimatorStateInfo;
	protected AnimatorStateInfo																m_AntecedentAnimatorStateInfo;
	protected int																			m_CurrentAnimatorFrame; // the frame of current animator state 
	protected int																			m_AntecedentAnimatorFrame;

	protected int																			m_FrameCount;

	[SerializeField] protected float														m_Speed;
	protected Vector3																		m_MoveDir;
	protected float																			m_TurnSpeed;

	protected bool 																			m_OnTrimToGround; //调整地面和人物接触位置对齐标识
	protected bool																			m_OnJump; //开始跳的标识 
	protected Vector3																		m_GroundPosition; //记录人物和地面接触的位置
	protected float																			m_FallSpeed; //每一帧下落的距离
	protected float																			m_GravityFact; //每一帧加速下落的距离
	protected int																			m_GravityFrameCount;

	protected const float																	m_ConstantAnimationSpeed = 300;
	protected float																			m_AnimationSpeed;

	protected void Awake ()
	{
		m_Animator = null;
		m_CurrentAnimatorFrame = 0;
		m_AntecedentAnimatorFrame = 0;

		m_FrameCount = 0;

		m_TurnSpeed = 15;
		m_MoveDir = transform.forward;

		m_OnTrimToGround = false;
		m_GroundPosition = Vector3.zero;
		m_FallSpeed = 0.005f;
		m_GravityFact = 0.98f;
		m_GravityFrameCount = 0;

		m_AnimationSpeed = m_ConstantAnimationSpeed;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Hit (Controller controller)
	{
		Debug.Log (name + " Hit !!! animator:" + m_Animator);
		if (null != m_Animator) 
		{
			Debug.Log (name + " Hit by " + controller.name);
			m_Animator.SetTriggle (AnimatorConfig.PARAM_TRIGGER_HIT);
		}
	}

	protected void UpdateAnimatorState ()
	{
		if (null == m_Animator) return;
	}

	#region get set
	public AnimatorStateInfo CurrentAnimatorStateInfo_
	{
		get 
		{
			return m_CurrentAnimatorStateInfo;
		}
		set
		{
			m_CurrentAnimatorStateInfo = value;
		}
	}

	public AnimatorStateInfo AntecedentAnimatorStateInfo_
	{
		get 
		{
			return m_AntecedentAnimatorStateInfo;
		}
		set
		{
			m_AntecedentAnimatorStateInfo = value;
		}
	}

	public int CurrentAnimatorFrame
	{
		get 
		{
			return m_CurrentAnimatorFrame;
		}
		set 
		{
			m_CurrentAnimatorFrame = value;
		}
	}

	public int AntecedentAnimatorFrame
	{
		get 
		{
			return m_AntecedentAnimatorFrame;
		}
		set 
		{
			m_AntecedentAnimatorFrame = value;
		}
	}
	#endregion
}
