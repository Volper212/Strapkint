using System;

namespace Strapkint.Entities
{
    sealed class Scoreboard : FixedEntity
    {
        const string text =
            "┌─────────┐" +
            "│ LVL 000 │" +
            "│  ©  000 │" +
            "└─────────┘";

        const int width = 11;
        readonly Counter levelCounter, coinsCounter;

        private int _levelCount, _coins;

        public Scoreboard() : base(text.To2DCharArray(width), Position.Zero, Corner.TopRight)
        {
            const int countersX = 6;
            const int levelY = 1;
            const int coinsY = 2;
            const int coinX = countersX - 3;
            const int zeros = 3;

            levelCounter = new Counter(this, countersX, levelY, zeros);
            coinsCounter = new Counter(this, countersX, coinsY, zeros);
            LightColorMap[coinX, coinsY] = ConsoleColor.DarkYellow;
            DarkColorMap[coinX, coinsY] = ConsoleColor.Yellow;
        }

        public int LevelCount
        {
            get => _levelCount;
            set
            {
                _levelCount = value;
                levelCounter.Update(LevelCount);
            }
        }

        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                coinsCounter.Update(Coins);
            }
        }

        class Counter
        {
            readonly Position position;
            readonly char[,] text;
            readonly string textPattern;

            public Counter(Scoreboard scoreboard, int x, int y, int zeros = 0)
            {
                text = scoreboard.Text;
                position = new Position(x, y);
                textPattern = $"D{zeros}";
            }

            public void Update(int value)
            {
                text.ReplaceAt(value.ToString(textPattern), position);
            }
        }
    }
}
