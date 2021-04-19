using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCheckPoint))]
public class LevelCheckPointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelCheckPoint _levelcheckpoint = target as LevelCheckPoint;

        SerializedProperty poiotType = serializedObject.FindProperty("levelState");
        EditorGUILayout.PropertyField(poiotType);

        switch (_levelcheckpoint.levelState) {
            case LevelCheckState.StartPoint:
                _levelcheckpoint.startPointColor = EditorGUILayout.ColorField("StartPointColor", _levelcheckpoint.startPointColor);
                break;
            case LevelCheckState.CheckPoint:
                SerializedProperty checkPointT = serializedObject.FindProperty("type");
                EditorGUILayout.PropertyField(checkPointT);
                _levelcheckpoint.checkPointColor = EditorGUILayout.ColorField("CheckPointColor", _levelcheckpoint.checkPointColor);


                GUILayout.Space(25);
                _levelcheckpoint.canActivatePositiveIndex = EditorGUILayout.Toggle("EnablePositiveIndex", _levelcheckpoint.canActivatePositiveIndex);
                if (_levelcheckpoint.canActivatePositiveIndex) {
                    EditorGUI.indentLevel++;

                    SerializedProperty positiveIndex = serializedObject.FindProperty("levelToShowIndex_positive");
                    EditorGUILayout.PropertyField(positiveIndex);

                    EditorGUI.indentLevel--;
                }
                
                _levelcheckpoint.canActivateNegativeIndex = EditorGUILayout.Toggle("EnableNegativeIndex", _levelcheckpoint.canActivateNegativeIndex);
                if (_levelcheckpoint.canActivateNegativeIndex) {
                    EditorGUI.indentLevel++;

                    SerializedProperty negativeIndex = serializedObject.FindProperty("levelToShowIndex_negative");
                    EditorGUILayout.PropertyField(negativeIndex);

                    EditorGUI.indentLevel--;
                }
                break;
            case LevelCheckState.EndPoint:
                _levelcheckpoint.endPointColor = EditorGUILayout.ColorField("EndPointColor", _levelcheckpoint.endPointColor);

                break;
        }

        GUILayout.Space(25);
        SerializedProperty shapeType = serializedObject.FindProperty("shape");
        EditorGUILayout.PropertyField(shapeType);

        switch (_levelcheckpoint.shape)
        {
            case CheckPointShape.Box:
                _levelcheckpoint.size = EditorGUILayout.Vector3Field("Box Size", _levelcheckpoint.size);
                break;
            case CheckPointShape.Sphere:
                _levelcheckpoint.radius = EditorGUILayout.FloatField("Sphere Size", _levelcheckpoint.radius);
                break;
         
        }




        serializedObject.ApplyModifiedProperties();
        //Not sure this is needed.
        //EditorUtility.SetDirty(_levelcheckpoint);
    }
}
