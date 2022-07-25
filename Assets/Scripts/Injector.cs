using AI.grid;
using Character.player;
using Characters.Avatar;
using Characters.Enemies;
using GameInfo;
using GameLogic;
using GUI;
using Items;
using Items.Bullet;
using Items.EnterArea;
using Scene;
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

    [SerializeField] private FollowCamera followCamera;

    //GUI

    [SerializeField] private StartGamePanel startGamePanel;

    [SerializeField] private PanelManager panelManager;
    
    [SerializeField] private EnemyStore enemyStore;
    
    [SerializeField] private PlayerStore playerStore;

    // Grid
    [SerializeField] private GridSetup gridSetup;
    private GridModule _gridModule;
    
    private AvatarStore _avatarStore;

    private readonly ItemStore<Ball> _ballStore = new();

    private void Awake()
    {
        tileSystem.Construct(playerStore);
        _gridModule = new GridModule(gridSetup);
        enemyFactory.Construct(enemyStore, playerStore, bulletFactory, gameManager, _gridModule);
        enemySpawner.Construct(enemyFactory);
        playerFactory.Construct(playerStore, enemyStore, _ballStore, gameInfoStore, healthBar, bulletFactory, gameManager);
        ballSpawner.Construct(_ballStore);

        _avatarStore = new AvatarStore();
        avatarFactory.Construct(_avatarStore);
        avatarFactory.Create();
        avatarFactory.Create();
        
        enterAreaStore.Construct(gameManager);
        
        gameManager.Construct(enterAreaStore, playerFactory, enemySpawner, followCamera, panelManager);

        panelManager.startGamePanel = startGamePanel;
    }
}
