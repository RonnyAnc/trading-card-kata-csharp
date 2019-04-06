using System.Collections.Generic;
using System.Collections.ObjectModel;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.Tests.Helpers {
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
            deck = DeckFactory.CompletedDeck();
            hand = EmptyHand();
            return this;
        }

        private static ReadOnlyCollection<CardState> EmptyHand() {
            return new List<CardState>().AsReadOnly();
        }
    }
}