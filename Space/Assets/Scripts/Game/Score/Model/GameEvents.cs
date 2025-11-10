using System;

namespace SpaceGame
{
    public sealed class GameEvents : IDisposable
    {
        public event Action<int> OnScoreChanged;

        public void Dispose()
        {
            OnScoreChanged = null;
        }

        public void RaiseScoreChanged(int value) => OnScoreChanged?.Invoke(value);
    }
}