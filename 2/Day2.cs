using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

public static class Day2
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("2/input.txt");

        var games = new List<Game>();
        foreach (var line in input)
        {
            games.Add(ParseMaxInputLines(line));
        }

        var actualContents = new Dictionary<string, int> {
            { "red", 12 },
            { "green", 13},
            { "blue", 14 }
        };
        var totalPossibleCubes = actualContents.Sum(it => it.Value);
        var sum = 0;
        foreach (var game in games)
        {
            var impossible = false;
            foreach (var color in actualContents.Keys)
            {
                if (game.Draws.ContainsKey(color) && game.Draws[color] > actualContents[color])
                {
                    impossible = true;
                }
            }

            impossible = impossible | (game.Draws.Sum(it => it.Value) > totalPossibleCubes);

            if (!impossible)
            {
                sum += game.Id;
            }
        }

        Console.WriteLine($"Day 2 - Part 1: {sum}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("2/input.txt");

        var games = new List<Game>();
        foreach (var line in input)
        {
            games.Add(ParseMaxInputLines(line));
        }

        long sum = 0;
        foreach (var game in games)
        {
            long seed = 1;
            var power = game.Draws.Values.Aggregate(seed, (aggregate, it) => aggregate * (long)it);
            sum += power;
        }

        Console.WriteLine($"Day 2 - Part 2: {sum}");
    }

    private static Game ParseMaxInputLines(string line)
    {
        var gameComponents = line.Split(": ");

        var id = int.Parse(gameComponents[0].Split(" ")[1]);

        var draws = new Dictionary<string, int>();
        var drawsStrings = gameComponents[1].Split(";");
        foreach (var drawString in drawsStrings)
        {
            var components = drawString.Trim().Split(", ");
            foreach (var component in components)
            {
                var componentPieces = component.Split(" ");
                var color = componentPieces[1].Trim();
                var amount = int.Parse(componentPieces[0]);

                if (draws.ContainsKey(color))
                {
                    draws[color] = Math.Max(draws[color], amount);
                }
                else
                {
                    draws.Add(color, amount);
                }
            }
        }

        return new Game()
        {
            Id = id,
            Draws = draws
        };
    }

    private class Game
    {
        public required int Id { get; set; }
        public required Dictionary<string, int> Draws { get; set; }
    }
}