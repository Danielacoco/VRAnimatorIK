﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIKMulti : MonoBehaviour {
	
	public FABRIKChain[] chains;
	int numChains;
	// Use this for initialization

	void Awake(){
		numChains = chains.Length;
		//Debug.Log (numChains);

//		chains [1].Target = chains [1].targetObj.transform.position;
//		chains [2].Target = chains [2].targetObj.transform.position;
	}
	void Start () {
		chains [0].Target = chains [0].joints [chains [0].numJoints - 1].position;
		//SolveFABRIKMulti ();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		SolveFABRIKMulti ();
		
	}

	void SolveFABRIKMulti(){
		chains [1].Target = chains [1].targetObj.transform.position;
		chains [2].Target = chains [2].targetObj.transform.position;
		// set origin 
		chains [1].origin = chains[1].joints[0].position;
		//Debug.Log ("after 1");
		chains [2].origin = chains[1].origin;
		//Debug.Log ("after 2");
		Vector3 originalPos = chains [2].joints [0].position;

		chains [2].BackwardStep ();
		Debug.Log ("backwardchain2 3");
		Debug.Log (chains [2].joints [0].position);

		Vector3 posChain2 = (chains[2].joints[0].position);

		chains [1].joints[0].position = originalPos;
		chains [1].BackwardStep ();
		Debug.Log ("backwardchain1 3");
		Debug.Log (chains [1].joints [0].transform.position);
		Vector3 posChain1 = (chains [1].joints [0].position );


		Vector3 centroid = (posChain1 + posChain2) / 2.0f;
		Debug.Log (centroid);

//		chains [1].origin = centroid;
//		chains [1].joints [0].position = centroid;
//		chains [2].joints [0].position = centroid;

			
		//chains [1].joints [0].position = 0;
		chains [0].Target= centroid; //-------->bug here?
		//Debug.Log ("after updating target");
		chains [0].SolveChainFABRIK ();
		//chains [0].BackwardStep ();
		//chains [0].ForwardStep ();
		chains [1].origin = chains [0].joints[chains[0].numJoints-1].position;
		chains [2].origin = chains[1].origin;
		//Debug.Log ("solving finalchain backwards and forwards");
		//chains [1].BackwardStep ();
		//chains [1].ForwardStep ();
		chains [1].SolveChainFABRIK ();
		//Debug.Log ("forward1");
		//chains [2].BackwardStep ();
		//chains [2].ForwardStep ();
		chains [2].SolveChainFABRIK();
		//Debug.Log ("forward 2");


		
	}
}