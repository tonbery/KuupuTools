using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Transform))]
public class TransformInspector : Editor
{
    enum ETransformDrawMode { standard, kuupu}
    ETransformDrawMode _drawMode = ETransformDrawMode.kuupu;

    Transform transform;

    private void OnEnable()
    {
        transform = (Transform)target;
    }

    public override void OnInspectorGUI()
    {

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Standard"))
        {
            _drawMode = ETransformDrawMode.standard;
        }
        if (GUILayout.Button("Kuupu"))
        {
            _drawMode = ETransformDrawMode.kuupu;
        }

        GUILayout.EndHorizontal();

       
        if(_drawMode == ETransformDrawMode.standard)
        {
            DrawDefaultTransform();
        }

        

    }

    void DrawDefaultTransform()
    {
        Vector3 position = EditorGUILayout.Vector3Field("Position", transform.localPosition);
        Vector3 eulerAngles = EditorGUILayout.Vector3Field("Rotation", transform.localEulerAngles);
        Vector3 scale = EditorGUILayout.Vector3Field("Scale", transform.localScale);
    }
}
