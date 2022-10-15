using UnityEngine;
using utils;

namespace game.scene.grid
{
    public class GridVisualizer : MonoBehaviour
    {

        private Mesh _mesh;

        private GridGraph _gridGraph;

        private int _radius = 5;

        private GameObject _radiusOrigin;

        private Vector2Int _radiusOriginPos;

        public bool IsVisualize { get; private set; }

        public void SetRadius(int radius)
        {
            _radius = radius;
        }

        public void SetRadiusOrigin(GameObject origin)
        {
            _radiusOrigin = origin;
        }

        public void SetGrid(GridGraph gridGraph, Transform parent)
        {
            Hide();
            _gridGraph = gridGraph;
            transform.position = parent.transform.position;
        }

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        private void Update()
        {
            if (_gridGraph != null && _radiusOrigin != null && IsVisualize)
            {
                var newPos = _gridGraph.GetGridPosition(_radiusOrigin.transform.position);

                if (newPos.Value != _radiusOriginPos)
                {
                    _radiusOriginPos = newPos.Value;
                    Remove();
                    Render();
                }
            }
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

        private void Remove()
        {
            _mesh.Clear();
        }

        private void Render()
        {
            MeshUtils.CreateEmptyMeshArrays(_gridGraph.Width * _gridGraph.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

            for (int x = 0; x < _gridGraph.Width; x++)
            {
                for (int y = 0; y < _gridGraph.Height; y++)
                {
                    if (IsWithinRadius(x, y))
                    {
                        RenderQuad(x, y, vertices, uv, triangles);
                    }
                }
            }

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }

        private bool IsWithinRadius(int x, int y)
        {
            if (_radius == -1 || _radiusOrigin == null)
            {
                return true;
            }

            var position = _radiusOrigin.transform.position;

            if (x >= _radiusOriginPos.x - _radius && x <= _radiusOriginPos.x + _radius)
            {
                if (y >= _radiusOriginPos.y - _radius && y <= _radiusOriginPos.y + _radius)
                {
                    return true;
                }
            }

            return false;
        }

        private void RenderQuad(int x, int y, Vector3[] vertices, Vector2[] uv, int[] triangles)
        {
            int index = y * _gridGraph.Width + x;
            Vector2 quadSize = new Vector3(1, 1) * _gridGraph.CellSize / 2.0f * 0.9f;

            var node = _gridGraph.GetNode(x, y);
            var pos = _gridGraph.GetWorldPosition(x, y);
            var pos3d = new Vector3(pos.x, pos.y, -0.5f);

            Vector2 uvVal = new Vector2(0, 0);

            if (!node.IsWalkable)
            {
                uvVal = new Vector2(0.1f, 0.5f);
            }

            MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, pos3d, quadSize, uvVal);
        }
    }
}
