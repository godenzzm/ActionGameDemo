using UnityEngine;
using System.Collections;

public class AnimatorStateMachine : StateMachineBehaviour 
{
	Controller m_Controller = null;

	void GetController (GameObject target)
	{
		if (null == m_Controller)
		{
			//Debug.Log ("AnimatorStateMachine GetController !!!");

			m_Controller = target.GetComponent<Controller> ();
		}
	}

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		if (null == m_Controller) GetController (animator.gameObject);

		m_Controller.CurrentAnimatorStateInfo_ = stateInfo;
		m_Controller.CurrentAnimatorFrame = 1;
	}
	
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		if (null == m_Controller) GetController (animator.gameObject);

		m_Controller.CurrentAnimatorFrame += 1;
	}
	
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		if (null == m_Controller) GetController (animator.gameObject);

		m_Controller.AntecedentAnimatorStateInfo_ = stateInfo;
		m_Controller.CurrentAnimatorFrame += 1;
		m_Controller.AntecedentAnimatorFrame = m_Controller.CurrentAnimatorFrame;
	}
	
	//public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
	//public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {}
	
	//public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash){}
	//public override void OnStateMachineExit(Animator animator, int stateMachinePathHash){}
}
