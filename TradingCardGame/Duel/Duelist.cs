using TradingCardGame.Duel.State;

namespace TradingCardGame.Duel {
    internal class Duelist : DuelistState {
        public string Id { get; }
        public int ManaSlots { get; }

        public Duelist(string id) {
            Id = id;
        }
    }
}