
using Assets.Scripts.debug;
using game.scene.grid;
using game.scene.level;
using UnityEditor;
using UnityEngine;

public class LevelDebug : MonoBehaviour
{
    [SerializeField] private Transform gridPositionTester;

    private LevelStore _levelStore;

    private LevelTileRenderer _tileRenderer;

    private int selectedLevelIndex = 0;

    public void RenderGui()
    {
        InitMembers();
        
        if (!_levelStore) { return; }
        
        selectedLevelIndex = EditorGUILayout.Popup("Level", selectedLevelIndex, _levelStore.GetLevelNames());

        if (GUILayout.Button("Visualize grid"))
        {
            var level = GetSelectedLevel();
            _tileRenderer.SetLevel(level);
            if (_tileRenderer.IsVisualize)
            {
                _tileRenderer.Hide();
            } else
            {
                _tileRenderer.Show();
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

        if (!_tileRenderer)
        {
            _tileRenderer = FindObjectOfType<LevelTileRenderer>();
        }
    }
}
