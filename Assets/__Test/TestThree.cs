using UnityEngine;
using System.Collections;

public class TestThree : MonoBehaviour {
	public GameObject buoyant;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit raycastHit;
		Vector3 origin = transform.localPosition;
		origin = new Vector3 (origin.x, origin.y + 1.2f, origin.z);
		Vector3 direction = new Vector3 (0, -1, 0);

		if (Physics.Raycast (origin, direction, out raycastHit, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Ground"))) 
		{
			//Debug.Log ("TestThree detect ground " + + raycastHit.point.x + " " + raycastHit.point.y + " " + + raycastHit.point.z);

			buoyant.transform.position = raycastHit.point;
		}
	}
}
