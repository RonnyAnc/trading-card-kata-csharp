using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TradingCardGame.DuelAggregate {
    internal class Deck {
        private readonly List<Card> cards;
        internal ReadOnlyCollection<Card> Cards => cards.AsReadOnly();

        public Deck() {
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
            var shuffledCards = new List<Card>();
            var random = new Random();
            while (orderedCards.Count > 0) {
                var randomIndex = random.Next(0, orderedCards.Count);
                shuffledCards.Add(orderedCards[randomIndex]);
                orderedCards.RemoveAt(randomIndex);
            }
            return shuffledCards;
        }

        private static IEnumerable<Card> CreateCards(int manaCost, int amountOfCards) {
            return Enumerable.Repeat(new Card(manaCost), amountOfCards);
        }
    }
}