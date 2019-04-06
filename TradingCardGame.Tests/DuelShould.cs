using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using LanguageExt;
using NUnit.Framework;
using TradingCardGame.DuelAggregate;
using TradingCardGame.DuelAggregate.Events;
using TradingCardGame.DuelAggregate.State;
using TradingCardGame.Tests.Builders;

namespace TradingCardGame.Tests {
    public class DuelShould {
        private const string DuelId = "anyId";
        private const string FirstDuelistId = "firstDuelist";
        private const string SecondDuelistId = "secondDuelist";

        [Test]
        public void prepare_a_duel_started_event_when_starting_a_duel() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.Events.Should().Contain(x => x.Equals(new DuelStarted(DuelId)));
        }

        [Test]
        public void start_a_duel_with_two_players() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.State.FirstDuelist.Id.Should().Be(FirstDuelistId);
            duel.State.SecondDuelist.Id.Should().Be(SecondDuelistId);
        }

        [Test]
        public void prepare_a_duelist_turn_started_event_when_starting_a_duel() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            duel.Events.Should().Contain(x => x.Equals(new DuelistTurnStarted(DuelId, FirstDuelistId)));
        }

        [Test]
        public void give_initial_turn_to_first_duelist() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            duel.State.Turn.DuelistId.Should().Be(FirstDuelistId);
        }


        [Test]
        public void prepare_mana_slot_set_event_when_setting_mana_slots_when_starting_a_duel() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            duel.Events.Should().Contain(x => x.Equals(new ManaSlotSet(DuelId, FirstDuelistId, 1)));
        }

        [Test]
        public void set_one_mana_slot_to_first_duelist_when_starting_a_duel() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            duel.State.FirstDuelist.ManaSlots.Should().Be(1);
        }

        [Test]
        public void refill_mana_to_first_duelist_when_starting_a_duel() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            duel.State.FirstDuelist.Mana.Should().Be(1);
        }

        [Test]
        public void prepare_mana_refilled_event_when_starting_a_turn() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            duel.Events.Should().Contain(x => x.Equals(new ManaRefilled(DuelId, FirstDuelistId, 1)));
        }

        [Test]
        public void start_with_complete_decks_for_each_player() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            var expectedDeck = new List<CardState>()
                .Concat(Enumerable.Repeat(Card(0), 2))
                .Concat(Enumerable.Repeat(Card(1), 2))
                .Concat(Enumerable.Repeat(Card(2), 3))
                .Concat(Enumerable.Repeat(Card(3), 4))
                .Concat(Enumerable.Repeat(Card(4), 3))
                .Concat(Enumerable.Repeat(Card(5), 2))
                .Concat(Enumerable.Repeat(Card(6), 2))
                .Concat(Enumerable.Repeat(Card(7), 1))
                .Concat(Enumerable.Repeat(Card(8), 1))
                .ToList();
            var firstDeck = duel.State.FirstDuelist.DeckCards;
            firstDeck.Should().BeEquivalentTo(expectedDeck);
            var secondDeck = duel.State.SecondDuelist.DeckCards;
            secondDeck.Should().BeEquivalentTo(expectedDeck);
        }

        [Test] // TODO: move to a DeckShould test class
        public void start_with_decks_shuffled_decks() {
            var duelOne = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);
            var duelTwo = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            var firstDeckFromOne = duelOne.State.FirstDuelist.DeckCards;
            var secondDeckFromOne = duelOne.State.SecondDuelist.DeckCards;
            ShouldNotBeInSameOrder(firstDeckFromOne, secondDeckFromOne);
            var firstDeckFromTwo = duelTwo.State.FirstDuelist.DeckCards;
            var secondDeckFromTwo = duelTwo.State.SecondDuelist.DeckCards;
            ShouldNotBeInSameOrder(firstDeckFromOne, firstDeckFromTwo);
            ShouldNotBeInSameOrder(secondDeckFromOne, secondDeckFromTwo);
        }

        [Test, Ignore("Pending Refactor")]
        public void draw_three_cards_for_first_turn() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.State.FirstDuelist.Hand.Should().HaveCount(3);
            duel.State.FirstDuelist.DeckCards.Should().HaveCount(17);
        }

        private static DuelBuilder GivenADuel() {
            return new DuelBuilder();
        }

        private static void ShouldNotBeInSameOrder(ReadOnlyCollection<CardState> firstDeckFromOne, ReadOnlyCollection<CardState> secondDeckFromOne) {
            var firstDeck = string.Join("", firstDeckFromOne.Select(c => c.ManaCost));
            var secondDeck = string.Join("", secondDeckFromOne.Select(c => c.ManaCost));
            firstDeck.Should().NotBe(secondDeck);
        }

        private static CardState Card(int manaCost) {
            return new CardState(manaCost, manaCost);
        }
    }
}