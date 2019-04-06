using System.Collections.Generic;

namespace TradingCardGame.DuelAggregate.State {
    public class DeckState {
        public List<CardState> Cards { get; }

        public DeckState(List<CardState> cards) {
            Cards = cards;
        }

        internal static DeckState From(Deck deck) {
            throw new System.NotImplementedException();
        }
    }
}