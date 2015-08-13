using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour 
{
	public enum PlayerCameraType
	{
		PlayerCameraTypeNone = 0,
		PlayerCameraTypeACT,
		PlayerCameraTypeRTS,
	};

	[SerializeField] private GameObject															m_CameraTarget;
	[SerializeField] private GameObject 														m_PlayCamera;

	[SerializeField] private Vector3															m_ACTGamePosition;
	[SerializeField] private Vector3 															m_ACTGameRotation;

	[SerializeField] private Vector3 															m_RTSGamePosition;
	[SerializeField] private Vector3 															m_RTSGameRotation;

	private bool																				m_IsUpdateCamera;

	private Vector3 																			m_CameraTargetDir;
	private float																				m_CameraTargetDis;

	private Vector3																				m_CurrentPosition;
	private Vector3 																			m_LastPosition;
	private float																				m_CameraHeight;
	Ray testRay;

	void Awake ()
	{
		m_RTSGamePosition = new Vector3 (0f, 10f, -2f);
		m_RTSGameRotation = new Vector3 (65f, 0f, 0f);
	
		m_ACTGamePosition = new Vector3 (0f, 1.8f, -2f);
		m_ACTGameRotation = new Vector3 (18f, 0f, 0f);

		m_CameraTargetDir = new Vector3 (0, 0.5f, 0.5f);
		m_CameraTargetDis = 10;

		m_IsUpdateCamera = true;
		m_PlayCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	// Use this for initialization
	void Start () 
	{
		float cameraX = m_CameraTarget.transform.position.x + m_CameraTargetDir.x * m_CameraTargetDis;
		float cameraY = m_CameraTarget.transform.position.y + m_CameraTargetDir.y * m_CameraTargetDis;
		float cameraZ = m_CameraTarget.transform.position.z + m_CameraTargetDir.z * m_CameraTargetDis;
		m_PlayCamera.transform.position = new Vector3 (cameraX, cameraY, cameraZ);
		Quaternion q = Quaternion.identity;
		q.eulerAngles = new Vector3 (0, 180, 0);
		m_PlayCamera.transform.rotation = q;

		//testRay = Camera.main.ViewportPointToRay ( Camera.main.WorldToViewportPoint ( m_CameraTarget.transform.position ) );
		//m_PlayCamera.transform.LookAt (m_CameraTarget.transform.up);
		//m_PlayCamera.transform.forward = testRay.direction;

		m_CurrentPosition = m_CameraTarget.transform.position;
		m_CameraHeight = 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_LastPosition = m_CurrentPosition;
		m_CurrentPosition = m_CameraTarget.transform.position;


	}
	
	void LateUpdate ()
	{
		if (m_IsUpdateCamera) 
		{
			//m_PlayCamera.transform.position = transform.position;
			//m_PlayCamera.transform.rotation = transform.rotation;
		}

		TargetMiddleFollow ();
	}

	void CameraRotation ()
	{
	}

	void TargetMiddleFollow ()
	{
		/*
		Vector3 targetViewportPoint = Camera.main.WorldToViewportPoint (new Vector3 (m_CameraTarget.transform.position.x, m_CameraTarget.transform.position.y-1, m_CameraTarget.transform.position.z));
		testRay = Camera.main.ViewportPointToRay (targetViewportPoint);
		//m_PlayCamera.transform.LookAt (m_CameraTarget.transform.up);
		//m_PlayCamera.transform.position = testRay.origin;
		Debug.Log ("TargetMiddleFollow " + testRay.origin);
		m_PlayCamera.transform.forward = testRay.direction;

		float extendFactor = 5;
		float extendX = m_CameraTarget.transform.position.x - extendFactor * testRay.direction.x;
		float extendY = m_CameraTarget.transform.position.y - extendFactor * testRay.direction.y;
		float extendZ = m_CameraTarget.transform.position.z - extendFactor * testRay.direction.z;
		m_PlayCamera.transform.position = new Vector3 (extendX, extendY, extendZ);
		*/

		Vector3 origin = m_PlayCamera.transform.position;
		m_PlayCamera.transform.position = new Vector3 (origin.x + m_CurrentPosition.x - m_LastPosition.x, m_CameraTarget.transform.position.y + m_CameraHeight, origin.z + m_CurrentPosition.z - m_LastPosition.z);
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay (testRay);
	}


	#region get set
	public GameObject PlayCamera
	{
		get
		{
			return m_PlayCamera;
		}
	}
	#endregion
}
