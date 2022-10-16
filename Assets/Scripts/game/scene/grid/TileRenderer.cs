using UnityEngine;
using utils;

namespace game.scene.grid
{
    public abstract class TileRenderer : MonoBehaviour
    {

        private Mesh _mesh;

        public bool IsVisualize { get; private set; }

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        public void Hide()
        {
            IsVisualize = false;

            Remove();
        }

        public void Show()
        {
            IsVisualize = true;

            if (!_mesh)
            {
                return;
            }

            Render();
        }

        protected void Remove()
        {
            _mesh.Clear();
        }

        protected void Render()
        {
            var grid = GetGridGraph();

            MeshUtils.CreateEmptyMeshArrays(grid.Width * grid.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    if (IsRenderQuad(x, y))
                    {
                        RenderQuad(x, y, vertices, uv, triangles);
                    }
                }
            }

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }

        protected void RenderQuad(int x, int y, Vector3[] vertices, Vector2[] uv, int[] triangles)
        {
            var graph = GetGridGraph();

            int index = y * graph.Width + x;
            Vector2 quadSize = new Vector3(1, 1) * graph.CellSize / 2.0f * 0.9f;

            var node = graph.GetNode(x, y);
            var pos = graph.GetWorldPosition(x, y) + GetOffsetPosition();
            var pos3d = new Vector3(pos.x, pos.y, -0.5f);

            Vector2 uvVal = GetUV(node);

            MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, pos3d, quadSize, uvVal);
        }

        abstract protected bool IsRenderQuad(int x, int y);

        abstract protected GridGraph GetGridGraph();

        abstract protected Vector2 GetUV(PathNode node);

        abstract protected Vector2 GetOffsetPosition();
    }
}
