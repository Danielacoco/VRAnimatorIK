using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour {


    public CatmullRomCurve curve;

    public float duration;

    private float progress;

    public bool play = false;

    // Use this for initialization
    void Start()
    {
        duration = curve.segDuration * curve.controlPoints.Count;

    }


    // Update is called once per frame
    void Update()
    {
        if (play &&  curve.controlPoints.Count == 5)
        {
            duration = curve.segDuration * curve.getNumCurves();
            progress += Time.deltaTime;
            float boundedTime = progress % duration;

            int curveNum = (int)(boundedTime / curve.segDuration);

            float timeParam = (boundedTime - curveNum * curve.segDuration) / curve.segDuration;

            //Debug.Log("ABOUT TO EVAL CURVE");
            //Debug.Log(boundedTime + " boundedTime");
            //Debug.Log(duration + " duration");
            //Debug.Log(curveNum + "curveNum");
            //Debug.Log(timeParam + "timeParam");
            if (play && curve.controlPoints.Count == 5)
            {
                transform.localPosition = curve.EvalCurvePointSeg(timeParam, curveNum);
            }
           
        }
    }
}
