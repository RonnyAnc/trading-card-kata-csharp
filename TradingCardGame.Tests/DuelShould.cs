using System;
using FluentAssertions;
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
            var duel = Duel.Rebuild(duelId);

            const string duelistId = "aDuelist";
            duel.AddDuelist(duelistId);

            duel.Events.Should().HaveCount(1);
            duel.Events.Should().Contain(x => x.Equals(new DuelistJoined(duelId, duelistId)));
        }

        [Test]
        public void prepare_an_all_duelists_joined_when_both_a_duelist() {
            const string duelId = "anyId";
            var duel = Duel.Rebuild(duelId);

            const string firstDuelist = "firstDuelist";
            duel.AddDuelist(firstDuelist);
            const string secondDuelist = "secondDuelist";
            duel.AddDuelist(secondDuelist);

            duel.Events.Should().HaveCount(3);
            duel.Events.Should().Contain(x => x.Equals(new AllDuelistsJoined(duelId)));
        }
    }

    public class AllDuelistsJoined : DomainEvent, IEquatable<AllDuelistsJoined> {
        public string DuelId { get; }

        public AllDuelistsJoined(string duelId) {
            DuelId = duelId;
        }

        public bool Equals(AllDuelistsJoined other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DuelId, other.DuelId);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AllDuelistsJoined) obj);
        }

        public override int GetHashCode() {
            return (DuelId != null ? DuelId.GetHashCode() : 0);
        }
    }
}