using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace game
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

        public void UpdateCurrentBallLength(float length)
        {
            if (currentBall)
            {
                currentBall.remainingLength = Mathf.Max(currentBall.length - length, 0f);
                remainingLengthText.text = Mathf.Round(currentBall.remainingLength).ToString();
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