using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    internal class Duelist : DuelistState {
        private int manaSlots;
        public string Id { get; }
        public int ManaSlots => manaSlots;

        internal Duelist(string id) {
            Id = id;
            manaSlots = 0;
        }

        internal void IncrementManaSlot() {
            manaSlots = manaSlots + 1;
        }
    }
}