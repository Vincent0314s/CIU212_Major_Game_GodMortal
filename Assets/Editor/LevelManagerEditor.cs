using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    public override void OnInspectorGUI() {
        LevelManager levelManager = target as LevelManager;
        
        levelManager.levelParentToLoad = (GameObject)EditorGUILayout.ObjectField("LevelParentObject", levelManager.levelParentToLoad, typeof(GameObject),true);

        if (levelManager.levelParentToLoad) {
            levelManager.UpdateList();
            var serializedObject = new SerializedObject(target);
            var property = serializedObject.FindProperty("levels");
            serializedObject.Update();
            EditorGUILayout.PropertyField(property, true);
        }
    }
}
