using System.Collections.Generic;

public static class Day7
{
    public static void Part1()
    {
        var input = Utilities.ReadFile("7/input.txt");

        var hands = new List<Hand>();
        foreach (var inputLine in input)
        {
            hands.Add(ParseHand(inputLine));
        }

        var orderedHands = hands.OrderBy(it => it, new HandComparer()).ToArray();

        var winnings = 0;
        for (var i = 0; i < orderedHands.Length; i++)
        {
            winnings += orderedHands[i].Bid * (i + 1);
        }

        Console.WriteLine($"Day 7 - Part 1: {winnings}");
    }

    public static void Part2()
    {
        var input = Utilities.ReadFile("7/input.txt");

        var hands = new List<Hand>();
        foreach (var inputLine in input)
        {
            hands.Add(ParseHand(inputLine));
        }

        var orderedHands = hands.OrderBy(it => it, new HandComparer2()).ToArray();

        var winnings = 0;
        for (var i = 0; i < orderedHands.Length; i++)
        {
            winnings += orderedHands[i].Bid * (i + 1);
        }

        Console.WriteLine($"Day 7 - Part 2: {winnings}");
    }

    private static Hand ParseHand(string inputLine)
    {
        var parts = inputLine.Split(" ");
        var cards = parts[0].Trim().ToCharArray();
        var bid = int.Parse(parts[1].Trim());

        return new Hand() { Cards = cards, Bid = bid };
    }

    private static readonly Dictionary<char, int> CardValues = new Dictionary<char, int>() {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'J', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 }
    };

    private static readonly Dictionary<char, int> CardValues2 = new Dictionary<char, int>() {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 },
        { 'J', 1 }
    };

    public class Hand
    {
        public required char[] Cards { get; set; }
        public required int Bid { get; set; }

        public int HandType
        {
            get
            {
                var counts = Cards.GroupBy(it => it).Select(it => it.Count()).OrderByDescending(it => it).ToArray();
                if (counts[0] == 5) return 7; // Five of a kind
                if (counts[0] == 4) return 6; // Four of a kind
                if (counts[0] == 3)
                {
                    if (counts[1] == 2)
                    {
                        return 5; // Full house
                    }
                    else
                    {
                        return 4; // Three of a kind
                    }
                }
                if (counts[0] == 2)
                {
                    if (counts[1] == 2)
                    {
                        return 3; // Two Pair
                    }
                    else
                    {
                        return 2; // One Pair
                    }
                }
                return 1; // High card
            }
        }

        public int HandType2
        {
            get
            {
                var countKeys = Cards.GroupBy(it => it).Select(it => new { it.Key, Count = it.Count() }).ToArray();
                var counts = countKeys.Where(it => it.Key != 'J').Select(it => it.Count).OrderByDescending(it => it).ToArray();
                if (countKeys.Any(it => it.Key == 'J'))
                {
                    if (counts.Length > 0)
                    {
                        counts[0] += countKeys.Where(it => it.Key == 'J').First().Count;
                    }
                    else
                    {
                        counts = countKeys.Where(it => it.Key == 'J').Select(it => it.Count).ToArray();
                    }
                }

                if (counts[0] == 5) return 7; // Five of a kind
                if (counts[0] == 4) return 6; // Four of a kind
                if (counts[0] == 3)
                {
                    if (counts[1] == 2)
                    {
                        return 5; // Full house
                    }
                    else
                    {
                        return 4; // Three of a kind
                    }
                }
                if (counts[0] == 2)
                {
                    if (counts[1] == 2)
                    {
                        return 3; // Two Pair
                    }
                    else
                    {
                        return 2; // One Pair
                    }
                }
                return 1; // High card
            }
        }
    }

    private class HandComparer : IComparer<Hand>
    {
        public int Compare(Hand? x, Hand? y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);

            if (x.HandType != y.HandType)
            {
                return x.HandType - y.HandType;
            }
            else
            {
                for (var i = 0; i < x.Cards.Length && i < y.Cards.Length; i++)
                {
                    if (CardValues[x.Cards[i]] != CardValues[y.Cards[i]])
                    {
                        return CardValues[x.Cards[i]] - CardValues[y.Cards[i]];
                    }
                }
            }
            return 0;
        }
    }

    private class HandComparer2 : IComparer<Hand>
    {
        public int Compare(Hand? x, Hand? y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);

            if (x.HandType2 != y.HandType2)
            {
                return x.HandType2 - y.HandType2;
            }
            else
            {
                for (var i = 0; i < x.Cards.Length && i < y.Cards.Length; i++)
                {
                    if (CardValues2[x.Cards[i]] != CardValues2[y.Cards[i]])
                    {
                        return CardValues2[x.Cards[i]] - CardValues2[y.Cards[i]];
                    }
                }
            }
            return 0;
        }
    }
}