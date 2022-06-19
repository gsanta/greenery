using System.Linq;
using Algorithms.RopeWrapping;
using UnityEngine;

public class DrawScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private float z = 0.5f;

    private RopeWrapper _ropeWrapper;

    private bool isStarted = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //Vector3[] positions = new Vector3[3] { new Vector3(0, 0, z), new Vector3(-1, 1, z), new Vector3(1, 1, z) };
        //DrawTriangle(positions);
    }

    public void StartDraw(Vector2 startPoint)
    {
        isStarted = true;
        var points = GameObject.FindGameObjectsWithTag("Anchor").Select((obj) =>
        {
            var position = obj.transform.position;
            return new Vector2(position.x, position.y);
        }).ToList();
        _ropeWrapper = new RopeWrapper(startPoint, points);
    }

    public void UpdateDraw(Vector2 currentPoint)
    {
        if (!isStarted) return;
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