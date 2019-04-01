namespace TradingCardGame.DuelAggregate.State {
    public interface DuelistState {
        string Id { get; }
        int ManaSlots { get; }
        int Mana { get; }
    }
}