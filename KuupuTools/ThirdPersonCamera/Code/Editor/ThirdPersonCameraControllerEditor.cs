using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[CustomEditor(typeof(ThirdPersonCameraController))]
public class ThirdPersonCameraControllerEditor : Editor
{
    VisualElement _root;
    VisualTreeAsset _assetTree;
    private void OnEnable()
    {
        _root = new VisualElement();

        _assetTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/KuupuTools/KuupuTools/ThirdPersonCamera/Code/EditorScripts/ThirdPersonCameraEditorTemplate.uxml");
        Debug.Log(_assetTree);

    }
}
