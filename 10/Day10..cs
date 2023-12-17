public static class Day10
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("10/input.txt");

        var matrix = input.Select(it => it.Trim().ToCharArray()).ToArray();


    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("10/input.txt");

        var matrix = input.Select(it => it.Trim().ToCharArray()).ToArray();
    }

    internal static Dictionary<(char pipe, Direction inDirection), Direction> directionChanges = new()
        {
            {('|', Direction.North), Direction.South },
            {('|', Direction.South), Direction.North },
            {('-', Direction.East), Direction.West },
            {('-', Direction.West), Direction.East },
            {('L', Direction.North), Direction.East },
            {('L', Direction.East), Direction.North },
            {('J', Direction.North), Direction.West },
            {('J', Direction.West), Direction.North },
            {('7', Direction.South), Direction.West },
            {('7', Direction.West), Direction.South },
            {('F', Direction.East), Direction.South },
            {('F', Direction.South), Direction.East }
        };

    internal enum Direction
    {
        North,
        South,
        East,
        West
    }
}