using UnityEngine;

namespace game.scene.area
{
    public class EnterArea : MonoBehaviour
    {
        private GameManager _gameManager;
        
        private bool _isEntryPoint;
    
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void SetEntryPoint(bool isEntryPoint)
        {
            _isEntryPoint = isEntryPoint;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isEntryPoint)
            {
                _gameManager.EndGame();
            }
        }
    }
}