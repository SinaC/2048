using System;

namespace _2048
{
    public class Grid
    {
        private readonly Random _random;

        public int Size { get; private set; }
        public int[,] Cells { get; private set; }
        public int Score { get; private set; }

        public Grid(int size)
        {
            _random = new Random();

            Size = size;
            Cells = new int[Size,Size];
            for(int y = 0; y < Size; y++)
                for(int x = 0; x < Size; x++)
                    Cells[x,y] = 0;
            Score = 0;
        }

        public bool RandomAvailableCell(out int x, out int y)
        {
            x = 0;
            y = 0;
            // Count number of available cell
            int count = 0;
            for (int yi = 0; yi < Size; yi++)
                for (int xi = 0; xi < Size; xi++)
                    if (Cells[xi, yi] == 0)
                        count++;

            // No more cell?
            if (count == 0)
                return false;

            // Get random available cell
            int availableIndex = _random.Next(count);
            int index = 0;
            for (int yi = 0; yi < Size; yi++)
                for (int xi = 0; xi < Size; xi++)
                    if (Cells[xi, yi] == 0)
                        if (index == availableIndex)
                        {
                            x = xi;
                            y = yi;
                            return true;
                        }
                        else
                            index++;
            return true;
        }

        public bool MoveUp()
        {
            bool moved = false;
            for (int x = 0; x < Size; x++)
            {
                int y = 0;
                int endY = Size;
                // 2 passes
                // 1st pass: remove 0
                while (y < endY)
                {
                    if (Cells[x, y] == 0) // empty -> remove
                    {
                        // Move one step on the up
                        for (int i = y; i < endY - 1; i++)
                        {
                            Cells[x, i] = Cells[x, i + 1];
                            if (Cells[x, i] > 0)
                                moved = true;
                        }
                        // Delete bottom cell
                        Cells[x, endY - 1] = 0;
                        endY -= 1;
                    }
                    else
                        y += 1;
                }
                y = 0;
                endY = Size;
                // 2nd pass: merge identical neighbours
                while (y != endY)
                {
                    if (y + 1 < Size && Cells[x, y] > 0 && Cells[x, y] == Cells[x, y+1]) // identical -> merge
                    {
                        moved = true;
                        // Merge
                        Cells[x, y] *= 2;
                        // Score
                        Score += Cells[x, y];
                        // Move one step on the up
                        for (int i = y + 1; i < endY - 1; i++)
                            Cells[x, i] = Cells[x, i + 1];
                        // Delete bottom cell
                        Cells[x, endY - 1] = 0;
                        endY -= 1;
                    }
                    else
                        y += 1;
                }
            }
            return moved;
        }

        public bool MoveRight()
        {
            bool moved = false;
            for (int y = 0; y < Size; y++)
            {
                int x = Size - 1;
                int endX = -1;
                // 2 passes
                // 1st pass: remove 0
                while (x > endX)
                {
                    if (Cells[x, y] == 0) // empty -> remove
                    {
                        // Move one step on the left
                        for (int i = x; i > endX + 1; i--)
                        {
                            Cells[i, y] = Cells[i - 1, y];
                            if (Cells[i, y] > 0)
                                moved = true;
                        }
                        // Delete leftmost cell
                        Cells[endX + 1, y] = 0;
                        endX += 1;
                    }
                    else
                        x -= 1;
                }
                x = Size - 1;
                endX = -1;
                // 2nd pass: merge identical neighbours
                while (x != endX)
                {
                    if (x - 1 >= 0 && Cells[x, y] > 0 && Cells[x, y] == Cells[x - 1, y]) // identical -> merge
                    {
                        moved = true;
                        // Merge
                        Cells[x, y] *= 2;
                        // Score
                        Score += Cells[x, y];
                        // Move one step on the left
                        for (int i = x - 1; i > endX + 1; i--)
                            Cells[i, y] = Cells[i - 1, y];
                        // Delete leftmost cell
                        Cells[endX + 1, y] = 0;
                        endX += 1;
                    }
                    else
                        x -= 1;
                }
            }
            return moved;
        }

        public bool MoveDown()
        {
            bool moved = false;
            for (int x = 0; x < Size; x++)
            {
                int y = Size - 1;
                int endY = -1;
                // 2 passes
                // 1st pass: remove 0
                while (y > endY)
                {
                    if (Cells[x, y] == 0) // empty -> remove
                    {
                        // Move one step on the down
                        for (int i = y; i > endY + 1; i--)
                        {
                            Cells[x, i] = Cells[x, i - 1];
                            if (Cells[x, i] > 0)
                                moved = true;
                        }
                        // Delete top cell
                        Cells[x, endY + 1] = 0;
                        endY += 1;
                    }
                    else
                        y -= 1;
                }
                y = Size - 1;
                endY = -1;
                // 2nd pass: merge identical neighbours
                while (y != endY)
                {
                    if (y - 1 >= 0 && Cells[x, y] > 0 && Cells[x, y] == Cells[x, y - 1]) // identical -> merge
                    {
                        moved = true;
                        // Merge
                        Cells[x, y] *= 2;
                        // Score
                        Score += Cells[x, y];
                        // Move one step on the udownp
                        for (int i = y - 1; i > endY + 1; i--)
                            Cells[x, i] = Cells[x, i - 1];
                        // Delete top cell
                        Cells[x, endY + 1] = 0;
                        endY += 1;
                    }
                    else
                        y -= 1;
                }
            }
            return moved;
        }

        public bool MoveLeft()
        {
            bool moved = false;
            for (int y = 0; y < Size; y++)
            {
                int x = 0;
                int endX = Size;
                // 2 passes
                // 1st pass: remove 0
                while (x < endX)
                {
                    if (Cells[x, y] == 0) // empty -> remove
                    {
                        // Move one step on the left
                        for (int i = x; i < endX - 1; i++)
                        {
                            Cells[i, y] = Cells[i + 1, y];
                            if (Cells[i, y] > 0)
                                moved = true;
                        }
                        // Delete rightmost cell
                        Cells[endX - 1, y] = 0;
                        endX -= 1;
                    }
                    else
                        x += 1;
                }
                x = 0;
                endX = Size;
                // 2nd pass: merge identical neighbours
                while (x != endX)
                {
                    if (x + 1 < Size && Cells[x, y] > 0 && Cells[x, y] == Cells[x + 1, y]) // identical -> merge
                    {
                        moved = true;
                        // Merge
                        Cells[x, y] *= 2;
                        // Score
                        Score += Cells[x, y];
                        // Move one step on the left
                        for (int i = x + 1; i < endX - 1; i++)
                            Cells[i, y] = Cells[i + 1, y];
                        // Delete rightmost cell
                        Cells[endX - 1, y] = 0;
                        endX -= 1;
                    }
                    else
                        x += 1;
                }
            }
            return moved;
        }
    }
}