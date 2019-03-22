namespace TradingCardGame {
    public class Player {
        private const int InitialHealth = 30;
        public int Health => InitialHealth;
        public int Mana => 0;
        public Deck Deck { get; }

        private Player() {
            Deck = Deck.Create();
        }

        public static Player Create() => new Player();
    }
}