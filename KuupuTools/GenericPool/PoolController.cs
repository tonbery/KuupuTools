using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolController:MonoBehaviour
{
    private static PoolController _instance;
    public static PoolController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PoolController>();
                if (_instance == null)
                {
                    var no = new GameObject("PoolController");
                    _instance = no.AddComponent<PoolController>();
                }
            }
            return _instance;
        }
    }

    Dictionary<int, List<GameObject>> _pools = new Dictionary<int, List<GameObject>>();
    Dictionary<int, int> _objectsInUse = new Dictionary<int, int>();

    public void CreatePoolChace(GameObject prefab)
    {


    }

    public GameObject Request(GameObject prefab) 
    {
        var prefabID = prefab.GetInstanceID();
        
        if (!_pools.ContainsKey(prefabID))
        {
            _pools.Add(prefabID, new List<GameObject>());
        }

        if(_pools[prefabID].Count <= 0)
        {
            _pools[prefabID].Add(Instantiate(prefab));
        }
        
        var obj = _pools[prefabID][0];
        _pools[prefabID].Remove(obj);
        _objectsInUse.Add(obj.GetInstanceID(), prefabID);
        obj.transform.rotation = Quaternion.identity;
        return obj;
    }


    public void ReturnToPool(GameObject instance)
    {
        var instanceID = instance.GetInstanceID();
        if (!_objectsInUse.ContainsKey(instanceID)) {
            Debug.LogError("Trying to return an object that is not from the pool");
            return;
        }

        if(instance.transform.parent != null) instance.transform.SetParent(null);
        instance.SetActive(false);

        _pools[_objectsInUse[instanceID]].Add(instance);
        _objectsInUse.Remove(instanceID);
    }
}