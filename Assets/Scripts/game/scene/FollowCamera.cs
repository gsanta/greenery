using Cinemachine;
using game.character.characters.player;
using UnityEngine;

namespace game.scene
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