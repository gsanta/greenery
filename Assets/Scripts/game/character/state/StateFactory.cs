using game.character.characters.enemy;
using game.character.characters.player;
using game.character.movement;
using game.character.state.chase;
using game.character.state.roam;
using game.scene.grid.path;
using UnityEngine;

namespace game.character.state
{
    public class StateFactory : MonoBehaviour
    {
        private PlayerStore _playerStore;

        public void Construct(PlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        public RoamingState CreateRoamingState(ICharacter character, GameObject parent)
        {
            var pathMovementMethod = parent.GetComponent<TargetPathFinder>();
            return new RoamingState(character, pathMovementMethod, _playerStore);
        } 
        
        public ChasingState CreateChasingState(Enemy enemy, GameObject parent, TargetPathFinder pathMover)
        {
            var chasingState = enemy.gameObject.AddComponent<ChasingState>();
            chasingState.Construct(enemy, pathMover, _playerStore);
            return chasingState;
        } 
    }
}