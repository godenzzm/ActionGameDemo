using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum AIState 
{
	AIStateNone = 0,
	AIStateIdle,
	AIStateMove,
	AIStateAttack,
	AIStateHit,
};

class AIActionState
{
	public AIState state = AIState.AIStateNone;
	public float duration = 0f;
	//public string action = "";

	public Vector3 targetPosition = Vector3.zero;
}

public class AIAction : MonoBehaviour 
{
	public AudioClip AudioClipHit;
	public AudioClip AudioClipAttack;


	public Collider RightHandCollider;

	Animator m_Animator;
	NavMeshAgent m_NavMeshAgent;
	AudioSource m_AudioSource;

	GameObject m_Target;
	
	float m_Count = 0.0f;

	AIState m_CurState = AIState.AIStateNone;
	AIActionState m_CurActionState = null;

	Queue<AIActionState> m_AIActionQueue = new Queue<AIActionState> ();

	float m_MoveDuration = 1.5f;
	float m_IdleDuration = 1.0f;
	float m_AttackDuration = 0.66f;

	// Use this for initialization
	void Start ()
	{
		m_Animator = GetComponent<Animator> ();
		m_NavMeshAgent = GetComponent<NavMeshAgent> ();
		m_AudioSource = GetComponent<AudioSource> ();

		UpdateTarget ();

		m_CurState = AIState.AIStateNone;

		AIActionState AIAS = new AIActionState ();
		AIAS.state = AIState.AIStateIdle;
		AIAS.duration = 1.0f;
		AIAS.targetPosition = m_Target.transform.position;
		m_CurActionState = AIAS;

		//NotificationCenter.DefaultCenter ().AddObserver (transform, "OnEnemyHitAction");
		//NotificationCenter.DefaultCenter ().AddObserver (RightHandCollider, "OnAIFistAction");
	}
	
	// Update is called once per frame
	void Update () 
	{
		RunActionQueue ();
	}

	void UpdateTarget ()
	{
		string targetTag = "Player";
		m_Target = GameObject.FindWithTag (targetTag);
	}

	void RunActionQueue ()
	{
		if (m_CurActionState == null) return;

		if (m_Count < 0)
		{
			if (m_AIActionQueue.Count < 1)
			{
				GenerateAIActionState ();
				return;
			}

			m_CurActionState = m_AIActionQueue.Dequeue ();
			GenerateAIActionState ();

			m_CurState = m_CurActionState.state;
			m_Count = m_CurActionState.duration;
		}

		//Debug.Log (m_CurState);

		switch (m_CurState)
		{
		case AIState.AIStateIdle:
			m_NavMeshAgent.ResetPath ();

			m_Animator.speed = 1.0f;
			//AnimatorControllerManager.SetStateIdle (m_Animator);

			m_Count -= Time.deltaTime;
			break;
		case AIState.AIStateMove:
			m_Animator.speed = 1.0f;
			//AnimatorControllerManager.SetStateWalk (m_Animator);

			if (m_Count == m_CurActionState.duration)
			{
				m_NavMeshAgent.SetDestination (m_CurActionState.targetPosition);
			}

			m_Count -= Time.deltaTime;
			break;
		case AIState.AIStateAttack:
			if (m_Count == m_CurActionState.duration) 
			{
				m_AudioSource.PlayOneShot (AudioClipAttack);

				//NotificationCenter.DefaultCenter ().PostNotification (RightHandCollider, "OnAIFistAction", gameObject);
			}
			// turn around first
			m_NavMeshAgent.ResetPath ();

			Vector3 dir = m_Target.transform.position - transform.position;
			transform.forward = dir;

			m_Animator.speed = 2.0f;
			//AnimatorControllerManager.SetStateAttack (m_Animator);

			m_Count -= Time.deltaTime;
			break;
		case AIState.AIStateHit:

			m_NavMeshAgent.ResetPath ();

			transform.position -= transform.forward.normalized * 0.02f;

			m_Count -= Time.deltaTime;
			break;
		case AIState.AIStateNone:

			m_NavMeshAgent.ResetPath ();

			m_Count -= Time.deltaTime;
			break;
		default:
			break;
		}
	}

	float m_SaveDistance = 10f; // move towards player
	float m_AlertDistance = 4f; // move towards/awayfrom player, attack action
	float m_SeperateDistance = 2f; // attack player

