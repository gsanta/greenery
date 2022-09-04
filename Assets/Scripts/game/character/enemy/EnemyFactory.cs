using game.character.ability.health;
using game.character.ability.shoot;
using game.character.characters.player;
using game.character.player;
using game.character.state;
using game.scene.grid.path;
using game.scene.level;
using game.tool.weapon;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private GameObject beetlePrefab;

        [SerializeField] private GameObject bumbleBeePrefab;

        [SerializeField] private GameObject spawningPrefab;

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

        public Enemy Create(CharacterType characterType, Vector3 pos, Level level)
        {
            var enemyBase = Instantiate(GetPrefab(characterType), pos, transform.rotation);
            var obj = enemyBase.gameObject;

            var enemy = obj.AddComponent(typeof(Enemy)) as Enemy;
            enemy.Construct(_enemyStore, _playerStore, _gameManager);
            enemy.Weapon = _weaponFactory.CreateGun(enemy);

            var shooting = obj.AddComponent(typeof(ShootingBehaviour)) as ShootingBehaviour;
            shooting.Speed = 8f;
            shooting.Construct(enemy, _playerStore);

            var health = obj.AddComponent(typeof(Health)) as Health;
            health.Construct(enemy, null, new PlayerStats(3));

            var pathMovement = obj.AddComponent(typeof(PathMovement)) as PathMovement;
            pathMovement.Construct(level.Grid.PathFinding);

            //var roamingState = _stateFactory.CreateRoamingState(enemy, enemy.gameObject);
            //enemy.States.AddState(roamingState, true);
            var chasingState = _stateFactory.CreateChasingState(enemy, enemy.gameObject);
            enemy.States.AddState(chasingState);
            enemy.LevelName = level.levelName;
            enemy.States.SetActiveState(CharacterStateType.ChasingState);

            _enemyStore.Add(enemy);

            return enemy;
        }

        private GameObject GetPrefab(CharacterType characterType)
        {
            switch(characterType)
            {
                case CharacterType.Beetle:
                    return beetlePrefab;
                case CharacterType.Bumblebee:
                default:
                    return bumbleBeePrefab;
            }
        }

        public GameObject CreateSpawnAnimation(Vector3 pos)
        {
            var obj = Instantiate(spawningPrefab, pos, transform.rotation);

            return obj.gameObject;
        }
    }
}