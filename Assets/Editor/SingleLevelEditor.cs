using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SingleLevel))]
public class SingleLevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SingleLevel sl = target as SingleLevel;

        sl.MatchGameobjectName();

    }
}
