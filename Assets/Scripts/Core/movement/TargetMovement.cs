using Character.utils;
using Characters;
using Characters.Helpers;
using UnityEngine;

namespace Core.movement
{
    public class TargetMovement
    {
        private Direction _moveDirection = Direction.Down;
        private Vector2 _movement;
        private Vector2 _target;

        private readonly IMoveAble _moveAble;
        
        public TargetMovement(IMoveAble moveAble)
        {
            _moveAble = moveAble;
        }

        public void Update()
        {
            var direction = _target - _moveAble.GetPosition();
            direction.Normalize();
            _movement = direction;
            
            _moveDirection = MovementUtil.UpdateMoveDirection(_movement, _moveDirection);
        }

        public void SetTarget(Vector2 target)
        {
            _target = target;
        }

        public Vector2 GetPosition()
        {
            return _moveAble.GetPosition();
        }
    }
}