
using game.scene.grid;
using game.scene.level;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene
{
    public class LevelInjector : MonoBehaviour
    {
        public static string UnityName = "Level Injector";

        [SerializeField] private Level level;

        [SerializeField] private Transform blocks;

        [SerializeField]  private Tilemap tilemapGround;

        [SerializeField]  private Tilemap tilemapObjects;

        [SerializeField] private GameObject border;

        [SerializeField] private GridVisualizer gridVisualizer;

        private Environment _environment;
        
        private LevelBounds _levelBounds;

        private grid.Grid _grid;

        public void Construct(Injector injector)
        {
            var levelLoader = injector.levelLoader;
            var gameManager = injector.gameManager;
            var levelStore = injector.LevelStore;

            level.Construct(border, levelLoader, levelStore, gameManager);

            _levelBounds = new LevelBounds(tilemapGround);

            _environment = new Environment(blocks, tilemapGround, tilemapObjects);

            _grid = new grid.Grid(level);

            gridVisualizer.Construct(_grid);

            level.LevelBounds = _levelBounds;
            level.Environment = _environment;
            level.Grid = _grid;
        }
    }
}
