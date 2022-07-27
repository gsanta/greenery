using game.item;
using UnityEngine;

namespace game.character.characters.player
{
    public class ItemPickup : MonoBehaviour
    {
        private ItemStore<Ball> _ballStore;
        private GameInfoStore _gameInfoStore;
        
        public void Construct(ItemStore<Ball> ballStore, GameInfoStore gameInfoStore)
        {
            _ballStore = ballStore;
            _gameInfoStore = gameInfoStore;
        }
        
        private void Update()
        {   
            if (Input.GetKeyDown("e"))
            {
                var closest = _ballStore.GetClosest(transform.position, 1f);
                if (closest)
                {
                    _ballStore.RemoveItem(closest);
                    Destroy(closest.gameObject);
                    
                    _gameInfoStore.CreateBall();
                }
            }
        }
    }
}