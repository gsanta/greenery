using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float moveSpeed = 5f;
    public Vector3 bottomLeftEdge;
    public Vector3 topRightEdge;
    public float rotation;
    public Direction moveDirection = Direction.Down;

    private Vector3 movement;
    private Rigidbody2D rigidBody;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            
            DontDestroyOnLoad(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontalMovement, verticalMovement);

        Debug.Log("horizontal movement: " + horizontalMovement);
        // if (horizontalMovement > 0)
        // {
        //     transform.localScale = new Vector3(-1, 1, 1);
        // }
        // else if (horizontalMovement < 0)
        // {
        //     transform.localScale = new Vector3(1, 1, 1);
        // }

        UpdateMoveDirection();
        UpdateBlendTrees();

        rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * moveSpeed;

        Vector3 mousePosition = Utilities.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        rotation = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
    }

    private void UpdateMoveDirection()
    {
        if (movement.x != 0 && movement.y != 0)
        {
            if (movement.x > 0 && movement.y > 0)
            {
                moveDirection = Direction.RightUp;
            } else if (movement.x > 0 && movement.y < 0)
            {
                moveDirection = Direction.RightDown;
            } else if (movement.x < 0 && movement.y > 0)
            {
                moveDirection = Direction.LeftUp;
            } else if (movement.x < 0 && movement.y < 0)
            {
                moveDirection = Direction.LeftDown;
            }
        } else if (movement.x == 0)
        {
            if (movement.y > 0)
            {
                moveDirection = Direction.Up;
            } else if (movement.y < 0)
            {
                moveDirection = Direction.Down;
            }
        }
        else
        {
            if (movement.x < 0)
            {
                moveDirection = Direction.Left;
            } else if (movement.x > 0)
            {
                moveDirection = Direction.Right;
            }
        }
    }

    private void UpdateBlendTrees()
    {
        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetFloat("horizontalMovement", movement.x);
            animator.SetFloat("verticalMovement", movement.y);
            animator.SetBool("isMoving", true);
        }
    }
}