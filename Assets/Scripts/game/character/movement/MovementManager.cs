
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
        private PlayerStore _playerStore;

        private KeyboardPathFinder _keyboardPathFinder;

        private TargetPathFinder _targetPathFinder;

        private LevelStore _levelStore;

        private FollowCamera _followCamera;

        private List<ICharacter> remainingCharactersInTurn = new();

        private ICharacter _currentCharacter;

        public MovementManager(CharacterEvents playerEvents, PlayerStore playerStore, KeyboardPathFinder keyboardPathFinder, TargetPathFinder targetPathFinder, LevelStore levelStore, FollowCamera followCamera)
        {
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
            var node = _levelStore.ActiveLevel.Grid.GetNodeAtWorldPos(_currentCharacter.GetPosition());
            node.character = _currentCharacter;
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

            if (prevCharacter)
            {
                prevCharacter.Movement.IsPaused = true;
            }

            _currentCharacter = _playerStore.SetNextPlayer();
            
            if (_currentCharacter.PlayerType == PlayerType.Enemy || _currentCharacter.PlayerType == PlayerType.Neutral)
            {
                _targetPathFinder.SetCharacter(_currentCharacter);
            }
            else
            {
                _keyboardPathFinder.SetCharacter(_currentCharacter);
                _keyboardPathFinder.Activate();
            }

            _currentCharacter.Movement.IsPaused = false;
            _currentCharacter.States.ActiveState.UpdateState();
            _followCamera.SetTarget(_currentCharacter);
        }

        private void SetNewTurn()
        {
            remainingCharactersInTurn.AddRange(_playerStore.GetAll());
        }
        
        private void HandleTargetStart(object sender, EventArgs args)
        {
            //_enemyStore.GetAll().ForEach((enemy) => enemy.Movement.IsPaused = false);
        }
    }
}
