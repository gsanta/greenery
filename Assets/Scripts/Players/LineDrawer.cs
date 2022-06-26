using System.Linq;
using Algorithms.RopeWrapping;
using GameInfo;
using UnityEngine;

namespace Players
{
    public class LineDrawer : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        [SerializeField]
        private float z = 0.5f;

        private RopeWrapper _ropeWrapper;

        private bool _isDrawing = false;

        private GameInfoStore _gameInfoStore;
        public void Construct(GameInfoStore gameInfoStore)
        {
            _gameInfoStore = gameInfoStore;
        }

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!_isDrawing)
                {
                    _isDrawing = true;
                    StartDraw(transform.position);
                }
            }

            if (_isDrawing)
            {
                UpdateDraw(transform.position);
                UpdateLength();
            }
        }

        private void StartDraw(Vector2 startPoint)
        {
            _isDrawing = true;
            var points = GameObject.FindGameObjectsWithTag("Anchor").Select((obj) =>
            {
                var position = obj.transform.position;
                return new Vector2(position.x, position.y);
            }).ToList();
            _ropeWrapper = new RopeWrapper(startPoint, points);
        }

        private void UpdateDraw(Vector2 currentPoint)
        {
            if (!_isDrawing) return;
            _ropeWrapper.Update(currentPoint);
            var points = _ropeWrapper.GetPoints();
            lineRenderer.positionCount = points.Count;
        
            foreach (var item in points.Select((value, i) => new { i, value }))
            {
                var point = item.value;
                var index = item.i;
                lineRenderer.SetPosition(index, new Vector3(point.x, point.y, -1f));
            }
        }

        private void UpdateLength()
        {
            if (_ropeWrapper != null)
            {
                float len = 0;
                var points = _ropeWrapper.GetPoints();
                for (int i = 0; i < points.Count - 1; i++)
                {
                    len += (points[i + 1] - points[i]).magnitude;
                }

                _gameInfoStore.UpdateCurrentBallLength(len);
            }
        }

        // public void AddPoint(Vector2 point)
        // {
        //     var currentSize = lineRenderer.positionCount;
        //     lineRenderer.positionCount = currentSize + 1;
        //     lineRenderer.SetPosition(currentSize, new Vector3(point.x, point.y, -1f)); 
        // }

        //void DrawTriangle(Vector3[] vertexPositions)
        //{
        //    lineRenderer.positionCount = 3;
        //    lineRenderer.positionCount
        //    lineRenderer.SetPositions(vertexPositions);
        //}
    }
}