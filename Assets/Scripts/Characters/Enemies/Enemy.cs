using System;
using Characters.Helpers;
using Players;
using UnityEngine;

namespace Characters.Enemies
{
    public class Enemy : MonoBehaviour, ICharacter
    {
        public float moveSpeed = 5f;
        
        private Animator _animator;
        
        private Rigidbody2D _rb;
        
        private Vector2 _movement;
        
        private PlayerStore _playerStore;

        private Shooting _shooting;
        
        private static readonly int HorizontalMovement = Animator.StringToHash("horizontalMovement");
        
        private static readonly int VerticalMovement = Animator.StringToHash("verticalMovement");

        private Direction _moveDirection = Direction.Down;

        public void Construct(PlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        private void Start()
        {
            _shooting = GetComponent<Shooting>();
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            InvokeRepeating(nameof(Shoot), 0, 2);
        }

        private void Update()
        {
            var player = _playerStore.GetActivePlayer();
            var direction = player.transform.position - transform.position;
            var radians = Mathf.Atan2(direction.y, direction.x);
            var rotationVector = new Vector2((float) Math.Cos(radians), (float) Math.Sin(radians));
            direction.Normalize();
            _movement = direction;
            
            _moveDirection = MovementHelper.UpdateMoveDirection(_movement, _moveDirection);
        
            _animator.SetFloat(HorizontalMovement, rotationVector.x);
            _animator.SetFloat(VerticalMovement, rotationVector.y);
        }

        private void Shoot()
        {
            _shooting.Shoot();
        }

        private void LateUpdate()
        {
            MoveCharacter(_movement);
        }

        private void MoveCharacter(Vector2 direction)
        {
            _rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
        }

        public Direction GetMoveDirection()
        {
            return _moveDirection;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public GameObject GetGameObjet()
        {
            return gameObject;
        }
    }
}