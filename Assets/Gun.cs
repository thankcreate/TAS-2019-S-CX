using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    int targetMask;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if(Input.GetButtonDown("Fire1"))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 100f))
        //    {
        //        Debug.Log(hit.collider.gameObject);
        //    }
        //}        
	}

    void OnMouseDown()
    {
        Debug.Log("haha");
    }

}
