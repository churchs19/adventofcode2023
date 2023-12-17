public static class Day17
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("17/input-test.txt");

        var matrix = input.Select(it => it.Trim().ToCharArray().Select(item => int.Parse(item.ToString())).ToArray()).ToArray();

        var result = ShortestPath(matrix, 0, 0);

        Console.WriteLine($"Day 17 - Part 1: {result}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("17/input.txt");
    }

    internal class Block: IEquatable<Block>
    {

        internal Block(int row, int column, int heatLoss, int streak)
        {
            Row = row;
            Column = column;
            HeatLoss = heatLoss;
            Streak = streak;
        }

        internal int Row { get; private set; }
        internal int Column { get; private set; }
        internal int HeatLoss { get; private set; }
        internal int Streak { get; private set; }

        public bool Equals(Block? other)
        {
            if(other == null) return false;
            return Row == other.Row && Column == other.Column &&  HeatLoss == other.HeatLoss && Streak == other.Streak;
        }
    }



    internal class HeatLossComparer : IComparer<Block>
    {
        public int Compare(Block? a, Block? b) => b?.HeatLoss ?? 0 - a?.HeatLoss ?? 0;
    }

    private static int ShortestPath(int[][] matrix, int row, int column)
    {
        int[] rowDeltas = [-1, 0, 1, 0];
        int[] columnDeltas = [0, 1, 0, -1];

        int[][] heatLoss = new int[matrix.Length][];
        for (int i = 0; i < matrix.Length; i++)
        {
            heatLoss[i] = new int[matrix[i].Length];
            for (int j = 0; j < matrix[i].Length; j++)
            {
                heatLoss[i][j] = int.MaxValue;
            }
        }

        // Initialized source distance as
        // initial grid position value
        heatLoss[0][0] = matrix[0][0];

        List<Block> visited = [new Block(0, 0, heatLoss[0][0], 0)];
        visited.Sort(new HeatLossComparer());

        while (visited.Count > 0)
        {
            Block currentBlock = visited[0];
            visited.RemoveAt(0);

            for (int i = 0; i < rowDeltas.Length; i++)
            {
                int rows = currentBlock.Row + rowDeltas[i];
                int cols = currentBlock.Column + columnDeltas[i];

                if (IsInsideGrid(matrix, rows, cols))
                {
                    if (heatLoss[rows][cols]
                        > heatLoss[currentBlock.Row][currentBlock.Column]
                              + matrix[rows][cols])
                    {
                        // If Block is already been reached
                        // once, remove it from list
                        if (heatLoss[rows][cols]
                            != int.MaxValue)
                        {
                            Block adj = new Block(
                                rows, cols,
                                heatLoss[rows][cols]);
                            visited.Remove(adj);
                        }

                        // Insert Block with updated distance
                        heatLoss[rows][cols]
                            = heatLoss[currentBlock.Row][currentBlock.Column]
                              + matrix[rows][cols];
                        visited.Add(new Block(rows, cols,
                                        heatLoss[rows][cols]));
                        visited.Sort(new HeatLossComparer());
                    }
                }
            }
        }

        return heatLoss[matrix.Length - 1][matrix[matrix.Length - 1].Length - 1];
    }

    private static bool IsInsideGrid(int[][] matrix, int row, int column)
    {
        if (row < 0 || column < 0) return false;
        if (row >= matrix.Length) return false;
        return column < matrix[row].Length;
    }
}