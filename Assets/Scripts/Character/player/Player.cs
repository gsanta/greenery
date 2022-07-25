using Character.utils;
using Characters;
using Characters.Common;
using GameLogic;
using UnityEngine;

namespace Character.player
{
    public class Player : MonoBehaviour, ICharacter, IMoveAble
    {
        private static Player _instance;
        
        public float moveSpeed = 5f;

        private GameManager _gameManager;
        
        private Direction _moveDirection = Direction.Down;

        private Vector3 _movement;
        
        private Rigidbody2D _rigidBody;
        
        private Animator _animator;

        private Shooting _shooting;

        private Health _health;

        private bool _isActive;

        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        void Start()
        {
            _shooting = GetComponent<Shooting>();
            _health = GetComponent<Health>();
            
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
            UpdateActive();

            if (!_isActive)
            {
                return;
            }
            
            var horizontalMovement = Input.GetAxisRaw("Horizontal");
            var verticalMovement = Input.GetAxisRaw("Vertical");

            _movement = new Vector2(horizontalMovement, verticalMovement);

            _moveDirection = MovementUtil.UpdateMoveDirection(_movement, _moveDirection);
            UpdateBlendTrees();

            _rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;

            var mousePosition = Utilities.GetMouseWorldPosition();

            if (Input.GetMouseButtonDown(0))
            {
                _shooting.Shoot();
            }
        }
        
        private void UpdateActive()
        {
            _isActive = _gameManager.IsGameStarted();
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

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public void SetMovement(Vector2 movement)
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetMovement()
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetGameObjet()
        {
            return gameObject;
        }

        public Health GetHealth()
        {
            return _health;
        }
        
        public void Die()
        {
            Debug.Log("player is dead");
        }
    }
}