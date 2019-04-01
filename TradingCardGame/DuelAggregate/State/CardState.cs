namespace TradingCardGame.DuelAggregate.State {
    public class CardState {
        public int ManaCost { get; }
        public int Damage { get; }

        public CardState(int manaCost, int damage) {
            ManaCost = manaCost;
            Damage = damage;
        }
    }
}