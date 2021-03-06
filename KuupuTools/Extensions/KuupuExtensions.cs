﻿using System.Collections;
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

    public static void Apply(this Transform transform, Transform copyFrom)
    {
        transform.SetParent(copyFrom.parent);
        transform.position = copyFrom.position;
        transform.localScale = copyFrom.localScale;
        transform.rotation = copyFrom.rotation;
    }
    public static void ResetScale(this Transform transform)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
    public static void AlignToGround(this Transform tranform, LayerMask layerMask, float maxDistance)
    {
        var result = KuupuTools.Raycast(tranform.position, Vector3.down, maxDistance, layerMask);
        if (result.collider != null) tranform.position = result.point;
    }
    public static void LookAtY(this Transform transform, Vector3 point)
    {
        var lookPos = -point.Direction(transform).Planar();
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
    }

    //Distance
    public static float Distance(this Vector3 from, Vector3 to)
    {
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
        return new Vector3(vector.x, 0, vector.z);
    }

    public static Vector3 NormalizedPlanar(this Vector3 vector)
    {
        var planar = vector.Planar();
        return planar.normalized;
    }

    public static Vector3 AlignToGround(this Vector3 vector, LayerMask layerMask, float maxDistance)
    {
        var result = KuupuTools.Raycast(vector + (Vector3.up * (maxDistance / 2)), Vector3.down * (maxDistance * 2), maxDistance, layerMask);

        if (result.collider == null) Debug.Log("To ground didn't find a ground!");
        return result.point;
    }

    public static Vector3 Rotate(this Vector3 vector, Vector3 axys, float angle)
    {
        return Quaternion.AngleAxis(angle, axys) * vector;
    }
    public static Vector3 RotateAroundY(this Vector3 vector, float angle)
    {
        return vector.Rotate(Vector3.up, angle);
    }

    //Vector2

    /// <summary>Return a random value between x and y of the vector.</summary>    
    public static float RandomBetweenXY(this Vector2 vector)
    {
        return Random.Range(vector.x, vector.y);
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




    //rigidbody
    public static void SetVelocity(this Rigidbody rigidbody, Vector3 velocity)
    {
        rigidbody.velocity = velocity;
    }
    public static void SetHorizontalVelocity(this Rigidbody rigidbody, Vector3 velocity)
    {
        var vel = rigidbody.velocity;
        vel.x = velocity.x;
        vel.z = velocity.z;
        rigidbody.velocity = vel;
    }
}
