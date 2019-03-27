using FluentAssertions;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class DuelShould {
        [Test]
        public void prepare_a_duel_called_event_when_new_duel_is_created() {
            var duel = Duel.Call("anyId");
            duel.Events.Should().Contain(x => x.Equals(new DuelCalled("anyId")));
        }
    }
}