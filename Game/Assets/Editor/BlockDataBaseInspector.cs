#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[InitializeOnLoad]
[CustomEditor(typeof(BlockDataBase))]
public class BlockDataBaseInspetor : Editor
{
    private BlockDataBase myTarget;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        myTarget = target as BlockDataBase;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("nowblockGroup"), true);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attackBlockGroup"), true);




        //GUILayout.Label("-----------------------------------------");
        //if (GUILayout.Button("ExportMesh"))
        //{
        //    Undo.RecordObject(myTarget, "ExportMesh");
        //    myTarget.ExportMesh();
        //    EditorUtility.SetDirty(myTarget);
        //}
        serializedObject.ApplyModifiedProperties();
    }
}
#endif