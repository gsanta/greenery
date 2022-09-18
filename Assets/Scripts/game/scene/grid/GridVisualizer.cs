using UnityEngine;
using utils;

namespace game.scene.grid
{
    public class GridVisualizer : MonoBehaviour
    {

        private Mesh _mesh;

        private Grid _grid;

        private bool isVisualize = false;

        public void Construct(Grid grid)
        {
            _grid = grid;
        }

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        public void ToggleVisualization()
        {
            isVisualize = !isVisualize;

            if (isVisualize)
            {
                Show();

                InvokeRepeating(nameof(Show), 0.5f, 0.5f); ;
            } else
            {
                CancelInvoke(nameof(Show));

                Hide();
            }
        }

        private void Hide()
        {
            _mesh.vertices = new Vector3[] {};
            _mesh.uv = new Vector2[] {};
            _mesh.triangles = new int[] {};
        }

        private void Show()
        {
            var grid = _grid.Graph;
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

                    Vector2 uvVal = new Vector2(0, 0);

                    if (!node.IsWalkable)
                    {
                        uvVal = new Vector2(0.5f, 0.5f);
                    }

                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, pos3d, 0f, quadSize, 0.1f, uvVal, uvVal);
                }
            }

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }
    }
}
