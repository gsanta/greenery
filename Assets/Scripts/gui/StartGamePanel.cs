using UnityEngine;
using UnityEngine.SceneManagement;

namespace GUI
{
    public class StartGamePanel : MonoBehaviour
    {
        public void StartGame(int pass)
        {
            SceneManager.LoadScene("Game");
        }
    }
}