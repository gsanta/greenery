using Characters.Enemies;
using GameInfo;
using GUI;
using Items;
using Items.Bullet;
using Players;
using Tile;
using UnityEngine;

public class Injector : MonoBehaviour
{
    [SerializeField] private TileSystem tileSystem;
    
    [SerializeField] private EnemyFactory enemyFactory;
    
    [SerializeField] private GameInfoStore gameInfoStore;

    [SerializeField] private BallSpawner ballSpawner;

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] private HealthBar healthBar;

    [SerializeField] private BulletFactory bulletFactory;

    private PlayerStore _playerStore;
    
    private EnemyStore _enemyStore;
    
    private readonly ItemStore<Ball> _ballStore = new();

    private void Awake()
    {
        _playerStore = new PlayerStore();
        _enemyStore = new EnemyStore();
        tileSystem.Construct(_playerStore);
        enemyFactory.Construct(_enemyStore, _playerStore, bulletFactory);
        playerFactory.Construct(_playerStore, _enemyStore, _ballStore, gameInfoStore, healthBar, bulletFactory);
        playerFactory.Create();
        ballSpawner.Construct(_ballStore);
    }
}
