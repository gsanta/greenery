using UnityEngine;

namespace gui.avatar
{
    public class AvatarFactory : MonoBehaviour
    {
        [SerializeField] private gui.avatar.Avatar avatarPrefab;

        [SerializeField] private Transform avatarContainer;

        private AvatarStore _avatarStore;
        
        public void Construct(AvatarStore avatarStore)
        {
            _avatarStore = avatarStore;
        }
        
        public void Create()
        {
            var avatar = Instantiate(avatarPrefab, avatarContainer);
            _avatarStore.AddAvatar(avatar);    
        }
    }
}