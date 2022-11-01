using game.character.movement.path;
using UnityEngine;

namespace game.character.movement
{
    public class MovementAnimator : MonoBehaviour
    {

        private Animator _animator;

        private ICharacter _character;

        private Movement _movementPath;

        public void Construct(ICharacter character, Movement movementPath)
        {
            _character = character;
            _movementPath = movementPath;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateBlendTrees();
            UpdateModelDirection();
        }


        private void UpdateBlendTrees()
        {
            if (!_animator)
            {
                return;
            }

            if (_movementPath.IsTargetReached)
            {
                _animator.SetBool("isMoving", false);
            }
            else
            {
                _animator.SetFloat("horizontalMovement", _movementPath.GetDirection().x);
                _animator.SetFloat("verticalMovement", _movementPath.GetDirection().y);
                _animator.SetBool("isMoving", true);
            }
        }

        private void UpdateModelDirection()
        {
            if (_movementPath.IsTargetReached)
            {
                return;
            }

            var isDefaultLeft = _character.defaultHorizontalAnimationDirection == Direction.Left;

            if (_movementPath.GetMoveDirection() == Direction.Right)
            {
                transform.localScale = isDefaultLeft ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            }
            else if (_movementPath.GetMoveDirection() == Direction.Left)
            {
                transform.localScale = isDefaultLeft ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            }
        }
    }
}
