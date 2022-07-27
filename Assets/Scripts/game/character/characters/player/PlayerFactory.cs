using game.character.ability.health;
using game.character.ability.shoot;
using game.character.characters.enemy;
using game.item;
using game.item.bullet;
using GUI;
using UnityEngine;

namespace game.character.characters.player
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

        private GameManager _gameManager;
        
        public void Construct(PlayerStore playerStore, EnemyStore enemyStore, ItemStore<Ball> ballStore, GameInfoStore gameInfoStore, HealthBar healthBar, BulletFactory bulletFactory, GameManager gameManager)
        {
            _playerStore = playerStore;
            _enemyStore = enemyStore;
            _ballStore = ballStore;
            _gameInfoStore = gameInfoStore;
            _healthBar = healthBar;
            _bulletFactory = bulletFactory;
            _gameManager = gameManager;
        }

        public Player Create(Vector3 position)
        {
            var player = Instantiate(playerPrefab, position, transform.rotation, playerList);
            player.Construct(_gameManager);
            player.GetComponent<ItemPickup>().Construct(_ballStore, _gameInfoStore);
            player.GetComponent<LineDrawer>().Construct(_gameInfoStore);
            player.GetComponent<Health>().Construct(player, 100, _healthBar);
            player.GetComponent<Shooting>().Construct(player.GetComponent<Player>(), _bulletFactory, _enemyStore);
            _playerStore.Add(player);
            return player;
        }
    }
}