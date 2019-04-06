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

        public static Duelist Create(string id, Deck deck) {
            return new Duelist(id, deck);
        }

        private Duelist(string id, Deck deck) {
            Id = id;
            this.deck = deck;
            manaSlots = new List<ManaSlot>();
        }

        internal void IncrementManaSlot() {
            manaSlots.Add(ManaSlot.Empty());
        }

        internal void RefillMana() {
            manaSlots = manaSlots.Map(_ => ManaSlot.Filled()).ToList();
        }
    }
}