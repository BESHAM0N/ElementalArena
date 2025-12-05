using System;
using System.Collections.Generic;

namespace SpaceGame
{
    public sealed class CardFactory : ICardFactory
    {
        private readonly CardView.Factory _viewFactory;

        public CardFactory(CardView.Factory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public List<Card> BuildModels(ListCardPrototypes source)
        {
            var result = new List<Card>();
            if (source?.Cards == null) return result;

            foreach (var proto in source.Cards)
            {
                if (proto == null) 
                    continue;
                
                var m = new Card();
                m.InitializeFromPrototype(proto);
                result.Add(m);
            }
            return result;
        }

        public CardView CreateView(Card model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var view = _viewFactory.Create();
            view.Initialize(model);
            return view;
        }

        public List<CardView> CreateViews(IReadOnlyList<Card> models)
        {
            var list = new List<CardView>(models?.Count ?? 0);
            
            if (models == null) 
                return list;
            
            for (int i = 0; i < models.Count; i++)
                list.Add(CreateView(models[i]));
            
            return list;
        }
    }
}