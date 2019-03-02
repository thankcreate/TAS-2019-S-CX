using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    #region vars

    public Transform focus;
    Vector3 lastFocusPoint;
    
    
    public int gridEachMesh = 8;
    public int gridLength = 1;

    Vector3[] gizmoPositions = new Vector3[4];
    HashSet<int> posiSet = new HashSet<int>();
    
    // the buffer of the meshes
    Queue<GameObject> meshQueue = new Queue<GameObject>();

    // a hashset to judge if we have already drawn a mesh
    HashSet<Vector3> taken = new HashSet<Vector3>();

    int firstMeshIndex = 0;
    int currMeshIndex = 0;

    public GameObject meshRoot;
    public GameObject meshPrefab;
    public int bufferSize = 6;

    #endregion

    private void Awake()
    {  
    }


    // Use this for initialization
    void Start () {
        focus = transform;
    }
	
	// Update is called once per frame
	void Update () {
        UpdateMeshes(false);
	}


    void UpdateMeshes(bool forceUpdate)
    {
        var currIndexPosi = GetIndexPosition(focus.position);
        if (forceUpdate || currIndexPosi != lastFocusPoint)
        {
            UpdateMeshesInner();
        }
        lastFocusPoint = currIndexPosi;

        Debug.DrawLine(gizmoPositions[0], gizmoPositions[1]);
        Debug.DrawLine(gizmoPositions[1], gizmoPositions[2]);
        Debug.DrawLine(gizmoPositions[2], gizmoPositions[3]);
        Debug.DrawLine(gizmoPositions[3], gizmoPositions[0]);
    }

  

    void UpdateMeshesInner()
    {
        Vector3 flooredCoord = GetIndexPosition(focus.position);
        float meshLength = gridEachMesh * gridLength;
        gizmoPositions[0] = (flooredCoord + new Vector3(0, 0, 0)) * meshLength;
        gizmoPositions[1] = (flooredCoord + new Vector3(0, 0, 1)) * meshLength;
        gizmoPositions[2] = (flooredCoord + new Vector3(1, 0, 1)) * meshLength;
        gizmoPositions[3] = (flooredCoord + new Vector3(1, 0, 0)) * meshLength;


        var newPosi = GetWorldPositionFromIndex(flooredCoord);
        if (taken.Contains(newPosi))
            return;

        if(meshQueue.Count != bufferSize) 
        {         
            var go = Instantiate(meshPrefab, newPosi, Quaternion.identity, meshRoot.transform);
            meshQueue.Enqueue(go);
            taken.Add(newPosi);
        }
        else
        {
            var firstGo = meshQueue.Dequeue();
            taken.Remove(firstGo.transform.position);
            firstGo.transform.position = newPosi;
            meshQueue.Enqueue(firstGo);            
        }
    }


    Vector3 GetWorldPositionFromIndex(Vector3 index)
    {
        float meshLength = gridEachMesh * gridLength;
        return index * meshLength;
    }

    Vector3 GetIndexPosition(Vector3 posi)
    {
        float meshLength = gridEachMesh * gridLength;
        Vector3 coord = posi / meshLength;
        Vector3 flooredCoord = new Vector3(Mathf.Floor(coord.x), 0, Mathf.Floor(coord.z));

        return flooredCoord;
    }
}
