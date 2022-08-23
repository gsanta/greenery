using game.character.ability.health;
using game.character.player;
using game.scene;
using game.tool.weapon;
using GUI;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab;

        [SerializeField] private Player cowPrefab;

        [SerializeField] private Transform playerList; 
        
        private PlayerStore _playerStore;

        private GameInfoStore _gameInfoStore;
        
        private HealthBar _healthBar;

        private WeaponFactory _weaponFactory;

        private FollowCamera _camera;

        public void Construct(PlayerStore playerStore, GameInfoStore gameInfoStore, HealthBar healthBar, WeaponFactory weaponFactory, FollowCamera camera)
        {
            _playerStore = playerStore;
            _gameInfoStore = gameInfoStore;
            _healthBar = healthBar;
            _weaponFactory = weaponFactory;
            _camera = camera;
        }

        public Player Create(Vector3 position, PlayerType playerType)
        {
            switch(playerType)
            {
                case PlayerType.Cat:
                    return CreateCat(position);
                case PlayerType.Cow:
                    return CreateCow(position);
                default:
                    return null;
            }
        }

        private Player CreateCat(Vector3 position) {
            var player = Instantiate(playerPrefab, position, transform.rotation, playerList);
            player.Construct(PlayerType.Cat);
            player.GetComponent<LineDrawer>().Construct(_gameInfoStore);
            player.GetComponent<Health>().Construct(player, 100, _healthBar);
            player.Weapon = _weaponFactory.CreateGun(player);
            ActivatePlayer(player);

            _playerStore.Add(player);

            return player;
        }

        private Player CreateCow(Vector3 position)
        {
            var player = Instantiate(cowPrefab, position, transform.rotation, playerList);
            player.Construct(PlayerType.Cow);
            player.GetComponent<Health>().Construct(player, 100, _healthBar);
            player.Weapon = _weaponFactory.CreateBomb(player);
            ActivatePlayer(player);

            _playerStore.Add(player);
            return player;
        }

        private void ActivatePlayer(Player player)
        {
            player.IsActive = true;
            _camera.SetTarget(player);
        }
    }
}