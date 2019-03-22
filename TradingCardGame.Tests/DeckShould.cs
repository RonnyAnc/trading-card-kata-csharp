using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class DeckShould {
        [Test]
        public void deck_must_have_20_cards() {
            var deck = new Deck();
            deck.Cards.Should().HaveCount(20);
        }
    }

    public class Deck {
        public List<Card> Cards { get; }

        public Deck() {
            Cards = new List<Card>();
            for (var i = 0; i < 20; i++) {
                Cards.Add(new Card());
            }
        }
    }

    public class Card { }
}