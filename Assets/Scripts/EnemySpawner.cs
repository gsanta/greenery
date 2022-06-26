using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private bool isDisabled = false;
    public Enemy enemyPrefab;
    public Transform player;
    public Transform spawnPosition;
    private float _timer;

    private void Start()
    {
        _timer = spawnTime;
    }

    private void Update()
    {
        if (isDisabled)
        {
            return;
        }
        
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            Enemy enemy = Instantiate(enemyPrefab, spawnPosition.position, transform.rotation);
            enemy.player = player;
            _timer = spawnTime;
        }
    }
}
