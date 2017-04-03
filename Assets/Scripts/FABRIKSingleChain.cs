using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIKSingleChain : MonoBehaviour {

	public Transform[] joints;


	public float tolerance = 0.1f;

	public Transform origin;
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
		distToTarget = (Target.position - origin.position).magnitude;

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
				//do forward
				//do backward
			}


		}
	
	}
}
