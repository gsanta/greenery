using game.character;
using UnityEngine;

namespace game.item.bullet
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _shootDir;
        
        private ICharacterStore<ICharacter> _characterStore;

        private bool _isUsed = false;
        
        public void Construct(ICharacterStore<ICharacter> characterStore)
        {
            _characterStore = characterStore;
        }
    
        public void SetDirection(Vector3 dir)
        {
            _shootDir = dir;
            transform.eulerAngles = new Vector3(0, 0, Utilities.GetAngleFromVectorFloat(_shootDir));
            Destroy(gameObject, 50f);
        }

        private void Update()
        {
            const float moveSpeed = 15f;
            transform.position += _shootDir * moveSpeed * Time.deltaTime;

            if (!_isUsed)
            {
                HitTarget();
            }
        }

        private void HitTarget()
        {
            const float hitDetectionSize = 1f;
            var target = TargetHelper.GetClosest(_characterStore.GetAll(), transform.position, hitDetectionSize);

            if (target != null)
            {
                target.GetHealth().HitByBullet();
                Invoke(nameof(DestroyBullet), 0.1f);
                _isUsed = true;
            }
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}
