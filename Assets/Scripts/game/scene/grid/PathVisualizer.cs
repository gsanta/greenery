using game.scene.grid.path;
using System.Collections.Generic;
using UnityEngine;
using utils;

namespace game.scene.grid
{
    public class PathVisualizer : MonoBehaviour
    {
        private Mesh _mesh;

        private GridGraph<PathNode> _graph;

        private PathMovement _pathMovement;

        private List<Vector2> _path;

        public void Construct(GridGraph<PathNode> graph)
        {
            _graph = graph;
        }

        public void SetPathMovement(PathMovement pathMovement)
        {
            _pathMovement = pathMovement;
        }

        public void SetPath(List<Vector2> path)
        {
            _path = path;
        }

        private void Awake()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;

            InvokeRepeating(nameof(UpdateMesh), 0.5f, 0.5f);
        }

        private void UpdateMesh()
        {
            var grid = _graph;
            MeshUtils.CreateEmptyMeshArrays(grid.Width * grid.Height, out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

            var path = _path != null ? _path : _pathMovement.GetPath(); 
            if (path != null)
            {
                CreatePath(vertices, uv, triangles);
            }

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }

        private void CreatePath(Vector3[] vertices, Vector2[] uv, int[] triangles)
        {
            var grid = _graph;
            var index = 0;
            Vector2 quadSize = new Vector3(1, 1) * grid.CellSize;


            if (_pathMovement.GetPath() != null)
            {
                _pathMovement.GetPath().ForEach((pos) =>
                {
                    var pos3d = new Vector3(pos.x, pos.y, -0.5f);

                    var uvVal = new Vector2(2f, 0);

                    MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, pos3d, 0f, quadSize, 0.1f, uvVal, uvVal);

                    index++;
                });
            }
        }
    }
}
