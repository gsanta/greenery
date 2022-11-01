
using game.character.characters.enemy;
using game.character.player;
using System;

namespace game.character.movement
{
    public class MovementManager
    {
        private EnemyStore _enemyStore;

        public MovementManager(PlayerEvents playerEvents, EnemyStore enemyStore)
        {
            _enemyStore = enemyStore;

            playerEvents.OnTargetEnd += HandleTargetEnd;

            playerEvents.OnTargetStart += HandleTargetStart;
        }

        private void HandleTargetEnd(object sender, EventArgs args)
        {
            _enemyStore.GetAll().ForEach((enemy) => enemy.Movement.IsPaused = true);
        }
        
        private void HandleTargetStart(object sender, EventArgs args)
        {
            _enemyStore.GetAll().ForEach((enemy) => enemy.Movement.IsPaused = false);
        }
    }
}
