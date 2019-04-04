namespace TradingCardGame.DuelAggregate {
    internal class ManaSlot {
        internal bool IsFilled { get; }

        private ManaSlot(bool isFilled) {
            IsFilled = isFilled;
        }

        internal static ManaSlot Filled() {
            return new ManaSlot(true);
        }

        internal static ManaSlot Empty() {
            return new ManaSlot(false);
        }
    }
}