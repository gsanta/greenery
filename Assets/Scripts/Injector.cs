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
using game.character.movement;
using Codice.CM.Interfaces;
using game.character.movement.path;
using game.GamePlay;

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
    
    [SerializeField] public EnemyFactory enemyFactory;

    [SerializeField] private EnemySpawner enemySpawner;
    
    private EnemyManager _enemyManager;

    [SerializeField] private FovVisualDecorator fovVisualDecorator;

    [SerializeField] private PathVisualDecorator pathVisualDecorator;

    // player

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] public PlayerStore playerStore;

    private PlayerSelector _playerSelector;

    private CharacterEvents _characterEvents = new CharacterEvents();

    private MovementManager _movementManager;

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

        stateFactory.Construct(playerStore, _characterEvents);

        _enemyManager = new EnemyManager(enemySpawner);

        _movementManager = new MovementManager(_characterEvents, playerStore, LevelStore, followCamera);

        // weapon
        weaponFactory.Construct(bulletFactory);
        weaponImageFactory.Construct(_weaponImageStore);
        _weaponSelector = new WeaponSelector(playerStore, _weaponImageStore);
        weaponHandler.Construct(weaponImageFactory, _weaponSelector);

        enemyFactory.Construct(playerStore, weaponFactory, stateFactory, _characterEvents, new EnemyDecorator[] { fovVisualDecorator, pathVisualDecorator });
        enemySpawner.Construct(enemyFactory, playerStore, LevelStore);
        playerFactory.Construct(playerStore, healthBar, bulletPanel, weaponFactory, followCamera, inputHandler, _characterEvents);

        _gunHandler = new GunHandler(playerStore, bulletPanel);

        playerManager = new PlayerManager(playerFactory, playerStore, LevelStore, scopedTileRenderer, followCamera, weaponHandler);
        _playerSelector = new PlayerSelector(playerStore);

        gameManager.Construct(playerManager, panelManager, stageManager, followCamera, levelLoader, _movementManager, _enemyManager);

        inventoryHandler.Construct(inventoryItemFactory, _inventoryStore, cursorHandler);
        inventoryItemFactory.Construct(_inventoryStore, cursorHandler);

        _itemHandler = new ItemHandler(_inventoryStore, itemFactory, LevelStore);

        // input handlers
        _gunHandler.Register(inputHandler);
        _itemHandler.Register(inputHandler);
        _playerSelector.Register(inputHandler);
        stageManager.Register(inputHandler);
        _weaponSelector.Register(inputHandler);
        //inputHandler.AddHandler(new PlayerCommander(playerStore, LevelStore));


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
