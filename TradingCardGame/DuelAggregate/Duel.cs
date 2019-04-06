using System.Linq;
using LanguageExt;
using TradingCardGame.DuelAggregate.Events;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    public class Duel : AggregateRoot {
        private readonly string id;
        private readonly Turn turn;
        private readonly Duelist firstDuelist;
        private readonly Duelist secondDuelist;

        public DuelState State => new DuelState(DuelistState.From(firstDuelist), DuelistState.From(secondDuelist), turn);

        private Duel(string id, Duelist firstDuelist, Duelist secondDuelist, Turn turn) {
            this.id = id;
            this.firstDuelist = firstDuelist;
            this.secondDuelist = secondDuelist;
            this.turn = turn;
        }

        public static Duel Start(string id, string firstDuelistId, string secondDuelistId) {
            var firstDuelist = Duelist.Create(firstDuelistId, Deck.Create());
            var secondDuelist = Duelist.Create(secondDuelistId, Deck.Create());
            var duel = new Duel(id, firstDuelist, secondDuelist, new Turn(firstDuelistId));
            duel.Start();
            return duel;
        }

        private void Start() {
            DomainEvents.Add(new DuelStarted(id));
            DomainEvents.Add(new DuelistTurnStarted(id, firstDuelist.Id));
            SetManaSlots();
            RefillMana();
        }

        private void RefillMana() {
            firstDuelist.RefillMana();
            DomainEvents.Add(new ManaRefilled(id, turn.DuelistId, 1));
        }

        private void SetManaSlots() {
            firstDuelist.IncrementManaSlot();
            DomainEvents.Add(new ManaSlotSet(id, turn.DuelistId, 1));
        }
    }
}