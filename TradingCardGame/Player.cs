namespace TradingCardGame {
    public class Player {
        private const int InitialHealth = 30;
        public int Health { get; }
        public int Mana => 0;

        private Player() {
            Health = InitialHealth;
        }

        public static Player Create() {
            return new Player();
        }
    }
}