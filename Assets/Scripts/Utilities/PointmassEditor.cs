using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pointmass))]
public class PointmassEditor : Editor
{
    private SerializedProperty mass;
    private SerializedProperty position;

    void OnEnable()
    {
        mass = serializedObject.FindProperty("mass");
        position = serializedObject.FindProperty("position");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Pointmass Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(mass);
        EditorGUILayout.PropertyField(position);
        
        serializedObject.ApplyModifiedProperties();
    }
}
