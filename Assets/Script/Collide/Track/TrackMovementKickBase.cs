using UnityEngine;
using System.Collections;

public class TrackMovementKickBase : TrackMovement
{
	void Start ()
	{
		m_Frame = 10;
		m_Dir = m_Target.transform.forward;
		transform.position = m_Target.transform.position;
		Vector3 moveDir = CoordinateTransform.RotationAroundAxisN (m_Dir, m_Target.transform.up, 90);
		transform.Translate (moveDir.normalized * 0.4f);

		m_Dis = 1.8f;
		m_Dir = CoordinateTransform.RotationAroundAxisN (m_Dir, m_Target.transform.right, 40);
		m_Dir = CoordinateTransform.RotationAroundAxisN (m_Dir, m_Target.transform.up, 45);

	}

	void Update () 
	{
		if (m_Switch) 
		{
			TrackMove ();
		}
	}
	
	public void TrackMove ()
	{
		if (m_Frame < m_FrameCount) return;
		
		++m_FrameCount;
		
		Vector3 tempPos = transform.localPosition;
		
		Vector3 FunctionVector3 = MovingFunctionLinear (m_Dir.normalized);
		
		float moveX = tempPos.x + FunctionVector3.x * m_Dis / m_Frame;
		float moveY = tempPos.y + FunctionVector3.y * m_Dis / m_Frame;
		float moveZ = tempPos.z + FunctionVector3.z * m_Dis / m_Frame;

		transform.localPosition = new Vector3 (moveX, moveY, moveZ);
	}
	
	public Vector3 MovingFunctionLinear (Vector3 x, float factor = 1)
	{
		Vector3 ret;
		
		ret = x * factor;
		
		return ret;
	}
}
