using game.character.movement;
using System.Collections.Generic;
using UnityEngine;

namespace game.scene.grid.path
{
    public class PathMovement : MonoBehaviour, IMovement
    {
        private List<Vector2> _pathVectorList = new();

        private List<PathNode> _pathNodeList = new();
        
        private int _currentPathIndex = 0;

        private const float Speed = 2f;

        private PathFinding _pathFinding;
        
        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Animator _animator;

        private Rigidbody2D _rigidBody;

        private Vector2 _targetPosition;

        private bool _isPaused;
        
        public bool IsTargetReached { get; private set; }

        public void Construct(PathFinding pathFinding)
        {
            _pathFinding = pathFinding;
            IsTargetReached = false;
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void PauseUntil(float time)
        {
            _isPaused = true;
            Invoke(nameof(Unpause), time);
        }

        private void Unpause()
        {
            _isPaused = false;
        }
       
        public void MoveTo(Vector2 targetPosition)
        {
            if (_isPaused)
            {
                return;
            }

            if (_targetPosition != targetPosition)
            {
                FinishMovement();
                SetTargetPosition(targetPosition);
            }
            
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_pathVectorList != null)
            {
                var position = (Vector2)transform.position;
                var targetPosition = _pathVectorList[_currentPathIndex];
                if (Vector2.Distance(position, targetPosition) > 0.2f)
                {
                    var moveDir = (targetPosition - position).normalized;

                    _rigidBody.velocity = moveDir * Speed;


                    _animator.SetFloat(HorizontalMovement, moveDir.x);
                    _animator.SetFloat(VerticalMovement, moveDir.y);
                }
                else
                {
                    _currentPathIndex++;
                    if (_currentPathIndex >= _pathVectorList.Count)
                    {
                        FinishMovement();
                        _animator.SetFloat(HorizontalMovement, 0);
                        _animator.SetFloat(VerticalMovement, 0);
                    }
                }
            }
            else
            {
                _animator.SetFloat(HorizontalMovement, 0);
                _animator.SetFloat(VerticalMovement, 0);
            }
        }

        public void FinishMovement()
        {
            IsTargetReached = true;
            _pathNodeList.ForEach(node => node.WalkCounter--);
            _pathNodeList = new List<PathNode>();
            _pathVectorList = null;
        }

        private void SetTargetPosition(Vector2 targetPosition)
        {
            IsTargetReached = false;
            _targetPosition = targetPosition;
            _currentPathIndex = 0;
            _pathNodeList = new List<PathNode>();
            _pathVectorList = _pathFinding.FindPath(transform.position, targetPosition, out _pathNodeList);

            _pathNodeList.ForEach(node => node.WalkCounter++);

            if (_pathVectorList is {Count: > 1})
            {
                _pathVectorList.RemoveAt(0);
            }
        }
    }
}