using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameInfo
{
    public class GameInfoStore : MonoBehaviour
    {
        [SerializeField] private Image ballInfoPrefab;
        private List<BallInfo> _balls = new();
        public BallInfo currentBall;

        [SerializeField] private TMP_Text remainingLengthText;

        public List<BallInfo> GetBalls()
        {
            return _balls;
        }

        public void RemoveBall(BallInfo ball)
        {
            _balls.Remove(ball);
            if (currentBall == ball)
            {
                currentBall = null;
            }
        }

        public void UpdateCurrentBallLength(float remainingLength)
        {
            if (currentBall)
            {
                currentBall._remainingLength = remainingLength;
                remainingLengthText.text = Mathf.Round(remainingLength).ToString();
            }
        }

        public void CreateBall()
        {
            var ball = Instantiate(ballInfoPrefab, transform, true);
            _balls.Add(ball.GetComponent<BallInfo>());
            if (!currentBall)
            {
                currentBall = ball.GetComponent<BallInfo>();
            }
        }
    }
}