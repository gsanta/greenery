using UnityEngine;

namespace game.weapon.bomb
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float impactField;

        [SerializeField] private float force;

        [SerializeField] private LayerMask layerMaskToHit;

        [SerializeField] private GameObject explosionPrefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Explosion();
            }
        }

        private void Explosion()
        {
            var objects = Physics2D.OverlapCircleAll(transform.position, impactField, layerMaskToHit);

            foreach(var obj in objects)
            {
                var dir = obj.transform.position - transform.position;

                obj.GetComponent<Rigidbody2D>().AddForce(dir * force);
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, impactField);
        }
    }
}
