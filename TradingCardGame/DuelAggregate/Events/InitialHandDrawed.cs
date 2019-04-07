using System;
using System.Collections.ObjectModel;
using System.Linq;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate.Events {
    public class InitialHandDrawed : DomainEvent, IEquatable<InitialHandDrawed> {
        public string DuelId { get; }
        public string DuelistId { get; }
        public ReadOnlyCollection<CardState> InitialHand { get; }

        public InitialHandDrawed(string duelId, string duelistId, ReadOnlyCollection<CardState> initialHand) {
            DuelId = duelId;
            DuelistId = duelistId;
            InitialHand = initialHand;
        }

        public bool Equals(InitialHandDrawed other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId) && string.Equals(DuelistId, other.DuelistId) && InitialHand.SequenceEqual(other.InitialHand);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((InitialHandDrawed) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (DuelId != null ? DuelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DuelistId != null ? DuelistId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InitialHand != null ? InitialHand.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}