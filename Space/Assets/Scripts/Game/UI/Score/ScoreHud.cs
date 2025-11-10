using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class ScoreHud : MonoBehaviour, IInitializable, System.IDisposable
    {
        [Inject] private GameEvents _gameEvents;
        [SerializeField] private TMP_Text _text;
        private int _shown;

        private void OnScoreChanged(int total)
        {
            int from = _shown;
            DOTween.Kill(this);
            DOTween.To(() => from, v => {
                    from = v;
                    if (_text) _text.text = v.ToString();
                }, total, 2f)
                .SetId(this);
            _shown = total;
        }

        public void Initialize()
        {
            _gameEvents.OnScoreChanged += OnScoreChanged;
            if (_text)
                _text.text = "0";
        }

        public void Dispose()
        {
            if (_gameEvents != null)
                _gameEvents.OnScoreChanged -= OnScoreChanged;
        }

        public void Reset()
        {
            if (_text)
                _text.text = "0";
        }
    }
}