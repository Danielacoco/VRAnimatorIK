using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CatmullRomCurve : MonoBehaviour
{

    public bool catmull;
    public float segDuration = 1;
    public List<Vector3> controlPoints;
    //public LineRenderer lineRenderer;

    private int numCurves = 0;
    private int layerOrder = 0;
    private int numSegments = 50;
    private int numPoints;
    public int[] indeces;

    // Use this for initialization
    void Start()
    {

        if (catmull)
        {
            numCurves = (int)controlPoints.Count;
        }
        else
        {
            numCurves = (int)controlPoints.Count + 1;
        }
        numPoints = (int)controlPoints.Count;

        // DrawCurve ();
    }

    // Update is called once per frame
    void Update()
    {
        numPoints = (int)controlPoints.Count;
        // DrawCurve();
    }

    public int getNumCurves()
    {
        return numCurves = (int)controlPoints.Count;
    }

    public void clearControlPoints()
    {
        controlPoints.Clear();
    }

    //void DrawCurve()
    //{
    //    for (int i = 0; i < numCurves; i++)
    //    {
    //        //if (i == 0 || i == controlPoints.Length - 2) continue;
    //        for (int seg = 1; seg <= numSegments; seg++)
    //        {
    //            int indexStart = getStartIndex(i);
    //            int indexEnd = getEndIndex(i);
    //            float time = seg / (float)numSegments;
    //            Vector3 point = EvalCurvePoint(time, indexStart, indexEnd);
    //           // Debug.Log((i * numSegments + seg) + "NUMSEGS");
    //            lineRenderer.numPositions = (i * numSegments) + seg;
    //            lineRenderer.SetPosition((i * numSegments) + seg-1, point);
    //        }
    //    }
    //}

    //public void ExtendCurve(ControlPoint cp)
    //{
    //    this.controlPoints.Add(cp.transform);
    //    this.numPoints++;
    //    this.numCurves++;
    //}

    public Vector3 EvalCurvePointSeg(float time, int segNum)
    {
      
        getIndexArray(segNum);
        
        return EvalCurvePoint(time);
        //Vector3 p = (controlPoints[0].position);
        //return p;
    }
    public void getIndexArray(int segNum)
    {
        int start = segNum - 1;
        //int end = segNum + 2;
        int currNum = 0;

        int i = 0;
        while (i < 4)
        {
            //Debug.Log(i + " index value");
            //Debug.Log(start + "startValue");
            if (start < 0)
            {
                currNum = numPoints + start;
                indeces[i] = currNum;
              
            }
            else if (start > controlPoints.Count - 1)
            {
                currNum = start - numPoints;
                indeces[i] = currNum;
             
            } else {
                indeces[i] = start;
                
               
            }
            start++;
            i++;
            

        }

    }

    public int getEndIndex(int numSeg)
    {
        if (catmull)
        {
            return numSeg + 2;
        }
        else
        {
            return numSeg + 1;
        }

    }

    public Vector3 EvalCurvePoint(float time)
    {

        //Debug.Log(Mathf.Max(start, 0) + "MAX");
        //Debug.Log(start + " sTART");
        //Debug.Log(controlPoints[indeces[0]]);
        //Debug.Log(controlPoints[indeces[1]]);
        //Debug.Log(controlPoints[indeces[2]]);
        //Debug.Log(controlPoints[indeces[3]]);
        Vector3 p1 = controlPoints[indeces[0]];
        Vector3 p2 = controlPoints[indeces[1]];
        Vector3 p3 = controlPoints[indeces[2]];
        Vector3 p4 = controlPoints[indeces[3]];
        //The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
        Vector3 a = 2f * p2;
        Vector3 b = p3 - p1;
        Vector3 c = 2f * p1 - 5f * p2 + 4f * p3 - p4;
        Vector3 d = -p1 + 3f * p2 - 3f * p3 + p4;

        //The cubic polynomial: a + b * t + c * t^2 + d * t^3
        Vector3 pos1 = 0.5f * ((2 * p2) + (-p1 + p3) * time + (2 * p1 - 5 * p2 + 4 * p3 - p4) * Mathf.Pow(time, 2) + (-p1 + 3 * p2 - 3 * p3 + p4) * Mathf.Pow(time, 3));
        Vector3 pos = 0.5f * (a + (b * time) + (c * time * time) + (d * time * time * time));

        return pos1;
    }


}
