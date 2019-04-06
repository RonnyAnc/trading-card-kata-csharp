using System.Collections.ObjectModel;
using System.Linq;

namespace TradingCardGame.DuelAggregate.State {
    public class DuelistState {
        public string Id { get; }
        public int ManaSlots { get; }
        public int Mana { get; }
        public ReadOnlyCollection<CardState> Deck { get; }

        private DuelistState(string id, int manaSlots, int mana, ReadOnlyCollection<CardState> deck) {
            Id = id;
            ManaSlots = manaSlots;
            Mana = mana;
            Deck = deck;
        }

        internal static DuelistState From(Duelist duelist) {
            var deck = duelist.Deck.Select(CardState.From).ToList().AsReadOnly();
            return new DuelistState(duelist.Id, duelist.ManaSlots, duelist.Mana, deck);
        }
    }
}