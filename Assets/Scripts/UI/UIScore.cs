using System;
using Data;
using Data.Variables;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class UIScore : MonoBehaviour
    {
        [SerializeField] private ObservableIntValue score;
        private TMP_Text _scoreText;
        
        private void Awake() => _scoreText = GetComponent<TMP_Text>();

        private void OnEnable() => score.OnValueChanged += OnScoreChanged;

        private void OnDisable() => score.OnValueChanged -= OnScoreChanged;

        private void OnScoreChanged(int scoreVal) => _scoreText.text = scoreVal.ToString();
    }
}