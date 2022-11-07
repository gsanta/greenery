using game.character.characters.player;
using game.scene.level;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace game.character.characters.enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyFactory _enemyFactory;

        private PlayerStore _playerStore;

        private LevelStore _levelStore;

        [SerializeField] private Transform manualSpawnPoint;

        public Vector2 editorSpawnPos;

        public bool IsDisabled { set; private get; } = true;

        public bool IsManualSpawning { set; get; }

        public CharacterType CharacterType { get; set; } = CharacterType.Beetle; 

        private bool isSpawning;

        public void Construct(EnemyFactory enemyFactory, PlayerStore playerStore, LevelStore levelStore)
        {
            _enemyFactory = enemyFactory;
            _playerStore = playerStore;
            _levelStore = levelStore;
        }
        
        private void Update()
        {
            if (IsDisabled || _levelStore.ActiveLevel.Grid == null || IsManualSpawning || _playerStore.Count() > 0 || isSpawning)
            {
                return;
            }

            isSpawning = true;
            SpawnRandom(PlayerType.Enemy);
        }

        public void SpawnRandom(PlayerType playerType) 
        {
            StartCoroutine(Spawn(playerType, GetRandomType(), GetRandomPos()));
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

        public void SpawnAtManualPosition(PlayerType playerType, CharacterType type)
        {
            StartCoroutine(Spawn(playerType, type, manualSpawnPoint.transform.position));
        }

        public IEnumerator Spawn(PlayerType playerType, CharacterType type, Vector3 spawnPos)
        {
            var level = _levelStore.ActiveLevel;

            var anim = _enemyFactory.CreateSpawnAnimation(spawnPos);
            yield return new WaitForSeconds(2f);
            Destroy(anim.gameObject, 0.1f);
            _enemyFactory.Create(playerType, type, anim.transform.position, level);
            isSpawning = false;
        }
    }

}