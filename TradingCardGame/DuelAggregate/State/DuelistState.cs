using System.Collections.ObjectModel;
using System.Linq;

namespace TradingCardGame.DuelAggregate.State {
    public class DuelistState {
        public string Id { get; }
        public int Health { get; }
        public int ManaSlots { get; }
        public int Mana { get; }
        public ReadOnlyCollection<CardState> DeckCards { get; }
        public ReadOnlyCollection<CardState> Hand { get; }

        public DuelistState(string id, int health, int manaSlots, int mana, ReadOnlyCollection<CardState> deckCards, ReadOnlyCollection<CardState> hand) {
            Id = id;
            Health = health;
            ManaSlots = manaSlots;
            Mana = mana;
            DeckCards = deckCards;
            Hand = hand;
        }

        internal static DuelistState From(Duelist duelist) {
            var deck = duelist.Deck.Select(CardState.From).ToList().AsReadOnly();
            var hand = duelist.Hand.Select(CardState.From).ToList().AsReadOnly();
            return new DuelistState(duelist.Id, duelist.Health, duelist.ManaSlots, duelist.Mana, deck, hand);
        }

        internal Duelist ToEntity() {
            var hand = Hand.Select(c => c.ToValueObject()).ToList();
            var deck = Deck.Restore(DeckCards.Select(c => c.ToValueObject()).ToList());
            return Duelist.Restore(Id, Health, Mana, ManaSlots, deck, hand);
        }
    }
}