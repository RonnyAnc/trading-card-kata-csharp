using System;

namespace TradingCardGame.DuelAggregate.Events {
    public class ManaRefilled : DomainEvent, IEquatable<ManaRefilled> {
        public string DuelId { get; }
        public string DuelistId { get; }
        public int Mana { get; }

        public ManaRefilled(string duelId, string duelistId, int mana) {
            DuelId = duelId;
            DuelistId = duelistId;
            Mana = mana;
        }

        public bool Equals(ManaRefilled other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId) && string.Equals(DuelistId, other.DuelistId) && Mana == other.Mana;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ManaRefilled) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (DuelId != null ? DuelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DuelistId != null ? DuelistId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Mana;
                return hashCode;
            }
        }
    }
}