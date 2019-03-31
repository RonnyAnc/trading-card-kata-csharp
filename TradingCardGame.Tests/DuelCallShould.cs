using System;
using FluentAssertions;
using LanguageExt;
using NUnit.Framework;
using TradingCardGame.Duel;
using TradingCardGame.Duel.State;
using TradingCardGame.DuelCall.Events;
using DuelistJoined = TradingCardGame.DuelCall.DuelistJoined;
using List = LanguageExt.List;

namespace TradingCardGame.Tests {
    public class DuelCallShould {
        [Test]
        public void prepare_a_duel_called_event_when_new_duel_is_created()
        {
            const string duelId = "anyId";
            var duelCall = DuelCall.DuelCall.Create(duelId);

            duelCall.Events.Should().HaveCount(1);
            duelCall.Events.Should().Contain(x => x.Equals(new DuelCalled(duelId)));
        }

        [Test]
        public void prepare_a_duelist_joined_when_adding_a_duelist() {
            const string duelId = "anyId";
            var duelCall = DuelCall.DuelCall.Restore(duelId, Option<DuelistState>.None, Option<DuelistState>.None);

            const string duelistId = "aDuelist";
            duelCall.AddDuelist(duelistId);

            duelCall.Events.Should().HaveCount(1);
            duelCall.Events.Should().Contain(x => x.Equals(new DuelistJoined(duelId, duelistId)));
        }

        [Test]
        public void prepare_an_all_duelists_joined_when_both_duelist_are_in_the_duel() {
            const string duelId = "anyId";
            var duelCall = DuelCall.DuelCall.Restore(duelId, new Duelist("firstDuelist", 0), Option<DuelistState>.None);

            const string secondDuelist = "secondDuelist";
            duelCall.AddDuelist(secondDuelist);

            duelCall.Events.Should().HaveCount(2);
            duelCall.Events.Should().Contain(x => x.Equals(new AllDuelistsJoined(duelId)));
        }
    }
}