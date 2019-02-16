using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelTrigger : MonoBehaviour
{

    public ThirdPersonCameraController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        controller.tunnerlDirection = transform.rotation.eulerAngles;
        controller.isInTunnel = true;
    }

    private void OnTriggerExit(Collider other)
    {
        controller.isInTunnel = false;
    }
}
