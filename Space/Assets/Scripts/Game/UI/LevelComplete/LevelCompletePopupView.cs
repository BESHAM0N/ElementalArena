using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public class LevelCompletePopupView : MonoBehaviour, ILevelEndUI
    {
        [Inject] private readonly ISoundService _sound;
        [SerializeField] private GameObject _root;
        [SerializeField] private TMP_Text _levelScoreText;
        [SerializeField] private TMP_Text _totalScoreText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private ScoreHud _scoreHud;

        public event Action NextClicked;
        public event Action MenuClicked;

        private void Awake()
        {
            _root.SetActive(false);

            if (_nextButton)
                _nextButton.onClick.AddListener(() =>
                {
                    _sound.Play(SoundType.ButtonClick);
                    NextClicked?.Invoke();
                });

            if (_menuButton)
                _menuButton.onClick.AddListener(() =>
                {
                    _sound.Play(SoundType.ButtonClick);
                    MenuClicked?.Invoke();
                });
        }

        public void Show(int score)
        {
            if (_levelScoreText)
                _levelScoreText.text = $"Your points for this scene: {score}";

            _root.SetActive(true);
            _scoreHud.Reset();
        }

        public void Show(int levelScore, int? totalScore)
        {
            if (_levelScoreText)
                _levelScoreText.text = $"Your points for this scene: {levelScore}";

            if (_totalScoreText && totalScore.HasValue)
                _totalScoreText.text = $"All your points: {totalScore.Value}";

            _root.SetActive(true);
            _scoreHud.Reset();
        }

        public void Hide()
        {
            _root.SetActive(false);
        }
    }
}