

using game.character.characters.player;
using UnityEngine;

namespace game.character.ability.field_of_view
{
    public class FieldOfView
    {
        public readonly float Fov = 90f;

        public readonly float ViewDistance = 50f;

        private PlayerStore _playerStore;

        private ICharacter _character;

        private string _targetLayerMask;

        private static readonly string WallLayerMask = "Wall";

        public FieldOfView(ICharacter character, PlayerStore playerStore, string targetLayerMask)
        {
            _character = character;
            _playerStore = playerStore;
            _targetLayerMask = targetLayerMask;
        }

        public ICharacter FindTarget()
        {
            var player = _playerStore.GetActivePlayer();

            return IsTargetWithinFieldOfView(player) && IsTargetVisible(player) ? player : null;
        }

        private bool IsTargetWithinFieldOfView(ICharacter target)
        {
            if (Vector2.Distance(_character.GetPosition(), target.GetPosition()) < ViewDistance)
            {
                Vector2 targetDirection = (target.GetPosition() - _character.GetPosition()).normalized;
                Direction dir = _character.Movement.GetLookDirection();
                Vector2 aimDirection = DirectionHelper.DirToVector(dir);

                return Vector2.Angle(aimDirection, targetDirection) < Fov / 2f;
            }

            return false;
        }

        private bool IsTargetVisible(ICharacter target)
        {
            Vector2 targetDirection = (target.GetPosition() - _character.GetPosition()).normalized;
            var mask = LayerMask.GetMask(WallLayerMask, _targetLayerMask);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(_character.GetPosition(), targetDirection, ViewDistance, mask);

            if (raycastHit2D.collider != null)
            {
                return raycastHit2D.collider.gameObject == target.GetGameObject();
            }

            return false;
        }
    }
}
