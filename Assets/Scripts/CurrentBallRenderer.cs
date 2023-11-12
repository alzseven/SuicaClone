using System;
using Data;
using Data.Variables;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CurrentBallRenderer : MonoBehaviour
{
    [SerializeField] private ObservableIntValue currentBallIndex;
    [SerializeField] private EveryBallData everyBallData;
    private SpriteRenderer _currentBallRenderer;

    private void Awake() => _currentBallRenderer = GetComponent<SpriteRenderer>();

    private void OnEnable() => currentBallIndex.OnValueChanged += UpdateCurrentBallSprite;
    
    private void OnDisable() => currentBallIndex.OnValueChanged -= UpdateCurrentBallSprite;
    
    private void UpdateCurrentBallSprite(int ballIndex)
    {
        transform.localScale = ballIndex > -1 ? Vector3.one : Vector3.zero;
        if (everyBallData.TryGetBallData(ballIndex, out var ball))
        {
            _currentBallRenderer.sprite = ball.BallSprite;
            _currentBallRenderer.transform.localScale = ball.BallSize;
        }
    }
}