using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TradingCardGame.DuelAggregate {
    internal class Deck {
        private readonly List<Card> cards;
        internal ReadOnlyCollection<Card> Cards => cards.AsReadOnly();

        public Deck() {
            cards = new List<Card>()
                .Concat(CreateCards(0, 2))
                .Concat(CreateCards(1, 2))
                .Concat(CreateCards(2, 3))
                .Concat(CreateCards(3, 4))
                .Concat(CreateCards(4, 3))
                .Concat(CreateCards(5, 2))
                .Concat(CreateCards(6, 2))
                .Concat(CreateCards(7, 1))
                .Concat(CreateCards(8, 1))
                .ToList();
        }

        private static IEnumerable<Card> CreateCards(int manaCost, int amountOfCards) {
            return Enumerable.Repeat(new Card(manaCost), amountOfCards);
        }
    }
}