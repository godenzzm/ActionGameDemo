using UnityEngine;
using System.Collections;

public class TrackMovementLinear : TrackMovement 
{
	void Update () 
	{
		if (m_Switch) 
		{
			TrackMove ();
		}
	}

	public void TrackMove ()
	{
		Debug.Log ("TrackMovementLinear TrackMove !!!");
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
