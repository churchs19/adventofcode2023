using System.Buffers;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

public static class Day14
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("14/input.txt");

        var matrix = input.Select(it => it.ToCharArray()).ToArray();

        matrix = matrix.RollNorth();

        var sum = CalculateTotalLoad(matrix);

        Console.WriteLine($"Day 14 - Part 1: {sum}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("14/input.txt");

        var matrix = input.Select(it => it.ToCharArray()).ToArray();

        Dictionary<string, List<long>> uniqueResults = [];
        var cycle = 0;
        long totalCycles = 1000000000;
        bool loopFound = false;
        while (cycle < totalCycles)
        {
            cycle++;
            matrix = matrix.RollNorth();
            // Console.Write($"After Cycle {cycle} North: \n{matrix.MatrixString()}\n\n");
            matrix = matrix.RollWest();
            // Console.Write($"After Cycle {cycle} West: \n{matrix.MatrixString()}\n\n");
            matrix = matrix.RollSouth();
            // Console.Write($"After Cycle {cycle} South: \n{matrix.MatrixString()}\n\n");
            matrix = matrix.RollEast();
            var result = matrix.MatrixString();
            if (!loopFound)
            {
                if (uniqueResults.ContainsKey(result))
                {
                    if (uniqueResults[result].Count >= 2)
                    {
                        //Loop detected
                        totalCycles = cycle + ((totalCycles - cycle) % (uniqueResults[result][1] - uniqueResults[result][0]));
                        loopFound = true;
                    }
                    else
                    {
                        uniqueResults[result].Add(cycle);
                    }
                }
                else
                {
                    uniqueResults.Add(result, [cycle]);
                }
            }
        }

        var sum = CalculateTotalLoad(matrix);

        Console.WriteLine($"Day 14 - Part 2: {sum}");
    }

    private static int CalculateTotalLoad(char[][] matrix)
    {
        var sum = 0;
        for (var i = 0; i < matrix.Length; i++)
        {
            var load = (matrix.Length - i) * matrix[i].Where(it => it == 'O').Count();
            sum += load;
        }
        return sum;
    }

    private static char[][] CloneMatrix(char[][] matrix)
    {
        char[][] clone = new char[matrix.Length][];
        for (var i = 0; i < matrix.Length; i++)
        {
            clone[i] = new char[matrix[i].Length];
            Array.Copy(matrix[i], clone[i], matrix[i].Length);
        }

        return clone;
    }

    private static char[][] RollNorth(this char[][] matrix)
    {
        var clone = CloneMatrix(matrix);
        for (var column = 0; column < clone[0].Length; column++)
        {
            for (var row = 1; row < clone.Length; row++)
            {
                if (clone[row][column] == 'O')
                {
                    var currentRow = row;
                    while (currentRow > 0 && clone[currentRow - 1][column] == '.')
                    {
                        //Roll north
                        clone[currentRow][column] = '.';
                        clone[currentRow - 1][column] = 'O';
                        currentRow--;
                    }
                }
            }
        }
        return clone;
    }

    private static char[][] RollSouth(this char[][] matrix)
    {
        var clone = CloneMatrix(matrix);
        for (var column = 0; column < clone[0].Length; column++)
        {
            for (var row = clone.Length - 2; row >= 0; row--)
            {
                if (clone[row][column] == 'O')
                {
                    var currentRow = row;
                    while (currentRow < clone.Length - 1 && clone[currentRow + 1][column] == '.')
                    {
                        //Roll south
                        clone[currentRow][column] = '.';
                        clone[currentRow + 1][column] = 'O';
                        currentRow++;
                    }
                }
            }
        }
        return clone;
    }

    private static char[][] RollEast(this char[][] matrix)
    {
        var clone = CloneMatrix(matrix);
        for (var row = 0; row < clone.Length; row++)
        {
            for (var column = clone[row].Length - 2; column >= 0; column--)
            {
                if (clone[row][column] == 'O')
                {
                    var currentColumn = column;
                    while (currentColumn < clone[row].Length - 1 && clone[row][currentColumn + 1] == '.')
                    {
                        //Roll east
                        clone[row][currentColumn] = '.';
                        clone[row][currentColumn + 1] = 'O';
                        currentColumn++;
                    }
                }
            }
        }
        return clone;
    }

    private static char[][] RollWest(this char[][] matrix)
    {
        var clone = CloneMatrix(matrix);
        for (var row = 0; row < clone.Length; row++)
        {
            for (var column = 1; column < clone[row].Length; column++)
            {
                if (clone[row][column] == 'O')
                {
                    var currentColumn = column;
                    while (currentColumn > 0 && clone[row][currentColumn - 1] == '.')
                    {
                        //Roll west
                        clone[row][currentColumn] = '.';
                        clone[row][currentColumn - 1] = 'O';
                        currentColumn--;
                    }
                }
            }
        }
        return clone;
    }

    private static string MatrixString(this char[][] matrix)
    {
        return string.Join('\n', matrix.Select(it => new string(it)));
    }
}