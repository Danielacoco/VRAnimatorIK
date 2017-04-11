using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCapture : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;

    //private SteamVR_Controller.Device Controller
    //{
    //    get { return SteamVR_Controller.Input((int)trackedObject.index); }
    //}

    // array that contains our end effectors        
    public GameObject[] targets;

    // total number of poses allowed;
    public int numCaptured;

    public float sightLength = 100.0f;

    public GameObject selectedObj;

    /* 2d array that will contain the saved positions by the user. These will be a curves control
     for each end effector */

    private Vector3[,] targetPositions;

    private int capturedSoFar;


    private void Awake()
    {
       

        targetPositions = new Vector3[targets.Length, numCaptured];
    }
    // Use this for initialization
    void Start()
    {
    }
    void CapturePos()
    {
        if (capturedSoFar < numCaptured)
        {
            int i = 0;
            foreach (GameObject eef in targets)
            {
                targetPositions[capturedSoFar, i] = eef.transform.position;
                i++;
            }
            capturedSoFar++;
        }
    }


    private void Update()
    {
    }

}
