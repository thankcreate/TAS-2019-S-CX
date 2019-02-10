using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BezierPointData  {

    public Vector3 position = new Vector3(-0.0f, 0.0f, 0.0f);
    public Vector3[] tangents = new Vector3[2];   

}
