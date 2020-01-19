using UnityEditor;
using UnityEngine;

public class DebugAssetPath
{
    [MenuItem("Tools/Kuupu/Debug Selected Asset Path")]
    static void DebugSelectedAssetPath()
    {
        if (Selection.activeObject == null) Debug.Log("No selected asset");        
        Debug.Log(AssetDatabase.GetAssetPath(Selection.activeObject));
    }



    [MenuItem("Tools/Kuupu/Debug Scene Objects")]
    static void DebugSceneObjects()
    {
        foreach (var item in GameObject.FindObjectsOfType<Transform>())
        {
            Transform parent = item.transform;
            int depth = 0;
            
            while(parent != null)
            {
                depth++;
                parent = parent.parent;
            }

            Debug.Log(item + " depth: " + depth.ToString(), item);
        }        
    }

    [MenuItem("Tools/Kuupu/Debug Scene Objects on Root")]
    static void DebugSceneObjectsRoot()
    {
        foreach (var item in GameObject.FindObjectsOfType<Transform>())
        {
            Transform parent = item.transform;
            int depth = 0;

            while (parent != null)
            {
                depth++;
                parent = parent.parent;
            }

            if(depth == 1) Debug.Log(item + " depth: " + depth.ToString(), item);
        }
    }

    [MenuItem("Tools/Kuupu/Align to ground #l")]
    static void AlignToGround()
    {
        var obj = Selection.activeGameObject;
        if (obj != null)
        {
            obj.transform.AlignToGround(LayerMasks.GROUND_MASK, 100);
        }
    }

    [MenuItem("Tools/Kuupu/Align to ground #l", true)]
    static bool ValidateGround()
    {
        return Selection.activeGameObject != null;
    }





    [MenuItem("Example/Rotate Green +45 _g")]
    static void RotateGreenPlus45()
    {
        GameObject obj = Selection.activeGameObject;
        obj.transform.Rotate(Vector3.up * 45);
    }

    [MenuItem("Example/Rotate Green +45 _g", true)]
    static bool ValidatePlus45()
    {
        return Selection.activeGameObject != null;
    }

    [MenuItem("Example/Rotate green -45 #g")]
    static void RotateGreenMinus45()
    {
        GameObject obj = Selection.activeGameObject;
        obj.transform.Rotate(Vector3.down * 45);
    }

    [MenuItem("Example/Rotate green -45 #g", true)]
    static bool ValidateMinus45()
    {
        return Selection.activeGameObject != null;
    }
}
