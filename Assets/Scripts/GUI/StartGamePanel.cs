using GameLogic;
using UnityEngine;

namespace GUI
{
    public class StartGamePanel : MonoBehaviour
    {
        private GameManager _gameManager;
        
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void StartGame()
        {
            _gameManager.StartGame();
        }
    }
}