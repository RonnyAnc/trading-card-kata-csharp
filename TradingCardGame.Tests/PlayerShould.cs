using FluentAssertions;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class PlayerShould {
        [Test]
        public void start_with_full_health() {
            var player = Player.Create();
            player.Health.Should().Be(30);
        }
    }

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