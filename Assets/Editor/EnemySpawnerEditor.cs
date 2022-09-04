using game.character;
using game.character.characters.enemy;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemySpawner enemySpawner = (EnemySpawner)target;

        enemySpawner.editorSpawnPos = EditorGUILayout.Vector2Field("Spawn Point", enemySpawner.editorSpawnPos);

        enemySpawner.CharacterType = (CharacterType) EditorGUILayout.EnumPopup("Character type", enemySpawner.CharacterType);

        enemySpawner.IsPausedInInspector = EditorGUILayout.Toggle("Pause Spawning", enemySpawner.IsPausedInInspector);

        if (GUILayout.Button("Spawn"))
        {
            var pos = enemySpawner.editorSpawnPos;
            enemySpawner.SpawnAt(enemySpawner.CharacterType, new Vector3(pos.x, pos.y, 0));
        }
    }
}
