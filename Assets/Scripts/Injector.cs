using game;
using game.character.characters.enemy;
using game.character.characters.player;
using game.character.state;
using game.item.bullet;
using game.scene;
using game.scene.grid;
using game.scene.level;
using GUI;
using gui.avatar;
using UnityEngine;
using game.tool.weapon;
using game.character.enemy;

public class Injector : MonoBehaviour
{
    [SerializeField] public EnemyFactory enemyFactory;

    [SerializeField] private EnemySpawner enemySpawner;
    
    [SerializeField] private GameInfoStore gameInfoStore;

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] private HealthBar healthBar;

    // weapon

    [SerializeField] private BulletFactory bulletFactory;

    [SerializeField] private WeaponFactory weaponFactory;

    [SerializeField] private AvatarFactory avatarFactory;

    [SerializeField] public GameManager gameManager;

    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private FollowCamera followCamera;

    //GUI

    [SerializeField] private StartGamePanel startGamePanel;

    [SerializeField] private PanelManager panelManager;

    // enemy
    
    [SerializeField] private EnemyStore enemyStore;

    private EnemyManager _enemyManager;

    // player
    
    [SerializeField] public PlayerStore playerStore;

    [SerializeField] public PlayerCommandHandler playerCommandHandler;

    // Scene
    [SerializeField] public LevelLoader levelLoader;
    
    [SerializeField] private GridSetup gridSetup;

    [SerializeField] public GridVisualizer gridVisualizer;

    public LevelStore LevelStore;

    // State
    [SerializeField] private StateFactory stateFactory;
    
    private AvatarStore _avatarStore;

    private void Awake()
    {
        LevelStore = new LevelStore();

        stateFactory.Construct(playerStore);

        weaponFactory.Construct(bulletFactory);

        enemyFactory.Construct(enemyStore, playerStore, weaponFactory, gameManager, stateFactory);
        enemySpawner.Construct(enemyFactory, enemyStore, LevelStore);
        playerFactory.Construct(playerStore, gameInfoStore, healthBar, weaponFactory, followCamera);

        playerCommandHandler.Construct(playerStore, playerFactory);

        _avatarStore = new AvatarStore();
        avatarFactory.Construct(_avatarStore);
        avatarFactory.Create();
        avatarFactory.Create();
        
        playerManager = new PlayerManager(playerFactory);

        _enemyManager = new EnemyManager(enemyFactory, enemySpawner);

        gameManager.Construct(playerManager, panelManager, _enemyManager);

        panelManager.startGamePanel = startGamePanel;
        
        levelLoader.Construct(this);
        levelLoader.LoadLevel("Level");
    }
}
