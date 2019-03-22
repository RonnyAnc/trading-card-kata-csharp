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
        public void deck_must_have_cards_with_same_mana_cost_than_damage(int manaCost, int amountOfCards) {
            var deck = new Deck();

            var damage = manaCost;
            var dudCards = deck.Cards.Where(card => card.ManaCost == manaCost && card.Damage == damage).ToList();
            dudCards.Should().HaveCount(amountOfCards);
        }
    }
}