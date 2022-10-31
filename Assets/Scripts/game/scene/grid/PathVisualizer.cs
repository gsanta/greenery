using game.character;
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

        private List<Vector2> _path;

        private Level _level;

        private ICharacter _character;

        private LineRenderer _lineRenderer;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = Color.green;
            _lineRenderer.endColor = Color.green;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
        }

        public void SetLevel(Level level)
        {
            _level = level;
        }

        public void SetPathMovement(ICharacter character)
        {
            _character = character;
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
            if (_character == null)
            {
                return;
            }

            //var path = _character.Movement.GetPath();
            //UpdateMesh(path);
            UpdateDirection(_character);

        }

        private void UpdateMesh(List<Vector2> path)
        {
            var grid = _level.Grid;
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
            var grid = _level.Grid;
            var index = 0;
            Vector2 quadSize = new Vector3(1, 1) * grid.CellSize / 2.0f;

            path.ForEach((pos) =>
            {
                var pos3d = new Vector3(pos.x, pos.y, -0.5f);

                var uvVal = new Vector2(2f, 0);

                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, pos3d, quadSize, uvVal);

                index++;
            });
        }

        private void UpdateDirection(ICharacter character)
        {
            //if (character.Movement.GetPath() == null || character.Movement.GetPath().Count == 0)
            //{
            //    return;
            //}

            //var startPos = character.GetPosition();
            //var endPos = character.Movement.GetPath()[0];
            //_lineRenderer.SetPosition(0, new Vector3(startPos.x, startPos.y, 0));
            //_lineRenderer.SetPosition(1, new Vector3(endPos.x, endPos.y, 0));
        }
    }
}
