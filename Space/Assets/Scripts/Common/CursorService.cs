using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class CursorService : ICursorService, IInitializable
    {
        private readonly Texture2D _cursorTexture;
        private readonly Vector2 _hotspot;

        public CursorService(Texture2D cursorTexture, Vector2 hotspot)
        {
            _cursorTexture = cursorTexture;
            _hotspot = hotspot;
        }

        public void ApplyCursor()
        {
            Cursor.SetCursor(_cursorTexture, _hotspot, CursorMode.Auto);
        }

        public void Initialize()
        {
            ApplyCursor();
        }
    }
}