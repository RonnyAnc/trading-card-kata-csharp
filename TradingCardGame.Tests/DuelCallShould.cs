using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAssertions;
using LanguageExt;
using NUnit.Framework;
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
            var duelCall = DuelCall.Restore(duelId, new Duelist("firstDuelist", 0), Option<DuelistState>.None);

            const string secondDuelist = "secondDuelist";
            duelCall.AddDuelist(secondDuelist);

            duelCall.Events.Should().HaveCount(2);
            duelCall.Events.Should().Contain(x => x.Equals(new AllDuelistsJoined(duelId)));
        }
    }

    public class DuelCall {
        private readonly string id;
        private readonly List<DomainEvent> events = new List<DomainEvent>();
        public ReadOnlyCollection<DomainEvent> Events => this.events.AsReadOnly();
        private Option<string> first = Option<string>.None;
        private Option<string> second = Option<string>.None;

        private DuelCall(string id, Option<DuelistState> duelistOne, Option<DuelistState> duelistTwo) {
            this.id = id;
            first = GetDuelistFrom(duelistOne);
            second = GetDuelistFrom(duelistTwo);
        }


        private static Option<string> GetDuelistFrom(Option<DuelistState> first) {
            return first.BiBind(
                duelist => duelist.Id,
                () => Option<string>.None);
        }

        public static DuelCall Create(string id) {
            var duelCall = new DuelCall(id,
                Option<DuelistState>.None,
                Option<DuelistState>.None);
            duelCall.events.Add(new DuelCalled(id));
            return duelCall;
        }

        public static DuelCall Restore(string id, Option<DuelistState> duelistOne, Option<DuelistState> duelistTwo) {
            return new DuelCall(id, duelistOne, duelistTwo);
        }

        public void AddDuelist(string duelistId) {
            first.BiIter(
                _ => second = second.IfNone(duelistId),
                () => first = first.IfNone(duelistId));
            events.Add(new DuelistJoined(id, duelistId));
            if (first.IsSome && second.IsSome)
                events.Add(new AllDuelistsJoined(id));
        }
    }
}