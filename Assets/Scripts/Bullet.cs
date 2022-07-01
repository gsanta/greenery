using System;
using Players;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _shootDir;
    public void Setup(Vector3 dir)
    {
        _shootDir = dir;
        transform.eulerAngles = new Vector3(0, 0, Utilities.GetAngleFromVectorFloat(_shootDir));
        Destroy(gameObject, 50f);
    }

    private void Update()
    {
        const float moveSpeed = 15f;
        transform.position += _shootDir * moveSpeed * Time.deltaTime;

        const float hitDetectionSize = 1f;
        var target = PlayerTarget.GetClosest(transform.position, hitDetectionSize);

        if (target != null)
        {
            PlayerTarget.RemoveTarget(target);
            Destroy(target.gameObject);
        }
    }
}
