using UnityEngine;
using System.Collections;

public interface IInputInterface 
{
	void InputListener ();
	void SetController (IAnimator animatorController, MonoBehaviour playerController);
}
