using Characters;
using Characters.Enemies;
using Characters.Players;
using GameInfo;
using GUI;
using Items;
using Items.Bullet;
using UnityEngine;

namespace Players
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab; 
        
        [SerializeField] private Transform playerList; 
        
        [SerializeField] private Transform playerSpawnPosition; 
        
        private PlayerStore _playerStore;

        private EnemyStore _enemyStore;
        
        private ItemStore<Ball> _ballStore;
        
        private GameInfoStore _gameInfoStore;
        
        private HealthBar _healthBar;
        
        private BulletFactory _bulletFactory;
        
        public void Construct(PlayerStore playerStore, EnemyStore enemyStore, ItemStore<Ball> ballStore, GameInfoStore gameInfoStore, HealthBar healthBar, BulletFactory bulletFactory)
        {
            _playerStore = playerStore;
            _enemyStore = enemyStore;
            _ballStore = ballStore;
            _gameInfoStore = gameInfoStore;
            _healthBar = healthBar;
            _bulletFactory = bulletFactory;
        }

        public Player Create()
        {
            var player = Instantiate(playerPrefab, playerSpawnPosition.position, transform.rotation, playerList);
            player.GetComponent<ItemPickup>().Construct(_ballStore, _gameInfoStore);
            player.GetComponent<LineDrawer>().Construct(_gameInfoStore);
            player.GetComponent<Health>().Construct(_healthBar);
            player.GetComponent<Shooting>().Construct(player.GetComponent<Player>(), _bulletFactory, _enemyStore);
            _playerStore.Add(player);
            return player;
        }
    }
}