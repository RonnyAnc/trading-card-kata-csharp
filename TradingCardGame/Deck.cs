using System.Collections.Generic;
using System.Linq;

namespace TradingCardGame {
    public class Deck {
        private const int NumberOfDudCards = 2;
        private const int NumberOfTotalCards = 20;
        public List<Card> Cards { get; }

        public Deck() {
            Cards = new List<Card>()
                .Concat(Enumerable.Repeat(new Card(0), NumberOfDudCards))
                .Concat(Enumerable.Repeat(new Card(10), NumberOfTotalCards - NumberOfDudCards))
                .ToList();
        }
    }
}