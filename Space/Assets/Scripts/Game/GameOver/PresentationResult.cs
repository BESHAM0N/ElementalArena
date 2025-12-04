using System.Collections.Generic;

namespace SpaceGame
{
    public readonly struct PresentationResult
    {
        public int TotalScore { get; }
        public IReadOnlyList<CardAnimEvent> Events { get; }

        public PresentationResult(int totalScore, IReadOnlyList<CardAnimEvent> events)
        {
            TotalScore = totalScore;
            Events = events;
        }
    }
}