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
        
        private HealthPanel _healthBar;

        private WeaponFactory _weaponFactory;

        private FollowCamera _camera;

        public void Construct(PlayerStore playerStore, GameInfoStore gameInfoStore, HealthPanel healthBar, WeaponFactory weaponFactory, FollowCamera camera)
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
            var stat = _playerStore.GetStat(PlayerType.Cat);
            player.Construct(PlayerType.Cat, stat);
            player.GetComponent<LineDrawer>().Construct(_gameInfoStore);
            player.GetComponent<Health>().Construct(player, _healthBar, _playerStore.GetStat(PlayerType.Cat));
            player.Weapon = _weaponFactory.CreateGun(player);
            player.Weapon.Bullets = stat.Bullets;
            ActivatePlayer(player);

            _playerStore.Add(player);

            return player;
        }

        private Player CreateCow(Vector3 position)
        {
            var player = Instantiate(cowPrefab, position, transform.rotation, playerList);
            var stat = _playerStore.GetStat(PlayerType.Cow);
            player.Construct(PlayerType.Cow, stat);
            player.GetComponent<Health>().Construct(player, _healthBar, _playerStore.GetStat(PlayerType.Cow));
            player.Weapon = _weaponFactory.CreateBomb(player);
            player.Weapon.Bullets = stat.Bullets;
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