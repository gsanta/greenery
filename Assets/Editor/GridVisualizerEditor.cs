using UnityEngine;
using UnityEditor;
using game.scene.grid;

[CustomEditor(typeof(GridVisualizer))]
public class GridVisualizerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GridVisualizer gridVisualizer = (GridVisualizer)target;

        if (GUILayout.Button("Visualize"))
        {
            gridVisualizer.ToggleVisualization();
        }
    }
}