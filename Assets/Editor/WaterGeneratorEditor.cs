using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WaterGenerator))]
public class WaterGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        WaterGenerator f = target as WaterGenerator;

        if (GUILayout.Button("GenerateWaterMesh"))
        {
            f.GenerateWaterMesh();
        }

    }
}
