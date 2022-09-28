﻿
using game.character.characters.enemy;
using game.character.enemy;
using UnityEditor;
using UnityEngine;

public class GameSettingsWindow : EditorWindow
{
    private LevelDebug _levelDebug;

    private EnemyDebug _enemyDebug;

    private EnemySpawner enemySpawner;

    private bool isEnemySpawnPointOn = false;

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

        RenderEnemySpawnPosition();
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
        if (!_levelDebug)
        {
            _levelDebug = FindObjectOfType<LevelDebug>();
        }

        if (!_levelDebug) { return; }

        _levelDebug.RenderGui();
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

    private void RenderEnemySpawnPosition()
    {
        if (!_enemyDebug)
        {
            _enemyDebug = FindObjectOfType<EnemyDebug>();
        }

        if (!enemySpawner)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        GUILayout.Space(10);

        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Enemy", new GUIStyle()
        {
            normal =
            {
                textColor = Color.white
            },
            fontStyle = FontStyle.Bold
        });

        _enemyDebug.RenderGui();
        isEnemySpawnPointOn = GUILayout.Toggle(isEnemySpawnPointOn, "Manual spawn");

        if (enemySpawner != null)
        {
                enemySpawner.IsManualSpawning = isEnemySpawnPointOn;
        }
    }
}
