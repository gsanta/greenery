
using game.character.characters.enemy;
using game.character.enemy;
using game.scene.level;
using UnityEditor;
using UnityEngine;
using static Codice.Client.BaseCommands.Import.Commit;

public class GameSettingsWindow : EditorWindow
{

    int selected = 0;
    string[] options = new string[]
    {
     "Option1", "Option2", "Option3",
    };

    [MenuItem("Window/Game Settings")]
    public static void ShowWindow()
    {
        GetWindow<GameSettingsWindow>("Game Settings");
    }

    private void OnGUI()
    {
        RenderFovToggle();

        RenderPathVisual();

        GUILayout.Space(10);

        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);

        GUILayout.Space(10);

        RenderLevelSelect();
    }

    private void RenderFovToggle()
    {
        FovVisualDecorator.IsOn = GUILayout.Toggle(FovVisualDecorator.IsOn, FovVisualDecorator.DecoratorName);

        if (FovVisualDecorator.IsOn != FovVisualDecorator.PrevIsOn)
        {
            HandleDecoratorChange(FovVisualDecorator.DecoratorName, FovVisualDecorator.IsOn);
            FovVisualDecorator.PrevIsOn = FovVisualDecorator.IsOn;
        }
    }

    private void RenderPathVisual()
    {
        PathVisualDecorator.IsOn = GUILayout.Toggle(PathVisualDecorator.IsOn, PathVisualDecorator.DecoratorName);

        if (PathVisualDecorator.IsOn != PathVisualDecorator.PrevIsOn)
        {
            HandleDecoratorChange(PathVisualDecorator.DecoratorName, PathVisualDecorator.IsOn);
            PathVisualDecorator.PrevIsOn = PathVisualDecorator.IsOn;
        }
    }

    private void RenderLevelSelect()
    {
        var levelStore = FindObjectOfType<LevelStore>();

        selected = EditorGUILayout.Popup("Level", selected, levelStore.GetLevelNames());
    }

    private void HandleDecoratorChange(string decoratorName, bool isOn)
    {
        if (isOn)
        {
            var enemyFactory = FindObjectOfType<EnemyFactory>();
            if (enemyFactory)
            {
                enemyFactory.ApplyDecorator(decoratorName);
            }
        }
        else
        {
            var enemyFactory = FindObjectOfType<EnemyFactory>();
            enemyFactory.RemoveDecorator(decoratorName);
        }
    }
}
