
using Assets.Scripts.debug;
using game.scene.level;
using UnityEditor;
using UnityEngine;

public class LevelDebug : MonoBehaviour
{
    [SerializeField] private Transform gridPositionTester;

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
            if (level.gridVisualizer.IsVisualize)
            {
                level.gridVisualizer.Hide();
            } else
            {
                level.gridVisualizer.Show();
            }
        }

        if (GUILayout.Button("Render debug path"))
        {
            var pathFindingDebug = FindObjectOfType<PathFindingDebug>();
            var level = GetSelectedLevel();
            pathFindingDebug.RenderPath(level);
        }

        if (GUILayout.Button("Get grid position"))
        {

            var level = GetSelectedLevel();
            var levelOffset = level.EnvironmentData.Center;
            var pos = new Vector2(gridPositionTester.position.x - levelOffset.x, gridPositionTester.position.y - levelOffset.y);
            var gridPos = level.Grid.GetGridPosition(pos);
            if (gridPos != null)
            {
                var isWalkable = level.Grid.GetNode(gridPos.Value.x, gridPos.Value.y).IsWalkable;
                Debug.Log("X: " + gridPos.Value.x + " Y: " + gridPos.Value.y + " isWalkable: " + isWalkable);
            } else
            {
                Debug.Log("Outside of grid");
            }
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
