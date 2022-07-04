using System.Collections.Generic;

namespace Characters.Avatar
{
    public class AvatarStore
    {
        private List<Avatar> avatars = new();

        public void AddAvatar(Avatar avatar)
        {
            avatars.Add(avatar);
        }
    }
}