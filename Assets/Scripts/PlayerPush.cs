using game.character.characters.player;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1.5f;
    
    public float pushForce = 10f;
    
    public LayerMask boxMask;
    
    private Player player;
    
    private Vector3 vectorDir = Vector2.right;
    
    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        //vectorDir = DirectionHelper.DirToVector(player.GetMoveDirection());
        // Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectorDir * transform.localScale.x, distance, boxMask);

        if (hit.collider != null)
        {
            var rigidbody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
            
            rigidbody.AddForce(vectorDir * pushForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2) (transform.position + vectorDir * transform.localScale.x  * distance));
    }
}
