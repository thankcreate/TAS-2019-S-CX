using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingObject : MonoBehaviour
{
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    int a = 1;

    Vector3 lastDir = Vector3.zero;
    void Update()
    {

        var follow = AutoFollowSpeed();
        var away = AutoAwaySpeed();
        var align = AutoAlignSpeed();
        var dest = AutoOwnDesitinationSpeed();


        // speed
        var dir =
            follow * LevelManager.Instance.followWeight
            + away * LevelManager.Instance.awayWeight
            + align * LevelManager.Instance.alignWeight
            + dest * LevelManager.Instance.destinationWieght;

        dir.Normalize();
        var finalDir = dir;
        dir = Vector3.Slerp(lastDir, dir, Time.deltaTime * LevelManager.Instance.dirLerp);
        var destPosi = transform.position + dir * LevelManager.Instance.speedBase * Time.deltaTime;

        transform.position = destPosi;
        lastDir = dir;



        // rotation
        var rotVector = dir;

        
        // var destRotation = rotVector == Vector3.zero ? transform.rotation : Quaternion.LookRotation(rotVector);
        // var rot = Quaternion.Lerp(transform.rotation, destRotation, LevelManager.Instance.rotationLerp * Time.deltaTime);
        
        var ftR = Quaternion.FromToRotation(lastDir, finalDir);
        var rtY = (ftR.eulerAngles.y > 180 ? 360f - ftR.eulerAngles.y : ftR.eulerAngles.y) / 180f * 1;
        var oriV = mat.GetFloat("_SideFaceCurve");
        var newV = Mathf.Lerp(oriV, rtY, 0.3f);
        mat.SetFloat("_SideFaceCurve", newV);


        var rt = Quaternion.LookRotation(dir);
        transform.rotation = rt;
    }

    Collider[] context = new Collider[100];

    Vector3 AutoFollowSpeed()
    {
        Vector3 folllowSpeed = Vector3.zero;

        for (int i = 0; i < context.Length; i++)
        {
            context[i] = null;
        }

        int count = 0;
        Vector3 sum = Vector3.zero;
        Physics.OverlapSphereNonAlloc(transform.position, LevelManager.Instance.followDetectRadius, context);
        foreach (var col in context)
        {
            if (col != null)
            {
                if (col.transform.parent.gameObject == gameObject)
                    continue;

                sum += col.transform.position;
                count++;
            }
        }

        if (count == 0)
            return folllowSpeed;

        folllowSpeed = (sum / count) - transform.position;

        //GameObject closetObject = GetCloset(LevelManager.Instance.followDetectRadius);
        //if (closetObject == null)
        //    return Vector3.zero;

        //var dir = closetObject.transform.position - transform.position;
        //dir.Normalize();

        folllowSpeed.Normalize();

        return folllowSpeed;
    }

    GameObject GetCloset(float radius)
    {
        float closest = float.MaxValue;
        GameObject closetObject = null;

        for (int i = 0; i < context.Length; i++)
        {
            context[i] = null;
        }

        Physics.OverlapSphereNonAlloc(transform.position, radius, context);
        foreach (var col in context)
        {
            if (col != null)
            {
                if (col.transform.parent.gameObject == gameObject)
                    continue;

                var dis = Vector3.Distance(col.transform.position, transform.position);
                if (dis < closest)
                {
                    closest = dis;
                    closetObject = col.transform.parent.gameObject;
                }

            }
        }
        return closetObject;
    }

    


    Vector3 AutoAwaySpeed()
    {
        Vector3 awaySpeed = Vector3.zero;
        Vector3 sumPosi = Vector3.zero;

        int count = 0;
        float closest = float.MaxValue;
        GameObject closetObject = GetCloset(LevelManager.Instance.awayDetectRadius);
        

        if (closetObject == null)
            return Vector3.zero;

        closest = Vector3.Distance(closetObject.transform.position, transform.position);

        var averPosi = sumPosi / count;

        var dir = closetObject.transform.position - transform.position;
        dir.Normalize();

        awaySpeed = -dir;

        float weightMax = 10;
        var adjustWeight = (LevelManager.Instance.awayDetectRadius - closest) / LevelManager.Instance.awayDetectRadius * weightMax;

        return awaySpeed;

    }

    Vector3 AutoAlignSpeed()
    {
        Vector3 dir = Vector3.zero;

        for (int i = 0; i < context.Length; i++)
        {
            context[i] = null;
        }

        int count = 0;
        Vector3 sum = Vector3.zero;
        Physics.OverlapSphereNonAlloc(transform.position, LevelManager.Instance.followDetectRadius, context);
        foreach (var col in context)
        {
            if (col != null)
            {
                if (col.transform.parent.gameObject == gameObject)
                    continue;

                sum += col.transform.parent.forward;
                count++;
            }
        }

        if (count == 0)
            return dir;

        dir = (sum / count);

        //GameObject closetObject = GetCloset(LevelManager.Instance.followDetectRadius);
        //if (closetObject == null)
        //    return Vector3.zero;

        //var dir = closetObject.transform.position - transform.position;
        //dir.Normalize();

        dir.Normalize();

        return dir;
    }

    Vector3 AutoOwnDesitinationSpeed()
    {
        var dest = new Vector3(10, 0, 0);
        var dir = dest - transform.position;
        return dir;
    }
}
