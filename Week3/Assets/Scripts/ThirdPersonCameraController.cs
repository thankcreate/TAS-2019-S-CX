using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour {

    #region Internal References
    private Transform app;
    private Transform view;
    private Transform cameraBaseTransform;

    private Transform camera;
    private Transform cameraRoot;
    private Transform cameraDistanceRoot;

    private Transform cameraLookTarget;
    private Transform avatar;
    private Rigidbody avatarRigidbody;
    #endregion

    #region Public Tuning Variables
    public Vector3 offset;
    public float followDistanceBase;
    public float verticalOffsetBase;
    public float pitchGreaterLimit;
    public float pitchLowerLimit;
    public float fovAtUp;
    public float fovAtDown;
    #endregion

    #region Persistent Outputs
    //Positions
    private Vector3 camRelativePostionAuto;

    //Directions
    private Vector3 avatarLookForward;

    //Scalars
    private Quaternion rootRotation;
    private float distance;
    private float rootVertical;
    #endregion

    ControlState controlState;

    private void Awake()
    {
        app = GameObject.Find("Application").transform;
        view = app.Find("View");
        cameraBaseTransform = view.Find("CameraBase");
        camera = Camera.main.transform;
 
        cameraRoot = cameraBaseTransform.Find("CameraRoot");
        cameraDistanceRoot = cameraRoot.Find("CameraDistanceRoot");
        

        cameraLookTarget = cameraBaseTransform.Find("CameraLookTarget");

        avatar = view.Find("AIThirdPersonController");
        avatarRigidbody = avatar.GetComponent<Rigidbody>();


        rootRotation = Quaternion.identity;
        rootVertical = verticalOffsetBase;
        distance = followDistanceBase;
    }

    private void Update()
    {
        UpdateControlState();
        UpdateCamera();

        FixOcclusion();
    }

    void FixOcclusion()
    {
        Ray ray = new Ray(cameraRoot.position, camera.position - cameraRoot.position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Vector3.Distance(cameraRoot.position, camera.position)))
        {
            Debug.Log("Hit: " + hit.collider.gameObject);
            var hitPoint = hit.point;

            distance = Vector3.Distance(hitPoint, cameraRoot.position);

            ApplyAll();
        }
        
    }


    void UpdateControlState()
    {
  
        if (Input.GetMouseButton(1))
            controlState = ControlState.MANUAL;
        else
            controlState = ControlState.AUTO_FOLLOW;

    }

    void UpdateCamera()
    {        
        switch(controlState)
        {
            case ControlState.AUTO_FOLLOW:
                AutoFollowUpdate();
                break;
            case ControlState.MANUAL:
                ManualUpdate();
                break;
        }

        ApplyAll();
    }
    
    void ApplyAll()
    {
        cameraRoot.rotation = rootRotation;
        cameraRoot.position = avatar.position + new Vector3(0, rootVertical, 0);
        cameraDistanceRoot.localPosition = new Vector3(0, 0, -distance);
    }

    void ManualUpdate()
    {
        if (!Input.GetMouseButton(1))
            return;

        Vector3 _camEulerHold = rootRotation.eulerAngles;

        if (Input.GetAxis("Mouse X") != 0)
            _camEulerHold.y += Input.GetAxis("Mouse X");

        if (Input.GetAxis("Mouse Y") != 0)
        {
            float temp = _camEulerHold.x - Input.GetAxis("Mouse Y");
            temp = (temp + 360) % 360;

            if (temp < 180)
                temp = Mathf.Clamp(temp, 0, 80);
            else
                temp = Mathf.Clamp(temp, 360 - 80, 360);

            _camEulerHold.x = temp;
        }

        
        rootRotation = Quaternion.Euler(_camEulerHold);

        //
        distance = followDistanceBase * 2;
    }


    float standingToWalkingSlider = 0;

    void AutoFollowUpdate()
    {
        if (IsWalking())
        {
            standingToWalkingSlider = Mathf.MoveTowards(standingToWalkingSlider, 1, Time.deltaTime * 3);
        }
        else
        {
            standingToWalkingSlider = Mathf.MoveTowards(standingToWalkingSlider, 0, Time.deltaTime);
        }


        float distanceWalking = followDistanceBase;
        float distanceStanding = followDistanceBase * 2;

        float pitchWalking = 10;
        float pitchStanding = 30;

        // pitch
        var euler = rootRotation.eulerAngles;
        euler.x = Mathf.Lerp(pitchStanding, pitchWalking, standingToWalkingSlider);
        rootRotation = Quaternion.Euler(euler);

        // yaw
        euler.y = avatar.eulerAngles.y;
        rootRotation = Quaternion.Euler(euler);

        // distance
        distance = Mathf.Lerp(distanceStanding, distanceWalking, standingToWalkingSlider);
    }


    #region Helper Functions

    private Vector3 lastPos;
    private Vector3 currentPos;
    private bool IsWalking()
    {
        lastPos = currentPos;
        currentPos = avatar.position;
        float velInst = Vector3.Distance(lastPos, currentPos) / Time.deltaTime;

        if (velInst > .15f)
            return true;
        else return false;
    }
    #endregion


    private enum ControlState
    {
        MANUAL,
        AUTO_FOLLOW
    }
}
