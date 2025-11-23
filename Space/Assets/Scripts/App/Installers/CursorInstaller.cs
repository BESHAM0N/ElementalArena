using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public class CursorInstaller : MonoInstaller
    {
        [SerializeField] private Texture2D _cursorTexture;
        [SerializeField] private Vector2 _hotspot;

        public override void InstallBindings()
        {
            Container
                .Bind<ICursorService>()
                .To<CursorService>()
                .AsSingle()
                .WithArguments(_cursorTexture, _hotspot)
                .NonLazy();
        }
    }
}