using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public sealed class SoundPlayer : MonoBehaviour
    {
        [Header("Config & Sources")]
        [SerializeField] private SoundConfig _config;
        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _sfx;

        private readonly Dictionary<SoundType, AudioClip> _map = new();

        private Coroutine _fadeCoroutine;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _map.Clear();
            if (_config?.Config != null)
                foreach (var s in _config.Config)
                    if (s.AudioClip && !_map.ContainsKey(s.SoundType))
                        _map.Add(s.SoundType, s.AudioClip);
        }

        public void SetMasterMute(bool mute)
        {
            _music.mute = mute;
            _sfx.mute = mute;
        }

        public void PlaySfx(SoundType type, float volume = 0.3f, float pitch = 1f)
        {
            if (!_map.TryGetValue(type, out var clip) || clip == null) return;
            _sfx.pitch = pitch;
            _sfx.PlayOneShot(clip, Mathf.Clamp01(volume));
        }

        public void PlayMusic(SoundType type, float volume = 0.2f)
        {
            if (!_map.TryGetValue(type, out var clip) || clip == null) return;
            if (_music == null) return;

            if (_music.clip == clip && _music.isPlaying)
                return;

            _music.clip = clip;
            _music.volume = Mathf.Clamp01(volume);
            _music.loop = true;
            _music.Play();
        }

        public void StopMusic()
        {
            if (_music == null) return;
            _music.Stop();
            _music.clip = null;
        }
    }
}
