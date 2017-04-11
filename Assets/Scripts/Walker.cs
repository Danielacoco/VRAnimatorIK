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
        duration = curve.segDuration * curve.controlPoints.Count - 1;

    }


    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            duration = curve.segDuration * curve.getNumCurves();
            progress += Time.deltaTime;
            float boundedTime = progress % duration;

            int curveNum = (int)(boundedTime / curve.segDuration);

            float timeParam = (boundedTime - curveNum * curve.segDuration) / curve.segDuration;

            transform.localPosition = curve.EvalCurvePointSeg(timeParam, curveNum);
        }
    }
}
