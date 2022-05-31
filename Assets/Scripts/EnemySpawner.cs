using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTime = 5f;
    public Enemy enemyPrefab;
    public Transform player;
    public Transform spawnPosition;
    private float timer;

    private void Start()
    {
        timer = spawnTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // Enemy enemy = Instantiate(enemyPrefab, spawnPosition.position, transform.rotation);
            // enemy.player = player;
            // timer = spawnTime;
        }
    }
}
