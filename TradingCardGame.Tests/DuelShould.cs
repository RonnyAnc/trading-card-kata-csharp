using System.Linq;
using FluentAssertions;
using LanguageExt;
using NSubstitute;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class DuelShould {
        [Test]
        public void prepare_a_duel_called_event_when_new_duel_is_created() {
            const string duelId = "anyId";
            var duel = Duel.Call(duelId);

            duel.Events.Should().HaveCount(1);
            duel.Events.Should().Contain(x => x.Equals(new DuelCalled(duelId)));
        }

        [Test]
        public void prepare_a_duelist_joined_when_adding_a_duelist() {
            const string duelId = "anyId";
            var duel = Duel.Rebuild(duelId, Option<DuelistState>.None, Option<DuelistState>.None);

            const string duelistId = "aDuelist";
            duel.AddDuelist(duelistId);

            duel.Events.Should().HaveCount(1);
            duel.Events.Should().Contain(x => x.Equals(new DuelistJoined(duelId, duelistId)));
        }

        [Test]
        public void prepare_an_all_duelists_joined_when_both_duelist_are_in_the_duel() {
            const string duelId = "anyId";
            var duel = Duel.Rebuild(duelId, new Duelist("firstDuelist", 0), Option<DuelistState>.None);

            const string secondDuelist = "secondDuelist";
            duel.AddDuelist(secondDuelist);

            duel.Events.Should().HaveCount(2);
            duel.Events.Should().Contain(x => x.Equals(new AllDuelistsJoined(duelId)));
        }

        [Test]
        public void prepare_a_duel_started_when_starting_a_duel() {
            const string duelId = "anyId";
            var duel = Duel.Rebuild(duelId, new Duelist("firstDuelist", 0), new Duelist("secondDuelist", 0));

            duel.Start();

            duel.Events.Should().HaveCount(2);
            duel.Events.Should().Contain(x => x.Equals(new DuelStarted(duelId)));
            duel.Events.Should().Contain(x => x.Equals(new DuelistTurnStarted(duelId, "firstDuelist")));
        }

        [Test]
        public void prepare_mana_slot_set_when_setting_mana_slots() {
            const string duelId = "anyId";
            var duel = Duel.Rebuild(duelId, new Duelist("firstDuelist", 0), new Duelist("secondDuelist", 0));

            duel.SetManaSlots();

            duel.Events.Should().HaveCount(1);
            duel.Events.Should().Contain(x => x.Equals(new ManaSlotSet(duelId, "firstDuelist", 1)));
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