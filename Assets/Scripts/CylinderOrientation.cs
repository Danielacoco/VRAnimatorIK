using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderOrientation : MonoBehaviour {

	public Transform lookAtPoint;
	public Transform startPoint;

	public float speed;

	// Use this for initialization
	void Start () {

		SetPosition (startPoint.position, lookAtPoint.position);
		
	}
	
	// Update is called once per frame
	void Update () {
		SetPosition (startPoint.position, lookAtPoint.position);
//		Vector3 newRotation = (lookAtPoint.position - transform.position);
//		//Quaternion toRot = Quaternion.FromToRotation (transform.up, newRotation);
//		transform.rotation = Quaternion.Lerp(transform.up, newRotation, );
	}

	void SetPosition(Vector3 start, Vector3 end){
		var direction = end - start;
		var midd = (direction) * 0.5f + start;
		transform.position = midd;
		transform.rotation = Quaternion.FromToRotation (Vector3.up, direction);
	}
}
