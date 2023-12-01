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

        int sum = 0;
        char[] numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
        foreach (var line in input)
        {
            var cleanedLine = Regex.Replace(
                line, @"(one|two|three|four|five|six|seven|eight|nine)",
                it =>
            {
                switch (it.Value)
                {
                    case "one":
                        return "1";
                    case "two":
                        return "2";
                    case "three":
                        return "3";
                    case "four":
                        return "4";
                    case "five":
                        return "5";
                    case "six":
                        return "6";
                    case "seven":
                        return "7";
                    case "eight":
                        return "8";
                    case "nine":
                        return "9";
                    default:
                        return it.Value;
                }
            });
            var chars = cleanedLine.ToCharArray();
            var digits = chars.Where(it => numbers.Contains(it));
            var calibrationString = new string(new[] { digits.First(), digits.Last() });

            Console.WriteLine(calibrationString);

            int lineValue;
            if (int.TryParse(calibrationString, out lineValue))
            {
                sum += lineValue;
            }
        }

        Console.WriteLine($"Day 1 - Part 2: {sum}");
    }
}