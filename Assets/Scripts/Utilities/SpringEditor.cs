using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spring))]
public class SpringEditor : Editor
{
    private SerializedProperty rootLinked;
    private SerializedProperty endLinked;
    private SerializedProperty springConstant;
    private SerializedProperty restLength;

    void OnEnable()
    {
        rootLinked = serializedObject.FindProperty("rootLinkedIndex");
        endLinked = serializedObject.FindProperty("endLinkedIndex");
        springConstant = serializedObject.FindProperty("springConstant");
        restLength = serializedObject.FindProperty("restLength");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Spring Properties", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(rootLinked);
        EditorGUILayout.PropertyField(endLinked);
        EditorGUILayout.PropertyField(springConstant);
        EditorGUILayout.PropertyField(restLength);
        
        serializedObject.ApplyModifiedProperties();
    }
}
