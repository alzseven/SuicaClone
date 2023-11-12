using System;
using Data;
using Data.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class UINextBall : MonoBehaviour
    {
        [SerializeField] private ObservableIntValue nextBallIndex;
        [SerializeField] private EveryBallData everyBallData;
        private Image _nextBallImage;
        
        private void Awake() => _nextBallImage = GetComponent<Image>();

        private void OnEnable() => nextBallIndex.OnValueChanged += OnNextBallIndexChanged;

        private void OnDisable() => nextBallIndex.OnValueChanged -= OnNextBallIndexChanged;

        private void OnNextBallIndexChanged(int index)
        {
            if (everyBallData.TryGetBallData(index, out var ball))
            {
                _nextBallImage.sprite = ball.BallSprite;
                gameObject.transform.localScale = ball.BallSize;
            }
            else
            {
                gameObject.transform.localScale = Vector3.zero;
            }
        }
    }
}