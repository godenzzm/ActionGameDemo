using UnityEngine;
using System.Collections;

public class TestOne : MonoBehaviour 
{
	public GameObject 									m_Target;
	public bool											m_BeginTurn = false;
	public float										m_Count = 0;
	public float										m_TurnDuration = 0.3f;
	public float										m_TurnAngle = 0;
	public float										m_TurnAnglePerFrame = 8;
	public bool											m_BeginMove = false;

	Animator 											m_Animator;
	CharacterController									m_C;
	// Use this for initialization
	void Start () 
	{
		m_Animator = m_Target.GetComponent<Animator> ();
		m_C = m_Target.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Debug.Log ("Forward:" + m_Target.transform.forward);

		if (Input.anyKeyDown) 
		{
			//m_Target.transform.Rotate (new Vector3 (0, 180, 0));

			m_BeginTurn = true;
			m_Count = 0;
			m_TurnAngle = 0;
		}

		if (m_BeginTurn) 
		{
			m_Count += Time.deltaTime;



			if (m_Count < m_TurnDuration)
			{
				m_TurnAngle += m_TurnAnglePerFrame;

				m_Target.transform.Rotate (new Vector3 (0, m_TurnAnglePerFrame, 0));

				if (m_TurnAngle > 90)
				{
					m_BeginTurn = false;
					m_BeginMove = true;

					m_Animator.CrossFade ("Walk_Tree", 0.1f);
				}
			}
		}

		if (Input.GetKey (KeyCode.W)) 
		{
			if (m_BeginMove)
			{
				//m_Target.transform.Translate (m_Target.transform.forward * 0.04f);
				m_C.Move (m_Target.transform.forward * 0.02f);

				//m_Animator.SetFloat (Action_1_Controller.ANIMATOR_PARAM_FLOAT_MOVE_DIR, 0);
				//m_Animator.SetFloat (Action_1_Controller.ANIMATOR_PARAM_FLOAT_SPEED, 1);
			}
		}

		if (Input.GetKeyUp (KeyCode.W)) 
		{
			m_BeginMove = false;

			m_Animator.CrossFade ("Idle", 0.1f);
		}
	}
}
