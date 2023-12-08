using System.Reflection.Metadata.Ecma335;

public static class Day8
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("8/input.txt");

        var directions = input[0].ToCharArray();
        var map = ParseInput(input.Skip(1));

        var currentKey = "AAA";
        var count = 0;
        while (currentKey != "ZZZ")
        {
            var direction = directions[count % directions.Length];
            var directionIndex = direction == 'L' ? 0 : 1;
            count++;
            currentKey = map[currentKey][directionIndex];
        }

        Console.WriteLine($"Day 8 - Part 1: {count}");
    }

    private static Dictionary<string, string[]> ParseInput(IEnumerable<string> input)
    {
        Dictionary<string, string[]> map = new Dictionary<string, string[]>();
        foreach (var inputLine in input)
        {
            var parts = inputLine.Split(" = ");
            var key = parts[0].Trim();
            var choices = parts[1].Substring(1, parts[1].Length - 2).Split(", ");
            map.Add(key, choices);
        }
        return map;
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("8/input.txt");


    }
}