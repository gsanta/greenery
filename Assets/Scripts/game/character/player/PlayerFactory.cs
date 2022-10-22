using Assets.Scripts.game.character.characters.player;
using game.character.ability.field_of_view;
using game.character.ability.health;
using game.Item.grass;
using game.scene;
using game.tool.weapon;
using gui;
using GUI;
using System;
using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerFactory : MonoBehaviour
    {
        [SerializeField] private Player playerPrefab;

        [SerializeField] private Player cowPrefab;

        [SerializeField] private FieldOfView fieldOfViewPrefab;

        [SerializeField] private Transform playerList; 
        
        private PlayerStore _playerStore;

        private HealthPanel _healthPanel;

        private BulletPanel _bulletPanel;

        private WeaponFactory _weaponFactory;

        private FollowCamera _camera;

        public void Construct(PlayerStore playerStore, HealthPanel healthPanel, BulletPanel bulletPanel, WeaponFactory weaponFactory, FollowCamera camera)
        {
            _playerStore = playerStore;
            _healthPanel = healthPanel;
            _bulletPanel = bulletPanel;
            _weaponFactory = weaponFactory;
            _camera = camera;
        }


        public Player Create(Vector3 position, CharacterType playerType)
        {
            //var prevPlayer = _playerStore.GetActivePlayer();

            Player newPlayer;
            switch(playerType)
            {
                case CharacterType.Cat:
                    newPlayer = CreateCat(position);
                    break;
                case CharacterType.Cow:
                    newPlayer = CreateCow(position);
                    break;
                default:
                    throw new ArgumentException("Player type not supported: " + playerType);
            }

            var movement = newPlayer.GetComponent<InputMovement>();
            movement.Construct(newPlayer);

            _playerStore.Add(newPlayer);
            var stat = _playerStore.GetStat(playerType);
            newPlayer.Weapon.Bullets = stat.Bullets;
            newPlayer.Weapon.SetBulletPanel(_bulletPanel);

            _bulletPanel.SetBullets(newPlayer.Weapon.Bullets);


            //if (prevPlayer)
            //{
            //    prevPlayer.Stats.Bullets = prevPlayer.Weapon.Bullets;
            //    _playerStore.DestroyActivePlayer();
            //}


            return newPlayer;
        }

        private Player CreateCat(Vector3 position) {
            var player = Instantiate(playerPrefab, position, transform.rotation, playerList);
            var stat = _playerStore.GetStat(CharacterType.Cat);
            player.Construct(CharacterType.Cat, stat);
            player.GetComponent<Health>().Construct(player, _healthPanel, _playerStore.GetStat(CharacterType.Cat));
            player.Weapon = _weaponFactory.CreateGun(player);

            return player;
        }

        private Player CreateCow(Vector3 position)
        {
            var player = Instantiate(cowPrefab, position, transform.rotation, playerList);
            var stat = _playerStore.GetStat(CharacterType.Cow);
            player.Construct(CharacterType.Cow, stat);
            player.GetComponent<Health>().Construct(player, _healthPanel, _playerStore.GetStat(CharacterType.Cow));
            player.Weapon = _weaponFactory.CreateBomb(player);
            var grassPickup = player.GetComponent<GrassPickup>();
            grassPickup.Construct(player.Weapon);
            player.ItemPickup = grassPickup;
            

            return player;
        }
    }
}