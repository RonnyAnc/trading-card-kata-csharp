using LanguageExt;

namespace TradingCardGame.DuelAggregate.State {
    public class TurnState {
        public string DuelistId { get; }
        public bool IsInDecisionPhase { get; }

        public TurnState(string duelistId, bool isInDecisionPhase = false) {
            DuelistId = duelistId;
            IsInDecisionPhase = isInDecisionPhase;
        }

        internal Turn ToValueObject() {
            return new Turn(DuelistId);
        }

        public static TurnState From(Option<Turn> turn) {
            return turn
                .Some(t => new TurnState(t.DuelistId, t.IsInDecisionPhase))
                .None(() => new TurnState(null, false));
        }
    }
}