using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spring))]
public class SpringEditor : Editor
{
    private SerializedProperty rootPosition;
    private SerializedProperty springConstant;
    private SerializedProperty restLength;

    void OnEnable()
    {
        rootPosition = serializedObject.FindProperty("rootPosition");
        springConstant = serializedObject.FindProperty("springConstant");
        restLength = serializedObject.FindProperty("restLength");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Spring Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(rootPosition);
        EditorGUILayout.PropertyField(springConstant);
        EditorGUILayout.PropertyField(restLength);
        
        serializedObject.ApplyModifiedProperties();
    }
}
