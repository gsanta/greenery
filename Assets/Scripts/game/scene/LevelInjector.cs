
using game.scene.grid;
using game.scene.level;
using game.scene.tile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene
{
    public class LevelInjector : MonoBehaviour
    {
        public static string UnityName = "Level Injector";

        [SerializeField] public Level level;

        [SerializeField] public Transform blocks;

        [SerializeField]  public Tilemap tilemapGround;

        [SerializeField]  public Tilemap tilemapObjects;

        [SerializeField]  public TilemapHandler tilemapHandler;

        [SerializeField] public GameObject border;

        [SerializeField] public TileRenderer gridVisualizer;
    }
}
