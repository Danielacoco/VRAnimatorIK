using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FABRIKMulti : MonoBehaviour {
	
	public FABRIKChain[] chains;

	//Store sub-base positions in this array

	private Vector3[] subBasePos = new Vector3[5];
	private Vector3 centroid = new Vector3 (0.0f, 0.0f, 0.0f);

	int numChains;
	// Use this for initialization

	void Awake(){
		numChains = chains.Length;
	}

	void Start () {
		chains [0].Target = chains [0].joints [chains [0].numJoints - 1].position;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		SolveFABRIKMulti ();
		
	}

	void SolveFABRIKMulti(){
		//set origins and targets
		for (int i = 1; i <= 5; i++){
			chains[i].Target = chains [i].targetObj.transform.position;
			chains [i].origin = chains [1].origin;
		}
//		chains [1].Target = chains [1].targetObj.transform.position;
//		chains [2].Target = chains [2].targetObj.transform.position;

		// set origin 
		//chains [2].origin = chains[1].origin;

		// saving sub-base original position to calculate centroid
		Vector3 originalPos = chains [1].joints [0].position;

		for (int j = 1; j <= 5; j++) {
			//reser subbase position everytime
			chains [j].joints[0].position = originalPos;
			chains [j].BackwardStep ();
			// save new subbase position in array
			subBasePos [j - 1] = chains [j].joints [0].position;
		}
		//chains [2].BackwardStep ();
		//Vector3 posChain2 = (chains[2].joints[0].position);

//		chains [1].joints[0].position = originalPos;
//		chains [1].BackwardStep ();

		Vector3 posChain1 = (chains [1].joints [0].position );


		foreach (Vector3 v in subBasePos) {
			centroid += v;
		
		}

		centroid = centroid / (subBasePos.Length);

		//set main chain's target to centroid position for subbase
		chains [0].Target= centroid;
		chains [0].SolveChainFABRIK ();

		chains [1].origin = chains [0].joints[chains[0].numJoints-1].position;
		chains [1].SolveChainFABRIK();
		for (int k = 2; k<=5; k++){
			chains [k].origin = chains[1].origin;
			chains [k].SolveChainFABRIK();
		}


//		chains [1].SolveChainFABRIK ();
//
//		chains [2].SolveChainFABRIK();

	}
}
