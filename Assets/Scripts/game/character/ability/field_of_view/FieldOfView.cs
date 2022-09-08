
using game.character.characters.player;
using UnityEngine;

namespace game.character.ability.field_of_view
{
    public class FieldOfView
    {
        public readonly float Fov = 90f;

        public readonly float ViewDistance = 50f;

        public FieldOfViewVisualizer Visualizer { get; set; }
        
        private PlayerStore _playerStore;

        private ICharacter _character;

        public FieldOfView(ICharacter character, PlayerStore playerStore)
        {
            _character = character;
            _playerStore = playerStore;
        }

        public ICharacter FindTarget()
        {
            var player = _playerStore.GetActivePlayer();
            if (Vector2.Distance(_character.GetPosition(), player.GetPosition()) < ViewDistance)
            {
                Vector2 targetDirection = (player.GetPosition() - _character.GetPosition()).normalized;
                Direction dir = _character.Movement.GetDirection();
                Vector2 aimDirection = DirectionHelper.DirToVector(dir);
                
                if (Vector2.Angle(aimDirection, targetDirection) < Fov / 2f)
                {
                    return player;
                }
            }

            return null;
        }
    }
}
