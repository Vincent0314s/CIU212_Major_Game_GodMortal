using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Platform platform = target as Platform;

        SerializedProperty colliderOffset = serializedObject.FindProperty("colliderOffset");
        EditorGUILayout.PropertyField(colliderOffset);

        SerializedProperty colliderSize = serializedObject.FindProperty("colliderSize");
        EditorGUILayout.PropertyField(colliderSize);

        SerializedProperty colliderColor = serializedObject.FindProperty("colliderColor");
        EditorGUILayout.PropertyField(colliderColor);

        GUILayout.Space(25);

        if (GUILayout.Button("GenerateBoderer")) {
            platform.GenerateConfineedCollider();
        }

        if (GUILayout.Button("ClearBorder")) {
            platform.ClearBorder();
        }

        if (!EditorApplication.isPlaying) {
            platform.FindExistedBorders();
        }

        if (!platform.AreCollidersNull()) {
            platform.UpdateData();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
