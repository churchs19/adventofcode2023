using System.Diagnostics;
using System.Dynamic;

public static class Day5
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("5/input-test.txt");

        var parsedInput = ParseInput(input);

        var seeds = parsedInput.Item1;
        var almanac = parsedInput.Item2;

        var locations = seeds.Select(it => GetSeedLocation(it, almanac)).ToArray();

        Console.WriteLine($"Day 5 - Part 1: {locations.OrderBy(it => it).First()}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("5/input.txt");

    }

    private static long GetSeedLocation(long step, List<List<AlmanacEntry>> almanac)
    {
        var currentStep = step;
        foreach (var entryList in almanac)
        {
            currentStep = GetSeedLocationInMap(currentStep, entryList);
        }
        return currentStep;
    }

    private static long GetSeedLocationInMap(long step, List<AlmanacEntry> map)
    {
        var currentStep = step;
        foreach (var entry in map)
        {
            if (entry.SourceRangeStart <= step && (entry.SourceRangeStart + entry.RangeLength) > step)
            {
                currentStep = entry.DestinationRangeStart + (currentStep - entry.SourceRangeStart);
            }
        }
        return currentStep;
    }

    private static Tuple<long[], List<List<AlmanacEntry>>> ParseInput(string[] input)
    {
        long[] seeds = input[0].Split(": ")[1].Split(" ").Select(long.Parse).ToArray();

        List<List<AlmanacEntry>> almanac = [];
        foreach (var inputLine in input.Skip(1))
        {
            if (inputLine.EndsWith("map:"))
            {
                almanac.Add([]);
                continue;
            }
            var map = inputLine.Split(" ").Select(long.Parse).ToArray();
            almanac.Last().Add(new AlmanacEntry()
            {
                DestinationRangeStart = map[0],
                SourceRangeStart = map[1],
                RangeLength = map[2]
            });
        }

        return new Tuple<long[], List<List<AlmanacEntry>>>(seeds, almanac);
    }

    public class AlmanacEntry
    {
        public long DestinationRangeStart { get; set; }
        public long SourceRangeStart { get; set; }
        public long RangeLength { get; set; }
    }
}