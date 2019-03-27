using System;

namespace TradingCardGame {
    public class AllDuelistsJoined : DomainEvent, IEquatable<AllDuelistsJoined> {
        public string DuelId { get; }

        public AllDuelistsJoined(string duelId) {
            DuelId = duelId;
        }

        public bool Equals(AllDuelistsJoined other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AllDuelistsJoined) obj);
        }

        public override int GetHashCode() {
            return (DuelId != null ? DuelId.GetHashCode() : 0);
        }
    }
}