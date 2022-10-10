﻿using UnityEngine;
using utils;

namespace game.scene.grid
{
    public class GridVisualizer : MonoBehaviour
    {

        private Mesh _mesh;

        private GridGraph _gridGraph;

        public bool IsVisualize { get; private set; }

        public void Construct(GridGraph gridGraph)
        {
            _gridGraph = gridGraph;

            if (IsVisualize)
            {
                Show();
            }
        }

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        public void Hide()
        {
            IsVisualize = false;

            _mesh.vertices = new Vector3[] {};
            _mesh.uv = new Vector2[] {};
            _mesh.triangles = new int[] {};
        }

        public void Show()
        {
            IsVisualize = true;


            if (!_mesh)
            {
                return;
            }

            MeshUtils.CreateEmptyMeshArrays(_gridGraph.Width * _gridGraph.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

            for (int x = 0; x < _gridGraph.Width; x++)
            {
                for (int y = 0; y < _gridGraph.Height; y++)
                {
                    RenderQuad(x, y, vertices, uv, triangles);
                }
            }

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
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
