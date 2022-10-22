using game.character.movement;
using game.character.utils;
using System.Collections.Generic;
using UnityEngine;

namespace game.scene.grid.path
{
    public class PathMovement : IMovement
    {
        private List<Vector2> _pathVectorList = new();

        private int _currentPathIndex = 0;

        [SerializeField] private float speed = 6f;

        private PathFinding _pathFinding;

        private GridGraph _gridGraph;

        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Rigidbody2D _rigidBody;

        private Vector2 _targetPosition;

        public bool IsTargetReached { get; private set; }

        public void Construct(GridGraph gridGraph)
        {
            _pathFinding = new PathFinding();
            IsTargetReached = false;
            _rigidBody = GetComponent<Rigidbody2D>();
            _gridGraph = gridGraph;
        }

        public void MoveTo(Vector2 targetPosition)
        {
            if (_targetPosition != targetPosition || _pathVectorList == null)
            {
                FinishMovement();
                SetTargetPosition(targetPosition);
            }
        }

        private void FixedUpdate()
        {
            if (_isPaused)
            {
                return;
            }

            HandleMovement();
        }

        private void HandleMovement()
        {
            if (_pathVectorList != null && _pathVectorList.Count > 0)
            {
                var position = (Vector2)transform.position;
                var targetPosition = _pathVectorList[_currentPathIndex];
                if (Vector2.Distance(position, targetPosition) > 0.2f)
                {
                    var moveDir = (targetPosition - position).normalized;

                    _rigidBody.AddForce(moveDir * speed);
                    _moveDirection = MovementUtil.UpdateMoveDirection(moveDir, _moveDirection);

                    //_animator.SetFloat(HorizontalMovement, moveDir.x);
                    //_animator.SetFloat(VerticalMovement, moveDir.y);
                }
                else
                {
                    _currentPathIndex++;
                    if (_currentPathIndex >= _pathVectorList.Count)
                    {
                        FinishMovement();
                        //_animator.SetFloat(HorizontalMovement, 0);
                        //_animator.SetFloat(VerticalMovement, 0);
                    }
                }
            }
            else
            {
                FinishMovement();
                //_animator.SetFloat(HorizontalMovement, 0);
                //_animator.SetFloat(VerticalMovement, 0);
            }
        }

        public void FinishMovement()
        {
            IsTargetReached = true;
            _pathVectorList = null;
        }

        private void SetTargetPosition(Vector2 targetPosition)
        {
            IsTargetReached = false;
            _targetPosition = targetPosition;
            _currentPathIndex = 0;
            var pathNodeList = new List<PathNode>();
            _pathVectorList = _pathFinding.FindPath(_gridGraph, transform.position, targetPosition, out pathNodeList);


            if (_pathVectorList is {Count: > 1})
            {
                _pathVectorList.RemoveAt(0);
            }
        }
    }
}