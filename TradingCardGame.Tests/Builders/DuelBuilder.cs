using TradingCardGame.DuelAggregate;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.Tests.Builders {
    public class DuelBuilder {
        private DuelistState firstDuelist;
        private DuelistState secondDuelist;
        private TurnState turn;
        private string id;

        public DuelBuilder WithFirstDuelist(DuelistBuilder duelist) {
            this.firstDuelist = duelist.Build();
            return this;
        }

        public DuelBuilder WithSecondDuelist(DuelistBuilder duelist) {
            this.secondDuelist = duelist.Build();
            return this;
        }

        public DuelBuilder WithNoTurn() {
            turn = new TurnState(null);
            return this;
        }

        public DuelBuilder WithId(string id) {
            this.id = id;
            return this;
        }

        public Duel Build() {
            return Duel.Restore(new DuelState(id, firstDuelist, secondDuelist, turn));
        }
    }
}