using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI.Tutor
{
    public sealed class TutorView : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _startGameButton;
        
        [SerializeField] private List<GameObject> _tutors;
        [SerializeField] private GameObject _tutorEndPanel;
        
        private int _currentIndex;

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(OnNextTutor);
            _backButton.onClick.AddListener(OnBackTutor);
            _startGameButton.onClick.AddListener(OnStartGame);
            ShowTutor(_currentIndex);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _startGameButton.onClick.RemoveAllListeners();
        }
        
        private void ShowTutor(int index)
        {
            foreach (var t in _tutors)
                t.SetActive(false);

            _tutorEndPanel.SetActive(false);
           
            if (index >= 0 && index < _tutors.Count)
            {
                _tutors[index].SetActive(true);
            }
            else
            {
                _tutorEndPanel.SetActive(true);
            }
        
            _backButton.gameObject.SetActive(index > 0);
            _nextButton.gameObject.SetActive(index < _tutors.Count - 1);
            _startGameButton.gameObject.SetActive(index == _tutors.Count - 1);
        }

        private void OnNextTutor()
        {
            if (_currentIndex < _tutors.Count)
            {
                _currentIndex++;
                ShowTutor(_currentIndex);
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
            SceneManager.LoadScene("LevelGame");
        }
    }
}