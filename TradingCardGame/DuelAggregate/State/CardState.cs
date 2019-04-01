namespace TradingCardGame.DuelAggregate.State {
    public interface CardState {
        int ManaCost { get; }
        int Damage { get; }
    }
}