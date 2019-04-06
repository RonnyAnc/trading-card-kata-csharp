using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    internal class Turn {
        public string DuelistId { get; }

        public Turn(string duelistId) {
            DuelistId = duelistId;
        }
    }
}