using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SharedKernel;

namespace TradingCardGame.DuelAggregate {
    internal class Deck {
        private readonly List<Card> cards;
        internal ReadOnlyCollection<Card> Cards => cards.AsReadOnly();

        public static Deck Create() {
            return new Deck();
        }

        private Deck() {
            var orderedCards = new List<Card>()
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
            cards = ShuffleCards(orderedCards);
        }

        private static List<Card> ShuffleCards(IList<Card> orderedCards) {
            return orderedCards.Shuffle().ToList();
        }

        private static IEnumerable<Card> CreateCards(int manaCost, int amountOfCards) {
            return Enumerable.Repeat(new Card(manaCost), amountOfCards);
        }
    }
}