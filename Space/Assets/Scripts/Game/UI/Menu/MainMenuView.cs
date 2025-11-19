using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public sealed class MainMenuView : MonoBehaviour
    {
        [SerializeField] private StartGameButtonView _startGameButtonView;
        [SerializeField] private Button _startTutorialButton;
        [SerializeField] private Button _exitGameButton;

        private void Start()
        {
            _startGameButtonView.gameObject.SetActive(true);
        }
    }
}