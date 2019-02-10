using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BezierLineData
{

    public Vector3 startPoint = new Vector3(-0.0f, 0.0f, 0.0f);
    public Vector3 endPoint = new Vector3(-2.0f, 2.0f, 0.0f);
    public Vector3 startTangent = Vector3.zero;
    public Vector3 endTangent = Vector3.zero;

}
