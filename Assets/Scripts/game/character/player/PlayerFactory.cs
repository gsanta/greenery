using Base.Input;
using game.character.ability.field_of_view;
using game.character.ability.health;
using game.character.movement;
using game.character.movement.path;
using game.character.player;
using game.scene;
using game.scene.level;
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

        private InputHandler _inputHandler;

        PlayerEvents _playerEvents;

        public void Construct(PlayerStore playerStore, HealthPanel healthPanel, BulletPanel bulletPanel, WeaponFactory weaponFactory, FollowCamera camera, InputHandler inputHandler, PlayerEvents playerEvents)
        {
            _playerStore = playerStore;
            _healthPanel = healthPanel;
            _bulletPanel = bulletPanel;
            _weaponFactory = weaponFactory;
            _camera = camera;
            _inputHandler = inputHandler;
            _playerEvents = playerEvents;
        }


        public Player Create(Vector3 position, CharacterType playerType, Level level)
        {
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

            var gameObject = newPlayer.gameObject;

            var movementPath = new Movement();

            newPlayer.Movement = movementPath;

            var pathFinder = new KeyboardPathFinder(newPlayer, level, movementPath);
            pathFinder.Register(_inputHandler);

            newPlayer.PathFinder = pathFinder;

            //var movementPathCalc = gameObject.AddComponent(typeof(KeyboardPathMovement)) as KeyboardDirectionMovement;
            //movementPathCalc.Construct(movementPath);

            var movement = gameObject.AddComponent(typeof(LerpMover)) as LerpMover;
            movement.Construct(newPlayer, movementPath);

            //newPlayer.Movement = movement;

            //var mover = gameObject.AddComponent(typeof(PathMover)) as PathMover;
            //mover.Construct(movement, level.Grid);

            var movementAnimation = gameObject.AddComponent(typeof(MovementAnimator)) as MovementAnimator;
            movementAnimation.Construct(true, movementPath);

            newPlayer.Construct(playerType, _playerStore.GetStat(playerType), _playerEvents, movementPath);


            //var mover = gameObject.AddComponent(typeof(KeyboardMover)) as KeyboardMover;
            //mover.Construct(movement);

            _playerStore.Add(newPlayer);
            var stat = _playerStore.GetStat(playerType);

            var gun = _weaponFactory.CreateGun(newPlayer);
            var bomb = _weaponFactory.CreateBomb(newPlayer);
            newPlayer.WeaponHolder.AddWeapon(gun);
            newPlayer.WeaponHolder.AddWeapon(bomb);
            newPlayer.WeaponHolder.ActivateWeapon(gun);


            //if (prevPlayer)
            //{
            //    prevPlayer.Stats.Bullets = prevPlayer.Weapon.Bullets;
            //    _playerStore.DestroyActivePlayer();
            //}


            return newPlayer;
        }

        private Player CreateCat(Vector3 position) {
            var player = Instantiate(playerPrefab, position, transform.rotation, playerList);
            player.GetComponent<Health>().Construct(player, _healthPanel, _playerStore.GetStat(CharacterType.Cat));
            
            return player;
        }

        private Player CreateCow(Vector3 position)
        {
            var player = Instantiate(cowPrefab, position, transform.rotation, playerList);
            player.GetComponent<Health>().Construct(player, _healthPanel, _playerStore.GetStat(CharacterType.Cow));

            return player;
        }
    }
}