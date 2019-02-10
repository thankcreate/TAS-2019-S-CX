using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


public class UnityBeizer : MonoBehaviour {

    public GameObject maker;
    public BezierExample bezEx;
    public GameObject myModel;
    public List<BezierExample> curveList;


    public void Start()
    {        
        putPointsOnCurve();
    }

    void putPointsOnCurve()
    {
        // Run through 100 points, and place a marker at those points on the bezier curve
        // Step 1: For loop through 100 points between 0 and 1
        // Step 2: Pass that fraction to a curve calc to find the resultant V3
        // Step 3: Place a marker at that V3

        for (int i = 0; i <= 100; i++)
        {
            float t = i / 100.0f;
            var pointOnCurve = CalculateBeizer(bezEx, t);
            Instantiate(maker, pointOnCurve, Quaternion.identity, null);
        }
    }

    Vector3 CalculateBeizer(BezierExample curvData, float t)
    {
        var a = curvData.startPoint;
        var b = curvData.startTangent;
        var c = curvData.endTangent;
        var d = curvData.endPoint;

        var ab = Vector3.Lerp(a, b, t);
        var bc = Vector3.Lerp(b, c, t);
        var cd = Vector3.Lerp(c, d, t);

        var abc = Vector3.Lerp(ab, bc, t);
        var bcd = Vector3.Lerp(bc, cd, t);

        var abcd = Vector3.Lerp(abc, bcd, t);

        return abcd;
    }
}

