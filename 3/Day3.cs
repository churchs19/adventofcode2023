using System.Buffers;
using System.Text.RegularExpressions;

public static class Day3
{
    private static char[] Symbols = ['@', '#', '$', '%', '&', '*', '/', '+', '-', '='];
    private static string Pattern = @"\d+";

    public static void Part1()
    {
        var input = Utilities.ReadFile("3/input.txt");

        int count = 0;
        for (int row = 0; row < input.Length; row++)
        {
            for (int col = 0; col < input[row].Length; col++)
            {
                var c = input[row][col];
                if (c == '.')
                {
                    continue;
                }

                if (Symbols.Contains(c))
                {
                    var result = Calculate(input[row - 1], col);
                    result += Calculate(input[row], col);
                    result += Calculate(input[row + 1], col);
                    count += result;
                }

            }
        }
        Console.WriteLine($"Day 3 - Part 1: {count}");
    }

    private static int Calculate(string line, int col)
    {
        List<int> indexesToCheck = new List<int> { col - 1, col, col + 1 };
        int count = 0;
        MatchCollection matches = Regex.Matches(line, Pattern);

        foreach (Match match in matches)
        {
            string number = match.Value;

            if (AnyIndexInList(indexesToCheck, match.Index, match.Length))
            {
                count += Int32.Parse(number);
            }
        }
        return count;
    }

    static bool AnyIndexInList(List<int> list, int startIndex, int length)
    {
        for (int i = startIndex; i < startIndex + length; i++)
        {
            if (list.Contains(i))
            {
                return true;
            }
        }
        return false;
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("3/input.txt");

        int count = 0;
        for (int row = 0; row < input.Length; row++)
        {
            for (int column = 0; column < input[row].Length; column++)
            {
                var c = input[row][column];
                if (c == '.')
                    continue;

                if (c == '*')
                {
                    var res1 = Calculate2(input[row - 1], column);
                    var res2 = Calculate2(input[row], column);
                    var res3 = Calculate2(input[row + 1], column);

                    count += (res1, res2, res3) switch
                    {
                        { res1: not null, res2: null, res3: null } when res1[1] != null => res1[0].Value * res1[1].Value,
                        { res1: null, res2: not null, res3: null } when res2[1] != null => res2[0].Value * res2[1].Value,
                        { res1: null, res2: null, res3: not null } when res3[1] != null => res3[0].Value * res3[1].Value,

                        { res1: not null, res2: not null, res3: null } => res1[0].Value * res2[0].Value,
                        { res1: not null, res2: null, res3: not null } => res1[0].Value * res3[0].Value,
                        { res1: null, res2: not null, res3: not null } => res2[0].Value * res3[0].Value,
                        { res1: not null, res2: not null, res3: not null } => res1[0].Value * res2[0].Value * res3[0].Value,

                        _ => 0
                    };
                }
            }
        }

        Console.WriteLine($"Day 3 - Part 2: {count}");
    }

    private static int?[]? Calculate2(string line, int col)
    {
        List<int> indexesToCheck = new List<int> { col - 1, col, col + 1 };
        int?[]? count = null;
        MatchCollection matches = Regex.Matches(line, Pattern);

        foreach (Match match in matches.Cast<Match>())
        {
            string number = match.Value;

            if (AnyIndexInList(indexesToCheck, match.Index, match.Length))
            {
                if (count == null)
                    count = [int.Parse(number), null];
                else
                {
                    count[1] = int.Parse(number);
                };
            }
        }
        return count;
    }
}