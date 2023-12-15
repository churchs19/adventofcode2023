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
                sum += HashStep(step);
            }
        }

        Console.WriteLine($"Day 15 - Part 1: {sum}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("15/input.txt");

        List<KeyValuePair<string, int>>[] boxes = new List<KeyValuePair<string, int>>[256];
        for (var i = 0; i < 256; i++)
        {
            boxes[i] = [];
        }

        var sum = 0;
        foreach (var inputLine in input)
        {
            var steps = inputLine.Split(',');
            foreach (var step in steps)
            {
                var parts = step.Split('=', '-');
                var index = HashStep(parts[0]);
                var lensIndex = boxes[index].FindIndex(it => it.Key == parts[0]);
                if (step.Contains('='))
                {
                    if (lensIndex == -1)
                    {
                        boxes[index].Add(new KeyValuePair<string, int>(parts[0], int.Parse(parts[1])));
                    }
                    else
                    {
                        boxes[index][lensIndex] = new KeyValuePair<string, int>(parts[0], int.Parse(parts[1]));
                    }
                }
                else
                {
                    if (lensIndex >= 0)
                    {
                        boxes[index].RemoveAt(lensIndex);
                    }
                }
            }

            for (var boxIndex = 0; boxIndex < boxes.Length; boxIndex++)
            {
                for (var lensIndex = 0; lensIndex < boxes[boxIndex].Count; lensIndex++)
                {
                    sum += (boxIndex + 1) * (lensIndex + 1) * boxes[boxIndex][lensIndex].Value;
                }
            }
        }

        Console.WriteLine($"Day 15 - Part 2: {sum}");
    }

    private static int HashStep(string step)
    {
        var currentValue = 0;
        foreach (var character in step.ToCharArray())
        {
            currentValue = (currentValue + character) * 17 % 256;
        }
        return currentValue;
    }
}