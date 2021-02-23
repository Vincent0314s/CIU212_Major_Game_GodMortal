using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnumListEditor : EditorWindow
{

    [SerializeField]
    private List<EnumList> enumLists = new List<EnumList> {
        new EnumList("VisualEffect"),
        new EnumList("SoundEffect"),
        new EnumList("UI_SoundEffects"),
        new EnumList("AIOwner"),
    };
    
    [MenuItem("VincentTools/Enum/Enum Generator")]
    public static void ShowWindow()
    {
         GetWindow<EnumListEditor>("EnumList");
    }

    [MenuItem("VincentTools/Enum/Generate Scene List")]
    public static void GenerateSceneList() {
        EnumListManager.AddSceneList();
    }

    private void OnEnable()
    {
        var data = EditorPrefs.GetString("SaveList", JsonUtility.ToJson(this, false));
        JsonUtility.FromJsonOverwrite(data, this);
    }


    void OnGUI()
    {
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty stringsProperty = so.FindProperty("enumLists");

        EditorGUILayout.PropertyField(stringsProperty,true);
        so.ApplyModifiedProperties();

        if (GUILayout.Button("Update Enum List",GUILayout.Width(200),GUILayout.Height(30)))
        {
            EnumListManager.AddNewEnum(enumLists);

            var data = JsonUtility.ToJson(this, false);
            EditorPrefs.SetString("SaveList", data);
        }
    }
}
