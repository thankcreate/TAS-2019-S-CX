using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UnityBeizer))]
public class ButtonsForLAUB : Editor
{
    public override void OnInspectorGUI()
    {
        UnityBeizer myLAUB = (UnityBeizer)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Make new curve"))
        {
            Debug.Log("Button pressed");
            BezierExample be = myLAUB.myModel.AddComponent<BezierExample>();

            if(myLAUB.curveList.Count > 0)
            {

                var last = myLAUB.curveList[myLAUB.curveList.Count - 1];
                be.startPoint = last.endPoint;
                be.startTangent = -last.endTangent;
                be.endPoint = last.endPoint;
                be.endTangent = last.endTangent;
            }

            myLAUB.curveList.Add(be);
        }      
    }
}