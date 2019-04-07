using System;

namespace TradingCardGame.DuelAggregate.Events {
    public class DecisionPhaseStarted : DomainEvent, IEquatable<DecisionPhaseStarted> {
        public string DuelId { get; }
        public string CurrentDuelistId { get; }

        public DecisionPhaseStarted(string duelId, string currentDuelistId) {
            DuelId = duelId;
            CurrentDuelistId = currentDuelistId;
        }

        public bool Equals(DecisionPhaseStarted other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId) && string.Equals(CurrentDuelistId, other.CurrentDuelistId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DecisionPhaseStarted) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((DuelId != null ? DuelId.GetHashCode() : 0) * 397) ^ (CurrentDuelistId != null ? CurrentDuelistId.GetHashCode() : 0);
            }
        }
    }
}