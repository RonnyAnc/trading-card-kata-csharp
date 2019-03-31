using TradingCardGame.Duel.State;

namespace TradingCardGame.Duel {
    internal class Turn : TurnState {
        public string DuelistId { get; }

        public Turn(string duelistId) {
            DuelistId = duelistId;
        }
    }
}