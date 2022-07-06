using GameLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Game");
        }
    }
}