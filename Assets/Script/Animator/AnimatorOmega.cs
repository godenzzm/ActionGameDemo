using UnityEngine;
using System.Collections;

public class AnimatorOmega : MonoBehaviour, IAnimator {

	private Animator 															m_Animator;
	private Controller															m_Controller;
	
	void Awake ()
	{
		m_Animator = GetComponent<Animator> ();
		m_Controller = GetComponent<Controller> ();
	}

	// Use this for initialization
	#region IAnimator implement
	public void Move (float speed = 0)
	{

	}
	
	public void StopMove ()
	{

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
	public void ActionJoyStickTriangle () {}
	public void ActionJoyStickSquare () {}
	
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
