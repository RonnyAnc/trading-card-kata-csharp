using System.Collections.Generic;

namespace TradingCardGame.DuelAggregate.State {
    public class DuelistState {
        public string Id { get; }
        public int ManaSlots { get; }
        public int Mana { get; }
        public DeckState Deck { get; }
        public List<CardState> Hand { get; }

        public DuelistState(string id, int manaSlots, DeckState deck) {
            Id = id;
            ManaSlots = manaSlots;
            Deck = deck;
        }

        private DuelistState(string id, int manaSlots, int mana, DeckState deck, List<CardState> hand) {
            Id = id;
            ManaSlots = manaSlots;
            Mana = mana;
            Deck = deck;
            Hand = hand;
        }

        internal static DuelistState From(Duelist duelist) {
            return new DuelistState(duelist.Id, duelist.ManaSlots, duelist.Mana, duelist.Deck, duelist.Hand);
        }
    }
}