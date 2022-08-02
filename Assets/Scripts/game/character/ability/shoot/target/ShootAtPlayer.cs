
using game.character.characters.player;
using UnityEngine;

namespace game.character.ability.shoot.target
{
    class ShootAtPlayer : IShootTarget
    {
        private ICharacter _character;
     
        private PlayerStore _playerStore;

        public ShootAtPlayer(ICharacter character, PlayerStore playerStore)
        {
            _character = character;
            _playerStore = playerStore;
        }

        public Vector2 GetTarget()
        {
            Vector2 direction = _playerStore.GetActivePlayer().GetPosition() - _character.GetPosition();
            direction.Normalize();

            return direction;
        }
    }
}
