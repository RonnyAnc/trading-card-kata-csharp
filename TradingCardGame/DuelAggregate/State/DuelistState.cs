using System.Collections.Generic;

namespace TradingCardGame.DuelAggregate.State {
    public interface DuelistState {
        string Id { get; }
        int ManaSlots { get; }
        int Mana { get; }
        DeckState Deck { get; }
        List<CardState> Hand { get; }
    }
}