using game.character.movement.path;
using UnityEngine;

namespace game.character.movement
{
    public class MovementAnimator : MonoBehaviour
    {

        private Animator _animator;

        private bool _flipWhenMovingRight;

        private MovementPath _movementPath;

        public void Construct(bool flipWhenMovingRight, MovementPath movementPath)
        {
            _flipWhenMovingRight = flipWhenMovingRight;
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

            if (_flipWhenMovingRight)
            {
                if (_movementPath.GetMoveDirection() == Direction.Right)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}
