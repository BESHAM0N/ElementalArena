using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SpaceGame
{
    public sealed class CardView : MonoBehaviour
    {
        public ICard Card => _card;

        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _pointsText;
        [SerializeField] private Image _suitIcon;
        [SerializeField] private SuitIcons _suitIcons;
        
        [SerializeField] private GameObject _destroyAnim;
        [SerializeField] private GameObject _bonusAnim;
        [SerializeField] private GameObject _absorptionAnim;

        private ICard _card;

        public void Initialize(ICard card)
        {
            _image.sprite = card.Image;
            _titleText.text = card.DisplayName;
            _pointsText.text = card.BasePoints.ToString();
            _suitIcon.sprite = _suitIcons.GetIcon(card.Suit);
            _card = card;
        }

        public void OnDestroyAnim()
        {
            _destroyAnim.SetActive(true);
        }
        
        public void OnBonusAnim()
        {
            _bonusAnim.SetActive(true);
        }
        
        public void OnNoneAnim()
        {
            if (TryGetComponent<RectTransform>(out var rt))
            {
                var start = rt.anchoredPosition;
                rt.DOAnchorPosY(start.y + 12, 0.25f * 0.5f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.OutQuad);
            }
            else
            {
                var start = transform.localPosition;
                transform.DOLocalMoveY(start.y + 12, 0.25f * 0.5f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.OutQuad);
            }
        }
        
        public void OnAbsorptionAnim()
        {
            _absorptionAnim.SetActive(true);
        }
        
        public class Factory : PlaceholderFactory<CardView> { }
    }
}