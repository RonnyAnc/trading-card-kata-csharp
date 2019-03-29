using System;

namespace TradingCardGame {
    public class ManaSlotSet : DomainEvent, IEquatable<ManaSlotSet> {
        public string DuelId { get; }
        public string DuelistId { get; }
        public int ManaSlots { get; }

        public ManaSlotSet(string duelId, string duelistId, int manaSlots) {
            DuelId = duelId;
            DuelistId = duelistId;
            ManaSlots = manaSlots;
        }

        public bool Equals(ManaSlotSet other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId) && string.Equals(DuelistId, other.DuelistId) && ManaSlots == other.ManaSlots;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ManaSlotSet) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (DuelId != null ? DuelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DuelistId != null ? DuelistId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ManaSlots;
                return hashCode;
            }
        }
    }
}