using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIKSingleChain : MonoBehaviour {

	public Transform[] joints;


	public float tolerance = 0.1f;

	public Vector3 origin;
	public float[] distances;


	private float totalLength = 0;

	private float distToTarget;

	public Transform Target;

	// Use this for initialization
	void Start () {

		foreach (float l in distances) {
			totalLength += l;
		}

		SolveSingleChainFABRIK ();
	}
	
	// Update is called once per frame
	void Update () {
		SolveSingleChainFABRIK ();
		
	}

	void SolveSingleChainFABRIK(){
		distToTarget = (Target.position - origin).magnitude;

		if (distToTarget > totalLength) {
			//target too far, out of reach
			for (int i = 0; i < joints.Length - 1; i++) {
				float r = (Target.position - joints [i].position).magnitude;
				float lambda = (distances [i] / r);
				joints [i + 1].position = (1 - lambda) * joints [i].position + lambda * Target.position;
			}
		} else {
			// target is reachable, we have our origin joint saved
			// check whether the distance btw end effector and target is grater than our tolerance 
			float endToTargetDist =  (Target.position - joints[joints.Length-1].position).magnitude;
			while (endToTargetDist > tolerance) {
				ForwardStep ();
				BackwardStep ();
				endToTargetDist = (Target.position - joints[joints.Length-1].position).magnitude;
			}


		}
	
	}

	void ForwardStep(){
		joints [joints.Length - 1].position = Target.position;
		for (int i = joints.Length - 2; i >= 0; i--) {
			//Debug.Log (i);
			float dist = (joints[i + 1].position - joints [i].position).magnitude;
			float lambda = distances [i] / dist;
			// find new joint position 
			joints[i].position = (1-lambda)*joints[i+1].position + lambda * joints[i].position;
		}
	}

	void BackwardStep(){
		//set first joint to origin
		joints [0].position = origin;

		for (int i = 0; i < joints.Length - 2; i++) {
			float dist = (joints [i + 1].position - joints [i].position).magnitude;
			float lambda = (distances [i] / dist);
			// new joint position
			joints[i+1].position = (1-lambda)*joints[i].position + lambda * joints[i+1].position;
			
		}
	
	}

}
