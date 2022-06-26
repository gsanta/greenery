using System;
using UnityEngine;

namespace GameInfo
{
    public class BallInfo : MonoBehaviour
    {
        public float length;
        public float remainingLength;

        private GameInfoStore _gameInfoStore;
        
        public void Construct(GameInfoStore gameInfoStore)
        {
            _gameInfoStore = gameInfoStore;
        }

        private void Awake()
        {
            length = UnityEngine.Random.Range(0f, 50f);
        }
    }
}