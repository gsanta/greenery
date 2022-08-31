using game;
using game.character.characters.enemy;
using game.character.characters.player;
using game.character.state;
using game.item.bullet;
using game.scene;
using game.scene.grid;
using game.scene.level;
using GUI;
using UnityEngine;
using game.tool.weapon;
using game.character.enemy;
using gui;

public class Injector : MonoBehaviour
{
    [SerializeField] public EnemyFactory enemyFactory;

    [SerializeField] private EnemySpawner enemySpawner;
    
    [SerializeField] private GameInfoStore gameInfoStore;

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] private HealthPanel healthBar;

    // weapon

    [SerializeField] private BulletFactory bulletFactory;

    [SerializeField] private WeaponFactory weaponFactory;

    [SerializeField] public GameManager gameManager;

    [SerializeField] private BulletPanel bulletPanel;

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

    public LevelStore LevelStore;

    // State
    [SerializeField] private StateFactory stateFactory;
    
    private void Awake()
    {
        LevelStore = new LevelStore(); 

        stateFactory.Construct(playerStore);

        weaponFactory.Construct(bulletFactory);

        enemyFactory.Construct(enemyStore, playerStore, weaponFactory, gameManager, stateFactory);
        enemySpawner.Construct(enemyFactory, enemyStore, LevelStore);
        playerFactory.Construct(playerStore, gameInfoStore, healthBar, bulletPanel, weaponFactory, followCamera);

        playerCommandHandler.Construct(playerStore, playerFactory, bulletPanel);

        playerManager = new PlayerManager(playerFactory);

        _enemyManager = new EnemyManager(enemyFactory, enemySpawner);

        gameManager.Construct(playerManager, panelManager, _enemyManager, followCamera);

        panelManager.startGamePanel = startGamePanel;
        
        levelLoader.Construct(this);
        levelLoader.Load("Level");
    }
}
