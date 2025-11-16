using System.Collections.Generic;
using UnityEngine;
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
        
        private GameObject _currentTutor;

        private void OnEnable()
        {
            _nextButton.onClick.AddListener(OnNextTutor);
            _backButton.onClick.AddListener(OnBackTutor);
            _startGameButton.onClick.AddListener(OnStartGame);
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _startGameButton.onClick.RemoveAllListeners();
        }

        private void OnNextTutor()
        {
            
        }

        private void OnBackTutor()
        {
            
        }

        private void OnStartGame()
        {
            
        }
    }
}