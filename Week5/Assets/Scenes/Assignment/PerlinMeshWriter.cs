using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class PerlinMeshWriter : MonoBehaviour {

    #region vars
    MeshFilter mf;
    MeshRenderer mr;

    Mesh mesh;
    Vector3[] vertices;
    Vector3[] normals;  
    int[] tris;
    Vector2[] uvs;
    int gridPerMeshSide = 8;
    public float heightFactor = 2;
    Vector3 lastPosi;
    #endregion

    // Use this for initialization
    void Start () {
        
        mf = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();

        Init();
        UpdateMesh(true);
    }


	
	// Update is called once per frame
	void Update () {
        UpdateMesh();
    }

    void UpdateMesh(bool forceUpdate = false)
    {       
        if(forceUpdate || transform.position != lastPosi)
        {
            UpdateMeshInner(forceUpdate);
            ApplyMeshData();
        }
        lastPosi = transform.position;
    }

    
    
    void Init()
    {        
        var verticesCount = (gridPerMeshSide + 1) * (gridPerMeshSide + 1);

        mesh = new Mesh();
        vertices = new Vector3[verticesCount];
        normals = new Vector3[verticesCount];
        tris = new int[gridPerMeshSide * gridPerMeshSide * 3 * 2];
        //uvs = new Vector2[verticesCount];

        mf.mesh = mesh;
    }

    void UpdateMeshInner(bool forceUpdate = false)
    {
        UpdateVerticesAndNormals();
        if (forceUpdate)
        {
            UpdateTris();
            UpdateUVs();
        }
        
    }

    void UpdateVerticesAndNormals()
    {
        float unitLength = 1;
        var maxLength = gridPerMeshSide * unitLength;
        var verticesCount = (gridPerMeshSide + 1) * (gridPerMeshSide + 1);

        for (int z = 0; z <= gridPerMeshSide; z++)
        {
            for (int x = 0; x <= gridPerMeshSide; x++)
            {
                int i = z * (gridPerMeshSide + 1) + x;
                var zRela = (float)z * unitLength;
                var xRela = (float)x * unitLength;
                var zCood = zRela + transform.position.z;
                var xCood = xRela + transform.position.x;
                                              
                float y0 = GetY(zCood, xCood);
                vertices[i] = new Vector3(xRela, y0, zRela);

                // used the cross product of partial derivatives to compute the normal
                var step = 0.01f;
                var y1 = GetY(zCood + step, xCood);
                var tanZ = new Vector3(0, y1 - y0, step);

                var y2 = GetY(zCood, xCood + step);
                var tanX = new Vector3(step, y2 - y0, 0);

                normals[i] = Vector3.Cross(tanZ, tanX).normalized;


                //uvs[i] = new Vector2((float)x / n, (float)z / n);
            }
        }
    }

    float GetY(float z, float x)
    {
        float unitLength = 1;
        var maxLength = gridPerMeshSide * unitLength;
        return Perlin.Noise(z / maxLength, x / maxLength) * heightFactor;
    }

    void UpdateTris()
    {
        int triIndex = 0;
        for (int z = 0; z < gridPerMeshSide; z++)
        {
            for (int x = 0; x < gridPerMeshSide; x++)
            {
                int leftBottom = z * (gridPerMeshSide + 1) + x;
                int rightBottom = z * (gridPerMeshSide + 1) + x + 1;
                int leftTop = (z + 1) * (gridPerMeshSide + 1) + x;
                int rightTop = (z + 1) * (gridPerMeshSide + 1) + x + 1;

                tris[triIndex++] = leftBottom;
                tris[triIndex++] = leftTop;
                tris[triIndex++] = rightBottom;
                tris[triIndex++] = leftTop;
                tris[triIndex++] = rightTop;
                tris[triIndex++] = rightBottom;
            }
        }
    }

    void UpdateUVs()
    {

    }


    private void ApplyMeshData()
    {
        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.normals = normals;

        // mesh.RecalculateNormals();                  
    }
}
