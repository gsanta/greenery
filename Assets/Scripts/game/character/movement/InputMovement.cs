using game.character.movement;
using UnityEngine;

namespace Assets.Scripts.game.character.characters.player
{
    public class InputMovement : MonoBehaviour, IMovement
    {
        public float moveSpeed = 2.5f;

        private Vector3 _movement;

        private Animator _animator;

        private Rigidbody2D _rigidBody;

        private bool _isPaused;

        public void PauseUntil(float time)
        {
            _isPaused = true;
            Invoke(nameof(Unpause), time);
        }

        private void Unpause()
        {
            _isPaused = false;
        }

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (_isPaused)
            {
                return;
            }

            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");
            _movement = new Vector2(horizontalMovement, verticalMovement);
            _rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;
            UpdateBlendTrees();
        }

        private void UpdateBlendTrees()
        {
            if (_movement.x == 0 && _movement.y == 0)
            {
                _animator.SetBool("isMoving", false);
            }
            else
            {
                _animator.SetFloat("horizontalMovement", _movement.x);
                _animator.SetFloat("verticalMovement", _movement.y);
                _animator.SetBool("isMoving", true);
            }
        }
    }
}