using System;

namespace TradingCardGame.DuelCall.Events {
    public class DuelistJoined : DomainEvent, IEquatable<DuelistJoined> {
        public string DuelId { get; }
        public string DuelistId { get; }

        public DuelistJoined(string duelId, string duelistId) {
            DuelId = duelId;
            DuelistId = duelistId;
        }

        public bool Equals(DuelistJoined other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId) && string.Equals(DuelistId, other.DuelistId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DuelistJoined) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((DuelId != null ? DuelId.GetHashCode() : 0) * 397) ^ (DuelistId != null ? DuelistId.GetHashCode() : 0);
            }
        }
    }
}