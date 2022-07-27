using game.character.characters.player;
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
            var pathMovement = parent.GetComponent<PathMovement>();
            return new RoamingState(character, pathMovement, _playerStore);
        } 
        
        public ChasingState CreateChasingState(ICharacter character, GameObject parent)
        {
            var pathMovement = parent.GetComponent<PathMovement>();
            var chasingState = parent.AddComponent<ChasingState>();
            chasingState.Construct(character, pathMovement, _playerStore);
            return chasingState;
        } 
    }
}