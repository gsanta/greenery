using UnityEngine;

namespace Players
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Transform bulletPrefab;
        [SerializeField] private Transform bulletContainer;
        private Player _player;
        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();    
            }
        }

        private void Shoot()
        {
            var vectorDir = DirectionHelper.DirToVectorDir(_player.moveDirection);
            var pos = transform.position;
            Transform bulletTransform = Instantiate(bulletPrefab, (Vector2) pos, Quaternion.identity, bulletContainer);
            bulletTransform.GetComponent<Bullet>().Setup(vectorDir);
        }
    }
}
