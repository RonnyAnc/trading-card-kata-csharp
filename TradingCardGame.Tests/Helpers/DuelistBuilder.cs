using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Win32.SafeHandles;
using TradingCardGame.DuelAggregate;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.Tests.Helpers {
    public class DuelistBuilder {
        private string id;
        private int manaSlots;
        private int mana;
        private ReadOnlyCollection<CardState> deck;
        private ReadOnlyCollection<CardState> hand;

        public DuelistState BuildState() {
            return new DuelistState(this.id, this.manaSlots, this.mana, this.deck, this.hand);
        }

        public Duelist Build() {
            var deckCards = deck.Select(card => card.ToValueObject()).ToList();
            return Duelist.Restore(id, Deck.Restore(deckCards));
        }

        public DuelistBuilder InitialDuelistState(string duelistId) {
            id = duelistId;
            manaSlots = 0;
            mana = 0;
            deck = DeckBuilder.CompletedDeck();
            hand = EmptyHand();
            return this;
        }

        private static ReadOnlyCollection<CardState> EmptyHand() {
            return new List<CardState>().AsReadOnly();
        }

        public DuelistBuilder WithDeck(DeckBuilder deck) {
            this.deck = deck.BuildState();
            return this;
        }
    }
}