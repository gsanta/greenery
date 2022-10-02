using game.scene.level;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace game.character.characters.enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyFactory _enemyFactory;

        private EnemyStore _enemyStore;

        private LevelStore _levelStore;

        [SerializeField] private Transform manualSpawnPoint;

        public Vector2 editorSpawnPos;

        public bool IsActive { set; private get; }

        public bool IsManualSpawning { set; get; }

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
            if (!IsActive || _levelStore.ActiveLevel.Grid == null || IsManualSpawning || _enemyStore.Count() > 0 || isSpawning)
            {
                return;
            }

            isSpawning = true;
            SpawnRandom();
        }

        public void SpawnRandom() 
        {
            StartCoroutine(Spawn(GetRandomType(), GetRandomPos()));
        }

        private Vector2 GetRandomPos()
        {
            var level = _levelStore.ActiveLevel;

            var node = level.Grid.GetRandomNode();
            var worldPos = level.Grid.GetWorldPosition(node.X, node.Y);

            return worldPos;
        }

        private CharacterType GetRandomType()
        {
            var types = new CharacterType[] { CharacterType.Beetle, CharacterType.Bumblebee };
            var randomIndex = Random.Range(0, types.Length);

            return types[randomIndex];
        }

        public void SpawnAtManualPosition(CharacterType type)
        {
            StartCoroutine(Spawn(type, manualSpawnPoint.transform.position));
        }

        private IEnumerator Spawn(CharacterType type, Vector3 spawnPos)
        {
            var level = _levelStore.ActiveLevel;

            var anim = _enemyFactory.CreateSpawnAnimation(spawnPos);
            yield return new WaitForSeconds(2f);
            Destroy(anim.gameObject, 0.1f);
            _enemyFactory.Create(type, anim.transform.position, level);
            isSpawning = false;
        }
    }

}