using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour 
{
	public float																	m_DelayTime = 0;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (Destruction (m_DelayTime));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Destruction (float time)
	{
		yield return new WaitForSeconds (time);

		DestroyImmediate (gameObject);
	}

	void OnAnimationEnd ()
	{
		StartCoroutine (Destruction (m_DelayTime));
	}
}
