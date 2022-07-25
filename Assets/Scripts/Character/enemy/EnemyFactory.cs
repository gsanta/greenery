using AI.grid;
using AI.grid.path;
using AI.state.character.states;
using Character.player;
using Characters.Common;
using GameLogic;
using Items.Bullet;
using UnityEngine;

namespace Characters.Enemies
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

        public void Construct(EnemyStore enemyStore, PlayerStore playerStore, BulletFactory bulletFactory, GameManager gameManager, GridModule gridModule)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _bulletFactory = bulletFactory;
            _gameManager = gameManager;
            _gridModule = gridModule;
        }

        public void Create()
        {
            var enemy = Instantiate(enemyPrefab, spawnPosition.position, transform.rotation);
            enemy.Construct(_enemyStore, _playerStore, _gameManager);
            enemy.GetComponent<Shooting>().Construct(enemy, _bulletFactory, _playerStore);
            enemy.GetComponent<Health>().Construct(enemy, 100, null);
            var pathMovement = enemy.GetComponent<PathMovement>();
            pathMovement.Construct(_gridModule.PathFinding);

            var roamingState = new RoamingState(_gridModule.Grid, enemy, pathMovement);
            enemy.SetRoamingState(roamingState);

            
            _enemyStore.Add(enemy);
        }
    }
}