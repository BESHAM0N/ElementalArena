using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceGame
{
    [RequireComponent(typeof(Button))]
    public sealed class StartGameButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Inject] private ISoundService _sound;
        [Inject] private IScore  _score;
        [SerializeField] private Button _button;
        [SerializeField] private float _hoverScale  = 1.1f;
        [SerializeField] private float _hoverDuration  = 0.2f;
        
        private Vector3 _originalScale;
        private Tween _scaleTween;

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        private void Awake()
        {
            if (_button == null) 
                _button = GetComponent<Button>();
            
            _originalScale = _button.transform.localScale;
            _button.onClick.AddListener(OnClick);
        }
        
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
            _scaleTween?.Kill();
        }

        private void OnClick()
        {
            _sound.Play(SoundType.ButtonClick);
            _score.Reset();
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _scaleTween = _button.transform
                .DOScale(_originalScale * _hoverScale, _hoverDuration)
                .SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _scaleTween = _button.transform
                .DOScale(_originalScale, _hoverDuration)
                .SetEase(Ease.InOutSine);
        }
    }
}