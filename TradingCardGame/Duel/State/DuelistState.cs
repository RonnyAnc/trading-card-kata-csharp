namespace TradingCardGame.Duel.State {
    public interface DuelistState {
        string Id { get; }
        int ManaSlots { get; }
    }
}