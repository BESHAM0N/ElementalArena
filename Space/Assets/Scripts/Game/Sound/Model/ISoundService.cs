namespace SpaceGame
{
    public interface ISoundService
    {
        bool IsSoundEnabled { get; }
        void SetEnabled(bool enabled);
        void ToggleSound();
        
        void Play(SoundType type, float volume = 0.2f, float pitch = 1f);
        void PlayLoop(SoundType type);
        void StopLoop();
    }
}