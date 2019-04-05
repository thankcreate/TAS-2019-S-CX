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

        var follow = AutoFollowDir();
        var away = AutoAwayDir();
        var align = AutoAlignDir();
        var dest = AutoOwnDesitinationDir();

        // var away = Vector3.zero;
        // var align = Vector3.zero;
        // var dest = Vector3.zero;

        // speed
        var finalDir =
            follow * LevelManager.Instance.followWeight
            + away * LevelManager.Instance.awayWeight
            + align * LevelManager.Instance.alignWeight
            + dest * LevelManager.Instance.destinationWieght;
        finalDir.Normalize();
        var lepredDir = finalDir;
        lepredDir = Vector3.Slerp(lastDir, finalDir, Time.deltaTime * LevelManager.Instance.dirLerp);
        var destPosi = transform.position + lepredDir * LevelManager.Instance.speedBase * Time.deltaTime;

        transform.position = destPosi;
        lastDir = lepredDir;



        // rotation
        var rotVector = lepredDir;      
        
        var needToTurn = Quaternion.FromToRotation(lastDir, finalDir);
        var rtY = (needToTurn.eulerAngles.y > 180 ? 360f - needToTurn.eulerAngles.y : needToTurn.eulerAngles.y) / 180f * 1;
        var rtX = (needToTurn.eulerAngles.x > 180 ? 360f - needToTurn.eulerAngles.x : needToTurn.eulerAngles.x) / 180f * -1;

        // LevelManager.Instance.TestBiggest(rtY);

        //var curSideCurve = mat.GetFloat("_SideFaceCurve");
        //var newSideCurve = Mathf.Lerp(curSideCurve, rtY, 0.5f * Time.deltaTime);

        //var curPitchCurve = mat.GetFloat("_PitchCurve");
        //var newPitchCurve = Mathf.Lerp(curPitchCurve, rtX, 0.5f * Time.deltaTime);
        
        //mat.SetFloat("_SideFaceCurve", newSideCurve);
        //mat.SetFloat("_PitchCurve", newPitchCurve);

        var rt = Quaternion.LookRotation(lepredDir);
        transform.rotation = rt;
    }
    
    Vector3 AutoFollowDir()
    {
        Vector3 folllowSpeed = Vector3.zero;
        

        int count = 0;
        Vector3 sum = Vector3.zero;
        var c = Physics.OverlapSphere(transform.position, LevelManager.Instance.followDetectRadius);
        foreach (var col in c)
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

        

        var c = Physics.OverlapSphere(transform.position, LevelManager.Instance.followDetectRadius);
        foreach (var col in c)
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

    


    Vector3 AutoAwayDir()
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

    Vector3 AutoAlignDir()
    {
        Vector3 dir = Vector3.zero;


        int count = 0;
        Vector3 sum = Vector3.zero;
        var c = Physics.OverlapSphere(transform.position, LevelManager.Instance.followDetectRadius);         
        foreach (var col in c)
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

    Vector3 AutoOwnDesitinationDir()
    {
        var dest = new Vector3(6, 0, 0);
        var dir = dest - transform.position;
        return dir;
    }
}
