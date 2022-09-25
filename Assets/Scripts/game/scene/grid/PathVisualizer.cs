using game.scene.grid.path;
using game.scene.level;
using System.Collections.Generic;
using UnityEngine;
using utils;

namespace game.scene.grid
{
    public class PathVisualizer : MonoBehaviour
    {
        private Mesh _mesh;

        private PathMovement _pathMovement;

        private List<Vector2> _path;

        private Level _level;

        public void SetLevel(Level level)
        {
            _level = level;
        }

        public void SetPathMovement(PathMovement pathMovement)
        {
            _pathMovement = pathMovement;
        }

        public void SetPath(List<Vector2> path)
        {
            _path = path;

            UpdateMesh(path);
        }

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;

            InvokeRepeating(nameof(UpdatePathMovement), 0.5f, 0.5f);
        }

        private void UpdatePathMovement()
        {
            if (_pathMovement == null)
            {
                return;
            }

            var path = _pathMovement.GetPath();
            UpdateMesh(path);
        }

        private void UpdateMesh(List<Vector2> path)
        {
            var grid = _level.Graph;
            MeshUtils.CreateEmptyMeshArrays(grid.Width * grid.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

            if (path != null)
            {
                CreatePath(vertices, uv, triangles, path);
            }

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }

        private void CreatePath(Vector3[] vertices, Vector2[] uv, int[] triangles, List<Vector2> path)
        {
            var grid = _level.Graph;
            var index = 0;
            Vector2 quadSize = new Vector3(1, 1) * grid.CellSize;

            path.ForEach((pos) =>
            {
                var pos3d = new Vector3(pos.x, pos.y, -0.5f);

                var uvVal = new Vector2(2f, 0);

                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, pos3d, 0f, quadSize, 0.1f, uvVal, uvVal);

                index++;
            });
        }
    }
}
