using UnityEngine;
using System.Collections;

public class CoordinateTransform 
{
	/*
		X Axis  [1    0    0    0 ]        Y Axis [cosQ  0  -sinQ  0 ]          Z Axis  [cosQ   sinQ   0    0 ]
			    [0  cosQ  sinQ  0 ]               [0     1    0    0 ]                  [-sinQ  cosQ   0    0 ] 
			    [0 -sinQ  cosQ  0 ]               [sinQ  0  cosQ   0 ]                  [0      0      1    0 ]
			    [0   0     0    1 ]               [0     0    0    1 ]                  [0      0      0    1 ]

		Vector3 N Axis  { N.x*N.x*(1-cosQ) + cosQ,   N.x*N.y*(1-cosQ) + N.z*sinQ,   N.x*N.z*(1-cosQ) - N.y*sinQ,   0 }
						{ N.x*N.y*(1-cosQ) - N.z*sinQ,   N.y*N.y*(1-cosQ) + cosQ,   N.y*N.z*(1-cosQ) + N.x*sinQ,   0 }
						{ N.x*N.z*(1-cosQ) + N.y*sinQ,   N.y*N.z*(1-cosQ) - N.x*sinQ,   N.z*N.z*(1-cosQ) + cosQ,   0 }
						{ 0,							0,							0,							   1 }
	*/
	public static Vector3 RotationAroundAxisX (Vector3 origin, float angle)
	{
		Vector3 ret = Vector3.zero;

		Matrix4x4 transformMatrix = Matrix4x4.identity;
		//行
		transformMatrix.SetRow (1, new Vector4 (0, Mathf.Cos (AngleToRadian (angle)), Mathf.Sin (AngleToRadian (angle)), 0) );
		transformMatrix.SetRow (2, new Vector4 (0, -1*Mathf.Sin (AngleToRadian (angle)), Mathf.Cos (AngleToRadian (angle)), 0) );

		ret = transformMatrix.MultiplyPoint3x4 (origin);

		return ret;
	}

	public static Vector3 RotationAroundAxisY (Vector3 origin, float angle)
	{
		Vector3 ret = Vector3.zero;

		Matrix4x4 transformMatrix = Matrix4x4.identity;
		//行
		transformMatrix.SetRow (0, new Vector4 (Mathf.Cos (AngleToRadian (angle)), 0, -1*Mathf.Sin (AngleToRadian (angle)), 0) );
		transformMatrix.SetRow (2, new Vector4 (Mathf.Sin (AngleToRadian (angle)), 0, Mathf.Cos (AngleToRadian (angle)), 0) );
		
		ret = transformMatrix.MultiplyPoint3x4 (origin);

		return ret;
	}

	public static Vector3 RotationAroundAxisZ (Vector3 origin, float angle)
	{
		Vector3 ret = Vector3.zero;

		Matrix4x4 transformMatrix = Matrix4x4.identity;

		transformMatrix.SetRow (0, new Vector4 (Mathf.Cos (AngleToRadian (angle)), Mathf.Sin (AngleToRadian (angle)), 0, 0) );
		transformMatrix.SetRow (1, new Vector4 (-1*Mathf.Sin (AngleToRadian (angle)), Mathf.Cos (AngleToRadian (angle)), 0, 0) );

		ret = transformMatrix.MultiplyPoint3x4 (origin);

		return ret;
	}

	public static Vector3 RotationAroundAxisN (Vector3 origin, Vector3 n, float angle)
	{
		Vector3 ret = Vector3.zero;

		Matrix4x4 transformMatrix = Matrix4x4.identity;

		float m00 = n.x * n.x * (1 - Mathf.Cos (AngleToRadian (angle))) + Mathf.Cos (AngleToRadian (angle));
		float m01 = n.x * n.y * (1 - Mathf.Cos (AngleToRadian (angle))) + n.z * Mathf.Sin (AngleToRadian (angle));
		float m02 = n.x * n.z * (1 - Mathf.Cos (AngleToRadian (angle))) - n.y * Mathf.Sin (AngleToRadian (angle));
		
		float m10 = n.x * n.y * (1 - Mathf.Cos (AngleToRadian (angle))) - n.z * Mathf.Sin (AngleToRadian (angle));
		float m11 = n.y * n.y * (1 - Mathf.Cos (AngleToRadian (angle))) + Mathf.Cos (AngleToRadian (angle));
		float m12 = n.y * n.z * (1 - Mathf.Cos (AngleToRadian (angle))) + n.x * Mathf.Sin (AngleToRadian (angle));
		
		float m20 = n.x * n.z * (1 - Mathf.Cos (AngleToRadian (angle))) + n.y * Mathf.Sin (AngleToRadian (angle));
		float m21 = n.y * n.z * (1 - Mathf.Cos (AngleToRadian (angle))) - n.x * Mathf.Sin (AngleToRadian (angle));
		float m22 = n.z * n.z * (1 - Mathf.Cos (AngleToRadian (angle))) + Mathf.Cos (AngleToRadian (angle));
		
		transformMatrix.SetRow (0, new Vector4 (m00, m01, m02, 0) );
		transformMatrix.SetRow (1, new Vector4 (m10, m11, m12, 0) );
		transformMatrix.SetRow (2, new Vector4 (m20, m21, m22, 0) );

		ret = transformMatrix.MultiplyPoint3x4 (origin);

		return ret;
	}

	public static float AngleToRadian (float angle)
	{
		float ret = 0;

		ret = Mathf.PI * angle / 180.0f;

		return ret;
	}

	public static float RadianToAngle (float radian)
	{
		float ret = 0;

		ret = 180.0f * radian / Mathf.PI;

		return ret;
	}
}
