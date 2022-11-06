using Assets.Scripts.game.scene;
using Cinemachine;
using game.character;
using game.scene.level;
using UnityEngine;

namespace game.scene
{
    public class FollowCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _vcam;

        private LevelStore _levelStore;

        [SerializeField] public CameraConfiner confiner;
 
        public void Constuct(LevelStore levelStore)
        {
            _levelStore = levelStore;
        }

        public void Init()
        {
            GetConfiner().SetDimensions(_levelStore.ActiveLevel);
        }

        void Awake()
        {
            confiner.Construct(this);
            _vcam = GetComponent<CinemachineVirtualCamera>();
        }

        public void SetTarget(ICharacter character)
        {
            _vcam.Follow = character.transform;
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