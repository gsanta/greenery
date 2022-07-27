using System.Collections.Generic;

namespace gui.avatar
{
    public class AvatarStore
    {
        private List<gui.avatar.Avatar> avatars = new();

        public void AddAvatar(gui.avatar.Avatar avatar)
        {
            avatars.Add(avatar);
        }
    }
}