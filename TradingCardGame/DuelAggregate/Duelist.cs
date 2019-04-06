using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    internal class Duelist {
        private List<ManaSlot> manaSlots;
        private Deck deck;
        public string Id { get; }
        public int ManaSlots => manaSlots.Count;
        public int Mana => manaSlots.Filter(slot => slot.IsFilled).Count();
        public ReadOnlyCollection<Card> Deck => deck.Cards;

        internal Duelist(string id) {
            Id = id;
            manaSlots = new List<ManaSlot>();
        }

        internal void IncrementManaSlot() {
            manaSlots.Add(ManaSlot.Empty());
        }

        internal void RefillMana() {
            manaSlots = manaSlots.Map(_ => ManaSlot.Filled()).ToList();
        }

        internal void AssignDeck(Deck deck) {
            this.deck = deck;
        }
    }
}