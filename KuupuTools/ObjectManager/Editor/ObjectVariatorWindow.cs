using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ObjectVariatorWindow : EditorWindow
{
    static Vector2 _rotation = new Vector2(0,360);
    static Vector2 _size = new Vector2(0.95f, 1.05f);




    [MenuItem("Window/Kuupu/Object Variator")]
    static void Init()
    {
        ObjectVariatorWindow window = (ObjectVariatorWindow)EditorWindow.GetWindow(typeof(ObjectVariatorWindow));
        window.Show();
        var v2 = window.minSize;
        v2.y = 20f;
        window.minSize = v2;        
    }


    void OnGUI()
    {

        ///////////TER MODO CHILDS E SELECTION

        GUILayout.Label("Rotation");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Min");
        _rotation.x = EditorGUILayout.FloatField(_rotation.x);
        GUILayout.Label("Max");
        _rotation.y = EditorGUILayout.FloatField(_rotation.y);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Randomize Rotation"))
        {
            RandomRotate();
        }



        GUILayout.Label("Size");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Min");
        _size.x = EditorGUILayout.FloatField(_size.x);
        GUILayout.Label("Max");
        _size.y = EditorGUILayout.FloatField(_size.y);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Randomize Size"))
        {
            RandomSize();
        }

        if (GUILayout.Button("Reset Size"))
        {
            ResetScale();
        }



        GUILayout.Label("Align");
        if (GUILayout.Button("Align to ground"))
        {
            AlignToGround();
        }
    }

    static void AlignToGround()
    {
        var selection = Selection.activeGameObject;
        var objs = selection.GetComponentsInChildren<Transform>();
        foreach (var item in objs)
        {
            if (item.parent == selection.transform)
            {
                Undo.RecordObject(item.transform, "Align to ground");

                item.AlignToGround(LayerMasks.GROUND, 100);
            }
        }

    }

    static void RandomRotate()
    {
        var selection = Selection.activeGameObject;
        var objs = selection.GetComponentsInChildren<Transform>();
        foreach (var item in objs)
        {
            if(item.parent == selection.transform)
            {
                var euler = item.localEulerAngles;
                euler.y = _rotation.RandomBetweenXY();

                Undo.RecordObject(item.transform, "Random rotation");

                item.localEulerAngles = euler;
            }
        }

    }

    static void RandomSize()
    {
        var selection = Selection.activeGameObject;
        var objs = selection.GetComponentsInChildren<Transform>();
        foreach (var item in objs)
        {
            if (item.parent == selection.transform)
            {
                var random = _size.RandomBetweenXY();
                var size = Vector3.one * random;

                Undo.RecordObject(item.transform, "Random size");
                item.localScale = size;
            }
        }
    }

    static void ResetScale()
    {
        var selection = Selection.activeGameObject;
        var objs = selection.GetComponentsInChildren<Transform>();
        foreach (var item in objs)
        {
            if (item.parent == selection.transform)
            {
                Undo.RecordObject(item.transform, "Reset size");
                item.localScale = Vector3.one;
            }
        }
    }
}
