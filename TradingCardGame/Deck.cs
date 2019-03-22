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
                .Concat(Enumerable.Repeat(new Card(0), Pair))
                .Concat(Enumerable.Repeat(new Card(1), Pair))
                .Concat(Enumerable.Repeat(new Card(2), Trio))
                .Concat(Enumerable.Repeat(new Card(3), Quartet))
                .Concat(Enumerable.Repeat(new Card(4), Trio))
                .Concat(Enumerable.Repeat(new Card(5), Pair))
                .Concat(Enumerable.Repeat(new Card(6), Pair))
                .Concat(Enumerable.Repeat(new Card(7), Single))
                .Concat(Enumerable.Repeat(new Card(8), Single))
                .ToList();
        }
    }
}