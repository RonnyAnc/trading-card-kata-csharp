namespace TradingCardGame {
    public class Player {
        private const int InitialHealth = 30;
        public int Health { get; }

        private Player() {
            Health = InitialHealth;
        }

        public static Player Create() {
            return new Player();
        }
    }
}