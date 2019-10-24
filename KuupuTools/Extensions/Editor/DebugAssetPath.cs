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
}
