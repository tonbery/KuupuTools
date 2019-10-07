using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KuupuExtensions
{

    //Transforms
    public static void ResetTransform(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }
    public static void ResetPosition(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
    }
    public static void ResetRotation(this Transform transform)
    {
        transform.localRotation = Quaternion.identity;
    }

    public static void ResetScale(this Transform transform)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    public static void AlignToGround(this Transform tranform, LayerMask layerMask, float maxDistance)
    {
        Vector3 groundPoint = tranform.position;

        KuupuTools.Raycast(tranform.position, Vector3.down, maxDistance, layerMask);

        tranform.position = groundPoint;
    }

    //Distance
    public static float Distance(this Vector3 from, Vector3 to) {
        return (from - to).magnitude;
    }       

    //Directions
    public static Vector3 Direction(this Vector3 from, Vector3 to)
    {
        return to - from;
    }
    public static Vector3 Direction(this Transform from, Transform to)
    {
        return to.position - from.position;
    }

    public static Vector3 Direction(this Transform from, Vector3 to)
    {
        return to - from.position;
    }

    public static Vector3 Direction(this Vector3 from, Transform to)
    {
        return to.position - from;        
    }

    //Vector operations
    public static Vector3 ToPlanar(this Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }
}
