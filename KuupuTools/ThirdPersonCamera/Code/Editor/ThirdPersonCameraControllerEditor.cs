﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

//[CustomEditor(typeof(ThirdPersonCameraController))]
public class ThirdPersonCameraControllerEditor : Editor
{
    VisualElement _root;
    VisualTreeAsset _visualTree;
    StyleSheet _style;
    private void OnEnable()
    {
        _root = new VisualElement();
        _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/KuupuTools/KuupuTools/ThirdPersonCamera/Code/EditorScripts/ThirdPersonCameraEditorTemplate.uxml");
        _style = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/KuupuTools/KuupuTools/ThirdPersonCamera/Code/EditorScripts/ThirdPersonCameraEditorStyles.uss");

        _root.styleSheets.Add(_style);
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = _root;
        root.Clear();

        _visualTree.CloneTree(root);

        root.Query(classes: new string[] { "property" }).ForEach((property) => { property.Add(CreateModuleUI(property.name)); });
        root.Query(classes: new string[] { "profiles" }).ForEach((property) => { property.Add(CreateModuleProfileList(property.name)); });
        root.Query(classes: new string[] { "currentProfile" }).ForEach((property) => { property.Add(CreateCurrentProfile(property.name)); });

        return root;
    }
    public VisualElement CreateModuleProfileList(string moduleName)
    {
        var element = new VisualElement();
        var prop = serializedObject.FindProperty(moduleName);
        var field = new PropertyField(prop);
        element.Add(field);
        return element;
    }
    public VisualElement CreateCurrentProfile(string moduleName)
    {
        var element = new VisualElement();

        var profiles = serializedObject.FindProperty("_profiles");
        if (profiles.arraySize <= 0) profiles.arraySize = 1;

        var currentProfile = profiles.GetArrayElementAtIndex(serializedObject.FindProperty("_currentProfileIndex").intValue);
        var heightProp = currentProfile.FindPropertyRelative("_height");
        var distanceProp = currentProfile.FindPropertyRelative("_distance");
        var xSpeed = currentProfile.FindPropertyRelative("_xSpeed");
        var ySpeed = currentProfile.FindPropertyRelative("_ySpeed");
        var invertY = currentProfile.FindPropertyRelative("_invertY");
        var minY = currentProfile.FindPropertyRelative("_minY");
        var maxY = currentProfile.FindPropertyRelative("_maxY");

        element.Add(new PropertyField(heightProp));
        element.Add(new PropertyField(distanceProp));
        element.Add(new PropertyField(xSpeed));
        element.Add(new PropertyField(ySpeed));
        element.Add(new PropertyField(invertY));
        element.Add(new PropertyField(minY));
        element.Add(new PropertyField(maxY));

        return element;
    }
    public VisualElement CreateModuleUI(string moduleName)
    {
        var element = new VisualElement();
        var prop = serializedObject.FindProperty(moduleName);
        var field = new PropertyField(prop);
        element.Add(field);
        return element;

    }
}
