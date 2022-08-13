using game.character.ability.health;
using game.character.ability.shoot;
using game.character.ability.shoot.target;
using game.character.characters.enemy;
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
        
        private GameInfoStore _gameInfoStore;
        
        private HealthBar _healthBar;
        
        private BulletFactory _bulletFactory;

        public void Construct(PlayerStore playerStore, EnemyStore enemyStore, GameInfoStore gameInfoStore, HealthBar healthBar, BulletFactory bulletFactory)
        {
            _playerStore = playerStore;
            _enemyStore = enemyStore;
            _gameInfoStore = gameInfoStore;
            _healthBar = healthBar;
            _bulletFactory = bulletFactory;
        }

        public Player Create(Vector3 position)
        {
            var player = Instantiate(playerPrefab, position, transform.rotation, playerList);
            player.Construct();
            player.GetComponent<LineDrawer>().Construct(_gameInfoStore);
            player.GetComponent<Health>().Construct(player, 100, _healthBar);

            var shootTarget = new ShootAtCursor(player);
            player.GetComponent<Shooting>().Construct(player.GetComponent<Player>(), _bulletFactory, shootTarget);
            _playerStore.Add(player);
            return player;
        }
    }
}