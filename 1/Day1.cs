using System.Text.RegularExpressions;

public static class Day1
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("1/input.txt");

        int sum = 0;
        char[] numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
        foreach (var line in input)
        {
            var chars = line.ToCharArray();
            var digits = chars.Where(it => numbers.Contains(it));
            var calibrationString = new string(new[] { digits.First(), digits.Last() });

            Console.WriteLine(calibrationString);

            int lineValue;
            if (int.TryParse(calibrationString, out lineValue))
            {
                sum += lineValue;
            }
        }

        Console.WriteLine($"Day 1 - Part 1: {sum}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("1/input.txt");
    }
}