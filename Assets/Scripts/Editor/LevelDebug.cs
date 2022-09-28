
using Assets.Scripts.debug;
using game.scene.level;
using UnityEditor;
using UnityEngine;

public class LevelDebug : MonoBehaviour
{
    private LevelStore _levelStore;

    private int selectedLevelIndex = 0;

    public void RenderGui()
    {
        InitMembers();
        
        if (!_levelStore) { return; }
        
        selectedLevelIndex = EditorGUILayout.Popup("Level", selectedLevelIndex, _levelStore.GetLevelNames());

        if (GUILayout.Button("Visualize grid"))
        {
            var level = GetSelectedLevel(); 
            level.gridVisualizer.ToggleVisualization();
        }

        if (GUILayout.Button("Render debug path"))
        {
            var pathFindingDebug = FindObjectOfType<PathFindingDebug>();
            var level = GetSelectedLevel();
            pathFindingDebug.RenderPath(level);
        }
    }

    private Level GetSelectedLevel()
    {
        var selectedLevelName = Levels.ReverseNameMap[_levelStore.GetLevelNames()[selectedLevelIndex]];
        return _levelStore.GetLevelByName(selectedLevelName);
    }

    private void InitMembers()
    {
        if (!_levelStore)
        {
            _levelStore = FindObjectOfType<LevelStore>();
        }
    }
}
