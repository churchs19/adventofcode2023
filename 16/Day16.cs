public static class Day16
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("16/input-test.txt");

        var matrix = input.Select(it => it.Trim().ToCharArray()).ToArray();
        bool[][] energized = new bool[matrix.Length][];
        for (var i = 0; i < energized.Length; i++)
        {
            energized[i] = new bool[matrix[i].Length];
        }

        Queue<Beam> beams = [];
        beams.Enqueue(new Beam(0, 0, Direction.Right));

        while (beams.Any())
        {
            var currentBeam = beams.Dequeue();

            if (currentBeam.Row >= 0 &&
                currentBeam.Row < matrix.Length &&
                currentBeam.Column >= 0 &&
                currentBeam.Column < matrix[currentBeam.Row].Length)
            {
                energized[currentBeam.Row][currentBeam.Column] = true;

                Beam? next = null;
                Beam? next2 = null;
                switch (matrix[currentBeam.Row][currentBeam.Column])
                {
                    case '.':
                        beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, currentBeam.Direction));
                        break;
                    case '/':
                        if (currentBeam.Direction == Direction.Right)
                        {
                            next = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Up);
                        }
                        else if (currentBeam.Direction == Direction.Left)
                        {
                            next = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Down);
                        }
                        else if (currentBeam.Direction == Direction.Up)
                        {
                            next = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Right);
                        }
                        else
                        {
                            next = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Left);
                        }
                        break;
                    case '\\':
                        if (currentBeam.Direction == Direction.Right)
                        {
                            next = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Down);
                        }
                        else if (currentBeam.Direction == Direction.Left)
                        {
                            next = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Up);
                        }
                        else
                        {
                            next = currentBeam.Direction == Direction.Up
                                ? GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Left)
                                : GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Right);
                        }
                        break;
                    case '|':
                        if (currentBeam.Direction == Direction.Up || currentBeam.Direction == Direction.Down)
                        {
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, currentBeam.Direction));
                        }
                        else
                        {
                            //Split
                            next = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Up);
                            next2 = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Down);
                        }
                        break;
                    case '-':
                        if (currentBeam.Direction == Direction.Left || currentBeam.Direction == Direction.Right)
                        {
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, currentBeam.Direction));
                        }
                        else
                        {
                            //Split
                            next = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Left);
                            next2 = GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Right);
                        }
                        break;
                }
                if (next != null && 
                    next.Row >= 0 &&
                    next.Row < matrix.Length &&
                    next.Column >= 0 &&
                    next.Column < matrix[next.Row].Length &&
                    !energized[next.Row][next.Column])
                {
                    beams.Enqueue(next);
                }
                if (next2 != null &&
                    next2.Row >= 0 &&
                    next2.Row < matrix.Length &&
                    next2.Column >= 0 &&
                    next2.Column < matrix[next2.Row].Length &&
                    !energized[next2.Row][next2.Column])
                {   
                    beams.Enqueue(next2);
                }
            }
        }

        Console.WriteLine("Energized:");
        foreach(var line in energized.Select(it => new string(it.Select(item => item ? '#' : '.').ToArray())))
        {
            Console.WriteLine(line);
        }
        Console.Write("\n\n");

        var energizedCount = energized.SelectMany(it => it.Where(item => item == true)).Count();

        Console.WriteLine($"Day 16 - Part 1: {energizedCount}");
    }

    private static Beam GetNextLocation(int row, int column, Direction direction)
    {
        switch (direction)
        {
            case Direction.Right:
                return new Beam(row, column + 1, direction);
            case Direction.Left:
                return new Beam(row, column - 1, direction);
            case Direction.Up:
                return new Beam(row - 1, column, direction);
            case Direction.Down:
                return new Beam(row + 1, column, direction);
        }
        return new Beam(row, column, direction);
    }

    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    internal class Beam
    {
        public Beam(int row, int column, Direction direction)
        {
            Row = row;
            Column = column;
            Direction = direction;
        }

        internal int Row { get; private set; }
        internal int Column { get; private set; }
        internal Direction Direction { get; private set; }
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("16/input.txt");


    }
}