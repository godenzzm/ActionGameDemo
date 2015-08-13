using UnityEngine;
using System.Collections;

public class EasyAI : MonoBehaviour 
{
	[SerializeField] private GameObject 								m_TargetObject;

	void Awake ()
	{
		m_TargetObject = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		LookAtTarget ();
	}

	void LookAtTarget ()
	{
		if (null == m_TargetObject) return;

		transform.LookAt (m_TargetObject.transform.position);
	}
}
