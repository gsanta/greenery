using Character.player;
using Cinemachine;
using UnityEngine;

namespace Scene
{
    public class FollowCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _vcam;
 
        void Start()
        {
            _vcam = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetTarget(Player player)
        {
            _vcam.Follow = player.transform;
        }
    }
}