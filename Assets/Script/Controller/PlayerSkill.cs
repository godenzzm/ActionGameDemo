using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkill : MonoBehaviour 
{


	public List<SkillInstance> 															m_SkillList;
	public static int																	m_IdGenerator;

	void Awake ()
	{
		m_IdGenerator = 0;
	}

	// Use this for initialization
	void Start () 
	{
		//temp
		m_SkillList = new List<SkillInstance> ();
	}

	public SkillInstance FindSkillByState (string animatorState)
	{
		return null;
	}

	public SkillInstance GetSkillByName (string Name)
	{
		return null;
	}

	public SkillInstance GetSkillInstanceById (int id)
	{
		SkillInstance ret = null;

		for (int i=0; i<m_SkillList.Count; ++i) 
		{
			if (id == m_SkillList[i].Id_)
			{
				ret = m_SkillList[i];
				break;
			}
		}

		return ret;
	}

	// Update is called once per frame
	void Update () {
	
	}

	#region get set
	public List<SkillInstance> SkillList
	{
		get 
		{
			return m_SkillList;
		}
	}
	#endregion
}
