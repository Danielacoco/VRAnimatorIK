﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIKChain : MonoBehaviour {

	// array of joints and distance, we could generate distances on the fly, but for now they are determined by the user
	public Transform[] joints;
	public float[] distances;

	public GameObject targetObj;
	public int numJoints;

	public float tolerance = 0.1f;

	// base position
	public Vector3 origin;
	// target obj
	public Vector3 Target;



	private float totalLength = 0;

	private float distToTarget;



	// Use this for initialization
	void Start () {
		foreach (float l in distances) {
			totalLength += l;
		}
		numJoints = joints.Length;
		Target = targetObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
		//SolveChainFABRIK ();
	}

	public void SolveChainFABRIK(){
		
		distToTarget = (Target - origin).magnitude;

		if (distToTarget > totalLength) {
			//target too far, out of reach
			for (int i = 0; i < numJoints - 1; i++) {
				float r = (Target - joints [i].position).magnitude;
				float lambda = (distances [i] / r);
				joints [i + 1].position = (1 - lambda) * joints [i].position + lambda * Target;
			}
		} else {
			// target is reachable, we have our origin joint saved
			// check whether the distance btw end effector and target is grater than our tolerance 
			int iteration = 0;
			float endToTargetDist =  (Target - joints[numJoints-1].position).magnitude;
			while (endToTargetDist > tolerance && iteration < 20) {
                ForwardStep();
                BackwardStep ();
				
				endToTargetDist = (Target - joints[numJoints-1].position).magnitude;
				iteration++;
			}


		}
	
	}

	public void ForwardStep(){
		//set first joint to origin
		joints [0].position = origin;

		for (int i = 0; i < numJoints - 2; i++) {
			float dist = (joints [i + 1].position - joints [i].position).magnitude;
			float lambda = (distances [i] / dist);
			// new joint position
			joints[i+1].position = (1-lambda)*joints[i].position + lambda * joints[i+1].position;

		}

	}

	public void BackwardStep(){
		// set end effector as target
		joints [numJoints - 1].position = Target;
		for (int i = numJoints - 2; i >= 0; i--) {
			float dist = (joints[i + 1].position - joints [i].position).magnitude;
			float lambda = distances [i] / dist;
			joints[i].position = (1-lambda)*joints[i+1].position + lambda * joints[i].position;
		}
		
	
	}

}
