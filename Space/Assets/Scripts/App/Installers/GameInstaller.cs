using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private CardView _cardViewPrefab;
        [SerializeField] private Transform _handParent;
        [SerializeField] private ListCardPrototypes _prototypes;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private LevelCompletePopupView _levelCompletePopupView;
        [SerializeField] private GameCompletePopupView _gameCompletePopupView;
        [SerializeField] private BoardController _boardController;

        public override void InstallBindings()
        {
            Container.Bind<BoardController>().FromInstance(_boardController).AsSingle();
            Container.Bind<IBoardAnimator>().To<BoardAnimator>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEvents>().AsSingle().NonLazy();
            LevelCircleInstaller.Install(Container, _levelController,_levelCompletePopupView,_gameCompletePopupView);
            FactoryInstaller.Install(Container, _cardViewPrefab, _handParent, _prototypes);
            Container.BindInterfacesTo<ScoreHud>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IPresentationService>().To<PresentationService>().AsSingle();
        }
    }
}