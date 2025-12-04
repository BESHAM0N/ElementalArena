using System;
using System.Collections.Generic;
using DG.Tweening;

namespace SpaceGame
{
    public sealed class BoardAnimator : IBoardAnimator
    {
        public bool IsPlaying => _seq != null && _seq.IsActive() && _seq.IsPlaying();
        
        private readonly BoardController _boardController;

        private const float NONE_DURATION= 0.35f;
        private const float BONUS_DURATION = 1.00f;
        private const float DESTROY_DURATION = 1.00f;
        private const float ABSORPTION_DURATION = 1.25f;
        private const float GAP_BETWEEN_STEPS = 0.12f;
        private const float POINTS_ANIM_DELAY = 0.20f;
        
        private Sequence _seq;
        
        public BoardAnimator(BoardController boardController)
        {
            _boardController = boardController;
        }
        
        public void Play(IReadOnlyList<CardAnimEvent> events, Action onComplete)
        {
             Stop();

            if (events == null || events.Count == 0)
            {
                onComplete?.Invoke();
                return;
            }

            var count = _boardController.Board.SlotsCount;
            _seq = DOTween.Sequence();

            for (int k = 0; k < events.Count; k++)
            {
                var evt = events[k];
                var view = SafeGetView(evt.Index, count);
                if (view == null) continue;

                // Пара бонусов одновременно
                if (evt.Type == CardAnimType.Bonus && k + 1 < events.Count)
                {
                    var next = events[k + 1];
                    if (next.Type == CardAnimType.Bonus)
                    {
                        var view2 = SafeGetView(next.Index, count);
                        _seq.AppendCallback(() =>
                        {
                            view.OnBonusAnim();
                            if (view2 != null) view2.OnBonusAnim();
                        });
                        _seq.AppendInterval(BONUS_DURATION + GAP_BETWEEN_STEPS);
                        k++;
                        continue;
                    }
                }

                _seq.AppendCallback(() =>
                {
                    switch (evt.Type)
                    {
                        case CardAnimType.NoneLift:
                            view.OnNoneAnim();
                            break;
                        case CardAnimType.Bonus:
                            view.OnBonusAnim();
                            break;
                        case CardAnimType.Destroy:
                            view.OnDestroyAnim();
                            break;
                        case CardAnimType.Absorption:
                            view.OnAbsorptionAnim(autoHideSeconds: 1f);
                            break;
                    }
                });

                _seq.AppendInterval(GetStepDuration(evt.Type) + GAP_BETWEEN_STEPS);
            }

            _seq.AppendInterval(POINTS_ANIM_DELAY);

            _seq.SetAutoKill(true)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    _seq = null;
                    onComplete?.Invoke();
                })
                .Play();
        }

        public void Stop()
        {
            if (_seq != null && _seq.IsActive())
            {
                _seq.Kill(true);
                _seq = null;
            }
        }
        
        private CardView SafeGetView(int index, int count)
        {
            if (index < 0 || index >= count) return null;
            return _boardController.GetView(index);
        }

        private float GetStepDuration(CardAnimType type) => type switch
        {
            CardAnimType.NoneLift => NONE_DURATION,
            CardAnimType.Bonus => BONUS_DURATION,
            CardAnimType.Destroy => DESTROY_DURATION,
            CardAnimType.Absorption => ABSORPTION_DURATION,
            _ => 0.8f
        };
    }
}