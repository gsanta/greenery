using game.scene;
using game.scene.level;
using UnityEngine;

namespace Assets.Scripts.game.scene
{
    public class CameraConfiner : MonoBehaviour
    {
        private PolygonCollider2D _collider;

        private FollowCamera _followCamera;

        public void Construct(FollowCamera followCamera)
        {
            _followCamera = followCamera;
        }

        private void Start()
        {
            _collider = GetComponent<PolygonCollider2D>();
        }

        public void SetDimensions(Level level)
        {
            var topLeft = level.EnvironmentData.TopLeft;
            var bottomRight = level.EnvironmentData.BottomRight;

            _collider.enabled = false;
            _collider.pathCount = 1;
            _collider.SetPath(0,
                new Vector2[] {
                    new Vector2(topLeft.x, bottomRight.y),
                    new Vector2(topLeft.x, topLeft.y),
                    new Vector2(bottomRight.x, topLeft.y),
                    new Vector2(bottomRight.x, bottomRight.y)
                }
            );
            _collider.enabled = true;

            _followCamera.GetCameraConfiner().InvalidateCache();
        }
    }
}