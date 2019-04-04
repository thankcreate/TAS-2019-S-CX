using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rt = Quaternion.LookRotation(target.transform.position - transform.position);
        this.transform.rotation = rt;
        Debug.Log(rt.eulerAngles);

        var vec1 = target.transform.position - transform.position;
        var vec2 = target2.transform.position - transform.position;
        Debug.Log(Quaternion.FromToRotation(vec2, vec1).eulerAngles);


    }
        
}
