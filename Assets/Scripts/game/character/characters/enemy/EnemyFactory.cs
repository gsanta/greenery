using game.character.ability.health;
using game.character.ability.shoot;
using game.character.characters.player;
using game.character.state;
using game.item.bullet;
using game.scene.grid;
using game.scene.grid.path;
using game.scene.level;
using game.tool.weapon;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public Enemy enemyPrefab;

        private float _timer;

        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private WeaponFactory _weaponFactory;

        private GameManager _gameManager;

        private StateFactory _stateFactory;

        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, WeaponFactory weaponFactory, GameManager gameManager, StateFactory stateFactory)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _weaponFactory = weaponFactory;
            _gameManager = gameManager;
            _stateFactory = stateFactory;
        }

        public void Create(Level level)
        {
            var spawnPosGrid = level.Grid.Graph.GetRandomGridPosition();
            var spawnPosWorld = level.Grid.Graph.GetWorldPosition(spawnPosGrid.x, spawnPosGrid.y);
            var enemy = Instantiate(enemyPrefab, spawnPosWorld, transform.rotation);
            enemy.Construct(_enemyStore, _playerStore, _gameManager);

            enemy.Weapon = _weaponFactory.CreateGun(enemy);

            var shooting = enemy.GetComponent<ShootingBehaviour>();
            shooting.Speed = 8f;
            shooting.Construct(enemy, _playerStore);
            
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