using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LanguageExt;
using NUnit.Framework;
using TradingCardGame.DuelAggregate;
using TradingCardGame.DuelAggregate.Events;
using TradingCardGame.DuelAggregate.State;
using List = LanguageExt.List;

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
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.Events.Should().Contain(x => x.Equals(new DuelistTurnStarted(DuelId, FirstDuelistId)));
        }

        [Test]
        public void give_initial_turn_to_first_duelist() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.State.Turn.DuelistId.Should().Be(FirstDuelistId);
        }

        [Test]
        public void prepare_mana_slot_set_event_when_setting_mana_slots_when_starting_a_duel() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.Events.Should().Contain(x => x.Equals(new ManaSlotSet(DuelId, FirstDuelistId, 1)));
        }

        [Test]
        public void set_one_mana_slot_to_first_duelist_when_starting_a_duel() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.State.FirstDuelist.ManaSlots.Should().Be(1);
        }

        [Test]
        public void refill_mana_to_first_duelist_when_starting_a_duel() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.State.FirstDuelist.Mana.Should().Be(1);
        }

        [Test]
        public void prepare_mana_refilled_event_when_starting_a_duel() {
            var duel = Duel.Start(DuelId, FirstDuelistId, SecondDuelistId);

            duel.Events.Should().Contain(x => x.Equals(new ManaRefilled(DuelId, FirstDuelistId, 1)));
        }

        [Test] // TODO: redo
        public void draw_three_cards_for_first_duelist_from_his_deck_when_his_first_turn_started() {
            var firstDuelistDeck = new DeckState(GetCardsForDeck());
            var firstDuelist = new DuelistState(FirstDuelistId, 0, firstDuelistDeck);
            var secondDuelist = new DuelistState(SecondDuelistId, 0, new DeckState(GetCardsForDeck()));

            var duel = Duel.Start(DuelId, firstDuelist, secondDuelist);

            var expectedDeck = firstDuelistDeck.Cards.Except(duel.State.FirstDuelist.Hand);
            duel.State.FirstDuelist.Deck.Cards.Should().BeEquivalentTo(expectedDeck);
            duel.State.FirstDuelist.Deck.Cards.Should().HaveCount(17);
            var expectedHand = firstDuelistDeck.Cards.Except(duel.State.FirstDuelist.Deck.Cards);
            duel.State.FirstDuelist.Hand.Should().BeEquivalentTo(expectedHand);
            duel.State.FirstDuelist.Hand.Should().HaveCount(3);
        }

        private List<CardState> GetCardsForDeck() {
            var cards = new List<CardState>();
            for (int i = 0; i < 20; i++) {
                cards.Add(new CardState(i, i));
            }

            return cards;
        }
    }
}