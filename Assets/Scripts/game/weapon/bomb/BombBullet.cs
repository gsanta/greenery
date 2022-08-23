using game.character.ability.health;
using UnityEngine;

namespace game.tool.weapon.bomb
{
    public class BombBullet : MonoBehaviour, ITool
    {
        [SerializeField] private float impactField;

        [SerializeField] private float force;

        private int _damage = 3;

        [SerializeField] private LayerMask layerMaskToHit;

        [SerializeField] private GameObject explosionPrefab;

        [SerializeField] private GameObject circlePrefab;

        private GameObject _circleGameObject;

        public void Fire()
        {
            var pos = transform.position;

            _circleGameObject = Instantiate(circlePrefab, pos, transform.rotation);
            _circleGameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);

            Invoke(nameof(Explode), 1f);
        }

        private void Explode()
        {
            Destroy(_circleGameObject);

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

        private void HitTarget(Collider2D target)
        {
            var dir = target.transform.position - transform.position;

            target.GetComponent<Rigidbody2D>().AddForce(dir * force);

            if (target.gameObject.tag == "Target")
            {
                var health = target.GetComponent<Health>();
                health.Decrease(_damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, impactField);
        }
    }
}
