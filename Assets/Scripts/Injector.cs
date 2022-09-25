﻿using game;
using game.character.characters.enemy;
using game.character.characters.player;
using game.character.state;
using game.item.bullet;
using game.scene;
using game.scene.level;
using GUI;
using UnityEngine;
using game.tool.weapon;
using game.character.enemy;
using gui;
using Assets.Scripts.debug;
using UnityEngine.SceneManagement;
using System;
using Environment = game.scene.level.Environment;

public class Injector : MonoBehaviour
{    
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

    [SerializeField] public EnemyFactory enemyFactory;

    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private FovVisualDecorator fovVisualDecorator;

    [SerializeField] private PathVisualDecorator pathVisualDecorator;

    private EnemyManager _enemyManager;

    // player

    [SerializeField] private PlayerFactory playerFactory;

    [SerializeField] public PlayerStore playerStore;

    [SerializeField] public PlayerCommandHandler playerCommandHandler;

    // Scene
    [SerializeField] public LevelLoader levelLoader;

    [SerializeField] public LevelStore LevelStore;

    // State
    [SerializeField] private StateFactory stateFactory;


    private void Awake()
    {
        stateFactory.Construct(playerStore);

        weaponFactory.Construct(bulletFactory);

        enemyFactory.Construct(enemyStore, playerStore, weaponFactory, gameManager, stateFactory, new EnemyDecorator[] { fovVisualDecorator, pathVisualDecorator });
        enemySpawner.Construct(enemyFactory, enemyStore, LevelStore);
        playerFactory.Construct(playerStore, healthBar, bulletPanel, weaponFactory, followCamera);

        playerCommandHandler.Construct(playerStore, playerFactory, bulletPanel);

        playerManager = new PlayerManager(playerFactory);

        _enemyManager = new EnemyManager(enemyFactory, enemySpawner);

        gameManager.Construct(playerManager, panelManager, _enemyManager, followCamera);

        panelManager.startGamePanel = startGamePanel;
        
        levelLoader.Construct(this);
        levelLoader.LevelLoadedEventHandler += LevelLoaded;
        levelLoader.Load("Level");
    }

    private void LevelLoaded(object sender, LevelLoadedEventArgs e)
    {
        InjectLevel(e.Scene, e.TranslateScene);
    }

    private void InjectLevel(Scene scene, Vector2 translate)
    {
        var levelInjectorGameObject = Array.Find(scene.GetRootGameObjects(), (obj) => obj.name == LevelInjector.UnityName);
        var rootGameObject = Array.Find(scene.GetRootGameObjects(), (gameObject) => gameObject.name == "Root");

        var levelInjector = levelInjectorGameObject.GetComponent<LevelInjector>();
        

        var level = levelInjector.level;
        level.RootGameObject = rootGameObject;
        level.Construct(levelInjector.border, levelLoader, gameManager, levelInjector.gridVisualizer);

        rootGameObject.transform.Translate(new Vector3(translate.x, translate.y, 0));

        LevelStore.AddLevel(level);
        LevelStore.ActiveLevel = level;

        level.LevelBounds = new LevelBounds(levelInjector.tilemapGround);

        level.Environment = new Environment(levelInjector.blocks, levelInjector.tilemapObjects, level.LevelBounds);
    }
}
