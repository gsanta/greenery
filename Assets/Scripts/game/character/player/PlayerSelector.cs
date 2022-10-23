﻿
using Base.Input;
using game.character.characters.player;

namespace game.character.player
{
    public class PlayerSelector : InputListener
    {
        private PlayerStore _playerStore;

        public PlayerSelector(PlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        public override void OnKeyPressed(InputInfo inputInfo)
        {
            if (inputInfo.IsTabPressed)
            {
                var nextPlayer = _playerStore.GetNextPlayer(_playerStore.GetActivePlayer());
                _playerStore.SetActivePlayer(nextPlayer);
            }
        }
    }
}
