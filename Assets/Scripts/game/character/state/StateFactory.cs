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
            var pathMovementMethod = parent.GetComponent<PathMovementMethod>();
            return new RoamingState(character, pathMovementMethod, _playerStore);
        } 
        
        public ChasingState CreateChasingState(Enemy enemy, GameObject parent)
        {
            var pathMovementMethod = parent.GetComponent<PathMovementMethod>();
            var chasingState = parent.AddComponent<ChasingState>();
            chasingState.Construct(enemy, pathMovementMethod, _playerStore);
            return chasingState;
        } 
    }
}