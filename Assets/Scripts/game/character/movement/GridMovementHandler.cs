
using game.character.player;
using System;
using UnityEngine;

namespace game.character.movement
{
    public class GridMovementHandler
    {

        private CharacterEvents _characterEvents;

        public GridMovementHandler(CharacterEvents characterEvents)
        {
            _characterEvents = characterEvents;

            _characterEvents.OnGridChange += HandleGridChange;
        }

        private void HandleGridChange(object sender, OnGridChangeEventArgs e)
        {
            if (e.from != null) {
                e.from.character = null;
            }
            if (e.to.character != null)
            {
                Debug.Log("Already occupied");
            }
            e.to.character = e.character;
        }
    }
}
