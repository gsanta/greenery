using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;
    private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();
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
        var vectorDir = DirectionHelper.DirToVectorDir(player.moveDirection);
        var pos = transform.position;
        Transform bulletTransform = Instantiate(bulletPrefab, (Vector2) pos, Quaternion.identity);
        bulletTransform.GetComponent<Bullet>().Setup(vectorDir);
    }
}
