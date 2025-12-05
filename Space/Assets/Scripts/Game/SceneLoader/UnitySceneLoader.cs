using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceGame
{
    public class UnitySceneLoader: ISceneLoader
    {
       // [Inject] private SoundPlayer _soundPlayer;
        [Inject] private ISoundService _soundService;
        public void LoadMainMenu()
        {
            //_soundService.StopMusic();
            _soundService.StopLoop();
            SceneManager.LoadScene("MainMenu");
        }
    }
}