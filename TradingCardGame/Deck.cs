using System.Collections.Generic;
using System.Linq;

namespace TradingCardGame {
    public class Deck {
        private const int Single = 1;
        private const int Pair = 2;
        private const int Trio = 3;
        private const int Quartet = 4;
        public List<Card> Cards { get; }

        public Deck() {
            Cards = new List<Card>()
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

        private static IEnumerable<Card> Group(Card card, int amount) {
            return Enumerable.Repeat(card, amount);
        }
    }
}