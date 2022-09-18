
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

        public void Construct(Injector injector)
        {
            var levelLoader = injector.levelLoader;
            var gameManager = injector.gameManager;
            var levelStore = injector.LevelStore;

            level.Construct(border, levelLoader, gameManager, gridVisualizer);

            levelStore.AddLevel(level);
            levelStore.ActiveLevel = level;

            _levelBounds = new LevelBounds(tilemapGround);

            _environment = new Environment(blocks, tilemapObjects, _levelBounds);


            level.LevelBounds = _levelBounds;
            level.Environment = _environment;
        }
    }
}
