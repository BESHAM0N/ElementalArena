namespace SpaceGame
{
    public struct CardAnimEvent
    {
        public int Index;
        public CardAnimType Type;

        public CardAnimEvent(int index, CardAnimType type)
        {
            Index = index;
            Type = type;
        }
    }
}