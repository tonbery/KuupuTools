using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KuupuTools
{
    public static void SetGameObjectsActivation(GameObject[] objects, bool active)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(active);
        }
    }

    public static void SetComponentsActivation(MonoBehaviour[] objects, bool active)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].enabled = active;
        }
    }

    public static RaycastHit Raycast(Vector3 start, Vector3 direction, float maxDistance, LayerMask mask)
    {
        Ray ray = new Ray(start, direction);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction *maxDistance, Color.yellow, 0.5f);
        Physics.Raycast(ray, out hit, maxDistance, mask);

        return hit;
    }

    public static bool CanSeePoint(Vector3 origin, GameObject target, Vector3 offset = default, bool debug = false)
    {
        Ray ray = new Ray(origin + offset, Direction(origin + offset, target.transform.position + offset));
        RaycastHit hit;
        LayerMask levelMask = LayerMask.GetMask("Level");
        var distance = Distance(origin, target.transform.position);

        if (Physics.Raycast(ray, out hit, distance, levelMask))
        {
            if (debug) Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 0.1f);
            return false;            /*
            if (hit.transform.gameObject == target)
            {
                if (debug) Debug.DrawRay(ray.origin, ray.direction * distance, Color.green, 0.1f);
                return true;
            }
            */
        }
        else
        {
            if (debug) Debug.DrawRay(ray.origin, ray.direction * distance, Color.green, 0.1f);
            return true;
        }
    }

    public static Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }
    public static float Distance(Vector3 from, Vector3 to)
    {
        return (to - from).magnitude;
    }

    public static Vector3 RandomVectorX(float variation)
    {
        return new Vector3(Random.Range(-variation, variation), 0, 0);
    }
    public static Vector3 RandomVectorXZ(float variation)
    {
        return new Vector3(Random.Range(-variation, variation), 0, Random.Range(-variation, variation));
    }

    public static Vector3 ToPlanar(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }

    public static Collider[] CollisionOnArea(Vector3 point, float radius, int layerMask)
    {
        return Physics.OverlapSphere(point, radius, layerMask);
    }

    public static int CollisionOnAreaNonAlloc(Vector3 point, float radius, int layerMask, Collider[] result)
    {
        return Physics.OverlapSphereNonAlloc(point, radius, result, layerMask);
    }

    public static RaycastHit RayFromMouse(LayerMask mask)
    {
        RaycastHit hit = new RaycastHit();
        //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, mask);
        return hit;
    }
    public static float Angle360(Vector3 from, Vector3 to)
    {
        float angle = Vector3.Angle(from, to);
        float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(from, to)));
        return angle * sign;
    }
    public static Vector3 RotateVector(Vector3 vector, float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.up) * vector;
    }
    public static Quaternion RandomRotationYRange(float min, float max)
    {
        return Quaternion.Euler(new Vector3(0, Random.Range(min, max), 0));
    }
    public static Quaternion RandomRotationY()
    {
        return Quaternion.Euler(new Vector3(0, Random.value * 360, 0));
    }
}
