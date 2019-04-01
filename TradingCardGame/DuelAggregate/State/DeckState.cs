using System.Collections.Generic;

namespace TradingCardGame.DuelAggregate.State {
    public interface DeckState {
        List<CardState> Cards { get; }
    }
}