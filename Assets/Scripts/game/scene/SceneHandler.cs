using UnityEngine;
using UnityEngine.SceneManagement;

namespace game.scene
{
    public class SceneHandler : MonoBehaviour
    {
        public void LoadScene()
        {
            SceneManager.LoadSceneAsync("Level2", LoadSceneMode.Additive);
        }
    }
}