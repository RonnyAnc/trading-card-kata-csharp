using System.Linq;
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

        [Test]
        public void have_start_with_a_deck_with_20_cards() {
            var player = Player.Create();
            player.CardsInDeck.Should().HaveCount(20);
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
            var player = Player.Create();

            var damage = manaCost;
            var cards = player.CardsInDeck.Where(card => card.ManaCost == manaCost && card.Damage == damage).ToList();
            cards.Should().HaveCount(amountOfCards);
        }
    }
}