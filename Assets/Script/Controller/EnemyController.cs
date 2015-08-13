using UnityEngine;
using System.Collections;

public class EnemyController : Controller
{


	// Use this for initialization
	void Awake () 
	{
		base.Awake ();

		m_Animator = GetComponent<IAnimator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
