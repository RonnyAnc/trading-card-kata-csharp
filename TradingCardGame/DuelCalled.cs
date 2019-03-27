using System;

namespace TradingCardGame {
    public class DuelCalled : DomainEvent, IEquatable<DuelCalled> {
        private readonly string duelId;

        public DuelCalled(string duelId) {
            this.duelId = duelId;
        }

        public bool Equals(DuelCalled other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(duelId, other.duelId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DuelCalled) obj);
        }

        public override int GetHashCode() {
            return (duelId != null ? duelId.GetHashCode() : 0);
        }
    }
}