using Assets.Scripts.game.scene;
using Cinemachine;
using game.character.characters.player;
using UnityEngine;

namespace game.scene
{
    public class FollowCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _vcam;

        [SerializeField] public CameraConfiner confiner;
 
        void Awake()
        {
            confiner.Construct(this);
            _vcam = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetTarget(Player player)
        {
            _vcam.Follow = player.transform;
        }

        public CinemachineConfiner2D GetCameraConfiner()
        {
            return _vcam.GetComponentInChildren<CinemachineConfiner2D>();
        }

        public CameraConfiner GetConfiner()
        {
            return confiner;
        }
    }
}