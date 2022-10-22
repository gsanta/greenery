
using game.character.characters.enemy;
using Game.Stage;
using System;
using UnityEditor;
using UnityEngine;

public class StageDebug : MonoBehaviour
{
    private StageType _selectedStageType = 0;

    private StageManager _stageManager;

    public void RenderGui()
    {
        InitMembers();
        RenderTitle();
        RenderControl();
    }

    private void InitMembers()
    {
        //if (!_stageManager)
        //{
        //    _stageManager = FindObjectOfType<StageManager>();
        //}

    }

    private void RenderTitle()
    {
        GUILayout.Space(10);

        EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 1), Color.gray);

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Stage", new GUIStyle()
        {
            normal =
        {
            textColor = Color.white
        },
            fontStyle = FontStyle.Bold
        });
    }

    private void RenderControl()
    {
        _selectedStageType = (StageType)EditorGUILayout.EnumPopup("Stage", _selectedStageType);

        if (GUILayout.Button("Set stage"))
        {
            //if (_stageManager)
            //{
            //    _stageManager.ActivateStage(_selectedStageType);
            //}
        }
    }
}
