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

        public static Duel Start(string id, string firstDuelist, string secondDuelist) {
            var duel = new Duel(id, new Duelist(firstDuelist), new Duelist(secondDuelist), new Turn(firstDuelist));
            duel.Start();
            return duel;
        }

        private void Start() {
            DomainEvents.Add(new DuelStarted(id));
            DomainEvents.Add(new DuelistTurnStarted(id, firstDuelist.Id));
            CreateDecks();
            SetManaSlots();
            RefillMana();
        }

        private void CreateDecks() {
            firstDuelist.AssignDeck(new Deck());
            secondDuelist.AssignDeck(new Deck());
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