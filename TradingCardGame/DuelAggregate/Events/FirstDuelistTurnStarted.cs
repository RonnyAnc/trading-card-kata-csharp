using System;

namespace TradingCardGame.DuelAggregate.Events {
    public class FirstDuelistTurnStarted : DomainEvent, IEquatable<FirstDuelistTurnStarted> {
        public string DuelId { get; }
        public string DuelistId { get; }

        public FirstDuelistTurnStarted(string duelId, string duelistId) {
            DuelId = duelId;
            DuelistId = duelistId;
        }

        public bool Equals(FirstDuelistTurnStarted other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId) && string.Equals(DuelistId, other.DuelistId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FirstDuelistTurnStarted) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((DuelId != null ? DuelId.GetHashCode() : 0) * 397) ^ (DuelistId != null ? DuelistId.GetHashCode() : 0);
            }
        }
    }
}