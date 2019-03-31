using FluentAssertions;
using LanguageExt;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class DuelShould {
        

        [Test]
        public void prepare_a_duel_started_when_starting_a_duel() {
            const string duelId = "anyId";
            var duel = Duel.Rebuild(duelId, new Duelist("firstDuelist", 0), new Duelist("secondDuelist", 0));

            duel.Start();

            duel.Events.Should().HaveCount(2);
            duel.Events.Should().Contain(x => x.Equals(new DuelStarted(duelId)));
            duel.Events.Should().Contain(x => x.Equals(new DuelistTurnStarted(duelId, "firstDuelist")));
        }

        [TestCase("firstDuelist")]
        [TestCase("secondDuelist")]
        public void prepare_mana_slot_set_when_setting_mana_slots_for_current_duelist(string currentDuelist) {
            const string duelId = "anyId";
            var duel = Duel.Rebuild(duelId, new Duelist("firstDuelist", 0), new Duelist("secondDuelist", 0), new Turn(currentDuelist));

            duel.SetManaSlots();

            duel.Events.Should().HaveCount(1);
            duel.Events.Should().Contain(x => x.Equals(new ManaSlotSet(duelId, currentDuelist, 1)));
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