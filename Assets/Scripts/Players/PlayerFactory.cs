using GameInfo;
using GUI;
using Items;
using UnityEngine;

namespace Players
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab; 
        [SerializeField] private Transform playerList; 
        [SerializeField] private Transform playerSpawnPosition; 
        private PlayerStore _playerStore;
        private ItemStore<Ball> _ballStore;
        private GameInfoStore _gameInfoStore;
        private HealthBar _healthBar;
        
        public void Construct(PlayerStore playerStore, ItemStore<Ball> ballStore, GameInfoStore gameInfoStore, HealthBar healthBar)
        {
            _playerStore = playerStore;
            _ballStore = ballStore;
            _gameInfoStore = gameInfoStore;
            _healthBar = healthBar;
        }

        public Player Create()
        {
            var player = Instantiate(playerPrefab, playerSpawnPosition.position, transform.rotation, playerList);
            player.GetComponent<ItemPickup>().Construct(_ballStore, _gameInfoStore);
            player.GetComponent<LineDrawer>().Construct(_gameInfoStore);
            player.GetComponent<Health>().Construct(_healthBar);
            _playerStore.AddPlayer(player);
            return player;
        }
    }
}