using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public sealed class SoundToggleButton : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private GameObject _onIcon;
        [SerializeField] private GameObject _offIcon;

        [Inject] private ISoundService _sound;

        private void Awake()
        {
            _btn.onClick.AddListener(() =>
            {
                _sound.Play(SoundType.ButtonClick);
                _sound.ToggleSound();
                Refresh();
            });
            Refresh();
        }

        private void Refresh()
        {
            if (_onIcon)  _onIcon.SetActive(_sound.IsSoundEnabled);
            if (_offIcon) _offIcon.SetActive(!_sound.IsSoundEnabled);
        }
    }
}
