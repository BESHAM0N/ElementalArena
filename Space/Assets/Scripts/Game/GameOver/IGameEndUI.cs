using System;

namespace SpaceGame
{
    public interface IGameEndUI
    {
        void Show(int totalScore);
        void Hide();
       
        event Action MenuClicked;  
    }
}