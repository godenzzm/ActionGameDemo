using UnityEngine;
using System.Collections;

public interface IAnimator  
{
	Animator GetAnimator ();
	AnimatorStateInfo GetCurrentAnimatorStateInfo ();

	void Move (float speed = 0);
	void StopMove ();

	void ActionJoyStickCircle ();
	void ActionJoyStickTriangle ();
	void ActionJoyStickSquare ();
	void ActionJoyStickCross ();

	void CrossFade (string name, float transition = 0.01f);
	void SetFloat (string name, float value);
	void SetBool (string name, bool value);
	void SetTriggle (string name);
}
