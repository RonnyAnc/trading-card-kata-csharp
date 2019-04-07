using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TradingCardGame.DuelAggregate {
    public class Duelist {
        private const int InitialHealth = 30;
        private List<ManaSlot> manaSlots;
        private Deck deck;
        private List<Card> hand;
        public int Health { get; }
        public string Id { get; }
        public int ManaSlots => manaSlots.Count;
        public int Mana => manaSlots.Filter(slot => slot.IsFilled).Count();
        public ReadOnlyCollection<Card> Deck => deck.Cards;
        public ReadOnlyCollection<Card> Hand => hand.AsReadOnly();

        public static Duelist Create(string id, Deck deck) {
            return new Duelist(id, InitialHealth, deck);
        }

        private Duelist(string id, int health, Deck deck) {
            Id = id;
            Health = health;
            this.deck = deck;
            manaSlots = new List<ManaSlot>();
            hand = new List<Card>();
        }

        internal void IncrementManaSlot() {
            manaSlots.Add(ManaSlot.Empty());
        }

        internal void RefillMana() {
            manaSlots = manaSlots.Map(_ => ManaSlot.Filled()).ToList();
        }

        public static Duelist Restore(string id, Deck deck) {
            return new Duelist(id, InitialHealth, deck);
        }

        public void DrawCard() {
            hand.Add(deck.TakeTopCard());
        }
    }
}