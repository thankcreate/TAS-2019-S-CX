using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BezierPath))]
public class DrawBezierGizmo : Editor
{
    
    private void OnSceneGUI()
    {
        
        BezierPath path = target as BezierPath;
        var count = path.pointList.Count;

        DrawPathRoot(path);
        DrawSmoothAll(path);

        for (int i = 1; i < path.pointList.Count; i++)
        {
            var startPoint = path.pointList[i - 1];
            var endPoint = path.pointList[i];

           

            if (i == 1)
            {
                if (startPoint == null)
                    Debug.Log("null");

                DrawPositionHandle(path,  i - 1);
                DrawTangentHandle(path,  false, i - 1);
            }

            DrawPositionHandle(path,  i);            
            DrawTangentHandle(path,  true, i);

            if(i != path.pointList.Count - 1)
            {
                DrawTangentHandle(path,  false, i);
            }

            DrawBezier(path, startPoint, endPoint);
        }

        if (path.isLoop && path.pointList.Count > 1)
        {
            DrawTangentHandle(path, true, 0);
            DrawTangentHandle(path, false, path.pointList.Count - 1);
            DrawBezier(path, path.pointList[path.pointList.Count - 1], path.pointList[0]);
        }

    }

    private void DrawTangentLines(BezierPath path, bool isIn, int index)
    {
        if (!path.GetComponent<BezierConfig>().showTangentLines)
            return;

        
        Handles.color = Color.cyan;
        Handles.DrawLine(path.GetPointWorldPosition(index), path.GetTangentWorldPosition(index, isIn ? 0 : 1));
    }


    private void DrawSmoothAll(BezierPath path)
    {
        var config = path.GetComponent<BezierConfig>();



        Handles.BeginGUI();
        
        if (GUI.Button(new Rect(10, 10, 80, 40), "Smooth"))
        {
            SmoothAll(path);
            Debug.Log("Got it to work.");
        }

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;

        // show tangent
        // show node

        var tResNode = GUI.Toggle(new Rect(10, 55, 300, 20), config.showNodeHandle, "Show Node Handle", config.skin.toggle);
        config.showNodeHandle = tResNode;

        var tResTangent = GUI.Toggle(new Rect(10, 70, 300, 20), config.showTangentHandle, "Show Tangent Handle", config.skin.toggle);
        config.showTangentHandle = tResTangent;

        var tResTangentLine = GUI.Toggle(new Rect(10, 85, 300, 20), config.showTangentLines, "Show Tangent Lines", config.skin.toggle);
        config.showTangentLines = tResTangentLine;


        Handles.EndGUI();
    }

    private void SmoothAll(BezierPath path)
    {
        for(int i = 0; i < path.pointList.Count; i++)
        {
            var p = path.pointList[i];
            p.tangents[1] = -p.tangents[0];
        }
    }

    private void DrawBezier(BezierPath path, BezierPointData startPoint, BezierPointData endPoint)
    {
        Handles.DrawBezier(
                startPoint.position + path.transform.position,
                endPoint.position + path.transform.position,
                startPoint.tangents[1] + startPoint.position + path.transform.position,
                endPoint.tangents[0] + endPoint.position + path.transform.position, Color.red, null, 2f);
    }

    private void DrawPathRoot(BezierPath path)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.blue;
        Handles.Label(path.transform.position, "PathRoot");
    }


    private void DrawPositionHandle(BezierPath path, int index)
    {
        if (!path.GetComponent<BezierConfig>().showNodeHandle)
            return;

        BezierPointData point = path.pointList[index];
        point.position = Handles.PositionHandle(
                    point.position + path.transform.position, Quaternion.identity) - path.transform.position;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        Handles.Label(point.position + path.transform.position, index + "" , style);



       
    }

    private void DrawTangentHandle(BezierPath path, bool isIn, int index)
    {
        DrawTangentLines(path, isIn, index);

        if (!path.GetComponent<BezierConfig>().showTangentHandle)
            return;

        BezierPointData point = path.pointList[index];
        int tangentIndex = isIn ? 0 : 1;
        point.tangents[tangentIndex] = Handles.PositionHandle(point.tangents[tangentIndex] + path.transform.position + point.position, Quaternion.identity)
            - path.transform.position - point.position;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.cyan;
        Handles.Label(point.tangents[tangentIndex] + path.transform.position + point.position, index + (isIn ? "_in" : "_out"), style);



    }

    
}
