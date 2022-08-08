
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

        private Environment environment;

        public void Construct(Injector injector)
        {
            environment = new Environment(blocks, tilemapGround);

            var enemyFactory = injector.enemyFactory;
            var playerStore = injector.playerStore;
            var levelLoader = injector.levelLoader;
            var gridVisualizer = injector.gridVisualizer;

            level.Construct(enemyFactory, playerStore, levelLoader, gridVisualizer, environment);
        }
    }
}
