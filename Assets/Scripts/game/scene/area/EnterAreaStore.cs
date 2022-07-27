using UnityEngine;

namespace game.scene.area
{
    public class EnterAreaStore : MonoBehaviour
    {
        [SerializeField] private game.scene.area.EnterArea[] enterAreaList;

        private GameManager _gameManager;

        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            foreach (Transform child in transform)
            {
                var enterArea = child.GetComponent<game.scene.area.EnterArea>();
                enterArea.Construct(_gameManager);
            }
        }

        public game.scene.area.EnterArea ChooseEnterArea()
        {
            enterAreaList[0].SetEntryPoint(true);
            return enterAreaList[0];
        }
    }
}