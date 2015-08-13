using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionEventController : MonoBehaviour, ITriggerEventInterface 
{
	//根据id取对应的技能动作状态,每个发动作的源（玩家角色，或者其他事物角色）都有对应的动作技能，攻击时都有对应的判定范围对象
	//生成判定对象后，根据碰撞结果，计算伤害结果（命中的对象，伤害判定次数，具体伤害计算等等）
	public int													m_SkillId;

	public TriggerEventObject[]  								m_TriggerObjects;

	[System.Serializable] 
	public class ColliderPair
	{
		public GameObject Source_ = null;
		public GameObject Target_ = null;
		public int HitCount_ = 0;

		public ColliderPair ()
		{
			Source_ = null;
			Target_ = null;
			HitCount_ = 0;
		}
	}

	[SerializeField] private Dictionary<ColliderPair, bool> 						m_ColliderDictionary;

	// Use this for initialization
	void Awake () 
	{
		InitTriggerObjects ();

		m_ColliderDictionary = new Dictionary<ColliderPair, bool> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitTriggerObjects ()
	{
		m_TriggerObjects = GetComponentsInChildren<TriggerEventObject> ();

		for (int i=0; i<m_TriggerObjects.Length; ++i) 
		{
			m_TriggerObjects[i].Owner = this;
		}
	}

	#region
	//frameCount，攻击动画开始判定的第几帧，因为有些攻击会根据持续判定的帧数再次判定攻击伤害
	//注意这里的判定方法都是对应角色攻击时的判定回调方法，可以具体在通过stateMachine严格在第X帧开始攻击判定
	//目前准备采用collider来计算，比如在“挥拳”动作开始时第x帧生成一个collider对象，判定碰撞
	public void OnActionAttackingJudge (int frameCount, AnimatorStateInfo state, int layerIndex)
	{
		PlayerController pc = GetComponentInChildren<PlayerController> ();

		if (null != pc) 
		{
			//pc.Attack (frameCount, state, layerIndex);
		}

		/*
		if (state.IsName (Action_1_Controller.AnimatorState_Idle_Combo_1_Jab)) 
		{
			//temp
			string ObjName = "Left Hand";
			int attackNum = 1;

			//到m_TriggerObjects里面去找对应的object
			GameObject sourceObject = FindTriggerObjectByName (ObjName);

			ExcecuteHit (sourceObject, attackNum);

		} else if (state.IsName (Action_1_Controller.AnimatorState_Jump_Kick))
		{
			//这个动作有两个触碰点
			string ObjName1 = "Left Foot";
			string ObjName2 = "Right Foot";
			int attackNum = 1;

			//到m_TriggerObjects里面去找对应的object
			GameObject sourceObject1 = FindTriggerObjectByName (ObjName1);
			GameObject sourceObject2 = FindTriggerObjectByName (ObjName2);

			ExcecuteHit (sourceObject1, attackNum);
			ExcecuteHit (sourceObject2, attackNum);
		}*/
	}

	public void OnActionAttackingEnd (int frameCount, AnimatorStateInfo state, int layerIndex)
	{
		//Debug.Log ("OnActionAttackEnd state:" + state.shortNameHash);

		PlayerController pc = GetComponentInChildren<PlayerController> ();
		
		if (null != pc) 
		{
			pc.AttackJudgeEnd (frameCount, state, layerIndex);
		}
	}

	void ExcecuteHit (GameObject sourceObject, int attackNum)
	{
		if (null != sourceObject)
		{
			//正在碰撞的对象列表
			List<ColliderPair> targetList = new List<ColliderPair> ();
			
			FindColliderPairDictionaryBySource (sourceObject, targetList);

			//Debug.Log ("aa ExcecuteHit " + targetList.Count);

			for (int i=0; i<targetList.Count; ++i)
			{
				if (targetList[i].HitCount_ < attackNum)
				{
					PlayerController pc = targetList[i].Target_.GetComponent<PlayerController> ();
					
					if (null != pc)
					{
						Debug.Log ("ExcecuteHit " + targetList[i].Target_);
						pc.Hit ();
					}
					
					++targetList[i].HitCount_;
				}
			}
		}
	}
	#endregion

	#region collider data
	public void AddKeyInColliderDictionary (GameObject source, GameObject target, bool isCollider)
	{
		foreach (ColliderPair cp in m_ColliderDictionary.Keys) 
		{
			if (source == cp.Source_ && target == cp.Target_)
			{
				if (!isCollider) //碰撞结束，从列表中拿掉
				{
					m_ColliderDictionary.Remove (cp);
				} else
				{
					m_ColliderDictionary[cp] = isCollider;
				}

				return;
			}
		}

		if (isCollider) //保存正在碰撞的
		{
			ColliderPair colliderPair = new ColliderPair ();
			colliderPair.Source_ = source;
			colliderPair.Target_ = target;
			m_ColliderDictionary [colliderPair] = isCollider;
		}
	}

	public void FindColliderPairDictionaryBySource (GameObject source, List<ColliderPair> targetList)
	{
		foreach (ColliderPair cp in m_ColliderDictionary.Keys) 
		{
			if (source == cp.Source_ && m_ColliderDictionary[cp])
			{
				targetList.Add (cp);
			}
		}
	}

	public GameObject FindTriggerObjectByName (string name)
	{
		GameObject ret = null;

		for (int i=0; i<m_TriggerObjects.Length; ++i) 
		{
			if (name.Equals (m_TriggerObjects[i].DelegateKey))
			{
				ret = m_TriggerObjects[i].gameObject;
				break;
			}
		}

		return ret;
	}
	#endregion

	#region ITriggerEventInterface
	public void TriggerStayOther (GameObject selfObject, Collider colliderObject)
	{
		//Debug.Log ("TriggerStayOther !!! " + selfObject + " " + colliderObject);
	} 

	public void TriggerEnterOther (GameObject selfObject, Collider colliderObject)
	{
		//Debug.Log ("TriggerEnterOther !!! " + selfObject + " " + colliderObject);

		//AddKeyInColliderDictionary (selfObject, colliderObject, true);
	}

	public void TriggerExitOther (GameObject selfObject, Collider colliderObject)
	{
		//Debug.Log ("TriggerExitOther !!! " + selfObject + " " + colliderObject);

		//AddKeyInColliderDictionary (selfObject, colliderObject, false);
	}
	#endregion
}
