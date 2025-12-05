using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public sealed class InteractionController : MonoBehaviour
    {
        [Header("UI")] 
        [SerializeField] private Button _playButton; // Начать шоу
        [SerializeField] private Button _finishButton; // Закончить шоу
        [SerializeField] private LevelController _levelController;
        
        [SerializeField] private BoardController _boardController;

        private BoardReactionsManager _manager;

        [Inject] private ILevelFlow _levelFlow;
        [Inject] private IScoreEvents _scoreEvents;
        [Inject] private IPresentationService _presentationService;
        [Inject] private IBoardAnimator _boardAnimator;
        [Inject] private ISoundService _soundService;
        
        private int _visualTotal;

        private void Start()
        {
            if (_playButton)
            {
                _playButton.onClick.AddListener(OnPlayClicked);
                _playButton.onClick.AddListener(LockAllCards);
            }

            if (_finishButton)
                _finishButton.onClick.AddListener(OnFinishClicked);

            SetUIStateIdle();
        }
        
        private void OnDestroy()
        {
            if (_playButton)
            {
                _playButton.onClick.RemoveListener(OnPlayClicked);
                _playButton.onClick.RemoveListener(LockAllCards);
            }

            if (_finishButton)
                _finishButton.onClick.RemoveListener(OnFinishClicked);
        }
        
        private void OnPlayClicked()
        {
            _soundService.Play(SoundType.ButtonClick);

            if (_boardAnimator.IsPlaying)
                return;

            SetUIStatePlaying();

            var result = _presentationService.RunPresentation();
            _visualTotal = result.TotalScore;

            if (result.Events.Count == 0)
            {
                SetUIStateWaitingFinish();
                return;
            }

            _boardAnimator.Play(result.Events, OnPresentationAnimationsCompleted);
        }
        
        private void OnPresentationAnimationsCompleted()
        {
            SetUIStateWaitingFinish();
        }

        private void OnFinishClicked()
        {
            _soundService.Play(SoundType.ButtonClick);

            _boardAnimator.Stop();

            _scoreEvents.RaiseLevelFinished(_visualTotal);
            _levelFlow.CompleteLevel(_visualTotal);

            SetUIStateIdle();
        }
        
        #region UI states

        private void SetUIStateIdle()
        {
            if (_playButton)
            {
                _playButton.gameObject.SetActive(true);
                _playButton.interactable = true;
            }

            if (_finishButton)
            {
                _finishButton.gameObject.SetActive(false);
                _finishButton.interactable = false;
            }
        }

        private void SetUIStatePlaying()
        {
            if (_playButton)
            {
                _playButton.interactable = false;
                _playButton.gameObject.SetActive(false);
            }

            if (_finishButton)
            {
                _finishButton.gameObject.SetActive(false);
                _finishButton.interactable = false;
            }
        }

        private void SetUIStateWaitingFinish()
        {
            if (_finishButton)
            {
                _finishButton.gameObject.SetActive(true);
                _finishButton.interactable = true;
            }
        }

        #endregion
        
        private void LockAllCards()
        {
            var board = _boardController.Board;
            var count = board.SlotsCount;

            for (int i = 0; i < count; i++)
            {
                var cardModel = board.GetCard(i);
                if (cardModel == null)
                    continue;

                var view = _boardController.GetView(i);
                if (view == null)
                    continue;

                var go = view.gameObject.GetComponent<CardDragHandler>();

                if (go != null)
                {
                    go.ChangeDragging(false);
                }
            }
        }
    }
}
