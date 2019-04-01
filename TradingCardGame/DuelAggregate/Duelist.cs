using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    internal class Duelist : DuelistState {
        private List<ManaSlot> manaSlots;
        private int mana;
        public string Id { get; }
        public int ManaSlots => manaSlots.Count;
        public int Mana => manaSlots.Filter(slot => !slot.IsEmpty).Count();

        internal Duelist(string id) {
            Id = id;
            manaSlots = new List<ManaSlot>();
        }

        internal void IncrementManaSlot() {
            manaSlots.Add(ManaSlot.Empty());
        }

        internal void RefillMana() {
            manaSlots = manaSlots.Map(_ => ManaSlot.Filled()).ToList();
        }
    }

    internal class ManaSlot {
        internal bool IsEmpty { get; }

        private ManaSlot(bool isEmpty) {
            IsEmpty = isEmpty;
        }

        internal static ManaSlot Filled() {
            return new ManaSlot(false);
        }

        internal static ManaSlot Empty() {
            return new ManaSlot(true);
        }
    }
}