using game.character;
using game.character.ability.health;
using UnityEngine;

namespace game.item.bullet
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _shootDir;

        private float _speed;

        private ICharacter _character;

        private bool _isUsed = false;

        public void Construct(ICharacter character, float speed)
        {
            _character = character;
            _speed = speed;
        }
    
        public void SetDirection(Vector3 dir)
        {
            _shootDir = dir;
            transform.eulerAngles = new Vector3(0, 0, Utilities.GetAngleFromVectorFloat(_shootDir));
            Destroy(gameObject, 50f);
        }

        private void Update()
        {
            transform.position += _shootDir * _speed * Time.deltaTime;

            if (!_isUsed)
            {
                //HitTarget();
            }
        }

        //private void HitTarget()
        //{
        //    const float hitDetectionSize = 1f;
        //    var target = TargetHelper.GetClosest(_characterStore.GetAll(), transform.position, hitDetectionSize);

        //    if (target != null)
        //    {
        //        target.GetHealth().HitByBullet();
        //        Invoke(nameof(DestroyBullet), 0.1f);
        //        _isUsed = true;
        //    }
        //}

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject != _character.GetGameObject() && collision.gameObject.tag == "Target") {

                var health = collision.GetComponent<Health>();
                if (health)
                {
                    health.HitByBullet();
                }
                Destroy(gameObject);
            }
        }
    }
}
