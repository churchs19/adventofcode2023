public static class Day6
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("6/input.txt");

        //quadratic function to determine if you beat record distance : x^2 - t x + d = 0
        // where x is the time you hold down the button
        // t is the total time for the race
        // d is the record distance + 1
        // bookend times for the button are then:
        // x1 = (t + SQRT(t*t - 4 * d))/2
        // x2 = (t - SQRT(t*t - 4 * d))/2
        var times = input[0].Split(" ").Where(it => !string.IsNullOrWhiteSpace(it) && it != "Time:").Select(int.Parse).ToArray();
        var distances = input[1].Split(" ").Where(it => !string.IsNullOrWhiteSpace(it) && it != "Distance:").Select(int.Parse).ToArray();

        double total = 1;
        for (var i = 0; i < times.Length; i++)
        {
            var button1 = Math.Floor((times[i] + Math.Sqrt(times[i] * times[i] - 4 * (distances[i] + 1))) / 2);
            var button2 = Math.Ceiling((times[i] - Math.Sqrt(times[i] * times[i] - 4 * (distances[i] + 1))) / 2);

            total *= button1 - button2 + 1;
        }

        Console.WriteLine($"Day 6 - Part 1: {total}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("6/input.txt");

        var times = input[0].Split(" ").Where(it => !string.IsNullOrWhiteSpace(it) && it != "Time:").ToArray();
        var time = double.Parse(string.Concat(times));
        var distances = input[1].Split(" ").Where(it => !string.IsNullOrWhiteSpace(it) && it != "Distance:").ToArray();
        var distance = double.Parse(string.Concat(distances));

        var button1 = Math.Floor((time + Math.Sqrt(time * time - 4 * (distance + 1))) / 2);
        var button2 = Math.Ceiling((time - Math.Sqrt(time * time - 4 * (distance + 1))) / 2);

        var total = button1 - button2 + 1;

        Console.WriteLine($"Day 6 - Part 2: {total}");
    }

}