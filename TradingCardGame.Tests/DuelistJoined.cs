using System;

namespace TradingCardGame.Tests {
    public class DuelistJoined : DomainEvent, IEquatable<DuelistJoined> {
        public string DuelistId { get; }

        public DuelistJoined(string duelistId) {
            DuelistId = duelistId;
        }

        public bool Equals(DuelistJoined other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelistId, other.DuelistId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DuelistJoined) obj);
        }

        public override int GetHashCode() {
            return (DuelistId != null ? DuelistId.GetHashCode() : 0);
        }
    }
}