using Microsoft.VisualBasic;

public static class Day4
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("4/input.txt");

        var cards = ParseInput(input);

        double totalPoints = 0;
        foreach (var card in cards)
        {
            var winningNumbersCount = card.WinningNumbers.Intersect(card.CardNumbers).Count();

            if (winningNumbersCount > 0)
            {
                var cardPoints = Math.Pow(2, winningNumbersCount - 1);
                totalPoints += cardPoints;
            }
        }

        Console.WriteLine($"Day 4 - Part 1: {totalPoints}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("4/input.txt");

        var cards = ParseInput(input);


    }

    public static IEnumerable<Card> ParseInput(string[] inputLines)
    {
        List<Card> cards = new List<Card>();
        foreach (var input in inputLines)
        {
            var initialSplit = input.Split(": ");
            var idSplit = initialSplit[0].Split(" ");
            var id = int.Parse(idSplit[^1].Trim());

            var numbers = initialSplit[1].Split(" | ");
            var winningNumbers = numbers[0].Split(" ").Where(it => !string.IsNullOrWhiteSpace(it)).Select(int.Parse).ToList();
            var cardNumbers = numbers[1].Split(" ").Where(it => !string.IsNullOrWhiteSpace(it)).Select(int.Parse).ToList();

            cards.Add(new Card()
            {
                Id = id,
                WinningNumbers = winningNumbers,
                CardNumbers = cardNumbers
            });
        }
        return cards;
    }

    public class Card
    {
        public Card()
        {
            Id = 0;
            WinningNumbers = [];
            CardNumbers = [];
        }

        public int Id { get; set; }
        public List<int> WinningNumbers { get; set; }

        public List<int> CardNumbers { get; set; }
    }
}