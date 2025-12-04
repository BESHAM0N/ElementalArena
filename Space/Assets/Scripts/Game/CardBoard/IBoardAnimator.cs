using System.Collections.Generic;

namespace SpaceGame
{
    public interface IBoardAnimator
    {
        bool IsPlaying { get; }
        void Play(IReadOnlyList<CardAnimEvent> events, System.Action onComplete);
        void Stop();
    }
}