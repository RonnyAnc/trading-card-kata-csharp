using System;

namespace TradingCardGame.DuelAggregate {
    internal class Card : IEquatable<Card> {
        public int ManaCost { get; }
        public int Damage { get; }

        public Card(int manaCost) {
            ManaCost = manaCost;
            Damage = manaCost;
        }

        public bool Equals(Card other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ManaCost == other.ManaCost && Damage == other.Damage;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Card) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (ManaCost * 397) ^ Damage;
            }
        }
    }
}