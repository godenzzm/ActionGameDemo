using UnityEngine;
using System.Collections;

public class TestTwo : MonoBehaviour 
{

	public float				m_Count = 0;
	public float				m_LogInterval = 2.5f;
	public float				m_MoveSpeed = 0.05f;
	public float				m_RotationSpeed = 30;

	CharacterController			m_C;
	Animator					m_Animator;
	Vector3						m_inputVec;

	// Use this for initialization
	void Start () 
	{
		m_C = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Count += Time.deltaTime;
		if (m_Count > m_LogInterval) 
		{
			Debug.Log ("Forward:" + transform.forward);

			m_Count = 0;
		}

		m_inputVec = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

		if (!Vector3.zero.Equals (m_inputVec))
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (m_inputVec), Time.deltaTime * m_RotationSpeed);
		}

		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.D))
		{
			m_C.Move (transform.forward * m_MoveSpeed);
		}
	}
}
