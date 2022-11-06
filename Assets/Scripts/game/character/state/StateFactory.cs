using game.character.characters.enemy;
using game.character.characters.player;
using game.character.movement;
using game.character.state.chase;
using game.character.state.roam;
using UnityEngine;

namespace game.character.state
{
    public class StateFactory : MonoBehaviour
    {
        private PlayerStore _playerStore;

        private TargetPathFinder _targetPathFinder;

        public void Construct(PlayerStore playerStore, TargetPathFinder targetPathFinder)
        {
            _playerStore = playerStore;
            _targetPathFinder = targetPathFinder;
        }

        public RoamingState CreateRoamingState(ICharacter character, GameObject parent)
        {
            var pathMovementMethod = parent.GetComponent<TargetPathFinder>();
            return new RoamingState(character, pathMovementMethod, _playerStore);
        } 
        
        public ChasingState CreateChasingState(Enemy enemy, GameObject parent)
        {
            var chasingState = enemy.gameObject.AddComponent<ChasingState>();
            chasingState.Construct(enemy, _targetPathFinder, _playerStore);
            return chasingState;
        } 
    }
}