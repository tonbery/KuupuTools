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
        Physics.Raycast(ray, out hit, maxDistance, mask);

        return hit;
    }
}
