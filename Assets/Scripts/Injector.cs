using Characters.Enemies;
using GameInfo;
using GUI;
using Items;
using Players;
using Tile;
using UnityEngine;

public class Injector : MonoBehaviour
{
    [SerializeField] private TileSystem tileSystem;
    
    [SerializeField] private EnemySpawner enemySpawner;
    
    [SerializeField] private GameInfoStore gameInfoStore;

    [SerializeField] private BallSpawner ballSpawner;

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] private HealthBar healthBar;
    
    private PlayerStore _playerStore;
    
    private EnemyStore _enemyStore;
    
    private readonly ItemStore<Ball> _ballStore = new();

    private void Awake()
    {
        _playerStore = new PlayerStore();
        tileSystem.Construct(_playerStore);
        enemySpawner.Construct(_enemyStore, _playerStore);
        playerFactory.Construct(_playerStore, _ballStore, gameInfoStore, healthBar);
        playerFactory.Create();
        ballSpawner.Construct(_ballStore);
    }
}
