using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class DeckShould {
        [Test]
        public void deck_must_have_20_cards() {
            var deck = new Deck();
            deck.Cards.Should().HaveCount(20);
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
            var deck = new Deck();

            var damage = manaCost;
            var cards = deck.Cards.Where(card => card.ManaCost == manaCost && card.Damage == damage).ToList();
            cards.Should().HaveCount(amountOfCards);
        }
    }
}