using System;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate.Events {
    public class CardDrawed : DomainEvent, IEquatable<CardDrawed> {
        public string DuelId { get; }
        public string DuelistId { get; }
        public CardState DrawedCard { get; }

        public CardDrawed(string duelId, string duelistId, CardState drawedCard) {
            DuelId = duelId;
            DuelistId = duelistId;
            DrawedCard = drawedCard;
        }

        public bool Equals(CardDrawed other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId) && string.Equals(DuelistId, other.DuelistId) && Equals(DrawedCard, other.DrawedCard);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CardDrawed) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (DuelId != null ? DuelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DuelistId != null ? DuelistId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DrawedCard != null ? DrawedCard.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}