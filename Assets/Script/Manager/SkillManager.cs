using UnityEngine;
using System.Collections;


public class SkillInstance
{
	public int Id_;
	public string Name_;
	public string ColliderName_;
	public Vector3 ColliderPos_;
	public GameObject Fx_;
	public AudioClip Sound_;
	public string TrackClass_;
}

public class SkillManager 
{
	private static int														m_Id = 0;
	private static SkillManager												m_Instance = null;
	public static SkillManager Instance
	{
		get 
		{
			if (null == m_Instance)
			{
				m_Instance = new SkillManager ();
			}
			return m_Instance;
		}
	}

	public SkillInstance GetSkillInstanceByName (string name)
	{
		SkillInstance ret = null;

		if (name.Equals ("Base_Kick")) 
		{
			ret = new SkillInstance ();
			ret.Id_ = ++m_Id;
			ret.Name_ = name;
			ret.ColliderName_ = "NormalAttackCollider";
			ret.Fx_ = null;
			ret.Sound_ = null;
			ret.TrackClass_ = "TrackMovementKickBase";
		}

		return ret;
	}

	public TrackMovement AddTrackComponent (GameObject go, string name)
	{
		TrackMovement ret = null;

		if (name.Equals ("TrackMovementKickBase"))
		{
			ret = go.AddComponent<TrackMovementKickBase> ();
		}

		return ret;
	}
}
