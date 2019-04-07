using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TradingCardGame.DuelAggregate.State;
using TradingCardGame.Tests.Helpers;

namespace TradingCardGame.Tests.DuelAggregate {
    public class DuelInDecisionPhaseShould {
        private const string DuelId = "anyId";
        private const string FirstDuelistId = "firstDuelist";
        private const string SecondDuelistId = "secondDuelist";

        [Test, Ignore("WIP")]
        public void play_a_card_from_turned_duelist_hand_damaging_other_duelist() {
            var maxMana = 10;
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(
                    new DuelistBuilder()
                        .WithDeck(new DeckBuilder().Empty())
                        .WithHand(new HandBuilder().Empty())
                        .WithMana(maxMana)
                        .WithManaSlots(maxMana)
                    )
                .WithSecondDuelist(
                    new DuelistBuilder()
                        .WithDeck(new DeckBuilder()
                            .WithCards(6, 1)
                            .WithCards(5, 2))
                        .WithHand(new HandBuilder()
                            .WithCard(2)
                            .WithCard(2)
                        )
                        .WithMana(maxMana)
                        .WithManaSlots(maxMana)
                    )
                .WithTurn(SecondDuelistId)
                .Build();

            var playedCard = new CardState(2, 2);
            duel.PlayCard(SecondDuelistId, playedCard);

            var remainingCard = new CardState(2, 2);
            duel.State.SecondDuelist.Hand.Single().Should().Be(remainingCard);
            duel.State.SecondDuelist.Mana.Should().Be(maxMana - playedCard.ManaCost);
        }

        private static DuelBuilder GivenADuel() {
            return new DuelBuilder();
        }
    }

    public class HandBuilder {
        private List<CardState> cards = new List<CardState>();

        public HandBuilder WithCard(int manaCost) {
            cards.Add(new CardState(manaCost, manaCost));
            return this;
        }

        public ReadOnlyCollection<CardState> Build() {
            return cards.AsReadOnly();
        }

        public HandBuilder Empty() {
            cards = new List<CardState>();
            return this;
        }
    }
}