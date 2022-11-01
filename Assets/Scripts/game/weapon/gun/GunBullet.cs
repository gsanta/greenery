using game.character;
using game.character.ability.health;
using UnityEngine;

namespace game.Item.bullet
{
    public class GunBullet : MonoBehaviour
    {
        [SerializeField] private float force = 150f;

        private Vector3 _shootDir;

        private float _speed;

        private int _damage = 1;

        private ICharacter _character;

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
            if (_character.Movement.IsPaused)
            {
                return;
            }

            transform.position += _shootDir * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var isSelfCollission = _character.GetGameObject() != null && collision.gameObject == _character.GetGameObject();
            if (!isSelfCollission && collision.gameObject.tag == "Target") {

                var character = collision.GetComponent<ICharacter>();
                if (character != null)
                {
                    var health = collision.GetComponent<Health>();
                    //character.Movement.PauseUntil(0.3f);
                    collision.GetComponent<Rigidbody2D>().AddForce(_shootDir * force);
                    health.Decrease(_damage);
                }
                Destroy(gameObject);
            }
        }
    }
}
