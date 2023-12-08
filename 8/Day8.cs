using System.Collections;
using System.Reflection.Metadata.Ecma335;

public static class Day8
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("8/input.txt");

        var directions = input[0].ToCharArray();
        var map = ParseInput(input.Skip(1));

        var count = StepsUntil("AAA", directions, map, it => it == "ZZZ");

        Console.WriteLine($"Day 8 - Part 1: {count}");
    }

    private static Dictionary<string, string[]> ParseInput(IEnumerable<string> input)
    {
        Dictionary<string, string[]> map = new Dictionary<string, string[]>();
        foreach (var inputLine in input)
        {
            var parts = inputLine.Split(" = ");
            var key = parts[0].Trim();
            var choices = parts[1][1..^1].Split(", ");
            map.Add(key, choices);
        }
        return map;
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("8/input.txt");

        var directions = input[0].ToCharArray();
        var map = ParseInput(input.Skip(1));

        var startingLocations = map.Keys.Where(it => it.EndsWith('A')).ToArray();
        var counts = startingLocations.Select(it => StepsUntil(it, directions, map, it => it.EndsWith('Z'))).ToArray();

        var count = LeastCommonMultiple(counts);

        Console.WriteLine($"Day 8 - Part 2: {count}");
    }

    private static long StepsUntil(string startingKey, char[] directions, Dictionary<string, string[]> map, Func<string, bool> endingCondition)
    {
        var currentKey = startingKey;
        long count = 0;
        while (!endingCondition(currentKey))
        {
            var direction = directions[count % directions.Length];
            var directionIndex = direction == 'L' ? 0 : 1;
            count++;
            currentKey = map[currentKey][directionIndex];
        }
        return count;
    }

    static long LeastCommonMultiple(long[] numbers)
    {
        return numbers.Aggregate(LeastCommonMultiple);
    }
    static long LeastCommonMultiple(long a, long b)
    {
        return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
    }
    static long GreatestCommonDivisor(long a, long b)
    {
        return b == 0 ? a : GreatestCommonDivisor(b, a % b);
    }
}