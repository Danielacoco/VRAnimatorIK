using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleChainCase : MonoBehaviour {

	public FABRIKChain chain;

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
		chain.Target = chain.targetObj.transform.position;
		chain.SolveChainFABRIK ();
		
	}
}
