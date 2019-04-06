namespace TradingCardGame.DuelAggregate.State {
    public class TurnState {
        public string DuelistId { get; }

        public TurnState(string duelistId) {
            DuelistId = duelistId;
        }

        internal Turn ToValueObject() {
            return new Turn(DuelistId);
        }
    }
}