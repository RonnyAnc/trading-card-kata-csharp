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