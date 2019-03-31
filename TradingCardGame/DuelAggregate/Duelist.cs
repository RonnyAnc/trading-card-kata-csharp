using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    internal class Duelist : DuelistState {
        private int manaSlots;
        private int mana;
        public string Id { get; }
        public int ManaSlots => manaSlots;
        public int Mana => mana;

        internal Duelist(string id) {
            Id = id;
            manaSlots = 0;
        }

        internal void IncrementManaSlot() {
            manaSlots = manaSlots + 1;
        }

        public void RefillMana() {
            mana = manaSlots;
        }
    }
}