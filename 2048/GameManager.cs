using System;

namespace _2048
{
    public class GameManager
    {
        private readonly Random _random;

        public int Size { get; private set; }
        public int StartTileCount { get; private set; }
        public Grid Grid { get; private set; }
        public int Moves { get; private set; }

        public GameManager(int size, int startTileCount)
        {
            _random = new Random();
            Size = size;
            StartTileCount = startTileCount;
        }

        public void Start()
        {
            Grid = new Grid(Size);
            Moves = 0;
            AddStartTiles();
        }

        public void Test(params int[] cells)
        {
            Grid = new Grid(Size);
            for(int i = 0; i < cells.Length; i++)
            {
                int x = i%Size;
                int y = i/Size;
                Grid.Cells[x, y] = cells[i];
            }
        }

        public bool Move(Directions direction)
        {
            bool moved = false;
            switch(direction)
            {
                case Directions.Up:
                    moved = Grid.MoveUp();
                    break;
                case Directions.Right:
                    moved = Grid.MoveRight();
                    break;
                case Directions.Down:
                    moved = Grid.MoveDown();
                    break;
                case Directions.Left:
                    moved = Grid.MoveLeft();
                    break;
            }
            if (moved)
            {
                Moves++;
                return AddRandomTile();
            }
            return true;
        }

        private void AddStartTiles()
        {
            for (int i = 0; i < StartTileCount; i++)
                AddRandomTile();
        }

        private bool AddRandomTile()
        {
            int x, y;
            bool hasAvailable = Grid.RandomAvailableCell(out x, out y);
            if (hasAvailable)
            {
                int value = _random.NextDouble() < 0.9 ? 2 : 4;
                Grid.Cells[x, y] = value;
            }
            return hasAvailable;
        }
    }
}
