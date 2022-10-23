using game;
using game.character.characters.enemy;
using game.character.characters.player;
using game.character.state;
using game.Item.bullet;
using game.scene;
using game.scene.level;
using GUI;
using UnityEngine;
using game.tool.weapon;
using game.character.enemy;
using gui;
using game.character.player;
using Base.Input;
using game.Item;
using game.Common;
using Game.Stage;
using game.item;
using game.scene.grid;
using game.weapon;
using Assetsgame.weapon;

public class Injector : MonoBehaviour
{    
    [SerializeField] private HealthPanel healthBar;

    // base

    [SerializeField] private InputHandler inputHandler;

    // weapon

    [SerializeField] private BulletFactory bulletFactory;

    [SerializeField] private WeaponFactory weaponFactory;

    [SerializeField] private WeaponImageFactory weaponImageFactory;

    [SerializeField] private WeaponHandler weaponHandler;

    private WeaponImageStore _weaponImageStore = new WeaponImageStore();

    private WeaponSelector _weaponSelector;

    [SerializeField] public GameManager gameManager;

    [SerializeField] private BulletPanel bulletPanel;

    [SerializeField] private PlayerManager playerManager;

    [SerializeField] private FollowCamera followCamera;

    //GUI

    [SerializeField] private StartGamePanel startGamePanel;

    [SerializeField] private PanelManager panelManager;

    // enemy
    
    [SerializeField] private EnemyStore enemyStore;

    [SerializeField] public EnemyFactory enemyFactory;

    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private FovVisualDecorator fovVisualDecorator;

    [SerializeField] private PathVisualDecorator pathVisualDecorator;

    // player

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] public PlayerStore playerStore;

    [SerializeField] public PlayerCommandHandler playerCommandHandler;

    private PlayerSelector _playerSelector;

    private GunHandler _gunHandler;

    // Scene
    [SerializeField] public LevelLoader levelLoader;

    [SerializeField] public LevelStore LevelStore;

    // State
    [SerializeField] private StateFactory stateFactory;

    // inventory
    [SerializeField] private InventoryItemFactory inventoryItemFactory;

    [SerializeField] private InventoryHandler inventoryHandler;

    private InventoryStore _inventoryStore = new InventoryStore();

    // item
    private ItemHandler _itemHandler;

    [SerializeField] private ItemFactory itemFactory;

    // stage
    private StageManager stageManager = new StageManager();

    [SerializeField] private ScopedTileRenderer scopedTileRenderer;

    // common
    [SerializeField] private CursorHandler cursorHandler;

    // debug
    [SerializeField] private DebugContainer debugContainer;
    
    [SerializeField] private LevelTileRenderer levelTileRenderer;


    private void Awake()
    {
        debugContainer.gridVisualizer = levelTileRenderer;

        followCamera.Constuct(LevelStore);

        stateFactory.Construct(playerStore);

        // weapon
        weaponFactory.Construct(bulletFactory);
        weaponImageFactory.Construct(_weaponImageStore);
        _weaponSelector = new WeaponSelector(playerStore);
        weaponHandler.Construct(weaponImageFactory, _weaponSelector);

        enemyFactory.Construct(enemyStore, playerStore, weaponFactory, gameManager, stateFactory, new EnemyDecorator[] { fovVisualDecorator, pathVisualDecorator });
        enemySpawner.Construct(enemyFactory, enemyStore, LevelStore);
        playerFactory.Construct(playerStore, healthBar, bulletPanel, weaponFactory, followCamera);

        playerCommandHandler.Construct(playerStore, playerFactory);
        _gunHandler = new GunHandler(playerStore, bulletPanel);

        playerManager = new PlayerManager(playerFactory, playerStore, LevelStore, followCamera);
        _playerSelector = new PlayerSelector(playerStore);

        gameManager.Construct(playerManager, panelManager, stageManager, followCamera, levelLoader);

        inventoryHandler.Construct(inventoryItemFactory, _inventoryStore, cursorHandler);
        inventoryItemFactory.Construct(_inventoryStore, cursorHandler);

        _itemHandler = new ItemHandler(_inventoryStore, itemFactory, LevelStore);

        // input handlers
        inputHandler.AddHandler(_gunHandler);
        inputHandler.AddHandler(_itemHandler);
        inputHandler.AddHandler(_playerSelector);
        inputHandler.AddHandler(stageManager);
        inputHandler.AddHandler(_weaponSelector);

        scopedTileRenderer.Construct(playerStore, LevelStore, 4);

        stageManager.AddStageHandler(new FightStageHandler(_gunHandler, enemySpawner, weaponHandler));
        stageManager.AddStageHandler(new BuildStageHandler(_itemHandler, LevelStore, scopedTileRenderer, inputHandler, inventoryHandler));

        panelManager.startGamePanel = startGamePanel;

        levelLoader.Construct(LevelStore);

        LevelStore.AddLevelToLoad(new LevelLoadingInfo(LevelName.Level, new Vector2(0, 0)));
        LevelStore.AddLevelToLoad(new LevelLoadingInfo(LevelName.Level2, new Vector2(28, 0)));
        levelLoader.LevelLoadedEventHandler += LevelLoaded;
        levelLoader.Load();

        cursorHandler.SetDefaultCursor();
    }

    private void LevelLoaded(object sender, LevelLoadedEventArgs args)
    {
        var levelInjector = args.LevelInjector;
        var level = levelInjector.level;
        level.RootGameObject = args.RootGameObject;
        level.TilemapHandler = levelInjector.tilemapHandler;
        level.TilemapHandler.GetTileAt(Vector2.right);

        level.Construct(gameManager);

        args.RootGameObject.transform.Translate(new Vector3(args.TranslateScene.x, args.TranslateScene.y, 0));

        LevelStore.AddLevel(level);
        if (!LevelStore.ActiveLevel)
        {
            LevelStore.ActiveLevel = level;
        }

        level.EnvironmentData = new EnvironmentData(levelInjector.border, levelInjector.tilemapObjects, levelInjector.blocks);
    }    
}
