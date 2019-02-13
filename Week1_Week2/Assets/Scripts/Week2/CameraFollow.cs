using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public BezierPath path;
    public Camera flyOverCamera;


    public int curIndex = 0;
    public float curRatio = 0;
    
	// Use this for initialization
	void Start () {
       

    }

    
       	
	// Update is called once per frame
	void Update () {
        StartFlyOver();
    }

    bool needRun = true;

    public void StartFlyOver()
    {
        if (!needRun)
            return;

        int count = path.pointList.Count;
        var prev = flyOverCamera.transform.position;
        curRatio = curRatio + Time.deltaTime * 0.1f;
        if (curRatio >= 1)
        {
            curIndex++;
            curRatio = 0;

            if (curIndex >= path.pointList.Count - 2 && !path.isLoop)
            {
                needRun = false;
                return;
            }
        }

        var posi = CalculateBezierPositionWithRatio(curRatio, path.pointList[curIndex % count], path.pointList[(curIndex + 1) % count]);

       
        Debug.Log(posi);
        flyOverCamera.transform.position = posi;

        if(prev != posi)
        {
            var rela = posi - prev;
            var rot = Quaternion.LookRotation(rela, Vector3.up);
            flyOverCamera.transform.rotation = rot;
        }
    }

    Vector3 CalculateBezierPositionWithRatio(float ratio, int point1, int point2)
    {
        float r = Mathf.Clamp(ratio, 0, 1);
        return CalculateBezierPositionWithRatio(r, path.pointList[point1], path.pointList[point2]);
    }

    Vector3 CalculateBezierPositionWithRatio(float ratio, BezierPointData point1, BezierPointData point2)
    {
        float r = Mathf.Clamp(ratio, 0, 1);
        return CalculateBezierPositionWithRatio(r,
            path.GetPointWorldPosition(point1),
            path.GetPointWorldPosition(point2),
            path.GetTangentWorldPosition(point1, 1),
            path.GetTangentWorldPosition(point2, 0));
    }

    Vector3 CalculateBezierPositionWithRatio(float ratio, Vector3 start, Vector3 end, Vector3 tangentStart, Vector3 tangentEnd) {
        
        Vector3[] input = new Vector3[] { start, tangentStart, tangentEnd, end };
        Vector3[] tempIter = new Vector3[input.Length - 1];
        
        for(float length = input.Length; length > 1; length--)
        {           
            for(int i = 0; i < length - 1; i++)
            {
                tempIter[i] = input[i] + (input[i + 1] - input[i]) * ratio;
            }
            input = tempIter;
        }

        return tempIter[0];
    }

    public void AddPoint()
    {
        var list = path.pointList;
       
        var newNode = new BezierPointData();

        if(list.Count > 0)
        {
            newNode.position = list[list.Count - 1].position;
        }
        
        if(list.Count > 1)
        {
            var bez = CalculateBezierPositionWithRatio(0.9f, list[list.Count - 2], list[list.Count - 1]);
            var lastNodeWorldPosi = path.GetPointWorldPosition(list[list.Count - 1]);
            var newWorldPosi = lastNodeWorldPosi * 2 - bez;
            path.SetPointPosition(newNode, newWorldPosi);
        }

        path.pointList.Add(newNode);
    }
}
