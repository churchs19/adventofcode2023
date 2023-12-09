using Microsoft.VisualBasic;

public static class Day9
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("9/input.txt");

        long sum = 0;
        foreach (var inputLine in input)
        {
            var parsedInput = inputLine.Split(" ").Select(long.Parse).ToList();

            sum += FindNextValue(parsedInput);
        }

        Console.WriteLine($"Day 9 - Part 1: {sum}");
    }

    private static long FindNextValue(List<long> inputLine)
    {
        if (inputLine.All(it => it == 0))
        {
            return 0;
        }

        var nextInput = new List<long>();

        for (var i = 1; i < inputLine.Count(); i++)
        {
            nextInput.Add(inputLine[i] - inputLine[i - 1]);
        }
        return inputLine.Last() + FindNextValue(nextInput);
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("9/input.txt");

        long sum = 0;
        foreach (var inputLine in input)
        {
            var parsedInput = inputLine.Split(" ").Select(long.Parse).ToList();

            var newFirst = FindFirstValue(parsedInput);

            sum += newFirst;
        }

        Console.WriteLine($"Day 9 - Part 2: {sum}");
    }

    private static long FindFirstValue(List<long> inputLine)
    {
        if (inputLine.All(it => it == 0))
        {
            return 0;
        }

        var nextInput = new List<long>();

        for (var i = 1; i < inputLine.Count(); i++)
        {
            nextInput.Add(inputLine[i] - inputLine[i - 1]);
        }
        return inputLine.First() - FindFirstValue(nextInput);
    }
}