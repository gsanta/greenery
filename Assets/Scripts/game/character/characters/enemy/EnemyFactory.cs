using game.character.ability.health;
using game.character.ability.shoot;
using game.character.ability.shoot.target;
using game.character.characters.player;
using game.character.state;
using game.item.bullet;
using game.scene.grid;
using game.scene.grid.path;
using game.scene.level;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public Enemy enemyPrefab;

        private float _timer;

        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private BulletFactory _bulletFactory;

        private GameManager _gameManager;

        private StateFactory _stateFactory;

        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, BulletFactory bulletFactory, GameManager gameManager, StateFactory stateFactory)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _bulletFactory = bulletFactory;
            _gameManager = gameManager;
            _stateFactory = stateFactory;
        }

        public void Create(Level level)
        {
            var spawnPosGrid = GridUtils.GetRandomGrid(level.Grid.Graph);
            var spawnPosWorld = level.Grid.Graph.GetWorldPosition(spawnPosGrid.x, spawnPosGrid.y);
            var enemy = Instantiate(enemyPrefab, spawnPosWorld, transform.rotation);
            enemy.Construct(_enemyStore, _playerStore, _gameManager);

            var shooting = enemy.GetComponent<Shooting>();
            shooting.Speed = 8f;
            var shootTarget = new ShootAtPlayer(enemy, _playerStore);
            shooting.Construct(enemy, _bulletFactory, _playerStore, shootTarget);
            
            enemy.GetComponent<Health>().Construct(enemy, 100, null);
            var pathMovement = enemy.GetComponent<PathMovement>();
            pathMovement.Construct(level.Grid.PathFinding);

            var roamingState = _stateFactory.CreateRoamingState(enemy, enemy.gameObject);
            enemy.States.AddState(roamingState, true);
            var chasingState = _stateFactory.CreateChasingState(enemy, enemy.gameObject);
            enemy.States.AddState(chasingState);
            enemy.LevelName = level.levelName;
            
            _enemyStore.Add(enemy);
        }
    }
}