using System;

namespace TradingCardGame.Duel.Events {
    public class DuelStarted : DomainEvent, IEquatable<DuelStarted> {
        public string DuelId { get; }

        public DuelStarted(string duelId) {
            DuelId = duelId;
        }

        public bool Equals(DuelStarted other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DuelStarted) obj);
        }

        public override int GetHashCode() {
            return (DuelId != null ? DuelId.GetHashCode() : 0);
        }
    }
}