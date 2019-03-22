using System.Collections.Generic;
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

        [Test]
        public void deck_must_have_2_dud_cards() {
            var deck = new Deck();

            var dudCards = deck.Cards.Where(card => card.ManaCost == 0 && card.Damage == 0).ToList();
            dudCards.Should().HaveCount(2);
        }
    }

    public class Deck {
        public List<Card> Cards { get; }

        public Deck() {
            Cards = new List<Card>()
                .Concat(Enumerable.Repeat(new Card(0), 2))
                .Concat(Enumerable.Repeat(new Card(10), 18))
                .ToList();
        }
    }

    public class Card {
        public int ManaCost { get; }
        public int Damage { get; }

        public Card(int manaCost) {
            ManaCost = manaCost;
            Damage = ManaCost;
        }
    }
}