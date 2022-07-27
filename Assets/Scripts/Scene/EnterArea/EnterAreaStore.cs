using System;
using GameLogic;
using UnityEngine;

namespace Items.EnterArea
{
    public class EnterAreaStore : MonoBehaviour
    {
        [SerializeField] private EnterArea[] enterAreaList;

        private GameManager _gameManager;

        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Start()
        {
            foreach (Transform child in transform)
            {
                var enterArea = child.GetComponent<EnterArea>();
                enterArea.Construct(_gameManager);
            }
        }

        public EnterArea ChooseEnterArea()
        {
            enterAreaList[0].SetEntryPoint(true);
            return enterAreaList[0];
        }
    }
}