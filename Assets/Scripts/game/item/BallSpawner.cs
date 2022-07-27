using UnityEngine;
using Random = UnityEngine.Random;

namespace game.item
{
    public class BallSpawner : MonoBehaviour
    {
        private ItemStore<Ball> _itemStore;

        [SerializeField] private Transform ballContainer;
        [SerializeField] private Ball ballPrefab;
        [SerializeField] private Transform[] spawnPositions;
        
        public void Construct(ItemStore<Ball> itemStore)
        {
            _itemStore = itemStore;
        }

        private void Start()
        {
            SpawnBall();
            SpawnBall();
            SpawnBall();
        }

        public void SpawnBall()
        {
            var ball = Instantiate(ballPrefab, GetRandomSpawnPosition(), Quaternion.identity, ballContainer);
            _itemStore.AddItem(ball);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            var pos = Random.Range(0, spawnPositions.Length);
            return spawnPositions[pos].position;
        }
    }
}