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

    private Transform whiskerRoot;
    private Transform whiskerProbe;

    private Transform cameraLookTarget;
    private Transform avatar;
    private Rigidbody avatarRigidbody;
    #endregion

    #region Public Tuning Variables
    public Vector3 offset;
    public float followDistanceBase;


    public float standingDistance;
    public float walkingDistance;
    public float tunnelDistance;
    public float standingPitch;
    public float walkingPitch;
    public float tunnelPitch;

    public float whiskerRange;
    public float objectOfInterestRange;

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

        whiskerRoot = cameraRoot.Find("WhiskerRoot");
        whiskerProbe = whiskerRoot.Find("WhiskerProbe");


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

        UpdateOcclusionDetect();
        UpdateObjectsOfInterst();
    }

    void UpdateObjectsOfInterst()
    {
        var mask = 1 << LayerMask.NameToLayer("ObjectOfInterest");
        var cols = Physics.OverlapSphere(avatar.position, objectOfInterestRange, mask);
        Quaternion dest = Quaternion.identity;
        if (cols.Length > 0)
        {
            var col = cols[0];
            
            dest = Quaternion.LookRotation(col.transform.position - camera.transform.position, Vector3.up);
            camera.rotation = Quaternion.Lerp(camera.rotation, dest, 3 * Time.deltaTime);
        }
        else
        {            
            camera.localRotation = Quaternion.Lerp(camera.localRotation, Quaternion.identity, 3 * Time.deltaTime);
        }
        
    }

    void UpdateOcclusionDetect()
    {
        if(controlState == ControlState.MANUAL)
        {
            CheckIfNeedTruncate();
        }
        else if (controlState == ControlState.AUTO_FOLLOW)
        {
            if(!isInTunnel)
            {
                CheckWhisker();
                UpdateSwingAway();
            }
        }
    }

    public bool isInTunnel;
    public Vector3 tunnerlDirection;
    bool CheckTunnel()
    {
        if(!isInTunnel)
        {
            return false;
        }

               
        float lerpSpeed = 4.0f;
        
        // pitch
        var euler = rootRotation.eulerAngles;
        euler.x = tunnelPitch;

        // yaw
        euler.y = -tunnerlDirection.y;
        var destRt = Quaternion.Euler(euler); 
     
        rootRotation = Quaternion.Lerp(rootRotation, destRt, lerpSpeed * Time.deltaTime);
        cameraRoot.rotation = rootRotation;

        // distance
        distance = Mathf.Lerp(distance, tunnelDistance, lerpSpeed * Time.deltaTime);
        cameraDistanceRoot.localPosition = new Vector3(0, 0, -distance);



        return isInTunnel;
    }

    void UpdateSwingAway()
    {
        if (!isInSwingAway)
            return;

       

        var ea = rootRotation.eulerAngles;

        var diff = Mathf.DeltaAngle(swingAwayDestination, ea.y);
        var absDiff = Mathf.Abs(diff);
        if (absDiff < 1)
        {
            isInSwingAway = false;
            return;
        }

        ea.y = swingAwayDestination;
        var desti = Quaternion.Euler(ea);
        

        rootRotation = Quaternion.Lerp(rootRotation, desti, 2.0f * Time.deltaTime);
        cameraRoot.rotation = rootRotation;
    }

    bool isInSwingAway = false;
    float swingAwayDestination;
    void CheckWhisker()
    {
        if (isInSwingAway)
            return;

        float range = whiskerRange;
        float checkInterval = 10;
        float adjustStep = 30;
        whiskerProbe.transform.localPosition = cameraDistanceRoot.transform.localPosition;

        for (float r = range; r >= -range; r -= checkInterval)
        {
            var lea = whiskerRoot.transform.localEulerAngles;
            lea.y = r;
            whiskerRoot.transform.localEulerAngles = lea;

            Ray ray = new Ray(cameraRoot.position, whiskerProbe.position - cameraRoot.position);
            RaycastHit hit;
            var dis = Vector3.Distance(cameraRoot.position, camera.position);
            dis *= (whiskerRange - Mathf.Abs(r)) / whiskerRange;
            if (Physics.Raycast(ray, out hit, dis))
            {
                int factor = r > 0 ? -1 : 1;


                isInSwingAway = true;        

                var el = rootRotation.eulerAngles;
                el.y += factor * adjustStep;

                swingAwayDestination = el.y + factor * adjustStep;


                //rootRotation = Quaternion.Euler(el);
                //cameraRoot.rotation = rootRotation;
                break;
            }
        }    
    }

    // Only truncate if the player control manually
    void CheckIfNeedTruncate()
    {
        Ray ray = new Ray(cameraRoot.position, camera.position - cameraRoot.position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Vector3.Distance(cameraRoot.position, camera.position)))
        {
            Debug.Log("Hit: " + hit.collider.gameObject);
            var hitPoint = hit.point;

            var tempDistance = Vector3.Distance(hitPoint, cameraRoot.position) - 0.2f;

            cameraDistanceRoot.localPosition = new Vector3(0, 0, -tempDistance);
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
        if(isInTunnel)
        {
            CheckTunnel();
        }
        else
        {
            switch (controlState)
            {
                case ControlState.AUTO_FOLLOW:
                    AutoFollowUpdate();
                    break;
                case ControlState.MANUAL:
                    ManualUpdate();
                    break;
            }
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
        // distance = followDistanceBase * 2;
    }


    float standingToWalkingSlider = 0;

    void AutoFollowUpdate()
    {
        bool isWalking = IsWalking();
        if (isWalking)
        {
            standingToWalkingSlider = Mathf.MoveTowards(standingToWalkingSlider, 1, Time.deltaTime * 3);
        }
        else
        {
            standingToWalkingSlider = Mathf.MoveTowards(standingToWalkingSlider, 0, Time.deltaTime);
        }


        //float distanceWalking = followDistanceBase;
        //float distanceStanding = followDistanceBase * 2;


        //float pitchWalking = 10;
        //float pitchStanding = 20;

        // pitch
        var euler = rootRotation.eulerAngles;
        euler.x = isWalking ? walkingPitch : standingPitch;

        // don't change yaw here
        var destRt = Quaternion.Euler(euler); ;


        float lerpSpeed = 4.0f;
        rootRotation = Quaternion.Lerp(rootRotation, destRt, lerpSpeed * Time.deltaTime);

        // distance
        // distance = Mathf.Lerp(distanceStanding, distanceWalking, standingToWalkingSlider);
        distance = Mathf.Lerp(distance, isWalking ? walkingDistance : standingDistance, lerpSpeed * Time.deltaTime);
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
