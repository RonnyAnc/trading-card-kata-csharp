using System;
using FluentAssertions;
using LanguageExt;
using NUnit.Framework;
using TradingCardGame.DuelAggregate.State;
using TradingCardGame.DuelCallAggregate;
using TradingCardGame.DuelCallAggregate.Events;
using List = LanguageExt.List;

namespace TradingCardGame.Tests {
    public class DuelCallShould {
        [Test]
        public void prepare_a_duel_called_event_when_new_duel_is_created()
        {
            const string duelId = "anyId";
            var duelCall = DuelCall.Create(duelId);

            duelCall.Events.Should().HaveCount(1);
            duelCall.Events.Should().Contain(x => x.Equals(new DuelCalled(duelId)));
        }

        [Test]
        public void prepare_a_duelist_joined_when_adding_a_duelist() {
            const string duelId = "anyId";
            var duelCall = DuelCall.Restore(duelId, Option<DuelistState>.None, Option<DuelistState>.None);

            const string duelistId = "aDuelist";
            duelCall.AddDuelist(duelistId);

            duelCall.Events.Should().HaveCount(1);
            duelCall.Events.Should().Contain(x => x.Equals(new DuelistJoined(duelId, duelistId)));
        }

        [Test]
        public void prepare_an_all_duelists_joined_when_both_duelist_are_in_the_duel() {
            const string duelId = "anyId";
            var duelCall = DuelCall.Restore(duelId, new DuelistState("firstDuelist", 0, new TestDeck()), Option<DuelistState>.None);

            const string secondDuelist = "secondDuelist";
            duelCall.AddDuelist(secondDuelist);

            duelCall.Events.Should().HaveCount(2);
            duelCall.Events.Should().Contain(x => x.Equals(new AllDuelistsJoined(duelId)));
        }
    }
}