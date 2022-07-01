using System;
using Players;
using UnityEngine;

namespace Characters.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private Animator _animator;
        private Rigidbody2D _rb;
        private Vector2 _movement;
        private PlayerStore _playerStore;

        public void Construct(PlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var player = _playerStore.GetActivePlayer();
            var direction = player.transform.position - transform.position;
            var radians = Mathf.Atan2(direction.y, direction.x);
            var rotationVector = new Vector2((float) Math.Cos(radians), (float) Math.Sin(radians));
            direction.Normalize();
            _movement = direction;
        
            _animator.SetFloat("horizontalMovement", rotationVector.x);
            _animator.SetFloat("verticalMovement", rotationVector.y);
        }

        private void LateUpdate()
        {
            MoveCharacter(_movement);
        }

        void MoveCharacter(Vector2 direction)
        {
            _rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }
}