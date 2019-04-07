using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.Tests.Helpers {
    public class DeckBuilder {
        private List<CardState> cards = new List<CardState>();

        public static ReadOnlyCollection<CardState> CompletedDeck() {
            return new List<CardState>()
                .Concat(Enumerable.Repeat(Card(0), 2))
                .Concat(Enumerable.Repeat(Card(1), 2))
                .Concat(Enumerable.Repeat(Card(2), 3))
                .Concat(Enumerable.Repeat(Card(3), 4))
                .Concat(Enumerable.Repeat(Card(4), 3))
                .Concat(Enumerable.Repeat(Card(5), 2))
                .Concat(Enumerable.Repeat(Card(6), 2))
                .Concat(Enumerable.Repeat(Card(7), 1))
                .Concat(Enumerable.Repeat(Card(8), 1))
                .ToList()
                .AsReadOnly();
        }

        private static CardState Card(int manaCost) {
            return new CardState(manaCost, manaCost);
        }

        public ReadOnlyCollection<CardState> BuildState() {
            return cards.AsReadOnly();
        }

        public DeckBuilder WithCards(int manaCost, int amountOfCards) {
            cards.AddRange(Enumerable.Repeat(new CardState(manaCost, manaCost), amountOfCards));
            return this;
        }

        public DeckBuilder Empty() {
            cards = new List<CardState>();
            return this;
        }
    }
}