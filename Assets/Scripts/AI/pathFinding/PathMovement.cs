using System.Collections.Generic;
using AI.GridSystem;
using UnityEngine;

namespace AI.pathFinding
{
    public class PathMovement : MonoBehaviour
    {
        private List<Vector2> _pathVectorList = new();
        
        private int _currentPathIndex = 0;

        private const float Speed = 5f;

        private PathFinding _pathFinding;
        
        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Animator _animator;

        private GridComponent _gridComponent;

        public void Construct(PathFinding pathFinding, GridComponent gridComponent)
        {
            _pathFinding = pathFinding;
            _gridComponent = gridComponent;
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
                if (Vector2.Distance(position, targetPosition) > 1f)
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
            _currentPathIndex = 0;
            _pathVectorList = _pathFinding.FindPath(transform.position, targetPosition);

            if (_pathVectorList != null && _pathVectorList.Count > 1)
            {
                _pathVectorList.RemoveAt(0);
            }
        }
    }
}