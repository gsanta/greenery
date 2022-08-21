using game;
using game.character.characters.enemy;
using game.character.characters.player;
using game.character.state;
using game.item;
using game.item.bullet;
using game.scene;
using game.scene.grid;
using game.scene.level;
using GUI;
using gui.avatar;
using UnityEngine;
using game.tool.weapon;

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
    
    [SerializeField] private EnemyStore enemyStore;
    
    [SerializeField] public PlayerStore playerStore;
    
    // Scene
    [SerializeField] public LevelLoader levelLoader;
    
    [SerializeField] private GridSetup gridSetup;

    [SerializeField] public GridVisualizer gridVisualizer;
    
    // State
    [SerializeField] private StateFactory stateFactory;
    
    private AvatarStore _avatarStore;

    private void Awake()
    {
        stateFactory.Construct(playerStore);

        weaponFactory.Construct(bulletFactory);

        enemyFactory.Construct(enemyStore, playerStore, weaponFactory, gameManager, stateFactory);
        enemySpawner.Construct(enemyFactory);
        playerFactory.Construct(playerStore, gameInfoStore, healthBar, weaponFactory);

        _avatarStore = new AvatarStore();
        avatarFactory.Construct(_avatarStore);
        avatarFactory.Create();
        avatarFactory.Create();
        
        playerManager = new PlayerManager(playerFactory, playerStore, followCamera);

        gameManager.Construct(playerManager, panelManager);

        panelManager.startGamePanel = startGamePanel;
        
        levelLoader.Construct(this);
        levelLoader.LoadLevel("Level");
    }
}
