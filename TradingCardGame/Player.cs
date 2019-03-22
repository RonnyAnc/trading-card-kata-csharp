using System.Collections.Generic;

namespace TradingCardGame {
    public class Player {
        private const int InitialHealth = 30;
        public int Health => InitialHealth;
        public int Mana => 0;
        public List<Card> CardsInDeck => deck.Cards;
        public string Id { get; }

        private readonly Deck deck;

        private Player(string id) {
            Id = id;
            deck = Deck.Create();
        }

        private Player() {
            deck = Deck.Create();
        }

        public static Player Create(string firstPlayerId) => new Player(firstPlayerId);
        public static Player Create() => new Player();
    }
}