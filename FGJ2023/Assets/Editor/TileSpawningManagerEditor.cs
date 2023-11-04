using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Adds a test button during play mode to spawn new tiles.
/// </summary>
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
