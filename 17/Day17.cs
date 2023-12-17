public static class Day17
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("17/input.txt");

        var matrix = input.Select(it => it.Trim().ToCharArray().Select(item => int.Parse(item.ToString())).ToArray()).ToArray();

        var visited = new bool[matrix.Length, matrix[0].Length, 4, 4];
        var work = new PriorityQueue<Block, long>();
        var minCost = new Dictionary<string, long>();

        work.Enqueue(new Block(0, 0, 0, Direction.Right), 0);
        work.Enqueue(new Block(0, 0, 0, Direction.Down), 0);

        minCost[new Block(0, 0, 0, Direction.Right).ToString()] = 0;
        minCost[new Block(0, 0, 0, Direction.Down).ToString()] = 0;

        bool found = false;
        var result = long.MaxValue;
        while (work.Count > 0 && !found)
        {
            var current = work.Dequeue();
            visited[current.Row, current.Column, (int)current.Direction, current.Streak] = true;
            //add new work to the queue
            if (current.Streak < 2)
            {
                TryToAddWork(matrix, work, minCost, current, current.Direction, current.Streak + 1, visited);
            }
            if (current.Direction == Direction.Up || current.Direction == Direction.Down)
            {
                TryToAddWork(matrix, work, minCost, current, Direction.Left, 0, visited);
                TryToAddWork(matrix, work, minCost, current, Direction.Right, 0, visited);
            }
            else if (current.Direction == Direction.Left || current.Direction == Direction.Right)
            {
                TryToAddWork(matrix, work, minCost, current, Direction.Up, 0, visited);
                TryToAddWork(matrix, work, minCost, current, Direction.Down, 0, visited);
            }
            if (current.Row == matrix.Length - 1 && current.Column == matrix[0].Length - 1)
            {
                found = true;
                result = minCost[current.ToString()];
            }
        }
        Console.WriteLine($"Day 17 - Part 1: {result}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("17/input.txt");

        var matrix = input.Select(it => it.Trim().ToCharArray().Select(item => int.Parse(item.ToString())).ToArray()).ToArray();

        var visited = new bool[matrix.Length, matrix[0].Length, 4, 10];
        var work = new PriorityQueue<Block, long>();
        var minCost = new Dictionary<string, long>();

        work.Enqueue(new Block(0, 0, 0, Direction.Right), 0);
        work.Enqueue(new Block(0, 0, 0, Direction.Down), 0);

        minCost[new Block(0, 0, 0, Direction.Right).ToString()] = 0;
        minCost[new Block(0, 0, 0, Direction.Down).ToString()] = 0;

        bool found = false;
        var result = long.MaxValue;

        while (work.Count > 0 && !found)
        {
            var current = work.Dequeue();
            visited[current.Row, current.Column, (int)current.Direction, current.Streak] = true;
            //add new work to the queue
            if (current.Streak < 9)
            {
                TryToAddWork(matrix, work, minCost, current, current.Direction, current.Streak + 1, visited);
            }
            if (current.Streak >= 3)
            {
                if (current.Direction == Direction.Up || current.Direction == Direction.Down)
                {
                    TryToAddWork(matrix, work, minCost, current, Direction.Left, 0, visited);
                    TryToAddWork(matrix, work, minCost, current, Direction.Right, 0, visited);
                }
                else if (current.Direction == Direction.Left || current.Direction == Direction.Right)
                {
                    TryToAddWork(matrix, work, minCost, current, Direction.Up, 0, visited);
                    TryToAddWork(matrix, work, minCost, current, Direction.Down, 0, visited);
                }
                if (current.Row == matrix.Length - 1 && current.Column == matrix[0].Length - 1)
                {
                    found = true;
                    result = minCost[current.ToString()];
                }
            }
        }

        Console.WriteLine($"Day 17 - Part 2: {result}");
    }

    static void TryToAddWork(int[][] matrix,
        PriorityQueue<Block, long> work,
        Dictionary<string, long> minCost,
        Block currentBlock,
        Direction direction,
        int newStreak,
        bool[,,,] visited)
    {
        Block newBlock = new Block(
            currentBlock.Row + DirectionDeltas[(int)direction].rowDelta,
            currentBlock.Column + DirectionDeltas[(int)direction].colDelta,
            newStreak,
            direction);
        if (newBlock.Row >= 0 &&
            newBlock.Column >= 0 &&
            newBlock.Row < matrix.Length &&
            newBlock.Column < matrix[0].Length &&
            !visited[newBlock.Row, newBlock.Column, (int)newBlock.Direction, newBlock.Streak])
        {
            var newValue = minCost[currentBlock.ToString()] + matrix[newBlock.Row][newBlock.Column];
            if (!minCost.ContainsKey(newBlock.ToString()))
            {
                minCost[newBlock.ToString()] = newValue;
                work.Enqueue(newBlock, newValue);
            }
            else if (minCost[newBlock.ToString()] > newValue)
            {
                minCost[newBlock.ToString()] = newValue;
                work.Enqueue(newBlock, newValue);
            }

        }
    }

    internal static readonly (int rowDelta, int colDelta)[] DirectionDeltas = [(-1, 0), (1, 0), (0, -1), (0, 1)];

    internal enum Direction
    {
        Up = 0,
        Down = 1,
        Left = 2,
        Right = 3
    }

    internal class Block
    {
        internal Block(int row, int column, int streak, Direction direction)
        {
            Row = row;
            Column = column;
            Streak = streak;
            Direction = direction;
        }

        internal int Row { get; private set; }
        internal int Column { get; private set; }
        internal int Streak { get; private set; }
        internal Direction Direction { get; private set; }

        public override string ToString()
        {
            return $"Block{Row},{Column},{Streak},{Enum.GetName(Direction)}";
        }
    }

}