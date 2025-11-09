using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public class LevelController : MonoBehaviour
    {
        [Inject] private readonly ISoundService _sound;
        [Inject] private readonly ISceneLoader _sceneLoader;
        
        [SerializeField] private TMP_Text _levelNamedText;
        [SerializeField] private Button _mainMenuButton;
        
        private readonly List<(string Name, SoundType Music)> _allThemes = new()
        {
            ("Aqua Mirage",   SoundType.LevelOneBackgroundMusic), // вода
            ("Blaze Spectacular",    SoundType.LevelTwoBackgroundMusic), // огонь
            ("Celestial Aerial ",    SoundType.LevelThreeBackgroundMusic), //воздух
            ("Voltage Velocity", SoundType.LevelFourBackgroundMusic), // электричество
            ("Carnival of Wonders",     SoundType.LevelFiveBackgroundMusic), // пропсы
            ("Wild Kingdom Revue",  SoundType.LevelSixBackgroundMusic) //животные
        };
        
        private static List<(string Name, SoundType Music)> _remainingThemes;

        private void Awake()
        {
            if (_mainMenuButton) 
                _mainMenuButton.onClick.AddListener(() =>
                {
                    _sound.Play(SoundType.ButtonClick);
                    _sceneLoader.LoadMainMenu();
                });
            
            if (_remainingThemes == null || _remainingThemes.Count == 0)
                _remainingThemes = new List<(string, SoundType)>(_allThemes);
        }

        /// <summary>
        /// Назначает случайную тему уровня и удаляет её из списка, чтобы не повторялась.
        /// </summary>
        public void SetRandomLevelTheme()
        {
            if (_remainingThemes.Count == 0)
            {
                // всё использовано — сбрасываем и начинаем заново
                _remainingThemes = new List<(string, SoundType)>(_allThemes);
            }
            
            int randomIndex = Random.Range(0, _remainingThemes.Count);
            var theme = _remainingThemes[randomIndex];
           
            _levelNamedText.text = theme.Name;
            _sound.StopLoop();
            _sound.PlayLoop(theme.Music);
          
            _remainingThemes.RemoveAt(randomIndex);
        }
    }
}