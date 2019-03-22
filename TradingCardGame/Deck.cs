using System.Collections.Generic;
using System.Linq;

namespace TradingCardGame {
    public class Deck {
        private const int Pair = 2;
        private const int NumberOfTotalCards = 20;
        public List<Card> Cards { get; }

        public Deck() {
            Cards = new List<Card>()
                .Concat(Enumerable.Repeat(new Card(0), Pair))
                .Concat(Enumerable.Repeat(new Card(1), Pair))
                .Concat(Enumerable.Repeat(new Card(10), NumberOfTotalCards - Pair - Pair))
                .ToList();
        }
    }
}