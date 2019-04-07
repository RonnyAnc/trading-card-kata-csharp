using System;

namespace TradingCardGame.DuelAggregate.Events {
    public class HandSizeApproved : DomainEvent, IEquatable<HandSizeApproved> {
        public string DuelId { get; }
        public string DuelistId { get; }
        public int CardsInHand { get; }

        public HandSizeApproved(string duelId, string duelistId, int cardsInHand) {
            DuelId = duelId;
            DuelistId = duelistId;
            CardsInHand = cardsInHand;
        }

        public bool Equals(HandSizeApproved other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId) && string.Equals(DuelistId, other.DuelistId) && CardsInHand == other.CardsInHand;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HandSizeApproved) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (DuelId != null ? DuelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DuelistId != null ? DuelistId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ CardsInHand;
                return hashCode;
            }
        }
    }
}