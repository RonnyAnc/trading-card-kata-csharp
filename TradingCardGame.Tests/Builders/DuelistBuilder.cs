using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.Tests.Builders {
    public class DuelistBuilder {
        private string id;
        private int manaSlots;
        private int mana;
        private ReadOnlyCollection<CardState> deck;
        private ReadOnlyCollection<CardState> hand;

        public DuelistState Build() {
            return new DuelistState(this.id, this.manaSlots, this.mana, this.deck, this.hand);
        }

        public DuelistBuilder InitialDuelistState(string duelistId) {
            id = duelistId;
            manaSlots = 0;
            mana = 0;
            deck = CompletedDeck();
            hand = EmptyHand();
            return this;
        }

        private static ReadOnlyCollection<CardState> EmptyHand() {
            return new List<CardState>().AsReadOnly();
        }

        private static ReadOnlyCollection<CardState> CompletedDeck() {
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