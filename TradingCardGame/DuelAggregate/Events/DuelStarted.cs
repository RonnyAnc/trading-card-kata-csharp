using System;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate.Events {
    public class DuelStarted : DomainEvent, IEquatable<DuelStarted> {
        public DuelistState FirstDuelist { get; }
        public DuelistState SecondDuelist { get; }
        public string DuelId { get; }

        public DuelStarted(string duelId, DuelistState firstDuelist, DuelistState secondDuelist) {
            FirstDuelist = firstDuelist;
            SecondDuelist = secondDuelist;
            this.DuelId = duelId;
        }

        public bool Equals(DuelStarted other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(FirstDuelist, other.FirstDuelist) && Equals(SecondDuelist, other.SecondDuelist) && string.Equals(DuelId, other.DuelId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DuelStarted) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (FirstDuelist != null ? FirstDuelist.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SecondDuelist != null ? SecondDuelist.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DuelId != null ? DuelId.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}