using AI.grid.path;
using AI.state.character.states;
using Character;
using Character.player;
using UnityEngine;

namespace AI.state.character
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