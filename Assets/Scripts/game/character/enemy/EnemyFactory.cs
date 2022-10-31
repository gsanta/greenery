using game.character.ability.field_of_view;
using game.character.ability.health;
using game.character.ability.shoot;
using game.character.characters.player;
using game.character.enemy;
using game.character.movement;
using game.character.movement.path;
using game.character.player;
using game.character.state;
using game.scene.grid.path;
using game.scene.level;
using game.tool.weapon;
using System;
using UnityEngine;

namespace game.character.characters.enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private GameObject beetlePrefab;

        [SerializeField] private GameObject bumbleBeePrefab;

        [SerializeField] private GameObject spawningPrefab;
        
        [SerializeField] private FieldOfViewVisualizer fieldOfViewPrefab;

        public bool WidthFieldOfViewVisualizer { set; private get; }

        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private WeaponFactory _weaponFactory;

        private GameManager _gameManager;

        private StateFactory _stateFactory;

        private EnemyDecorator[] _enemyDecorators;

        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, WeaponFactory weaponFactory, GameManager gameManager, StateFactory stateFactory, EnemyDecorator[] enemyDecorators)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _weaponFactory = weaponFactory;
            _gameManager = gameManager;
            _stateFactory = stateFactory;
            _enemyDecorators = enemyDecorators;
        }

        public Enemy Create(CharacterType characterType, Vector3 pos, Level level)
        {
            var enemyBase = Instantiate(GetPrefab(characterType), pos, transform.rotation);
            var obj = enemyBase.gameObject;

            var enemy = obj.AddComponent(typeof(Enemy)) as Enemy;
            
            var gun = _weaponFactory.CreateGun(enemy);
            enemy.WeaponHolder.AddWeapon(gun);
            enemy.WeaponHolder.ActivateWeapon(gun);
            enemy.Level = level;

            var fieldOfView = new FieldOfView(enemy, _playerStore, "Characters");
            enemy.FieldOfView = fieldOfView;

            //if (WidthFieldOfViewVisualizer)
            //{
            //    var fieldOfViewVisualizer = Instantiate(fieldOfViewPrefab, new Vector3(0, 0, 0), transform.rotation);
            //    fieldOfViewVisualizer.Construct(fieldOfView, enemy, _playerStore);
            //    enemy.AddDestroyable(fieldOfViewVisualizer.gameObject);
            //}

            var shooting = obj.AddComponent(typeof(ShootingBehaviour)) as ShootingBehaviour;
            shooting.Speed = 8f;
            shooting.Construct(enemy, _playerStore);

            var health = obj.AddComponent(typeof(Health)) as Health;
            health.Construct(enemy, null, new PlayerStats(3));

            var movementPath = new MovementPath();
            enemy.MovementPath = movementPath;

            var movementPathCalc = obj.AddComponent(typeof(TargetPathFinder)) as TargetPathFinder;
            movementPathCalc.Construct(level.Grid, movementPath);

            var movement = obj.AddComponent(typeof(LerpMover)) as LerpMover;
            movement.Construct(enemy, movementPath);

            //enemy.Movement = movement;

            //var mover = obj.AddComponent(typeof(PathMover)) as PathMover;
            //mover.Construct(movement, level.Grid);

            enemy.Construct(_enemyStore, _playerStore, _gameManager);

            //var roamingState = _stateFactory.CreateRoamingState(enemy, enemy.gameObject);
            //enemy.States.AddState(roamingState, true);
            var chasingState = _stateFactory.CreateChasingState(enemy, enemyBase, movementPathCalc);
            enemy.States.AddState(chasingState);
            enemy.States.SetActiveState(CharacterStateType.ChasingState);

            _enemyStore.Add(enemy);

            return enemy;
        }

        public void ApplyDecorator(string name)
        {
            _enemyStore.GetAll().ForEach((enemy) => Array.Find(_enemyDecorators, decorator => decorator.Name == name)?.Apply(enemy));
        }

        public void RemoveDecorator(string name)
        {
            _enemyStore.GetAll().ForEach((enemy) => Array.Find(_enemyDecorators, decorator => decorator.Name == name)?.Remove(enemy));
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