
using UnityEngine;

namespace game.character.ability.field_of_view
{
    public class FieldOfView : MonoBehaviour
    {

        private void Start()
        {
            Mesh mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            Vector3[] vertices = new Vector3[3];
            Vector2[] uv = new Vector2[3];
            int[] triangles = new int[3];

            vertices[0] = Vector3.zero;
            vertices[1] = new Vector3(50, 0);
            vertices[2] = new Vector3(0, -50);

            triangles[0] = 0;
            triangles[1] = 2;
            triangles[2] = 1;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
        }
    }
}
