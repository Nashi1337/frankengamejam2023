using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileSpawningManager))]
public class TileSpawningManagerEditor : Editor
{
    private Vector2Int _position;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(!Application.isPlaying )
        {
            return;
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.LabelField("Testing the manager");
        TileSpawningManager manager = (TileSpawningManager)target;
        _position = EditorGUILayout.Vector2IntField("Position", _position);
        if(GUILayout.Button("Spawn next"))
        {
            manager.SpawnTile(_position);
        }
    }
}
