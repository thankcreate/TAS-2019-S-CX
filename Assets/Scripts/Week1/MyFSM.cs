using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFSM : MonoBehaviour {

    public ParticleSystem ps;

    public ParticleSystem rt1;
    public ParticleSystem rt2;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AnimateSpeed()
    {
        DOTween.To(() => ps.emissionRate, x => ps.emissionRate = x, 15, 10);


        float b = rt1.rotationOverLifetime.zMultiplier;

        var rol1 = rt1.rotationOverLifetime;
        var rol2 = rt2.rotationOverLifetime;

        var co1 = rt1.colorOverLifetime;
       // co1.enabled = false;

        var co2 = rt1.colorOverLifetime;
       // co2.enabled = false;

        DOTween.To(() => rol1.zMultiplier, x => rol1.zMultiplier = x, b * 24.0f, 10f).SetEase(Ease.InSine); ;
        DOTween.To(() => rol2.zMultiplier, x => rol2.zMultiplier = x, b * -24f, 10f).SetEase(Ease.InSine); ;

    }
}
