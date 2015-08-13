using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputJoyStick : MonoBehaviour, IInputInterface
{
	private PlayerController 						m_PlayerController;
	private IAnimator								m_AnimatorController;

	const KeyCode KeyBoard_Move_Up = KeyCode.W;// | KeyCode.UpArrow;
	const KeyCode KeyBoard_Move_Down = KeyCode.S;// | KeyCode.DownArrow;
	const KeyCode KeyBoard_Move_Left = KeyCode.A;// | KeyCode.LeftArrow;
	const KeyCode KeyBoard_Move_Right = KeyCode.D;// | KeyCode.RightArrow;
	const KeyCode KeyBoard_Cross = KeyCode.J; //Cross
	const KeyCode KeyBoard_Circle = KeyCode.K; //Circle
	const KeyCode KeyBoard_Square = KeyCode.U; //Squre
	const KeyCode KeyBoard_Triangle = KeyCode.I; //Triangle
	KeyCode[] keyCodeArray = { 
								KeyBoard_Move_Up, 
								KeyBoard_Move_Down,
								KeyBoard_Move_Left,
								KeyBoard_Move_Right,
								KeyBoard_Cross,
								KeyBoard_Circle,
								KeyBoard_Square,
								KeyBoard_Triangle,
							 };

	private float									m_Count;
	
	class InputKeyInfo
	{
		public KeyCode		m_KeyCode = KeyCode.None;
		public float		m_time;
	}
	private List<InputKeyInfo>						m_keyInputList;
	private int										m_maxKeyInputNum;

	//void Awake ()
	public InputJoyStick ()
	{

		m_Count = 0;
		m_keyInputList = new List<InputKeyInfo> ();
		m_maxKeyInputNum = 5;
	}

	void Start ()
	{
	}

	/*
	public void Update ()
	{
		InputListener ();
	}*/

	#region interface InputInterface
	//每一帧的输入事件处理，配合按钮监听取得数据
	public void InputListener ()
	{
		InputRecordListener ();

		InputListenerMove ();

		InputListernerCombo ();
		//TestInput ();
	}
	
	public void SetController (IAnimator animatorController, MonoBehaviour playerController)
	{
		m_AnimatorController = animatorController;
		m_PlayerController = playerController as PlayerController;
	}
	#endregion

	//record the input info
	void InputRecordListener ()
	{
		for (int i=0; i<keyCodeArray.Length; ++i) 
		{
			if (Input.GetKeyDown ( keyCodeArray[i] ))
			{
				InputKeyInfo info = new InputKeyInfo ();
				info.m_KeyCode = keyCodeArray[i];
				info.m_time = Time.time;
				m_keyInputList.Add (info);

				TrimInputList ();
			}
		}
	}

	void TrimInputList ()
	{
		if (m_keyInputList.Count > 10) 
		{
			m_keyInputList.RemoveAt (m_keyInputList.Count - 1);
		}
	}

	float AngleToRadian (float angle)
	{
		return Mathf.PI * angle / 180.0f;
	}

	//move listener
	void InputListenerMove ()
	{
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");
		float angle = Camera.main.transform.rotation.eulerAngles.y;

		//Debug.Log ("InputListenerMove angle:" + angle);

		float newX = Mathf.Cos (AngleToRadian (angle)) * x + Mathf.Sin (AngleToRadian (angle)) * z;
		float newY = Mathf.Sin (AngleToRadian (angle)) * x * (-1) + Mathf.Cos (AngleToRadian (angle)) * z;

		if (0 != x || 0 != z)
		{
			m_AnimatorController.Move (0.5f);
			m_PlayerController.MoveAndTurn (new Vector3 (newX , 0, newY ));
		} else 
		{
			m_AnimatorController.StopMove ();
		}

		if (Input.GetKeyDown (KeyBoard_Cross))
		{
			m_AnimatorController.SetTriggle (AnimatorConfig.PARAM_TRIGGER_JUMP_START);
			m_AnimatorController.SetBool (AnimatorConfig.PARAM_BOOL_IN_AIR, true);
			m_PlayerController.JumpAndMove ();
		}
	}

	//△□◎X 
	// 1 □=>□=>△ 2 △=>□=>△ 
	void InputListernerCombo ()
	{
		if (Input.GetKeyDown (KeyBoard_Triangle)) 
		{
			m_AnimatorController.ActionJoyStickTriangle ();
		} 
		else if (Input.GetKeyDown (KeyBoard_Square)) 
		{
			m_AnimatorController.ActionJoyStickSquare ();
		} else {}
	}
	
	void AddToInputKeyList (InputKeyInfo iki)
	{
		if (m_keyInputList.Count >= 6)
			m_keyInputList.RemoveAt (0);
		
		m_keyInputList.Add (iki);
	}
	
}
