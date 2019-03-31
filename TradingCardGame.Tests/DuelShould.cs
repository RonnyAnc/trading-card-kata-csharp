using FluentAssertions;
using LanguageExt;
using NUnit.Framework;

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
        public void prepare_mana_slot_set_when_setting_mana_slots_when_starting_a_duel() {
            const string duelId = "anyId";

            var duel = Duel.Start(duelId, "firstDuelist", "secondDuelist");
            
            duel.Events.Should().Contain(x => x.Equals(new ManaSlotSet(duelId, "firstDuelist", 1)));
        }
    }

    internal class Turn : TurnState {
        public string DuelistId { get; }

        public Turn(string duelistId) {
            DuelistId = duelistId;
        }
    }

    internal class Duelist : DuelistState {
        public string Id { get; }
        public int ManaSlots { get; }

        public Duelist(string id, int manaSlots) {
            Id = id;
            ManaSlots = manaSlots;
        }
    }
}