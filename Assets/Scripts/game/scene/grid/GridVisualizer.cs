using UnityEngine;
using utils;

namespace game.scene.grid
{
    public class GridVisualizer : MonoBehaviour
    {

        private Mesh _mesh;

        public GridSystem GridSystem { set; get; }

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        public void UpdateHeatMapVisual()
        {
            var grid = GridSystem.Grid;
            MeshUtils.CreateEmptyMeshArrays(grid.Width * grid.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    int index = y * grid.Width + x;
                    Vector2 quadSize = new Vector3(1, 1) * grid.CellSize;

                    var node = grid.GetNode(x, y);

                    var pos = grid.GetWorldPosition(x, y);
                    var pos3d = new Vector3(pos.x, pos.y, -0.5f);

                    Vector2 uvVal = new Vector2(1f, 0);

                    if (!node.IsWalkable)
                    {
                        uvVal = new Vector2(0, 0);
                    }
                    else if (node.WalkCounter > 0)
                    {
                        var val = (float) node.WalkCounter / PathNode.MAX_WALK_COUNTER * 0.9f;
                        uvVal = new Vector2(val, 0);
                    }

                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, pos3d, 0f, quadSize, 0.1f, uvVal, uvVal);
                }
            }

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }

        public void Show()
        {
            UpdateHeatMapVisual();

            InvokeRepeating(nameof(UpdateHeatMapVisual), 0.5f, 0.5f);

            var grid = GridSystem.Grid;

            var halfCellSize = new Vector2(grid.CellSize, grid.CellSize) / 2;
            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    var pos = grid.GetWorldPosition(x, y);
                    var right = grid.GetWorldPosition(x + 1, y);
                    var bottom = grid.GetWorldPosition(x, y + 1);
                    var node = grid.GetNode(x, y);
                    //Utilities.CreateWorldText( node.IsWalkable  ? 0.ToString() : 1.ToString(), null, grid.GetWorldPosition(x, y), 5, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(pos, bottom, Color.blue, 100f);
                    Debug.DrawLine(pos, right, Color.blue, 100f);
                }
            }

            var bottomLeft = grid.GetWorldPosition(0, grid.Height);
            var bottomRight = grid.GetWorldPosition(grid.Width, grid.Height);
            var topLeft = grid.GetWorldPosition(grid.Width, 0);
            var topRight = grid.GetWorldPosition(grid.Width, grid.Height);
            Debug.DrawLine(bottomLeft, bottomRight, Color.blue, 100f);
            Debug.DrawLine(topLeft, topRight, Color.blue, 100f);
        }
    }
}
