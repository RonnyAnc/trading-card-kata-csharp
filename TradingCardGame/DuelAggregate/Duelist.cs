using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    internal class Duelist : DuelistState {
        public string Id { get; }
        public int ManaSlots { get; }

        public Duelist(string id) {
            Id = id;
        }
    }
}