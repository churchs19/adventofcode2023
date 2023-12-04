using System.ComponentModel;

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
            var winningNumbersCount = card.CardWinningNumbersCount;

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

        var cardList = cards.Select(it => new CardCopy() { Card = it, Copies = 1 }).ToArray();

        var result = CountCards(cardList, 0);

        Console.WriteLine($"Day 4 - Part 2: {result}");
    }

    private static long CountCards(CardCopy[] cards, long count)
    {
        if (cards.Length == 1)
        {
            return count += cards[0].Copies;
        }

        var winningNumbersCount = cards[0].Card.CardWinningNumbersCount;
        if (winningNumbersCount > 0)
        {
            for (var i = 1; i <= winningNumbersCount && i < cards.Length; i++)
            {
                cards[i].Copies += cards[0].Copies;
            }
        }

        var newCount = count + cards[0].Copies;
        return CountCards(cards.Skip(1).ToArray(), newCount);
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

    public class CardCopy
    {
        public CardCopy()
        {
            Card = new();
        }

        public Card Card { get; set; }
        public int Copies { get; set; } = 1;
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

        public int CardWinningNumbersCount
        {
            get
            {
                return WinningNumbers.Intersect(CardNumbers).Count();
            }
        }
    }
}