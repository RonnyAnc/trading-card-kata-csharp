using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    public class Turn {
        public string DuelistId { get; }
        public bool IsInDecisionPhase { get; private set; }


        public Turn(string duelistId) {
            DuelistId = duelistId;
            IsInDecisionPhase = false;
        }

        public void StartDecisionPhase() {
            IsInDecisionPhase = true;
        }
    }
}