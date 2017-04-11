using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatmullRomCurve : MonoBehaviour {

    public bool catmull;
    public float segDuration = 1;
    public List<Transform> controlPoints;
    //public LineRenderer lineRenderer;

    private int numCurves = 0;
    private int layerOrder = 0;
    private int numSegments = 50;
    private int numPoints;

    // Use this for initialization
    void Start()
    {
        //if (!lineRenderer)
        //{
        //    lineRenderer = GetComponent<LineRenderer>();
        //}
        //lineRenderer.sortingOrder = layerOrder;

        if (catmull)
        {
            numCurves = (int)controlPoints.Count - 1;
        }
        else
        {
            numCurves = (int)controlPoints.Count + 1;
        }
        numPoints = (int)controlPoints.Count;

        DrawCurve();
    }

    // Update is called once per frame
    void Update()
    {

        DrawCurve();
    }

    public int getNumCurves()
    {
        return numCurves;
    }

    void DrawCurve()
    {
        for (int i = 0; i < numCurves; i++)
        {
            //if (i == 0 || i == controlPoints.Length - 2) continue;
            for (int seg = 1; seg <= numSegments; seg++)
            {
                int indexStart = getStartIndex(i);
                int indexEnd = getEndIndex(i);
                float time = seg / (float)numSegments;
                Vector3 point = EvalCurvePoint(time, indexStart, indexEnd);
                // Debug.Log((i * numSegments + seg) + "NUMSEGS");
                //lineRenderer.numPositions = (i * numSegments) + seg;
                //lineRenderer.SetPosition((i * numSegments) + seg - 1, point);
            }
        }
    }

    public Vector3 EvalCurvePointSeg(float time, int segNum)
    {
        return EvalCurvePoint(time, getStartIndex(segNum), getEndIndex(segNum));
    }
    public int getStartIndex(int numSeg)
    {
        if (catmull)
        {
            return numSeg - 1;
        }
        else
        {
            return numSeg - 2;
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

    public Vector3 EvalCurvePoint(float time, int start, int end)
    {

        //Debug.Log(Mathf.Max(start, 0) + "MAX");
        //Debug.Log(start + " sTART");
        Vector3 p1 = controlPoints[Mathf.Max(start, 0)].position;
        Vector3 p2 = controlPoints[Mathf.Max(start + 1, 0)].position;
        Vector3 p3 = controlPoints[Mathf.Min(end - 1, controlPoints.Count - 1)].position;
        Vector3 p4 = controlPoints[Mathf.Min(end, controlPoints.Count - 1)].position;
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
