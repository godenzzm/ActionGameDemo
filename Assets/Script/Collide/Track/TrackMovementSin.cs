using UnityEngine;
using System.Collections;

public class TrackMovementSin : TrackMovement, ITrack 
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
		if (m_Frame < m_FrameCount) return;
		
		++m_FrameCount;
		
		Vector3 tempPos = transform.localPosition;
		
		Vector3 FunctionVector3 = MovingFunctionSin (m_Dir.normalized);
		
		float moveX = tempPos.x + FunctionVector3.x * m_Dis / m_Frame;
		float moveY = tempPos.y + FunctionVector3.y * m_Dis / m_Frame;
		float moveZ = tempPos.z + FunctionVector3.z * m_Dis / m_Frame;

		transform.localPosition = new Vector3 (moveX, moveY, moveZ);
	}
	
	public Vector3 MovingFunctionSin (Vector3 x, float factor = 1)
	{
		Vector3 ret;
		
		float vary = Mathf.Sin (m_FrameCount * 1.0f / m_Frame * Mathf.PI * 2) * factor;
		
		ret = new Vector3 (x.x, x.y + vary, x.z);
		
		return ret;
	}

}
