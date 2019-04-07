using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TradingCardGame.DuelAggregate;
using TradingCardGame.DuelAggregate.Events;
using TradingCardGame.DuelAggregate.State;
using TradingCardGame.Tests.Helpers;

namespace TradingCardGame.Tests.DuelAggregate {
    public class DuelShould {
        private const string DuelId = "anyId";
        private const string FirstDuelistId = "firstDuelist";
        private const string SecondDuelistId = "secondDuelist";

        [Test]
        public void prepare_a_duel_started_event_when_starting_a_duel() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            var duelStarted = duel.Events.OfType<DuelStarted>().Single();
            duelStarted.DuelId.Should().Be(DuelId);
            duelStarted.FirstDuelist.Should().BeEquivalentTo(InitialDuelistState(FirstDuelistId));
            duelStarted.SecondDuelist.Should().BeEquivalentTo(InitialDuelistState(SecondDuelistId));
        }

        [Test]
        public void start_a_duel_with_two_players() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.State.FirstDuelist.Id.Should().Be(FirstDuelistId);
            duel.State.SecondDuelist.Id.Should().Be(SecondDuelistId);
        }

        [Test]
        public void start_with_decks_for_each_player() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            var expectedDeck = DeckBuilder.CompletedDeck();
            var firstDeck = duel.State.FirstDuelist.DeckCards;
            firstDeck.Should().BeEquivalentTo(expectedDeck);
            var secondDeck = duel.State.SecondDuelist.DeckCards;
            secondDeck.Should().BeEquivalentTo(expectedDeck);
        }

        [Test]
        public void give_initial_turn_to_first_duelist_when_starting_first_turn() {
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
        public void draw_three_cards_as_initial_hand_plus_one_card_when_starting_first_duelist_turn() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            const int initialHandSize = 3;
            duel.State.FirstDuelist.Hand.Should().HaveCount(initialHandSize + 1);
            duel.State.FirstDuelist.DeckCards.Should().HaveCount(16);
        }

        [Test]
        public void prepare_first_duelist_turn_started_event_when_starting_first_duelist_turn() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            duel.Events.Should().Contain(@event => @event.Equals(new FirstDuelistTurnStarted(DuelId, FirstDuelistId)));
        }

        [Test]
        public void prepare_a_initial_hand_drawed_after_first_turn_started() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            var initialHand = duel.State.FirstDuelist.Hand.ToList().SkipLast().ToList().AsReadOnly();
            duel.Events.Should().ContainInOrder(new FirstDuelistTurnStarted(DuelId, FirstDuelistId),
                new InitialHandDrawed(DuelId, FirstDuelistId, initialHand));
        }

        [Test]
        public void increase_one_mana_slot_to_duelist_when_starting_his_turn() {
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
        public void prepare_mana_slot_set_event_after_drawing_initial_hand() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            var initialHand = duel.State.FirstDuelist.Hand.ToList().SkipLast().ToList().AsReadOnly();
            duel.Events.Should().ContainInOrder(new InitialHandDrawed(DuelId, FirstDuelistId, initialHand),
                new ManaSlotSet(DuelId, FirstDuelistId, 1));
        }

        [Test]
        public void refill_mana_to_duelist_when_starting_his_turn() {
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
        public void prepare_mana_refilled_event_after_setting_mana_slots() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();
            duel.Events.Should().ContainInOrder(new ManaSlotSet(DuelId, FirstDuelistId, 1),
                new ManaRefilled(DuelId, FirstDuelistId, 1));
        }

        [Test]
        public void prepare_a_card_drawed_event_after_refilling_mana() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            var drawedCard = duel.State.FirstDuelist.Hand.Last();
            duel.Events.Should().ContainInOrder(new ManaRefilled(DuelId, FirstDuelistId, 1),
                new CardDrawed(DuelId, FirstDuelistId, drawedCard));
        }

        [Test]
        public void prepare_a_hand_size_approved_event_after_drawing_a_card_without_exceeding_the_max_hand_size() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            var drawedCard = duel.State.FirstDuelist.Hand.Last();
            var cardsInHand = 4;
            duel.Events.Should().ContainInOrder(new CardDrawed(DuelId, FirstDuelistId, drawedCard),
                new HandSizeApproved(DuelId, FirstDuelistId, cardsInHand));
        }

        [Test]
        public void start_decision_phase_after_checking_hand_size() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            duel.State.Turn.IsInDecisionPhase.Should().BeTrue();
        }

        [Test]
        public void prepare_a_decision_phase_started_event_after_checking_hand_size() {
            var duel = GivenADuel()
                .WithId(DuelId)
                .WithFirstDuelist(new DuelistBuilder().InitialDuelistState(FirstDuelistId))
                .WithSecondDuelist(new DuelistBuilder().InitialDuelistState(SecondDuelistId))
                .WithNoTurn()
                .Build();

            duel.StartNextTurn();

            var cardsInHand = 4;
            duel.Events.Should().ContainInOrder(
                new HandSizeApproved(DuelId, FirstDuelistId, cardsInHand),
                new DecisionPhaseStarted(DuelId, FirstDuelistId));
        }

        private static DuelistState InitialDuelistState(string firstDuelistId) {
            return new DuelistBuilder().InitialDuelistState(firstDuelistId).BuildState();
        }

        private static DuelBuilder GivenADuel() {
            return new DuelBuilder();
        }
    }
}