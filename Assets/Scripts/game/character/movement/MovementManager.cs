
using game.character.characters.enemy;
using game.character.characters.player;
using game.character.movement.path;
using game.character.player;
using game.scene;
using game.scene.level;
using System;
using System.Collections.Generic;

namespace game.character.movement
{
    public class MovementManager
    {
        private EnemyStore _enemyStore;

        private PlayerStore _playerStore;

        private KeyboardPathFinder _keyboardPathFinder;

        private TargetPathFinder _targetPathFinder;

        private LevelStore _levelStore;

        private FollowCamera _followCamera;

        private List<ICharacter> remainingCharactersInTurn = new();

        private ICharacter _currentCharacter;

        public MovementManager(CharacterEvents playerEvents, PlayerStore playerStore, EnemyStore enemyStore, KeyboardPathFinder keyboardPathFinder, TargetPathFinder targetPathFinder, LevelStore levelStore, FollowCamera followCamera)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _keyboardPathFinder = keyboardPathFinder;
            _targetPathFinder = targetPathFinder;
            _levelStore = levelStore;
            _followCamera = followCamera;

            playerEvents.OnTargetEnd += HandleTargetEnd;
            playerEvents.OnTargetStart += HandleTargetStart;
        }

        public void Activate()
        {
            _keyboardPathFinder.SetLevel(_levelStore.ActiveLevel);
            _targetPathFinder.SetLevel(_levelStore.ActiveLevel);

            SetNewTurn();
            UpdateCurrentCharacter();
        }

        private void HandleTargetEnd(object sender, EventArgs args)
        {
            UpdateCurrentCharacter();

            //_enemyStore.GetAll().ForEach((enemy) => enemy.Movement.IsPaused = true);
        }

        private void UpdateCurrentCharacter()
        {
            if (remainingCharactersInTurn.Count == 0)
            {
                SetNewTurn();
            }

            _keyboardPathFinder.Deactivate();

            var prevCharacter = _currentCharacter;
            _currentCharacter = remainingCharactersInTurn[0];
            remainingCharactersInTurn.Remove(_currentCharacter);

            if (prevCharacter && prevCharacter.IsEnemy)
            {
                prevCharacter.Movement.IsPaused = true;
            }

            if (_currentCharacter.IsEnemy)
            {
                _currentCharacter = _enemyStore.SetNextEnemy();
                _currentCharacter.Movement.IsPaused = false;
                _targetPathFinder.SetCharacter(_currentCharacter);
                _currentCharacter.States.ActiveState.UpdateState();
            }
            else
            {
                _currentCharacter = _playerStore.SetNextPlayer();
                _keyboardPathFinder.SetCharacter(_currentCharacter);
                _keyboardPathFinder.Activate();
            }
            
            _followCamera.SetTarget(_currentCharacter);
        }

        private void SetNewTurn()
        {
            remainingCharactersInTurn.AddRange(_playerStore.GetAll());
            remainingCharactersInTurn.AddRange(_enemyStore.GetAll());
        }
        
        private void HandleTargetStart(object sender, EventArgs args)
        {
            //_enemyStore.GetAll().ForEach((enemy) => enemy.Movement.IsPaused = false);
        }
    }
}
