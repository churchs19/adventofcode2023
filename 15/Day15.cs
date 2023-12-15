public static class Day15
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("15/input.txt");

        var sum = 0;
        foreach (var inputLine in input)
        {
            var steps = inputLine.Split(',');
            foreach (var step in steps)
            {
                var currentValue = 0;
                foreach (var character in step.ToCharArray())
                {
                    currentValue = (currentValue + character) * 17 % 256;
                }
                sum += currentValue;
            }
        }

        Console.WriteLine($"Day 15 - Part 1: {sum}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("15/input.txt");

    }
}