using game.scene.level;
using System.Collections;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyFactory _enemyFactory;

        private EnemyStore _enemyStore;

        private LevelStore _levelStore;

        public Vector2 editorSpawnPos;

        public bool IsActive { set; private get; }

        public bool IsPausedInInspector { set; get; }

        public CharacterType CharacterType { get; set; } = CharacterType.Beetle; 

        private bool isSpawning;

        public void Construct(EnemyFactory enemyFactory, EnemyStore enemyStore, LevelStore levelStore)
        {
            _enemyFactory = enemyFactory;
            _enemyStore = enemyStore;
            _levelStore = levelStore;
        }
        
        private void Update()
        {
            if (!IsActive || IsPausedInInspector || _enemyStore.Count() > 0 || isSpawning)
            {
                return;
            }

            isSpawning = true;
            SpawnAtRandomPos();
        }

        public void SpawnAtRandomPos()
        {
            var level = _levelStore.Level;

            var spawnPosGrid = level.Grid.Graph.GetRandomGridPosition();
            var spawnPosWorld = level.Grid.Graph.GetWorldPosition(spawnPosGrid.x, spawnPosGrid.y);

            StartCoroutine(Spawn(CharacterType.Beetle, spawnPosWorld));
        }

        public void SpawnAt(CharacterType type, Vector3 pos)
        {
            StartCoroutine(Spawn(type, pos));
        }

        private IEnumerator Spawn(CharacterType type, Vector3 spawnPos)
        {
            var level = _levelStore.Level;

            var anim = _enemyFactory.CreateSpawnAnimation(spawnPos);
            yield return new WaitForSeconds(2f);
            Destroy(anim.gameObject, 0.1f);
            _enemyFactory.Create(type, anim.transform.position, level);
            isSpawning = false;
        }
    }
}