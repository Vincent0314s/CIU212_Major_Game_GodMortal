using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]
public class LevelManagerEditor : Editor
{
    public override void OnInspectorGUI() {
        LevelManager levelManager = target as LevelManager;

        var ser = new SerializedObject(target);
        var pro = serializedObject.FindProperty("levelsToStart");
        serializedObject.Update();
        EditorGUILayout.PropertyField(pro, true);

        levelManager.levelLayoutParentToLoad = (GameObject)EditorGUILayout.ObjectField("LevelLayoutParentObject", levelManager.levelLayoutParentToLoad, typeof(GameObject),true);

        if (levelManager.levelLayoutParentToLoad) {
            levelManager.UpdateLayoutList();
            var serializedObject = new SerializedObject(target);
            var property = serializedObject.FindProperty("levels");
            serializedObject.Update();
            EditorGUILayout.PropertyField(property, true);
        }

        levelManager.levelInformationParentToLoad = (GameObject)EditorGUILayout.ObjectField("LevelInfoParentObject", levelManager.levelInformationParentToLoad, typeof(GameObject), true);
        if (levelManager.levelInformationParentToLoad)
        {
            levelManager.UpdateLevelInfoList();
            var serializedObject = new SerializedObject(target);
            var property = serializedObject.FindProperty("levelinfos");
            serializedObject.Update();
            EditorGUILayout.PropertyField(property, true);
        }
    }
}
