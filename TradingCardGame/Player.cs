using System.Collections.Generic;

namespace TradingCardGame {
    public class Player {
        private const int InitialHealth = 30;
        public int Health => InitialHealth;
        public int Mana => 0;
        public List<Card> CardsInDeck => deck.Cards;
        private readonly Deck deck;

        private Player() {
            deck = Deck.Create();
        }

        public static Player Create() => new Player();
    }
}