
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

        private Environment _environment;
        
        private LevelBounds _levelBounds;

        private grid.Grid _gridSystem;


        public void Construct(Injector injector)
        {
            var enemyFactory = injector.enemyFactory;
            var levelLoader = injector.levelLoader;
            var gridVisualizer = injector.gridVisualizer;

            level.Construct(enemyFactory, levelLoader, gridVisualizer);

            _levelBounds = new LevelBounds(tilemapGround);

            _environment = new Environment(level, blocks, tilemapGround);

            _gridSystem = new grid.Grid(level);

            level.LevelBounds = _levelBounds;
            level.Environment = _environment;
            level.Grid = _gridSystem;
        }
    }
}
