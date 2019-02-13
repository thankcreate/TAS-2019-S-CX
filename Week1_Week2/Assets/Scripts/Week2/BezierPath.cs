using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class BezierPath : MonoBehaviour {

    public bool isLoop = false;
    public List<BezierPointData> pointList = new List<BezierPointData>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {


    }

    public Vector3 GetPointWorldPosition(int index)
    {
        return GetPointWorldPosition(pointList[index]);
    }

    public Vector3 GetPointWorldPosition(BezierPointData point)
    {
        return this.transform.position + point.position;
    }

    public void SetPointPosition(BezierPointData point, Vector3 worldPosi)
    {
        point.position = worldPosi - transform.position;
    }

    public Vector3 GetTangentWorldPosition(int index, int tangentIndex)
    {
        return GetTangentWorldPosition(pointList[index], tangentIndex);
    }

    public Vector3 GetTangentWorldPosition(BezierPointData point, int tangentIndex)
    {
        return this.transform.position + point.position + point.tangents[tangentIndex];
    }

    
}
