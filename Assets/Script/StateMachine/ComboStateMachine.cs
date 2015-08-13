using UnityEngine;
using System.Collections;

//[SharedBetweenAnimators]
public class ComboStateMachine : StateMachineBehaviour 
{
	public int m_FrameCount = 0;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		m_FrameCount = 0;

		++m_FrameCount;

		Debug.Log ("Frame:" + m_FrameCount + " ComboStateMachine OnStateEnter animator:" + animator + " stateInfo:" + stateInfo.shortNameHash + " layerIndex:" + layerIndex);
	}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		++m_FrameCount;

		Debug.Log ("Frame:" + m_FrameCount + " ComboStateMachine OnStateUpdate animator:" + animator + " stateInfo:" + stateInfo.shortNameHash + " layerIndex:" + layerIndex);
	}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		++m_FrameCount;

		Debug.Log ("Frame:" + m_FrameCount + " ComboStateMachine OnStateExit animator:" + animator + " stateInfo:" + stateInfo.shortNameHash + " layerIndex:" + layerIndex);
	}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		//Debug.Log ("Frame:" + m_FrameCount + " ComboStateMachine OnStateMove animator:" + animator + " stateInfo:" + stateInfo.shortNameHash + " layerIndex:" + layerIndex);
	}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
	{
		Debug.Log ("Frame:" + m_FrameCount + " ComboStateMachine OnStateIK animator:" + animator + " stateInfo:" + stateInfo.shortNameHash + " layerIndex:" + layerIndex);
	}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
	{
		Debug.Log ("Frame:" + m_FrameCount + " ComboStateMachine OnStateMachineEnter animator:" + animator + " stateMachinePathHash:" + stateMachinePathHash);
	}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	public override void OnStateMachineExit(Animator animator, int stateMachinePathHash) 
	{
		Debug.Log ("Frame:" + m_FrameCount + " ComboStateMachine OnStateMachineExit animator:" + animator + " stateMachinePathHash:" + stateMachinePathHash);
	}
}
