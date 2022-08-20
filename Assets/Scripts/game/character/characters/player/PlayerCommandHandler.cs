using UnityEngine;

namespace game.character.characters.player
{
    public class PlayerCommandHandler
    {
        private Player _player;

        public PlayerCommandHandler(Player player)
        {
            _player = player;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _player.Weapon.OnFire(pos);
            }
        }
    }
}