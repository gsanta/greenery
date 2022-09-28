

using game.character;
using game.character.characters.enemy;
using UnityEditor;
using UnityEngine;

public class EnemyDebug : MonoBehaviour {

    private EnemyStore enemyStore;

    private EnemySpawner enemySpawner;

    private static CharacterType selectedEnemyType = 0;

    public void RenderGui()
    {
        InitMembers();

        selectedEnemyType = (CharacterType) EditorGUILayout.EnumPopup("Enemy Type", selectedEnemyType);

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create enemy", GUILayout.Width(200)))
        {
            enemySpawner.SpawnAtManualPosition(selectedEnemyType);
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Remove enemies", GUILayout.Width(200)))
        {
            while(enemyStore.GetAll().Count > 0)
            {
                enemyStore.GetAll()[0].Die();
            }
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    private void InitMembers()
    {
        if (!enemyStore)
        {
            enemyStore = FindObjectOfType<EnemyStore>();
        }

        if (!enemySpawner)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }
    }

}
