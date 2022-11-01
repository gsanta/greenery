using Base.Input;
using game.character.characters.player;
using game.scene.level;
using UnityEngine;

namespace game.character.player
{
    public class PlayerCommander : InputListener
    {
        private PlayerStore _playerStore;

        private LevelStore _levelStore;

        public PlayerCommander(PlayerStore playerStore, LevelStore levelStore)
        {
            _playerStore = playerStore;
            _levelStore = levelStore;
        }

        public override void OnKeyPressed(InputInfo inputInfo)
        {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var player = _playerStore.GetCurrentPlayer();

            var level = _levelStore.ActiveLevel;

            var playerGridPos = level.Grid.GetGridPosition(player.GetPosition());
            Vector2Int targetGridPos = playerGridPos.Value;

            if (inputInfo.IsAPressed)
            {
                targetGridPos.x -= 1;
            }

            if (inputInfo.IsWPressed)
            {
                targetGridPos.y += 1;
            }

            if (inputInfo.IsDPressed)
            {
                targetGridPos.x += 1;
            }

            if (inputInfo.IsSPressed)
            {
                targetGridPos.y -= 1;
            }

            var targetWorldPos = level.Grid.GetWorldPosition(targetGridPos.x, targetGridPos.y);

            //player.Mover.MoveTo(targetWorldPos);
        }
    }
}
