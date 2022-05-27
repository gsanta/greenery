using UnityEngine;

public class DrawScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private float z = 0.5f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //Vector3[] positions = new Vector3[3] { new Vector3(0, 0, z), new Vector3(-1, 1, z), new Vector3(1, 1, z) };
        //DrawTriangle(positions);
    }

    public void AddPoint(Vector2 point)
    {
        var currentSize = lineRenderer.positionCount;
        lineRenderer.positionCount = currentSize + 1;
        lineRenderer.SetPosition(currentSize, new Vector3(point.x, point.y, -1f)); 
    }

    //void DrawTriangle(Vector3[] vertexPositions)
    //{
    //    lineRenderer.positionCount = 3;
    //    lineRenderer.positionCount
    //    lineRenderer.SetPositions(vertexPositions);
    //}
}