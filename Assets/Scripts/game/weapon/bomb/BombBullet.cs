using UnityEngine;

namespace game.tool.weapon.bomb
{
    public class BombBullet : MonoBehaviour, ITool
    {
        [SerializeField] private float impactField;

        [SerializeField] private float force;

        [SerializeField] private LayerMask layerMaskToHit;

        [SerializeField] private GameObject explosionPrefab;

        public void Explode()
        {
            var pos = transform.position;
            var objects = Physics2D.OverlapCircleAll(pos, impactField, layerMaskToHit);

            foreach (var obj in objects)
            {
                var dir = obj.transform.position - pos;

                obj.GetComponent<Rigidbody2D>().AddForce(dir * force);
            }
            Instantiate(explosionPrefab, pos, transform.rotation);
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, impactField);
        }
    }
}
