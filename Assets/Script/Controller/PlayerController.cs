using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState
{
	PlayerStateNone = 0,
	PlayerStateIdle,
	PlayerStateMove,
	PlayerStateRun,
	PlayerStateJumpUp,
	PlayerStateJumpFloat,
	PlayerStateJumpDown,
	PlayerStateHit,
	PlayerStateEvade,
	PlayerStateDie,
}

public class PlayerController : Controller, ITriggerEventInterface
{

	private PlayerState																		m_PlayerState;			
	private CharacterController 															m_CharacterController;


	public SkillInstance																	m_CurrentSkillInstance;
	public List<GameObject>																	m_SkillTriggerObjectList;

	protected float																			m_JumpUpDuration = 0.64f;// fixedupdate 0.02 32次
	protected float																			m_JumpHeightFU = 0.03125f; // 1.0f / 32 = 0.03125 
	protected float																			m_JumpTimeCount = 0.0f; //跳跃，浮空，下落的计时变量，由于这3个状态不会同时进行，所以公用
	protected float																			m_FloatDuration = 0;

	// Use this for initialization
	void Awake () 
	{
		base.Awake ();

		m_PlayerState = PlayerState.PlayerStateIdle;

		m_CharacterController = GetComponent<CharacterController> ();

		m_CurrentSkillInstance = null;
		m_SkillTriggerObjectList = new List<GameObject> ();

		m_Animator = GetComponent<AnimatorAlpha> ();

		if (m_CharacterController) {}
	}

	public CharacterController GetCharacterController
	{
		get
		{
			return m_CharacterController;
		}
	}

	#region player motion
	public void Hit ()
	{

	}

	public void MoveAndTurn (Vector3 inputVec)
	{
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (inputVec), Time.deltaTime * m_TurnSpeed);

