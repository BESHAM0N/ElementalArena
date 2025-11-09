using System;

namespace SpaceGame
{
    public interface IGameEndUI
    {
        void Show(int totalScore);
        void Hide();

        // event Action RestartClicked; // начать новую игру (по желанию)
        event Action MenuClicked;  
    }
}