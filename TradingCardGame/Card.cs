namespace TradingCardGame {
    public class Card {
        public int ManaCost { get; }
        public int Damage { get; }

        public Card(int manaCost) {
            ManaCost = manaCost;
            Damage = ManaCost;
        }
    }
}