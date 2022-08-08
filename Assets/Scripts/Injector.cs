using game;
using game.character.characters.enemy;
using game.character.characters.player;
using game.character.state;
using game.item;
using game.item.bullet;
using game.scene;
using game.scene.area;
using game.scene.grid;
using game.scene.level;
using GUI;
using gui.avatar;
using UnityEngine;

public class Injector : MonoBehaviour
{
    [SerializeField] public EnemyFactory enemyFactory;

    [SerializeField] private EnemySpawner enemySpawner;
    
    [SerializeField] private GameInfoStore gameInfoStore;

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
    
    [SerializeField] public PlayerStore playerStore;
    
    // Scene
    [SerializeField] public LevelLoader levelLoader;
    
    [SerializeField] private GridSetup gridSetup;

    [SerializeField] public GridVisualizer gridVisualizer;
    
    // State
    [SerializeField] private StateFactory stateFactory;
    
    private AvatarStore _avatarStore;

    private readonly ItemStore<Ball> _ballStore = new();

    private void Awake()
    {
        stateFactory.Construct(playerStore);
        
        enemyFactory.Construct(enemyStore, playerStore, bulletFactory, gameManager, stateFactory);
        enemySpawner.Construct(enemyFactory);
        playerFactory.Construct(playerStore, enemyStore, _ballStore, gameInfoStore, healthBar, bulletFactory, gameManager);

        _avatarStore = new AvatarStore();
        avatarFactory.Construct(_avatarStore);
        avatarFactory.Create();
        avatarFactory.Create();
        
        enterAreaStore.Construct(gameManager);
        
        gameManager.Construct(enterAreaStore, playerFactory, enemySpawner, followCamera, panelManager);

        panelManager.startGamePanel = startGamePanel;
        
        levelLoader.Construct(playerStore, this);
        levelLoader.LoadLevel("Level");
    }
}
