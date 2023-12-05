using System.Buffers;

public static class Day3
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("3/input.txt");

        var lines = input.Select(it => it.ToCharArray()).ToArray();

        SearchValues.Create("\n.0123456789");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("3/input.txt");


    }
}
