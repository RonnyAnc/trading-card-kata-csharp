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
            cards = new List<Card>();
            var r = new Random();
            while (orderedCards.Count > 0)
            {
                var randomIndex = r.Next(0, orderedCards.Count);
                cards.Add(orderedCards[randomIndex]); 
                orderedCards.RemoveAt(randomIndex);
            }
        }

        private static IEnumerable<Card> CreateCards(int manaCost, int amountOfCards) {
            return Enumerable.Repeat(new Card(manaCost), amountOfCards);
        }
    }
}