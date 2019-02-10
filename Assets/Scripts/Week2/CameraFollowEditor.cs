using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(CameraFollow))]
public class CameraFollowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CameraFollow cf = (CameraFollow)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Make new point"))
        {
            Debug.Log("Button pressed.  Congrats!!!");
            cf.AddPoint();
        }
    }
}
