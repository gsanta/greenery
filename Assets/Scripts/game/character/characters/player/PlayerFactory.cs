using game.character.ability.health;
using game.tool.weapon;
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

        private GameInfoStore _gameInfoStore;
        
        private HealthBar _healthBar;

        private WeaponFactory _weaponFactory;

        public void Construct(PlayerStore playerStore, GameInfoStore gameInfoStore, HealthBar healthBar, WeaponFactory weaponFactory)
        {
            _playerStore = playerStore;
            _gameInfoStore = gameInfoStore;
            _healthBar = healthBar;
            _weaponFactory = weaponFactory;
        }

        public Player Create(Vector3 position)
        {
            var player = Instantiate(playerPrefab, position, transform.rotation, playerList);
            player.Construct();
            player.GetComponent<LineDrawer>().Construct(_gameInfoStore);
            player.GetComponent<Health>().Construct(player, 100, _healthBar);
            player.Weapon = _weaponFactory.CreateBomb(player);

            _playerStore.Add(player);
            return player;
        }
    }
}