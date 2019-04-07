using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TradingCardGame.DuelAggregate;
using TradingCardGame.Tests.Helpers;

namespace TradingCardGame.Tests.DuelAggregate {
    public class DuelistShould {
        [Test]
        public void draw_top_deck_card() {
            var duelist = GivenADuelist()
                .WithDeck(
                    GivenADeck()
                        .WithCards(3, 1)
                        .WithCards(2, 2)
                        .WithCards(3, 1))
                .Build();

            duelist.DrawCard();

            var drawedCard = new Card(3);
            duelist.Hand.Single().Should().Be(drawedCard);
            duelist.Deck.Should().HaveCount(3);
            duelist.Deck.Last().Should().NotBe(new Card(3));
            duelist.Deck.Count(card => card.ManaCost is 3).Should().Be(1);
        }

        [Test]
        public void create_a_duelist_with_initial_state() {
            var deck = Deck.Create();
            var duelist = Duelist.Create("duelistId", deck);

            duelist.Health.Should().Be(30);
            duelist.Deck.Should().BeEquivalentTo(deck.Cards);
            duelist.Hand.Should().BeEmpty();
            duelist.ManaSlots.Should().Be(0);
            duelist.Mana.Should().Be(0);
        }

        private static DeckBuilder GivenADeck() {
            return new DeckBuilder();
        }

        private static DuelistBuilder GivenADuelist() {
            return new DuelistBuilder();
        }
    }
}