using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TradingCardGame.DuelAggregate;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.Tests.DuelAggregate {
    public class DeckShould {
        [Test]
        public void be_created_with_cards() {
            var deck = Deck.Create();

            var expectedDeck = new List<Card>()
                .Concat(Enumerable.Repeat(new Card(0), 2))
                .Concat(Enumerable.Repeat(new Card(1), 2))
                .Concat(Enumerable.Repeat(new Card(2), 3))
                .Concat(Enumerable.Repeat(new Card(3), 4))
                .Concat(Enumerable.Repeat(new Card(4), 3))
                .Concat(Enumerable.Repeat(new Card(5), 2))
                .Concat(Enumerable.Repeat(new Card(6), 2))
                .Concat(Enumerable.Repeat(new Card(7), 1))
                .Concat(Enumerable.Repeat(new Card(8), 1))
                .ToList();
            deck.Cards.Should().BeEquivalentTo(expectedDeck);
        }

        [Test]
        public void be_shuffled_when_created() {
            var deckOne = Deck.Create();
            var deckTwo = Deck.Create();

            ShouldNotBeInSameOrder(deckOne, deckTwo);
        }

        private static void ShouldNotBeInSameOrder(Deck aDeck, Deck otherDeck) {
            var firstDeck = string.Join("", aDeck.Cards.Select(c => c.ManaCost));
            var secondDeck = string.Join("", otherDeck.Cards.Select(c => c.ManaCost));
            firstDeck.Should().NotBe(secondDeck);
        }
    }
}