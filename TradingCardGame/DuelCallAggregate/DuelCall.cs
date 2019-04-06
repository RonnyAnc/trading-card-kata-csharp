using LanguageExt;
using TradingCardGame.DuelCallAggregate.Events;

namespace TradingCardGame.DuelCallAggregate {
    public class DuelCall : AggregateRoot {
        private readonly string id;
        private Either<FreeSpot, string> spotOne = new FreeSpot();
        private Either<FreeSpot, string> spotTwo = new FreeSpot();

        private DuelCall(string id, Either<FreeSpot, string> duelistOne, Either<FreeSpot, string> duelistTwo) {
            this.id = id;
            duelistOne.Map(duelistId => spotOne = duelistId);
            duelistTwo.Map(duelistId => spotOne = duelistId);
        }

        public static DuelCall Create(string id) {
            var duelCall = new DuelCall(id,
                new FreeSpot(),
                new FreeSpot());
            duelCall.DomainEvents.Add(new DuelCalled(id));
            return duelCall;
        }

        // TODO: remove DuelistState from here
        public static DuelCall Restore(string id, Either<FreeSpot, string> duelistOne, Either<FreeSpot, string> duelistTwo) {
            return new DuelCall(id, duelistOne, duelistTwo);
        }

        public void AddDuelist(string duelistId) {
            spotOne.Map(_ => spotTwo = duelistId)
                .MapLeft(_ => spotOne = duelistId);
            DomainEvents.Add(new DuelistJoined(id, duelistId));
            if (spotOne.IsRight && spotTwo.IsRight)
                DomainEvents.Add(new AllDuelistsJoined(id));
        }
    }

    public class FreeSpot { }
}