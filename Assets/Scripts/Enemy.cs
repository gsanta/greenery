using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        float radians = Mathf.Atan2(direction.y, direction.x);
        Vector2 rotationVector = new Vector2((float) Math.Cos(radians), (float) Math.Sin(radians));
        direction.Normalize();
        movement = direction;
        
        animator.SetFloat("horizontalMovement", rotationVector.x);
        animator.SetFloat("verticalMovement", rotationVector.y);
    }

    private void LateUpdate()
    {
        MoveCharacter(movement);
    }

    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2) transform.position + (direction * moveSpeed * Time.deltaTime));
    }
}