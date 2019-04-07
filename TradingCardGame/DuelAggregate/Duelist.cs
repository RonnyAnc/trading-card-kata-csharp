using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TradingCardGame.DuelAggregate {
    public class Duelist {
        private const int InitialHealth = 30;
        private List<ManaSlot> manaSlots;
        private Deck deck;
        private List<Card> hand;
        public int Health { get; private set; }
        public string Id { get; }
        public int ManaSlots => manaSlots.Count;
        public int Mana => manaSlots.Filter(slot => slot.IsFilled).Count();
        public ReadOnlyCollection<Card> Deck => deck.Cards;
        public ReadOnlyCollection<Card> Hand => hand.AsReadOnly();

        public static Duelist Create(string id, Deck deck) {
            return new Duelist(id, InitialHealth, new List<ManaSlot>(),  deck, new List<Card>());
        }

        private Duelist(string id, int health, List<ManaSlot> manaSlots, Deck deck, List<Card> hand) {
            Id = id;
            Health = health;
            this.deck = deck;
            this.manaSlots = manaSlots;
            this.hand = hand;
        }

        internal void IncrementManaSlot() {
            manaSlots.Add(ManaSlot.Empty());
        }

        internal void RefillMana() {
            manaSlots = manaSlots.Map(_ => ManaSlot.Filled()).ToList();
        }

        public static Duelist Restore(string id, int health, int mana, int manaSlots, Deck deck, IList<Card> hand) {
            var completeManaSlots = new List<ManaSlot>()
                .Concat(Enumerable.Repeat(ManaSlot.Filled(), mana))
                .Concat(Enumerable.Repeat(ManaSlot.Empty(), manaSlots - mana))
                .ToList();
            return new Duelist(id, health, completeManaSlots, deck, hand.ToList());
        }

        public void DrawCard() {
            hand.Add(deck.TakeTopCard());
        }

        public void PlayCard(Card card) {
            hand.Remove(card);
            ConsumeMana(card.ManaCost);
        }

        private void ConsumeMana(int manaToConsume) {
            manaSlots.RemoveRange(0, manaToConsume);
            var consumedMana = manaToConsume;
            manaSlots.AddRange(Enumerable.Repeat(ManaSlot.Empty(), consumedMana));
        }

        public void ReceiveDamage(int damage) {
            Health = Health - damage;
        }
    }
}