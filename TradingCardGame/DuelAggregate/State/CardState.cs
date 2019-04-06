namespace TradingCardGame.DuelAggregate.State {
    public class CardState {
        public int ManaCost { get; }
        public int Damage { get; }

        public CardState(int manaCost, int damage) {
            ManaCost = manaCost;
            Damage = damage;
        }

        protected bool Equals(CardState other) {
            return ManaCost == other.ManaCost && Damage == other.Damage;
        }

        internal static CardState From(Card card) {
            return new CardState(card.ManaCost, card.Damage);
        }

        internal Card ToEntity() {
            return new Card(ManaCost);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CardState) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (ManaCost * 397) ^ Damage;
            }
        }
    }
}