using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour {

    #region Internal References
    private Transform app;
    private Transform view;
    private Transform cameraBaseTransform;
    private Transform cameraTransform;

    private Transform cameraRoot;
    private Transform cameraDistanceRoot;
    private Transform cameraVerticalRoot;

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
    private float distance;
    private float vertical;
    #endregion

    private void Awake()
    {
        app = GameObject.Find("Application").transform;
        view = app.Find("View");
        cameraBaseTransform = view.Find("CameraBase");
        cameraTransform = Camera.main.transform;
 
        cameraRoot = cameraBaseTransform.Find("CameraRoot");
        cameraDistanceRoot = cameraRoot.Find("CameraDistanceRoot");
        cameraVerticalRoot = cameraDistanceRoot.Find("CameraVerticalRoot");

        cameraLookTarget = cameraBaseTransform.Find("CameraLookTarget");

        avatar = view.Find("AIThirdPersonController");
        avatarRigidbody = avatar.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
            ManualUpdate();
        else
            AutoUpdate();
    }

    #region States
    private void AutoUpdate()
    {
        ComputeData();
        FollowAvatar();
        LookAtAvatar();
    }
    private void ManualUpdate()
    {
        FollowAvatar();
        ManualControl();
    }
    #endregion

    #region Internal Logic

    float standingToWalkingSlider = 0;

    private void ComputeData()
    {
        avatarLookForward = Vector3.Normalize(Vector3.Scale(avatar.forward, new Vector3(1, 0, 1)));

        if (IsWalking())
        {
            standingToWalkingSlider = Mathf.MoveTowards(standingToWalkingSlider, 1, Time.deltaTime * 3);
        }
        else
        {
            standingToWalkingSlider = Mathf.MoveTowards(standingToWalkingSlider, 0, Time.deltaTime);
        }

        float followDistance_Walking = followDistanceBase;
        float followDistance_Standing = followDistanceBase * 2;

        float verticalOffset_Walking = verticalOffsetBase;
        float verticalOffset_Standing = verticalOffsetBase * 4;

        distance = Mathf.Lerp(followDistance_Standing, followDistance_Walking, standingToWalkingSlider);
        vertical = Mathf.Lerp(verticalOffset_Standing, verticalOffset_Walking, standingToWalkingSlider);
    }

    private void FollowAvatar()
    {
        camRelativePostionAuto = avatar.position;

        cameraLookTarget.position = avatar.position + offset;




        cameraRoot.position = avatar.position;        
        cameraRoot.eulerAngles = Vector3.Scale(avatar.eulerAngles, new Vector3(0, 1, 0));
        cameraDistanceRoot.localPosition = new Vector3(0, 0, -distance);
        cameraVerticalRoot.localPosition = new Vector3(0, vertical, 0);

        cameraTransform.localPosition = Vector3.zero;

        // cameraTransform.position = avatar.position - avatarLookForward * distance + Vector3.up * vertical;
    }

    private void LookAtAvatar()
    {
        cameraTransform.LookAt(cameraLookTarget);
    }

    private void ManualControl()
    {
        Vector3 camEulerHold = cameraTransform.localEulerAngles;

        if (Input.GetAxis("Mouse X") != 0)
            camEulerHold.y += Input.GetAxis("Mouse X");

        if (Input.GetAxis("Mouse Y") != 0)
        {
            float temp = camEulerHold.x - Input.GetAxis("Mouse Y");
            temp = (temp + 360) % 360;

            if (temp < 180)
                temp = Mathf.Clamp(temp, 0, 80);
            else
                temp = Mathf.Clamp(temp, 360 - 80, 360);

            camEulerHold.x = temp;
        }

        Debug.Log("The V3 to be applied is " + camEulerHold);
        cameraTransform.localRotation = Quaternion.Euler(camEulerHold);
    }
    #endregion

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
}
