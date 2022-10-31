
using game.character.utils;
using System;
using UnityEngine;

namespace game.character.movement.path
{
    public class MovementPath
    {
        private Vector2 _direction;

        private Vector2? _destination = null;

        protected Direction _moveDirection = Direction.Down;

        public event EventHandler OnTargetEnd;

        public event EventHandler OnTargetStart;

        private bool _isTargetReached;
        
        public bool IsTargetReached { 
            get { return _isTargetReached; } 
            set { 
                _isTargetReached = value;
                if (_isTargetReached)
                {
                    OnTargetEnd?.Invoke(this, EventArgs.Empty);
                } else
                {
                    OnTargetStart?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool IsPaused { get; set; }

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
