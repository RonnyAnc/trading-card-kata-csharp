using System.Collections.ObjectModel;
using System.Linq;

namespace TradingCardGame.DuelAggregate.State {
    public class DuelistState {
        public string Id { get; }
        public int ManaSlots { get; }
        public int Mana { get; }
        public ReadOnlyCollection<CardState> DeckCards { get; }
        public ReadOnlyCollection<CardState> Hand { get; }

        public DuelistState(string id, int manaSlots, int mana, ReadOnlyCollection<CardState> deckCards, ReadOnlyCollection<CardState> hand) {
            Id = id;
            ManaSlots = manaSlots;
            Mana = mana;
            DeckCards = deckCards;
            Hand = hand;
        }

        internal static DuelistState From(Duelist duelist) {
            var deck = duelist.Deck.Select(CardState.From).ToList().AsReadOnly();
            var hand = duelist.Hand.Select(CardState.From).ToList().AsReadOnly();
            return new DuelistState(duelist.Id, duelist.ManaSlots, duelist.Mana, deck, hand);
        }

        internal Duelist ToEntity() {
            return Duelist.Restore(Id, Deck.Restore(DeckCards.Select(c => c.ToEntity()).ToList()));
        }
    }
}