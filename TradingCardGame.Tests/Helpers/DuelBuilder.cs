﻿using TradingCardGame.DuelAggregate;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.Tests.Helpers {
    public class DuelBuilder {
        private DuelistState firstDuelist;
        private DuelistState secondDuelist;
        private TurnState turn;
        private string id;

        public DuelBuilder WithFirstDuelist(DuelistBuilder duelist) {
            this.firstDuelist = duelist.BuildState();
            return this;
        }

        public DuelBuilder WithSecondDuelist(DuelistBuilder duelist) {
            this.secondDuelist = duelist.BuildState();
            return this;
        }

        public DuelBuilder WithNoTurn() {
            turn = new TurnState(null);
            return this;
        }

        public DuelBuilder WithTurn(string duelistId) {
            turn = new TurnState(duelistId, true);
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