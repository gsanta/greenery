using Characters.Avatar;
using Characters.Enemies;
using Characters.Players;
using GameInfo;
using GameLogic;
using GUI;
using Items;
using Items.Bullet;
using Items.EnterArea;
using Players;
using Tile;
using UnityEngine;

public class Injector : MonoBehaviour
{
    [SerializeField] private TileSystem tileSystem;
    
    [SerializeField] private EnemyFactory enemyFactory;

    [SerializeField] private EnemySpawner enemySpawner;
    
    [SerializeField] private GameInfoStore gameInfoStore;

    [SerializeField] private BallSpawner ballSpawner;

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] private HealthBar healthBar;

    [SerializeField] private BulletFactory bulletFactory;

    [SerializeField] private AvatarFactory avatarFactory;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private EnterAreaStore enterAreaStore;

    //GUI

    [SerializeField] private StartGamePanel startGamePanel;
    
    private AvatarStore _avatarStore;

    private PlayerStore _playerStore;
    
    private EnemyStore _enemyStore;
    
    private readonly ItemStore<Ball> _ballStore = new();

    private void Awake()
    {
        _playerStore = new PlayerStore();
        _enemyStore = new EnemyStore();
        tileSystem.Construct(_playerStore);
        enemyFactory.Construct(_enemyStore, _playerStore, bulletFactory);
        enemySpawner.Construct(enemyFactory);
        playerFactory.Construct(_playerStore, _enemyStore, _ballStore, gameInfoStore, healthBar, bulletFactory);
        ballSpawner.Construct(_ballStore);

        _avatarStore = new AvatarStore();
        avatarFactory.Construct(_avatarStore);
        avatarFactory.Create();
        avatarFactory.Create();
        
        gameManager.Construct(enterAreaStore, playerFactory, enemySpawner);
        startGamePanel.Construct(gameManager);
    }
}