	void GenerateAIActionState ()
	{
		float distance = Vector3.Distance (transform.position, m_Target.transform.position);

		//Debug.Log (distance);

		AIActionState AIAS = new AIActionState ();

		switch (m_CurState)
		{
		case AIState.AIStateIdle:
		case AIState.AIStateMove:
			if (distance > m_SaveDistance)
			{
				AIAS.state = RandomAIState (AIState.AIStateIdle, AIState.AIStateMove, AIState.AIStateMove, AIState.AIStateMove);
				if (AIState.AIStateIdle == AIAS.state) AIAS.duration = m_IdleDuration;
				else AIAS.duration = m_MoveDuration;
				AIAS.targetPosition = m_Target.transform.position;
			} else if (distance > m_AlertDistance)
			{
				AIAS.state = RandomAIState (AIState.AIStateIdle, AIState.AIStateMove, AIState.AIStateMove);
				if (AIState.AIStateIdle == AIAS.state) AIAS.duration = m_IdleDuration;
				else if (AIState.AIStateMove == AIAS.state) AIAS.duration = m_MoveDuration;
				else if (AIState.AIStateAttack == AIAS.state) AIAS.duration = 0.66f;
				else {}

				//2 possible, toward player or another direction

				AIAS.targetPosition = RandomTargetPosition (transform.position, m_Target.transform.position);
			} else if (distance > m_SeperateDistance)
			{
				AIAS.state = RandomAIState (AIState.AIStateAttack, AIState.AIStateAttack, AIState.AIStateAttack, AIState.AIStateMove);
				if (AIState.AIStateMove == AIAS.state) AIAS.duration = m_MoveDuration;
				else if (AIState.AIStateAttack == AIAS.state) AIAS.duration = 0.66f;
				else {}
			} else 
			{
				AIAS.state = AIState.AIStateAttack;
				AIAS.duration = m_AttackDuration;
				//AIAS.targetPosition = LeaveTargetPosition (transform.position, m_Target.transform.position);
			}

			break;
		case AIState.AIStateAttack:
		case AIState.AIStateHit:
			//easy mode, leave first
			AIAS.state = AIState.AIStateMove;
			AIAS.duration = m_MoveDuration;
			AIAS.targetPosition = LeaveTargetPosition (transform.position, m_Target.transform.position);
			break;
		case AIState.AIStateNone:
			AIAS.state = AIState.AIStateIdle;
			AIAS.duration = m_IdleDuration;
			AIAS.targetPosition = m_Target.transform.position;
			break;
		default:
			break;
		}

		m_AIActionQueue.Enqueue (AIAS);
	}

	AIState RandomAIState (params AIState[] actions)
	{
		if (actions.Length < 1) return AIState.AIStateNone;

		int randomIndex = Random.Range (0, actions.Length-1);

		return actions[randomIndex];
	}

	Vector3 RandomTargetPosition (Vector3 selfPos, Vector3 targetPos)
	{
		Vector3 ret;

		int dice = Random.Range (0, 5);

		if (dice > 0) { ret = targetPos; }
		else 
		{
			Vector3 dir = selfPos - targetPos;
			
			Matrix4x4 temp = Matrix4x4.identity;
			float angle = Random.Range (-180, 180);
			
			temp.SetRow (0, new Vector4 (Mathf.Cos (AngleToRadius (angle)), 0, -Mathf.Sin (AngleToRadius (angle)), 0));
			temp.SetRow (2, new Vector4 (Mathf.Sin (AngleToRadius (angle)), 0, Mathf.Cos (AngleToRadius (angle)), 0));
			
			Vector3 rotateDir = temp.MultiplyPoint3x4 (dir);
			
			ret = selfPos + rotateDir.normalized * 5;		
		}

		return ret;
	}


	/*
		X Axis  [1    0    0    0 ]        Y Axis [cosQ  0  -sinQ  0 ]          Z Axis  [cosQ   sinQ   0    0 ]
			    [0  cosQ  sinQ  0 ]               [0     1    0    0 ]                  [-sinQ  cosQ   0    0 ] 
			    [0 -sinQ  cosQ  0 ]               [sinQ  0  cosQ   0 ]                  [0      0      1    0 ]
			    [0   0     0    1 ]               [0     0    0    1 ]                  [0      0      0    1 ]
	*/
	Vector3 LeaveTargetPosition (Vector3 selfPos, Vector3 targetPos)
	{
		Vector3 ret;

		Vector3 dir = selfPos - targetPos;

		Matrix4x4 temp = Matrix4x4.identity;
		float angle = Random.Range (-60, 60);

		//绕Y轴转角度Q
		temp.SetRow (0, new Vector4 (Mathf.Cos (AngleToRadius (angle)), 0, -Mathf.Sin (AngleToRadius (angle)), 0));
		temp.SetRow (2, new Vector4 (Mathf.Sin (AngleToRadius (angle)), 0, Mathf.Cos (AngleToRadius (angle)), 0));

		Vector3 rotateDir = temp.MultiplyPoint3x4 (dir);

		ret = selfPos + rotateDir.normalized * 5;

		return ret;
	}



	void OnEnemyHitAction (GameObject sender)
	{
		Debug.Log ("AIAction OnEnemyHitAction " + sender.transform.name + " " + sender.transform.tag);

		transform.forward = sender.transform.forward * -1;

		m_NavMeshAgent.ResetPath ();

		m_AudioSource.PlayOneShot (AudioClipHit);

		//AnimatorControllerManager.SetStateHit (m_Animator);

		m_CurState = AIState.AIStateHit;

		m_Count = 0.72f;

		//Clear current Action
		if (m_AIActionQueue.Count > 0)
			m_AIActionQueue.Clear (); 

		StartCoroutine (OnEnemyHitActionEnd(0.72f));
	}

	float AngleToRadius (float angle)
	{
		return Mathf.PI / 180.0f * angle;
	}

	void OnCollisionEnter (Collision other)
	{
		//Debug.Log ("other tag:" + other.transform.name);
	}

	IEnumerator OnEnemyHitActionEnd (float time)
	{
		yield return new WaitForSeconds (time);

		//m_Animator.SetFloat ("hit", 0.0f);

		m_CurState = AIState.AIStateNone;
	}
}
