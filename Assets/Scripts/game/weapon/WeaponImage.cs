
using UnityEngine.UI;

namespace game.weapon
{
    public class WeaponImage
    {
        public WeaponType Type { get; set; }

        public Image Image { get; set; }

        public WeaponImage(WeaponType type, Image image)
        {
            Type = type;
            Image = image;
        }
    }
}
