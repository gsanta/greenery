
using UnityEngine;
using UnityEngine.Tilemaps;

namespace game.scene.level
{
    public class EnvironmentData
    {
        public Tilemap TileMapObjects { private set; get; }

        public Transform ObjectsContainer { private set; get; }

        public Vector3 TopLeft { get; private set; }

        public Vector3 BottomRight { get; private set; }

        public Vector3 Center { get; private set; }

        public Vector2 Size { get; private set; }

        public float GridSize { get; private set; }

        public Vector2Int GridCenter { get; private set; }

        private GameObject _border;

        public EnvironmentData(GameObject border, Tilemap tilemapObjects, Transform objectsContainer)
        {
            _border = border;
            TileMapObjects = tilemapObjects;
            ObjectsContainer = objectsContainer;
        }

        public void Init()
        {
            var bounds = _border.GetComponent<Renderer>().bounds;

            TopLeft = new Vector2(bounds.min.x, bounds.max.y);
            BottomRight = new Vector2(bounds.max.x, bounds.min.y);
            GridSize = 1;
            Center = new Vector3((TopLeft.x + BottomRight.x) / 2, (TopLeft.y + BottomRight.y) / 2, bounds.center.y);

            Size = new Vector2(bounds.size.x, bounds.size.y);

            var offsetX = Mathf.RoundToInt(Size.x / GridSize / 2);
            var offsetY = Mathf.RoundToInt(Size.y / GridSize / 2);
            GridCenter = new Vector2Int(offsetX, offsetY);
        }
    }
}
