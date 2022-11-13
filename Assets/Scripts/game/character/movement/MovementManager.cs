
using game.character.characters.player;
using game.character.movement.path;
using game.character.player;
using game.character.state;
using game.scene;
using game.scene.level;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace game.character.movement
{
    public struct MovementAction
    {
        public ICharacter character;

        public Vector2? targetPosition;

        public MovementAction(ICharacter character, Vector2? targetPosition)
        {
            this.character = character;
            this.targetPosition = targetPosition;
        }

        public void Execute()
        {

        }
    }

    public class MovementManager
    {
        private PlayerStore _playerStore;

        private LevelStore _levelStore;

        private FollowCamera _followCamera;

        private List<ICharacter> remainingCharactersInTurn = new();

        private ICharacter _currentCharacter;

        private GridMovementHandler _gridMovementHandler;

        private Stack<MovementAction> actionStack = new();

        public MovementManager(CharacterEvents playerEvents, PlayerStore playerStore, LevelStore levelStore, FollowCamera followCamera)
        {
            _playerStore = playerStore;
            _levelStore = levelStore;
            _followCamera = followCamera;
            _gridMovementHandler = new GridMovementHandler(playerEvents);

            playerEvents.OnTargetEnd += HandleTargetEnd;
            playerEvents.OnTargetStart += HandleTargetStart;
        }

        public void Activate()
        {
            HandleNewTurn();
            UpdateCurrentCharacter(new MovementAction(remainingCharactersInTurn[0], null));
        }

        private void HandleTargetEnd(object sender, EventArgs args)
        {
            var node = _levelStore.ActiveLevel.Grid.GetNodeAtWorldPos(_currentCharacter.GetPosition());
            node.character = _currentCharacter;
            HandleNewTurn();
            UpdateCurrentCharacter(new MovementAction(remainingCharactersInTurn[0], null));

            //_enemyStore.GetAll().ForEach((enemy) => enemy.Movement.IsPaused = true);
        }

        private void UpdateCurrentCharacter(MovementAction action)
        {

            var prevCharacter = _currentCharacter;
            prevCharacter.States.SetActiveState(CharacterStateType.Idle);
            _currentCharacter = action.character;
            remainingCharactersInTurn.Remove(_currentCharacter);

            if (prevCharacter)
            {
                prevCharacter.Movement.IsPaused = true;
            }

            _currentCharacter = _playerStore.SetNextPlayer();
            
            if (action.targetPosition.HasValue)
            {
                //_targetPathFinder.SetCharacter(action.character);
                //_targetPathFinder.MoveTo(action.targetPosition.Value);
            }
            else if (_currentCharacter.PlayerType == PlayerType.Enemy)
            {
                _currentCharacter.States.SetActiveState(CharacterStateType.ChasingState);
            } else if (_currentCharacter.PlayerType == PlayerType.Neutral)
            {
                _currentCharacter.States.SetActiveState(CharacterStateType.Idle);
            }
            else
            {
                _currentCharacter.States.SetActiveState(CharacterStateType.KeyboardMovement);
            }

            _currentCharacter.Movement.IsPaused = false;
            _followCamera.SetTarget(_currentCharacter);
        }

        private void HandleNewTurn()
        {
            if (remainingCharactersInTurn.Count == 0)
            {
                remainingCharactersInTurn.AddRange(_playerStore.GetAll());
            }
        }
        
        private void HandleTargetStart(object sender, EventArgs args)
        {
            //_enemyStore.GetAll().ForEach((enemy) => enemy.Movement.IsPaused = false);
        }
    }
}
