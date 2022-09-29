

using game.character;
using game.character.characters.enemy;
using UnityEditor;
using UnityEngine;

public class EnemyDebug : MonoBehaviour {

    private EnemyStore enemyStore;

    private EnemySpawner enemySpawner;

    private static CharacterType selectedEnemyType = 0;

    private bool isEnemySpawnPointOn = false;

    public void RenderGui()
    {
        InitMembers();

        RenderTitle();

        selectedEnemyType = (CharacterType) EditorGUILayout.EnumPopup("Enemy Type", selectedEnemyType);

        GUILayout.Space(10);

        RenderEnemyCreation();

        GUILayout.Space(10);

        if (GUILayout.Button("Remove enemies"))
        {
            while(enemyStore.GetAll().Count > 0)
            {
                enemyStore.GetAll()[0].Die();
            }
        }
    }

    private void RenderTitle()
    {

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
    }

    private void RenderEnemyCreation()
    {

        if (GUILayout.Button("Create enemy"))
        {
            enemySpawner.SpawnAtManualPosition(selectedEnemyType);
        }

        isEnemySpawnPointOn = GUILayout.Toggle(isEnemySpawnPointOn, "Manual spawn");

        if (enemySpawner != null)
        {
            enemySpawner.IsManualSpawning = isEnemySpawnPointOn;
        }
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
