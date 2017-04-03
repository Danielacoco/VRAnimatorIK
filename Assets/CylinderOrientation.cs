using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderOrientation : MonoBehaviour {

	public Transform lookAtPoint;

	public float speed;

	// Use this for initialization
	void Start () {

		Vector3 newRotation = (lookAtPoint.position - transform.position);
		//Quaternion toRot = Quaternion.FromToRotation (transform.up, newRotation);
		transform.rotation = Quaternion.FromToRotation (transform.up, newRotation);
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newRotation = (lookAtPoint.position - transform.position);
		//Quaternion toRot = Quaternion.FromToRotation (transform.up, newRotation);
		transform.rotation = Quaternion.FromToRotation (transform.up, newRotation);
	}
}
