using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SpaceGame
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _startTutorialButton;
        [SerializeField] private Button _exitGameButton;

        private void OnEnable()
        {
            _startTutorialButton.onClick.AddListener(OnStartTutorButtonClicked);
            _exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
        }

        private void OnDisable()
        {
            _startTutorialButton.onClick.RemoveListener(OnStartTutorButtonClicked);
            _exitGameButton.onClick.RemoveListener(OnExitGameButtonClicked);
        }
        
        private void OnStartTutorButtonClicked()
        {
            SceneManager.LoadScene("TutorScene");
        }

        private void OnExitGameButtonClicked()
        {
            gameObject.SetActive(false);
        }
    }
}