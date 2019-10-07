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
    public static void LookAtY(this Transform transform, Vector3 point)
    {        
        var lookPos = -point.Direction(transform).Planar();        
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }

    //Distance
    public static float Distance(this Vector3 from, Vector3 to) {
        return (from - to).magnitude;
    }

    public static float PlanarDistance(this Vector3 from, Vector3 to)
    {
        return (from.Planar() - to.Planar()).magnitude;
    }

    //Directions
    public static Vector3 Direction(this Vector3 from, Vector3 to)
    {
        return to - from;
    }
    public static Vector3 PlanarDirection(this Vector3 from, Vector3 to)
    {
        return to.Planar() - from.Planar();
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
    public static Vector3 Planar(this Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.y);
    }




    //Lists
    public static T RandomItem<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    public static int RandomIndex<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot find a random index from an empty list");
        return UnityEngine.Random.Range(0, list.Count);
    }
}
