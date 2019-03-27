using FluentAssertions;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class DuelShould {
        [Test]
        public void prepare_a_duel_called_event_when_new_duel_is_created() {
            const string duelId = "anyId";
            var duel = Duel.Call(duelId);
            duel.Events.Should().Contain(x => x.Equals(new DuelCalled(duelId)));
        }

        [Test]
        public void prepare_a_duelist_joined_when_adding_a_duelist() {
            var duel = Duel.Rebuild("anyId");

            const string duelistId = "aDuelist";
            duel.AddDuelist(duelistId);

            duel.Events.Should().Contain(x => x.Equals(new DuelistJoined(duelistId)));
        }
    }
}