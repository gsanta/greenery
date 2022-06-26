using UnityEngine;

namespace GameInfo
{
    public class BallInfo : MonoBehaviour
    {
        public float _length;
        public float _remainingLength;

        private GameInfoStore _gameInfoStore;
        
        public void Construct(GameInfoStore gameInfoStore)
        {
            _gameInfoStore = gameInfoStore;
        }
    }
}