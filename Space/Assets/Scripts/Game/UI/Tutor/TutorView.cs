using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public sealed class TutorView : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _exitButton;
        
        [SerializeField] private List<GameObject> _tutors;
        [SerializeField] private GameObject _tutorEndPanel;
        
        [Inject] private SoundPlayer _soundPlayer;
        
        private int _currentIndex;

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(OnNextTutor);
            _exitButton.onClick.AddListener(OnExitGame);
            _nextButton.onClick.AddListener(OnButtonClick);
            _exitButton.onClick.AddListener(OnButtonClick);
            _backButton.onClick.AddListener(OnBackTutor);
            _backButton.onClick.AddListener(OnButtonClick);
            _startGameButton.onClick.AddListener(OnStartGame);
            _startGameButton.onClick.AddListener(OnButtonClick);
            ShowTutor(_currentIndex);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _startGameButton.onClick.RemoveAllListeners();
        }

        private void Awake()
        {
            _soundPlayer.PlayMusic(SoundType.LevelThreeBackgroundMusic);
        }
        
        private void ShowTutor(int index)
        {
            foreach (var t in _tutors)
                t.SetActive(false);

            _tutorEndPanel.SetActive(false);
           
            if (index >= 0 && index < _tutors.Count)
            {
                _tutors[index].SetActive(true);
                _backButton.gameObject.SetActive(index > 0);
                _nextButton.gameObject.SetActive(true);
                _startGameButton.gameObject.SetActive(false);
            }
            else
            {
                _tutorEndPanel.SetActive(true);
                _nextButton.gameObject.SetActive(false);
                _startGameButton.gameObject.SetActive(true);
            }
        }

        private void OnNextTutor()
        {
            if (_currentIndex < _tutors.Count)
            {
                _currentIndex++;
                ShowTutor(_currentIndex);
            }
            else if(_currentIndex == _tutors.Count)
            {
                _tutorEndPanel.SetActive(true);
                _nextButton.gameObject.SetActive(false);
                _startGameButton.gameObject.SetActive(true);
            }
        }

        private void OnBackTutor()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                ShowTutor(_currentIndex);
            }
        }

        private void OnStartGame()
        {
            SceneManager.LoadScene("LevelScene");
        }
        
        private void OnExitGame()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OnButtonClick()
        {
            _soundPlayer.PlaySfx(SoundType.ButtonClick);
        }
    }
}