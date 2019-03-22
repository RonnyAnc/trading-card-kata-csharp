using System.Collections.Generic;
using System.Linq;

namespace TradingCardGame {
    internal class Deck {
        private const int Single = 1;
        private const int Pair = 2;
        private const int Trio = 3;
        private const int Quartet = 4;
        private const int DeckSize = 20;
        public List<Card> Cards { get; }

        private Deck() {
            Cards = new List<Card>(DeckSize)
                .Concat(Group(new Card(0), Pair))
                .Concat(Group(new Card(1), Pair))
                .Concat(Group(new Card(2), Trio))
                .Concat(Group(new Card(3), Quartet))
                .Concat(Group(new Card(4), Trio))
                .Concat(Group(new Card(5), Pair))
                .Concat(Group(new Card(6), Pair))
                .Concat(Group(new Card(7), Single))
                .Concat(Group(new Card(8), Single))
                .ToList();
        }

        internal static Deck Create() {
            return new Deck();
        }

        private static IEnumerable<Card> Group(Card card, int amount) {
            return Enumerable.Repeat(card, amount);
        }
    }
}