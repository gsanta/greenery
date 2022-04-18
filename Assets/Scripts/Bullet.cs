using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 shootDir;
    public void Setup(Vector3 dir)
    {
        shootDir = dir;
        transform.eulerAngles = new Vector3(0, 0, Utilities.GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        float moveSpeed = 100f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;

        float hitDetectionSize = 3f;
        Target target = Target.GetClosest(transform.position, hitDetectionSize);

        if (target != null)
        {
            Target.RemoveTarget(target);
            Destroy(target.gameObject);
        }
    }
}
