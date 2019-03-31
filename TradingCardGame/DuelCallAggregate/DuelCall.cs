using LanguageExt;
using TradingCardGame.DuelAggregate.State;
using TradingCardGame.DuelCallAggregate.Events;

namespace TradingCardGame.DuelCallAggregate {
    public class DuelCall : AggregateRoot {
        private readonly string id;
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
            duelCall.DomainEvents.Add(new DuelCalled(id));
            return duelCall;
        }

        public static DuelCall Restore(string id, Option<DuelistState> duelistOne, Option<DuelistState> duelistTwo) {
            return new DuelCall(id, duelistOne, duelistTwo);
        }

        public void AddDuelist(string duelistId) {
            first.BiIter(
                _ => second = second.IfNone(duelistId),
                () => first = first.IfNone(duelistId));
            DomainEvents.Add(new DuelistJoined(id, duelistId));
            if (first.IsSome && second.IsSome)
                DomainEvents.Add(new AllDuelistsJoined(id));
        }
    }
}