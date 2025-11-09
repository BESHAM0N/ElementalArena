using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceGame
{
    public sealed class HandService : IHandService
    {
        private readonly ICardFactory _factory;
        private readonly List<CardView> _views = new();
        [Inject(Id = "HandParent")] private Transform _handRoot;

        public HandService(ICardFactory factory) => _factory = factory;

        public void BuildHand(IReadOnlyList<Card> cards)
        {
            ClearHand();
            if (cards == null) return;
            for (int i = 0; i < cards.Count; i++)
                _views.Add(_factory.CreateView(cards[i]));
        }
        
        public void ClearHand()
        {
            if (_handRoot != null)
            {
                for (int i = _handRoot.childCount - 1; i >= 0; i--)
                {
                    var child = _handRoot.GetChild(i);
                    if (child.TryGetComponent<CardView>(out var cv))
                        Object.Destroy(child.gameObject);
                }
            }
          
            for (int i = 0; i < _views.Count; i++)
                if (_views[i]) Object.Destroy(_views[i].gameObject);

            _views.Clear();
        }
    }
}