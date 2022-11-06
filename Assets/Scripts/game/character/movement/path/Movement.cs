
using game.character.player;
using game.character.utils;
using System;
using UnityEngine;

namespace game.character.movement.path
{
    public class Movement
    {
        private Vector2 _direction;

        private Vector2? _destination = null;

        private CharacterEvents _characterEvents;

        protected Direction _moveDirection = Direction.Down;

        private bool _isTargetReached = true;
        
        public bool IsTargetReached { 
            get { return _isTargetReached; } 
            set { 
                _isTargetReached = value;
                if (_isTargetReached)
                {
                    _characterEvents.EmitTargetEnd();
                } else
                {
                    _characterEvents.EmitTargetStart();
                }
            }
        }

        public bool IsPaused { get; set; }

        public Movement(CharacterEvents characterEvents)
        {
            _characterEvents = characterEvents;
        }

        public Vector2 GetDirection()
        {
            return _direction;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
            _moveDirection = MovementUtil.UpdateMoveDirection(GetDirection(), _moveDirection);
        }

        public Direction GetMoveDirection()
        {
            return _moveDirection;
        }

        public Vector2? GetDestination()
        {
            return _destination;
        }

        public void SetDestination(Vector2 destination)
        {
            _destination = destination;
        }
    }
}
