using System.Security.Cryptography;

public static class Day16
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("16/input.txt");
        var matrix = input.Select(it => it.Trim().ToCharArray()).ToArray();

        var energizedCount = GetEnergizedCount(new Beam(0, 0, Direction.Right), matrix);

        Console.WriteLine($"Day 16 - Part 1: {energizedCount}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("16/input.txt");

        var matrix = input.Select(it => it.Trim().ToCharArray()).ToArray();

        var startingBeams = new List<Beam>();
        for(var i = 0; i<matrix.Length; i++)
        {
            startingBeams.Add(new Beam(i, 0, Direction.Right));
            startingBeams.Add(new Beam(i, matrix[i].Length - 1, Direction.Left));
        }
        for(var i= 0; i< matrix[0].Length; i++)
        {
            startingBeams.Add(new Beam(0, i, Direction.Down));
            startingBeams.Add(new Beam(matrix.Length - 1, i, Direction.Up));
        }

        var maxEnergizedCount = int.MinValue;
        foreach(var beam in startingBeams)
        {
            var energizedCount = GetEnergizedCount(beam, matrix);
            if(energizedCount > maxEnergizedCount)
            {
                maxEnergizedCount = energizedCount;
            }
        }

        Console.WriteLine($"Day 16 - Part 2: {maxEnergizedCount}");
    }

    private static int GetEnergizedCount(Beam beam, char[][] matrix)
    {
        CellVisit[][] energized = new CellVisit[matrix.Length][];
        for (var i = 0; i < energized.Length; i++)
        {
            energized[i] = new CellVisit[matrix[i].Length];
            for (var j = 0; j < energized[i].Length; j++)
            {
                energized[i][j] = new CellVisit();
            }
        }

        Queue<Beam> beams = [];
        beams.Enqueue(beam);

        while (beams.Any())
        {
            var currentBeam = beams.Dequeue();

            if (currentBeam.Row >= 0 &&
                currentBeam.Row < matrix.Length &&
                currentBeam.Column >= 0 &&
                currentBeam.Column < matrix[currentBeam.Row].Length &&
                !energized[currentBeam.Row][currentBeam.Column].Contains(currentBeam.Direction))
            {
                energized[currentBeam.Row][currentBeam.Column].Add(currentBeam.Direction);

                switch (matrix[currentBeam.Row][currentBeam.Column])
                {
                    case '.':
                        beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, currentBeam.Direction));
                        break;
                    case '/':
                        if (currentBeam.Direction == Direction.Right)
                        {
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Up));
                        }
                        else if (currentBeam.Direction == Direction.Left)
                        {
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Down));
                        }
                        else if (currentBeam.Direction == Direction.Up)
                        {
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Right));
                        }
                        else
                        {
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Left));
                        }
                        break;
                    case '\\':
                        if (currentBeam.Direction == Direction.Right)
                        {
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Down));
                        }
                        else if (currentBeam.Direction == Direction.Left)
                        {
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Up));
                        }
                        else
                        {
                            beams.Enqueue(currentBeam.Direction == Direction.Up
                                ? GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Left)
                                : GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Right));
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
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Up));
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Down));
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
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Left));
                            beams.Enqueue(GetNextLocation(currentBeam.Row, currentBeam.Column, Direction.Right));
                        }
                        break;
                }
            }
        }

        return energized.SelectMany(it => it.Where(item => item.Count > 0)).Count();
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

    internal class CellVisit : List<Direction>
    {

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
}