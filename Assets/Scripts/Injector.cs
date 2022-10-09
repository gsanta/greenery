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
using UnityEngine.SceneManagement;
using System;
using game.character.player;
using Base.Input;
using game.Item;
using game.Common;
using Game.Stage;
using game.item;
using System.Linq;

public class Injector : MonoBehaviour
{    
    [SerializeField] private HealthPanel healthBar;

    // base

    [SerializeField] private InputManager inputManager;

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

    [SerializeField] public EnemyFactory enemyFactory;

    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private FovVisualDecorator fovVisualDecorator;

    [SerializeField] private PathVisualDecorator pathVisualDecorator;

    // player

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] public PlayerStore playerStore;

    [SerializeField] public PlayerCommandHandler playerCommandHandler;

    private GunInputHandler _gunInputHandler;

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
    private ItemInputHandler _itemInputHandler;

    [SerializeField] private ItemFactory itemFactory;

    // stage
    [SerializeField] private StageManager stageManager;

    // common
    [SerializeField] private CursorHandler cursorHandler;

    private void Awake()
    {
        stateFactory.Construct(playerStore);

        weaponFactory.Construct(bulletFactory);

        enemyFactory.Construct(enemyStore, playerStore, weaponFactory, gameManager, stateFactory, new EnemyDecorator[] { fovVisualDecorator, pathVisualDecorator });
        enemySpawner.Construct(enemyFactory, enemyStore, LevelStore);
        playerFactory.Construct(playerStore, healthBar, bulletPanel, weaponFactory, followCamera);

        playerCommandHandler.Construct(playerStore, playerFactory);
        _gunInputHandler = new GunInputHandler(playerStore, bulletPanel);
        inputManager.AddHandler(_gunInputHandler);

        playerManager = new PlayerManager(playerFactory);

        gameManager.Construct(playerManager, panelManager, followCamera);

        inventoryHandler.Construct(inventoryItemFactory, _inventoryStore, cursorHandler);
        inventoryItemFactory.Construct(_inventoryStore, cursorHandler);

        _itemInputHandler = new ItemInputHandler(_inventoryStore, itemFactory);
        inputManager.AddHandler(_itemInputHandler);

        stageManager.AddStageHandler(new FightStageHandler(_gunInputHandler, enemySpawner));
        stageManager.AddStageHandler(new BuildStageHandler(_itemInputHandler, LevelStore));

        panelManager.startGamePanel = startGamePanel;

        levelLoader.Construct(LevelStore);

        LevelStore.AddLevelToLoad(new LevelLoadingInfo(LevelName.Level, new Vector2(0, 0)));
        LevelStore.AddLevelToLoad(new LevelLoadingInfo(LevelName.Level2, new Vector2(28, 0)));
        levelLoader.LevelLoadedEventHandler += LevelLoaded;
        levelLoader.Load();

        cursorHandler.SetDefaultCursor();
    }

    private void LevelLoaded(object sender, LevelLoadedEventArgs e)
    {
        InjectLevel(e.Scene, e.TranslateScene);

        if (LevelStore.GetLevelsToLoad().All(levelLoadingInfo => levelLoadingInfo.IsLoaded))
        {
            AllLevelsLoaded();
        }
    }

    private void AllLevelsLoaded()
    {
        stageManager.DeactivateAllStages();
        stageManager.ActivateStage(StageType.BuildStage);
    }

    private void InjectLevel(Scene scene, Vector2 translate)
    {
        var levelInjectorGameObject = Array.Find(scene.GetRootGameObjects(), (obj) => obj.name == LevelInjector.UnityName);
        var rootGameObject = Array.Find(scene.GetRootGameObjects(), (gameObject) => gameObject.name == "Root");

        var levelInjector = levelInjectorGameObject.GetComponent<LevelInjector>();
        

        var level = levelInjector.level;
        level.RootGameObject = rootGameObject;


        level.Construct(gameManager, levelInjector.gridVisualizer);

        rootGameObject.transform.Translate(new Vector3(translate.x, translate.y, 0));

        LevelStore.AddLevel(level);
        if (!LevelStore.ActiveLevel)
        {
            LevelStore.ActiveLevel = level;
        }

        level.EnvironmentData = new EnvironmentData(levelInjector.border, levelInjector.tilemapObjects, levelInjector.blocks);
    }
}