		if (m_CharacterController) 
		{
			m_MoveDir = transform.forward;
			m_CharacterController.Move (m_MoveDir * m_Speed);

			DetectTrimToGround ();
		}
	}

	public void JumpAndMove ()
	{
		m_JumpTimeCount = 0;
		m_PlayerState = PlayerState.PlayerStateJumpUp;
	}

	public void DoubleJump ()
	{

	}
	
	//skillname:string, 
	public void Attack (string name)
	{
		//Debug.Log ("Attack name:" + name);

		SkillInstance si = SkillManager.Instance.GetSkillInstanceByName (name);

		ExcecuteSkillInstance (si);
	}

	public void ExcecuteSkillInstance (SkillInstance si)
	{
		GameObject go = ColliderManager.Instance.SpawnCollider (si.ColliderName_);
		TriggerEventObject teObj = go.AddComponent<TriggerEventObject> ();
		teObj.Owner = this;

		TrackMovement trackMovement = SkillManager.Instance.AddTrackComponent (go, si.TrackClass_);
		trackMovement.Target = gameObject;
		trackMovement.Switch = true;

		if (!m_SkillTriggerObjectList.Contains (go))
		{
			m_SkillTriggerObjectList.Add (go);
		}
	}

	public void AttackJudgeEnd (int frameCount, AnimatorStateInfo state, int layerIndex)
	{

	}
	#endregion

	#region action state
	void JumpUp ()
	{
		if (PlayerState.PlayerStateJumpUp == m_PlayerState) 
		{
			//Debug.Log ("PlayerController JumpUp !!!");

			m_JumpTimeCount += Time.deltaTime;

			if (m_JumpTimeCount < m_JumpUpDuration)
			{
				Vector3 curPos = transform.position;

				transform.position = new Vector3 (curPos.x, curPos.y + m_JumpHeightFU, curPos.z);
			} else 
			{
				m_JumpTimeCount = 0;

				m_PlayerState = PlayerState.PlayerStateJumpFloat;
				m_FloatDuration = 0.3f;
			}
		}
	}

	void JumpFloat ()
	{
		if (PlayerState.PlayerStateJumpFloat == m_PlayerState)
		{
			//Debug.Log ("PlayerController JumpFloat !!!");

			m_JumpTimeCount += Time.deltaTime;

			if (m_JumpTimeCount < m_FloatDuration)
			{

			} else 
			{
				m_JumpTimeCount = 0;

				m_PlayerState = PlayerState.PlayerStateJumpDown;
			}
		}
	}

	void JumpDown ()
	{
		if (PlayerState.PlayerStateJumpDown == m_PlayerState)
		{
			//Debug.Log ("PlayerController JumpDown !!!");

			RaycastHit raycastHit;
			Vector3 origin = transform.localPosition;
			origin = new Vector3 (origin.x, origin.y + 10f, origin.z);
			Vector3 direction = new Vector3 (0, -1, 0);

			bool onGround = false;
			bool onPlayGroundAnim = false;
			float verDistanceGround = 0.5f;
			if (Physics.Raycast (origin, direction, out raycastHit, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Ground"))) 
			{
				if (transform.position.y < raycastHit.point.y + verDistanceGround)
				{
					onPlayGroundAnim = true;
				}

				if (transform.position.y > raycastHit.point.y)
				{
					float downY = transform.position.y - m_FallSpeed - Time.deltaTime * m_GravityFact;
					transform.position = new Vector3 (transform.position.x, downY, transform.position.z);
				} else //着陆
				{
					onGround = true;
				}

				if (transform.position.y < raycastHit.point.y) //着陆
				{
					transform.position = new Vector3 (transform.position.x, raycastHit.point.y, transform.position.z);
					onGround = true;
				}
			} else //着陆
			{
				onGround = true;
				onPlayGroundAnim = true;
			}

			if (onPlayGroundAnim)
			{
				m_Animator.SetBool (AnimatorConfig.PARAM_BOOL_IN_AIR, false);
			}

			if (onGround)
			{
				m_PlayerState = PlayerState.PlayerStateIdle;
				//m_Animator.SetBool (AnimatorConfig.PARAM_BOOL_IN_AIR, false);
			}
		}
	}

	#endregion

	#region detect standing ground
	//修正人物和地面之间的距离
	public void DetectTrimToGround ()
	{
		if (m_OnTrimToGround) return;

		RaycastHit raycastHit;
		Vector3 origin = transform.localPosition;
		origin = new Vector3 (origin.x, origin.y + 100f, origin.z);
		Vector3 direction = new Vector3 (0, -1, 0);

		if (Physics.Raycast (origin, direction, out raycastHit, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Ground"))) 
		{
			//Debug.Log ("detect ground " + + raycastHit.point.x + " " + raycastHit.point.y + " " + + raycastHit.point.z);

			m_GroundPosition = raycastHit.point;

			m_GroundPosition = new Vector3 (m_GroundPosition.x, m_GroundPosition.y + 0.0f, m_GroundPosition.z);

			m_OnTrimToGround = true;

			transform.position = raycastHit.point;
		}
	}

	void OnGravityGround ()
	{
		if (!m_OnTrimToGround) return;

		Vector3 curPos = transform.position;

		++m_GravityFrameCount;

		//if (0 != m_GravityFrameCount%3) return; //每隔3帧对齐一次地面距离

		transform.position = new Vector3 (curPos.x, m_GroundPosition.y, curPos.z);

		//transform.position = m_GroundPosition;

		/*
		if (curPos.y > m_GroundPosition.y)
		{
			curPos = new Vector3 (curPos.x, curPos.y - m_FallSpeed - m_GravityFrameCount * m_GravityFact, curPos.z);

			if (curPos.y < m_GroundPosition.y) curPos.y = m_GroundPosition.y;

			transform.position = curPos;
		} else 
		{
			m_OnTrimToGround = false;

			m_GravityFrameCount = 0;

			transform.position = new Vector3 (curPos.x, m_GroundPosition.y, curPos.z);
		}*/
	}
	#endregion

	// Update is called once per frame
	void Update () 
	{
		++m_FrameCount;

		UpdateAnimatorState ();

	}

	void LateUpdate ()
	{
		//OnGravityGround ();
	}

	void FixedUpdate ()
	{
		JumpUp ();
		JumpFloat ();
		JumpDown ();
	}

	void OnDrawGizmos ()
	{
		Vector3 origin = new Vector3 (transform.position.x, transform.position.y + 1.0f, transform.position.z);

		Ray ray = new Ray (origin, new Vector3 (0, -10, 0));
		Gizmos.color = Color.red;
		Gizmos.DrawRay (ray);

		Ray worldUpRay = new Ray (transform.position, transform.localToWorldMatrix.MultiplyPoint3x4 (transform.up).normalized);
		Gizmos.color = Color.blue;
		Gizmos.DrawRay (worldUpRay);
	}

	#region ITriggerEventInterface
	public void TriggerStayOther (GameObject selfObject, Collider collider)
	{


	} 
	
	public void TriggerEnterOther (GameObject selfObject, Collider collider)
	{
		if (m_SkillTriggerObjectList.Contains (selfObject) && collider.gameObject.CompareTag ("Enemy")) 
		{
			Debug.Log ("PlayerController TriggerEnterOther !!! " + selfObject.name + " " + collider.gameObject.name + " contact offset:" + collider.contactOffset);
			
			Controller selfCtrl = selfObject.GetComponent<TriggerEventObject> ().Owner as Controller;
			
			Controller enemyCtrl = collider.gameObject.GetComponent<Controller> ();

			if (null != selfCtrl && null != enemyCtrl)
			{
				enemyCtrl.Hit (selfCtrl);

				Object obj = Resources.Load ("FX/FX_Test");
				GameObject go = Instantiate (obj) as GameObject;
				go.transform.position = selfObject.transform.position;
				//go.transform.localScale = Vector3.one;
			}
		}
	}
	
	public void TriggerExitOther (GameObject selfObject, Collider collider)
	{
		//Debug.Log ("PlayerController TriggerExitOther !!! " + selfObject + " " + colliderObject);
	}
	#endregion

	#region get set
	public CharacterController CharacterController
	{
		get 
		{
			return m_CharacterController;
		}
	}
	#endregion
}
