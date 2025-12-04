using System.Collections.Generic;

namespace SpaceGame
{
    public sealed class PresentationService : IPresentationService
    {
        private readonly IBoard _board;
        private readonly LevelController _levelController;
        private readonly GameEvents _gameEvents;
        private readonly BoardReactionsManager _manager;
        private readonly List<CardAnimEvent> _buffer = new();
        
        public PresentationService(BoardController boardController, LevelController levelController, GameEvents gameEvents)
        {
            _board = boardController.Board;
            _levelController = levelController;
            _gameEvents = gameEvents;

            _manager = new BoardReactionsManager(new StaticInteractionMatrix(), _gameEvents, comboBonus: 30);
        }
        
        public PresentationResult RunPresentation()
        {
            _buffer.Clear();

            var count = _board.SlotsCount;
            var slots = new ICard[count];

            for (int i = 0; i < count; i++)
                slots[i] = _board.GetCard(i);

            // Основные реакции + очки
            var total = _manager.RunDetailed(slots, _buffer);

            // Бонус за элемент уровня
            total += CalculateLevelBonus();

            // Уведомить UI/игру о текущем счёте (если нужно сразу показать)
            _gameEvents.RaiseScoreChanged(total);

            return new PresentationResult(total, _buffer);
        }
        
        private int CalculateLevelBonus()
        {
            var levelSuit = _levelController.GetCurrentElement();
            int bonusTotal = 0;
            var count = _board.SlotsCount;

            for (int i = 0; i < count; i++)
            {
                var card = _board.GetCard(i);
                if (card == null) continue;

                if (card.Suit == levelSuit && !card.IsDestroyed)
                    bonusTotal += 10;
            }

            return bonusTotal;
        }
    }
}