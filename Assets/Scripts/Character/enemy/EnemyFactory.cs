using AI.grid;
using AI.grid.path;
using AI.state.character;
using Character.ability.abilities.shoot;
using Character.player;
using Characters;
using Characters.Common;
using Characters.Enemies;
using GameLogic;
using Items.Bullet;
using UnityEngine;

namespace Character.enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        public Enemy enemyPrefab;

        public Transform spawnPosition;

        private float _timer;

        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;

        private BulletFactory _bulletFactory;

        private GameManager _gameManager;

        private GridModule _gridModule;

        private StateFactory _stateFactory;

        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, BulletFactory bulletFactory, GameManager gameManager, GridModule gridModule, StateFactory stateFactory)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _bulletFactory = bulletFactory;
            _gameManager = gameManager;
            _gridModule = gridModule;
            _stateFactory = stateFactory;
        }

        public void Create()
        {
            var enemy = Instantiate(enemyPrefab, spawnPosition.position, transform.rotation);
            enemy.Construct(_enemyStore, _playerStore, _gameManager);
            enemy.GetComponent<Shooting>().Construct(enemy, _bulletFactory, _playerStore);
            enemy.GetComponent<Health>().Construct(enemy, 100, null);
            var pathMovement = enemy.GetComponent<PathMovement>();
            pathMovement.Construct(_gridModule.PathFinding);

            var roamingState = _stateFactory.CreateRoamingState(enemy, enemy.gameObject);
            enemy.StateHandler.AddState(roamingState, true);
            var chasingState = _stateFactory.CreateChasingState(enemy, enemy.gameObject);
            enemy.StateHandler.AddState(chasingState);
            
            _enemyStore.Add(enemy);
        }
    }
}