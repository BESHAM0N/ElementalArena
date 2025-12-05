using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceGame
{
    public class UnitySceneLoader: ISceneLoader
    {
        [Inject] private ISoundService _soundService;
        public void LoadMainMenu()
        {
            _soundService.StopLoop();
            SceneManager.LoadScene("MainMenu");
        }
    }
}