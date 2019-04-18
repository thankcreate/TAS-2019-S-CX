using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScirpt : MonoBehaviour
{

    float footBlendX;
    float footBlendY;

    float armBlencX;
    float armBlendY;

    float time;

    Animator myAnimator;

    [Header("Tuning Values")]  

    [Range(0.001f, 4f)]
    public float cyclePerSecond;

    [Range(0.001f, 1.00f)]
    public float walkToRun;

    [Range(0.001f, 1.00f)]
    public float armOffsetPhase;

    

    
    

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    float idleTime;
    // Update is called once per frame
    void Update()
    {
        myAnimator.SetBool("IdleFalseMoveTrue", Input.GetKey(KeyCode.W));
        idleTime += Time.deltaTime * 6;

        myAnimator.SetFloat("IdleX", (Mathf.Sin(idleTime) + 1) / 2);

        time += Time.deltaTime * cyclePerSecond * Mathf.PI * 2;

        // Foot
        var walkRunBlendFactor = Mathf.Lerp(0.25f, 1, walkToRun);
        footBlendX = Mathf.Cos(time) * walkRunBlendFactor;
        footBlendY = Mathf.Sin(time) * walkRunBlendFactor;

        myAnimator.SetFloat("FootValX", footBlendX);
        myAnimator.SetFloat("FootValY", footBlendY);


        // Arm
        var offsetPhase = 2 * Mathf.PI * armOffsetPhase;
        armBlencX = Mathf.Cos(time + offsetPhase) * walkRunBlendFactor;
        armBlendY = Mathf.Sin(time + offsetPhase) * walkRunBlendFactor;

        myAnimator.SetFloat("ArmValX", armBlencX);
        myAnimator.SetFloat("ArmValY", armBlendY);

        // Lean
        myAnimator.SetFloat("LeanVal", walkToRun);

        // UpDown
        // myAnimator.runtimeAnimatorController
    }
}
