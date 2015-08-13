using UnityEngine;
using System.Collections;

public class AnimatorAlpha : MonoBehaviour, IAnimator 
{
	private Animator 															m_Animator;
	private Controller															m_Controller;

	void Awake ()
	{
		m_Animator = GetComponent<Animator> ();
		m_Controller = GetComponent<Controller> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IAnimator implement
	public void Move (float speed = 0)
	{
		if (m_Animator.GetCurrentAnimatorStateInfo (0).IsName (AnimatorConfig.STATE_IDLE))
		{
			m_Animator.SetFloat (AnimatorConfig.PARAM_FLOAT_MOVE_SPEED, speed);
		}
	}

	public void StopMove ()
	{
		m_Animator.SetFloat (AnimatorConfig.PARAM_FLOAT_MOVE_SPEED, 0);
	}

	public Animator GetAnimator () 
	{
		return m_Animator;
	}

	public AnimatorStateInfo GetCurrentAnimatorStateInfo () 
	{
		return m_Animator.GetCurrentAnimatorStateInfo (0);
	}

	//△□◎X
	public void ActionJoyStickCircle () {}
	public void ActionJoyStickCross () {}

	// 1 □=>□=>△ 2 △=>□=>△ 
	public void ActionJoyStickTriangle ()  //△
	{
		//combo △=>□=>△
		if (m_Animator.GetCurrentAnimatorStateInfo (0).IsName (AnimatorConfig.STATE_IDLE)
		    || m_Animator.GetCurrentAnimatorStateInfo (0).IsName (AnimatorConfig.STATE_MOVE) )
		{
			//basic triangle action
			if (!m_Animator.IsInTransition (0))
			{
				if (m_Controller is PlayerController)
				{
					((PlayerController)m_Controller).Attack ("Base_Kick");
				}
				m_Animator.SetTrigger (AnimatorConfig.PARAM_TRIGGER_KICK_BASE);
			}
		} else if ( m_Controller.CurrentAnimatorStateInfo_.IsName (AnimatorConfig.STATE_KICK_ROUND) )
		{
			//combo □=>□=>△ 3
			ExcecuteComboAction (AnimatorConfig.STATE_KICK_ROUND, AnimatorConfig.PARAM_TRIGGER_RISING_DRAGON);
		}
		else if ( m_Controller.CurrentAnimatorStateInfo_.IsName (AnimatorConfig.STATE_KICK_THRUST))
		{
			//combo △=>□=>△ step 3 
			ExcecuteComboAction (AnimatorConfig.STATE_KICK_THRUST, AnimatorConfig.PARAM_TRIGGER_KICK_SPIN);
		} 
		else {}
	}

	public void ActionJoyStickSquare () //□
	{
		if (m_Animator.GetCurrentAnimatorStateInfo (0).IsName (AnimatorConfig.STATE_IDLE)
		    || m_Animator.GetCurrentAnimatorStateInfo (0).IsName (AnimatorConfig.STATE_MOVE) )
		{
			if (!m_Animator.IsInTransition (0)) 
			{ 
				m_Animator.SetTrigger (AnimatorConfig.PARAM_TRIGGER_FIST_BASE);
			}
		} else if ( m_Controller.CurrentAnimatorStateInfo_.IsName (AnimatorConfig.STATE_FIST_BASE))
		{
			//combo □=>□=>△ 2
			ExcecuteComboAction (AnimatorConfig.STATE_FIST_BASE, AnimatorConfig.PARAM_TRIGGER_KICK_ROUND);
		}
		//else if ( m_Animator.GetCurrentAnimatorStateInfo (0).IsName (AnimatorConfig.STATE_KICK_BASE) )
		else if ( m_Controller.CurrentAnimatorStateInfo_.IsName (AnimatorConfig.STATE_KICK_BASE))
		{
			// combo △=>□=>△ step 2
			ExcecuteComboAction (AnimatorConfig.STATE_KICK_BASE, AnimatorConfig.PARAM_TRIGGER_KICK_THRUST);
		}
	}

	//antecedentStateName：将要执行的连击动作的前一个状态名
	//currentParamName：将要执行的连击动作的参数名
	void ExcecuteComboAction (string antecedentStateName, string currentParamName)
	{
		Skill antecedentSkill = SkillConfigReader.GetSkillByState (antecedentStateName);
		int currentFrameOfantecedentSkill = m_Controller.CurrentAnimatorFrame; //当前动作执行到第几帧

		//Debug.Log ("antecedentSkill ID:" +antecedentSkill.Id_);
		//Debug.Log ("ActionJoyStickSquare Controller □ CurrentAnimatorStateInfo:" + m_Controller.CurrentAnimatorStateInfo_.fullPathHash + " currentframe:" + m_Controller.CurrentAnimatorFrame);

		//这里的current animator frame 记录是当前将要判定执行的动作的上一个动作，也就是在当前动作的第a帧~第b帧间，判定执行下一个动作
		//所以这里的antecedentSkill和current animator frame所属的技能是同一个技能
		if (currentFrameOfantecedentSkill >= antecedentSkill.ComboTriggerFrameBegin_ 
		    && currentFrameOfantecedentSkill <= antecedentSkill.ComboTriggerFrameEnd_)
		{
			if (!m_Animator.IsInTransition (0)) 
			{
				m_Animator.SetTrigger (currentParamName);
			}
		}
	}

	public void CrossFade (string name, float transition = 0.01f)
	{
		m_Animator.CrossFade (name, transition);
	}

	public void SetFloat (string name, float value)
	{
		m_Animator.SetFloat (name, value);
	}

	public void SetBool (string name, bool value)
	{
		m_Animator.SetBool (name, value);
	} 

	public void SetTriggle (string name)
	{
		m_Animator.SetTrigger (name);
	}
	#endregion
}
