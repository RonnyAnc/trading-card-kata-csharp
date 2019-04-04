using System.Collections.Generic;
using System.Linq;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    internal class Duelist {
        private List<ManaSlot> manaSlots;
        public string Id { get; }
        public int ManaSlots => manaSlots.Count;
        public int Mana => manaSlots.Filter(slot => slot.IsFilled).Count();
        public DeckState Deck { get; private set; }
        public List<CardState> Hand { get; private set; }

        internal Duelist(string id, DeckState deck = null) {
            Id = id;
            Deck = deck is null ? null : new DeckState(new List<CardState>(deck?.Cards));
            manaSlots = new List<ManaSlot>();
            Hand = new List<CardState>();
        }

        internal void IncrementManaSlot() {
            manaSlots.Add(ManaSlot.Empty());
        }

        internal void RefillMana() {
            manaSlots = manaSlots.Map(_ => ManaSlot.Filled()).ToList();
        }

        public void DrawCard() {
            // TODO: deck shouldn't be a DeckState but only a Deck
            var card = Deck.Cards.First();
            Deck.Cards.Remove(card);
            Hand.Add(card);
        }
    }
}