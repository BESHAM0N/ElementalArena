using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class SoundServiceInstaller : MonoInstaller
    {
        [SerializeField] private SoundPlayer _soundPlayerPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<SoundPlayer>()
                .FromComponentInNewPrefab(_soundPlayerPrefab)
                .AsSingle()
                .NonLazy();
            Container.Bind<ISoundService>().To<SoundService>().AsSingle().NonLazy();
        }
    }
}