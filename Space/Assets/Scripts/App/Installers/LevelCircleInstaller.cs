using Zenject;

namespace SpaceGame
{
    public class LevelCircleInstaller : Installer<LevelController, LevelCompletePopupView, GameCompletePopupView, LevelCircleInstaller>
    {
        [Inject] private LevelController _levelController;
        [Inject] private LevelCompletePopupView _levelCompletePopupView;
        [Inject] private GameCompletePopupView _gameCompletePopupView;
        
        public override void InstallBindings()
        {
            Container.Bind<ILevelEndUI>().To<LevelCompletePopupView>().FromInstance(_levelCompletePopupView).AsSingle();
            Container.Bind<IGameEndUI>().To<GameCompletePopupView>().FromInstance(_gameCompletePopupView).AsSingle();
            Container.BindInterfacesAndSelfTo<HandService>().AsSingle();
            Container.Bind<ISceneLoader>().To<UnitySceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelFlowController>().AsSingle();
            Container.Bind<LevelController>().FromInstance(_levelController).AsSingle();
        }
    }
}