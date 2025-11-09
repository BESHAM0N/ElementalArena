using DG.Tweening;
using UnityEngine;

namespace SpaceGame
{
    public class BackgroundBreathing : MonoBehaviour
    {
        [SerializeField] private float zoomInScale = 1.1f;
        [SerializeField] private float duration = 4f;
        [SerializeField] private Ease easeType = Ease.InOutSine;

        private Tween _zoomTween;

        private void Start()
        {
            _zoomTween = transform
                .DOScale(zoomInScale, duration / 2f)
                .SetEase(easeType)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            _zoomTween?.Kill();
        }
    }
}