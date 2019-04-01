using System.Collections.Generic;
using FluentAssertions;
using LanguageExt;
using NUnit.Framework;
using TradingCardGame.DuelAggregate;
using TradingCardGame.DuelAggregate.Events;
using TradingCardGame.DuelAggregate.State;
using List = LanguageExt.List;

namespace TradingCardGame.Tests {
    public class DuelShould {
        [Test]
        public void prepare_a_duel_started_when_starting_a_duel() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");

            duel.Events.Should().Contain(x => x.Equals(new DuelStarted(duelId)));
        }

        [Test]
        public void start_a_duel_with_two_players() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");

            duel.State.FirstDuelist.Id.Should().Be("firstDuelist");
            duel.State.SecondDuelist.Id.Should().Be("secondDuelist");
        }

        [Test]
        public void prepare_a_duelist_turn_started_when_starting_a_duel() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");

            duel.Events.Should().Contain(x => x.Equals(new DuelistTurnStarted(duelId, "firstDuelist")));
        }

        [Test]
        public void give_initial_turn_to_first_duelist() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");

            duel.State.Turn.DuelistId.Should().Be("firstDuelist");
        }

        [Test]
        public void prepare_mana_slot_set_when_setting_mana_slots_when_starting_a_duel() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");

            duel.Events.Should().Contain(x => x.Equals(new ManaSlotSet(duelId, "firstDuelist", 1)));
        }

        [Test]
        public void set_one_mana_slot_to_first_duelist_when_starting_a_duel() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");

            duel.State.FirstDuelist.ManaSlots.Should().Be(1);
        }

        [Test]
        public void refill_mana_to_first_duelist_when_starting_a_duel() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");

            duel.State.FirstDuelist.Mana.Should().Be(1);
        }

        [Test]
        public void prepare_mana_refilled_when_starting_a_duel() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");

            duel.Events.Should().Contain(x => x.Equals(new ManaRefilled(duelId, "firstDuelist", 1)));
        }

        [Test]
        public void draw_three_cards_for_first_duelist_from_his_deck_when_his_first_turn_started() {
            const string duelId = "anyId";
            var firstDuelist = new DuelistState("firstDuelist", 0, new TestDeck());
            var secondDuelist = new DuelistState("firstDuelist", 0, new TestDeck());

            var duel = Duel.Start(duelId, firstDuelist, secondDuelist);

            duel.State.FirstDuelist.Deck.Cards.Should().HaveCount(17);
            duel.State.FirstDuelist.Hand.Should().HaveCount(3);
        }
    }

    public class TestDeck : DeckState {
        public List<CardState> Cards { get; } = new List<CardState>();

        public TestDeck() {
            for (var i = 0; i < 20; i++) {
                Cards.Add(new Card(i, i));
            }
        }
    }

    public class Card : CardState {
        public int ManaCost { get; }
        public int Damage { get; }

        public Card(int manaCost, int damage) {
            ManaCost = manaCost;
            Damage = damage;
        }
    }
}