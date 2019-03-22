using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LanguageExt;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class TradingCardGameEngineShould {
        //TODO: probably move all players tests here

        [Test]
        public void start_a_game_where_players_start_with_initial_health() {
            var game = CardGame.Start("GameId", "FirstPlayer", "SecondPlayer");

            var gameStarted = game.DomainEvents.OfType<GameStarted>().Single();
            gameStarted.GameId.Should().Be("GameId");
            gameStarted.FirstPlayer.Id.Should().Be("FirstPlayer");
            gameStarted.FirstPlayer.Health.Should().Be(30);
            gameStarted.SecondPlayer.Id.Should().Be("SecondPlayer");
            gameStarted.SecondPlayer.Health.Should().Be(30);
        }

        [Test]
        public void start_a_game_where_both_players_has_no_mana() {
            var game = CardGame.Start("GameId", "FirstPlayer", "SecondPlayer");

            var gameStarted = game.DomainEvents.OfType<GameStarted>().Single();
            gameStarted.FirstPlayer.Mana.Should().Be(0);
            gameStarted.SecondPlayer.Mana.Should().Be(0);
        }


        [Test]
        public void have_start_with_a_deck_with_20_cards() {
            var game = CardGame.Start("GameId", "FirstPlayer", "SecondPlayer");

            var gameStarted = game.DomainEvents.OfType<GameStarted>().Single();
            gameStarted.FirstPlayer.CardsInDeck.Should().HaveCount(20);
            gameStarted.SecondPlayer.CardsInDeck.Should().HaveCount(20);
        }

        [TestCase(0, 2)]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        [TestCase(3, 4)]
        [TestCase(4, 3)]
        [TestCase(5, 2)]
        [TestCase(6, 2)]
        [TestCase(7, 1)]
        [TestCase(8, 1)]
        public void deck_must_have_N_cards_with_same_X_mana_cost_than_damage(int manaCost, int amountOfCards) {
            var game = CardGame.Start("GameId", "FirstPlayer", "SecondPlayer");

            var gameStarted = game.DomainEvents.OfType<GameStarted>().Single();
            var damage = manaCost;
            var firstPlayerCards = gameStarted
                .FirstPlayer
                .CardsInDeck
                .Filter(card => card.ManaCost == manaCost && card.Damage == damage).ToList();
            firstPlayerCards.Should().HaveCount(amountOfCards);
            var secondPlayerCards = gameStarted
                .SecondPlayer
                .CardsInDeck
                .Filter(card => card.ManaCost == manaCost && card.Damage == damage).ToList();
            secondPlayerCards.Should().HaveCount(amountOfCards);
        }
    }

    public class GameStarted : DomainEvent {
        public Player FirstPlayer { get; set; }
        public Player SecondPlayer { get; set; }
        public string GameId { get; set; }

        public GameStarted(string gameId, Player firstPlayer, Player secondPlayer) {
            GameId = gameId;
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
        }
    }

    public class CardGame {
        public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();

        public static CardGame Start(string gameId, string firstPlayerId, string secondPlayerId) {
            var cardGame = new CardGame();
            var firstPlayer = Player.Create(firstPlayerId);
            var secondPlayer = Player.Create(secondPlayerId);
            cardGame.DomainEvents.Add(new GameStarted(gameId, firstPlayer, secondPlayer));
            return cardGame;
        }
    }

    public abstract class DomainEvent { }
}