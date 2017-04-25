using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCapture : MonoBehaviour {

    //private SteamVR_TrackedObject trackedObject;

    //private SteamVR_Controller.Device Controller
    //{
    //    get { return SteamVR_Controller.Input((int)trackedObject.index); }
    //}

    // array that contains our end effectors        
    public Walker[] targets;
    public GameObject[] geom;

    // total number of poses allowed;
    public int numCaptured;

    //public float sightLength = 100.0f;

    //public GameObject selectedObj;

    /* 2d array that will contain the saved positions by the user. These will be a curves control
     for each end effector */

    //private Vector3[,] targetPositions;

    private int capturedSoFar;


    private void Awake()
    {
       

       // targetPositions = new Vector3[targets.Length, numCaptured];
    }
    // Use this for initialization
    void Start()
    {
    }
    public void CapturePos()
    {
        if (capturedSoFar < numCaptured)
        {
            int i = 0;
            foreach (Walker w in targets)
            {
                w.curve.controlPoints.Add(geom[i].transform.position);
                i++;
            }
            capturedSoFar++;
        } else
        {
            //display something on UI?
        }
    }


    private void Update()
    {
    }

    public void PlayAnim()
    {
        foreach (Walker w in targets)
        {
            //Debug.Log("CALLED");
            w.play = true;

        }

    }

    public void PauseAnim()
    {
        foreach (Walker w in targets)
        {
            Debug.Log("CALLED");
            w.play = false;

        }

    }

    public void ResetAnim()
    {
        foreach (Walker w in targets)
        {
            w.play = false;
            w.curve.clearControlPoints();

        }
    }


}
