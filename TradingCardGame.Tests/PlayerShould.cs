using FluentAssertions;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class PlayerShould {
        private const int InitialHealth = 30;

        [Test]
        public void start_with_full_health() {
            var player = Player.Create();
            player.Health.Should().Be(InitialHealth);
        }

        [Test]
        public void start_with_no_mana() {
            var player = Player.Create();
            player.Mana.Should().Be(0);
        }
    }
}