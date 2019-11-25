using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class ObjectReplacer : EditorWindow
{

    static GameObject _replaceFor;

    [MenuItem("Window/Kuupu/Object Replacer")]
    static void Init()
    {
        ObjectReplacer window = (ObjectReplacer)EditorWindow.GetWindow(typeof(ObjectReplacer));
        window.Show();
        var v2 = window.minSize;
        v2.y = 20f;
        window.minSize = v2;
    }

    void OnGUI()
    {
        _replaceFor = EditorGUILayout.ObjectField(_replaceFor, typeof(GameObject), false) as GameObject;

        if(_replaceFor != null)
        {
            if (GUILayout.Button("Replace") && _replaceFor != null)
            {
                Replace();
            }
        }        

        var objects = Selection.gameObjects;
        if (objects.Length <= 0) return;

        GUILayout.Label(objects.Length.ToString() + " objects selected...");

        foreach (var item in objects)
        {
            GUILayout.Label(item.name);
        }
    }

    public void Replace()
    {
        var objects = Selection.gameObjects;

        foreach (var item in objects)
        {
            var nO = PrefabUtility.InstantiatePrefab(_replaceFor) as GameObject;
            Undo.RegisterCreatedObjectUndo(nO, "Created go");

            nO.transform.Apply(item.transform);
            
            Undo.DestroyObjectImmediate(item.gameObject);
        }
    }
}
