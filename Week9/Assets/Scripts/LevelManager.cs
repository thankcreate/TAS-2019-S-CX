using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float initSpawnRadius = 5;
    public int initSpawnNumber = 50;

    public GameObject[] koinoboriArray;
    public GameObject spawnRoot;
    public float speedBase = 1;

    [Header("Radius")]
    public float followDetectRadius = 1;
    public float awayDetectRadius = 0.5f;
    public float alignRadius = 1;
    

    [Header("Weight")]
    public float followWeight = 1;
    public float awayWeight = 1;
    public float alignWeight = 1;
    public float destinationWieght = 1;

    [Header("Lerp")]
    public float dirLerp = 5;

    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSpawn()
    {
        Collider[] context = new Collider[initSpawnNumber];

        for (int i = 0; i < initSpawnNumber; i++)
        {
            var randomPosi = Random.insideUnitSphere * initSpawnRadius;
            var ko = Instantiate(koinoboriArray[i % koinoboriArray.Length], randomPosi, Quaternion.identity, spawnRoot.transform);

        }
    }


}
