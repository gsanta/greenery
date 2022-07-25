using System.Collections.Generic;
using UnityEngine;

namespace AI.grid.path
{
    public class PathMovement : MonoBehaviour
    {
        private List<Vector2> _pathVectorList = new();
        
        private int _currentPathIndex = 0;

        private const float Speed = 2f;

        private PathFinding _pathFinding;
        
        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Animator _animator;

        private Vector2 _targetPosition;
        
        public bool IsTargetReached { get; private set; }

        public void Construct(PathFinding pathFinding)
        {
            _pathFinding = pathFinding;
            IsTargetReached = false;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void HandleMovement()
        {
            if (_pathVectorList != null)
            {
                var position = (Vector2) transform.position;
                var targetPosition = _pathVectorList[_currentPathIndex];
                if (Vector2.Distance(position, targetPosition) > 0.2f)
                {
                    var moveDir = (targetPosition - (Vector2) position).normalized;
                    transform.position = position + moveDir * Speed * Time.deltaTime; 
                    
                    _animator.SetFloat(HorizontalMovement, moveDir.x);
                    _animator.SetFloat(VerticalMovement, moveDir.y);
                } else
                {
                    _currentPathIndex++;
                    if (_currentPathIndex >= _pathVectorList.Count)
                    {
                        _pathVectorList = null;
                        IsTargetReached = true;
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
        
        public void MoveTo(Vector2 targetPosition)
        {
            if (_targetPosition != targetPosition)
            {
                SetTargetPosition(targetPosition);
            }
            
            HandleMovement();
        }

        private void SetTargetPosition(Vector2 targetPosition)
        {
            IsTargetReached = false;
            _targetPosition = targetPosition;
            _currentPathIndex = 0;
            _pathVectorList = _pathFinding.FindPath(transform.position, targetPosition);

            if (_pathVectorList is {Count: > 1})
            {
                _pathVectorList.RemoveAt(0);
            }
        }
    }
}