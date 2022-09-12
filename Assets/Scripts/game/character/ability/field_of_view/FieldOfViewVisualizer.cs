
using game.character.characters.player;
using UnityEngine;

namespace game.character.ability.field_of_view
{
    public class FieldOfViewVisualizer : MonoBehaviour
    {
        private Mesh _mesh;

        private readonly int _rayCount = 20;

        private PlayerStore _playerStore;

        private ICharacter _character;

        private FieldOfView _fieldOfView;

        public void Construct(FieldOfView fieldOfView, ICharacter character, PlayerStore playerStore)
        {
            _fieldOfView = fieldOfView;
            _character = character;
            _playerStore = playerStore;
        }

        public ICharacter FindTarget()
        {
            var player = _playerStore.GetActivePlayer();
            if (Vector2.Distance(_character.GetPosition(), player.GetPosition()) < _fieldOfView.ViewDistance)
            {
                Vector2 targetDirection = (player.GetPosition() - _character.GetPosition()).normalized;
                Direction dir = _character.Movement.GetDirection();
                Vector2 aimDirection = DirectionHelper.DirToVector(dir);
                if (Vector2.Angle(aimDirection, targetDirection) < _fieldOfView.Fov / 2f)
                {
                    return player;
                }
            }

            return null;
        }

        private void Start()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        private void LateUpdate()
        {

            Vector3[] vertices = new Vector3[_rayCount + 1 + 1];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[_rayCount * 3];

            BuildMesh(vertices, triangles);

            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }

        private float GetStartAngle()
        {
            var direction = _character.Movement.GetDirection();
            var vector = Utilities.ToVector3(DirectionHelper.DirToVector(direction));
            var angle = Utilities.GetAngleFromVectorFloat(vector) + _fieldOfView.Fov / 2f;
            return angle;
        }

        private void BuildMesh(Vector3[] vertices, int[] triangles)
        {
            var origin = Utilities.ToVector3(_character.GetPosition());
            var angle = GetStartAngle();

            int vertexIndex = 1;
            int triangleIndex = 0;

            vertices[0] = origin;

            for (int i = 0; i <= _rayCount; i++)
            {
                Vector3 vertex;
                LayerMask mask = LayerMask.GetMask("Wall");
                RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Utilities.GetVectorFromAngle(angle), _fieldOfView.ViewDistance, mask);

                if (raycastHit2D.collider == null)
                {
                    vertex = origin + Utilities.GetVectorFromAngle(angle) * _fieldOfView.ViewDistance;
                }
                else
                {
                    vertex = raycastHit2D.point;
                }

                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;
                    triangleIndex += 3;
                }

                vertexIndex++;

                angle -= (_fieldOfView.Fov / _rayCount);
            }
        }
    }
}
