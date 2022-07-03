using Characters.Helpers;
using UnityEngine;

namespace Characters.Players
{
    public class Player : MonoBehaviour, ICharacter
    {
        private static Player _instance;
        
        public float moveSpeed = 5f;
        
        private Direction _moveDirection = Direction.Down;

        private Vector3 _movement;
        
        private Rigidbody2D _rigidBody;
        
        private Animator _animator;

        private Shooting _shooting;
        
        void Start()
        {
            _shooting = GetComponent<Shooting>();
            
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                _rigidBody = GetComponent<Rigidbody2D>();
                _animator = GetComponent<Animator>();
            
                DontDestroyOnLoad(gameObject);
            }
        }
        
        void Update()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");

            _movement = new Vector2(horizontalMovement, verticalMovement);

            _moveDirection = MovementHelper.UpdateMoveDirection(_movement, _moveDirection);
            UpdateBlendTrees();

            _rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;

            var mousePosition = Utilities.GetMouseWorldPosition();

            if (Input.GetMouseButtonDown(0))
            {
                _shooting.Shoot();
            }
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