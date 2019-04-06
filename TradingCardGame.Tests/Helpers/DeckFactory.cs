using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.Tests.Helpers {
    internal static class DeckFactory {
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
    }
}