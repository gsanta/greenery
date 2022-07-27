﻿using game;
using game.character.characters.enemy;
using game.character.characters.player;
using game.character.state;
using game.item;
using game.item.bullet;
using game.scene;
using game.scene.area;
using game.scene.grid;
using game.scene.tile;
using GUI;
using gui.avatar;
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
    
    // Tile
    [SerializeField] private TileMapBase tileMapBase;

    private TileModule _tileModule;
    
    // State
    [SerializeField] private StateFactory stateFactory;
    
    private AvatarStore _avatarStore;

    private readonly ItemStore<Ball> _ballStore = new();

    private void Awake()
    {
        stateFactory.Construct(playerStore);
        
        tileSystem.Construct(playerStore);
        _tileModule = new TileModule(tileMapBase);
        
        _gridModule = new GridModule(gridSetup, _tileModule);
        enemyFactory.Construct(enemyStore, playerStore, bulletFactory, gameManager, _gridModule, stateFactory);
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
