using System.Linq;
using TradingCardGame.DuelAggregate.Events;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    public class Duel : AggregateRoot {
        private readonly string id;
        private Turn turn;
        private readonly Duelist firstDuelist;
        private readonly Duelist secondDuelist;

        public DuelState State => new DuelState(id, DuelistState.From(firstDuelist), DuelistState.From(secondDuelist), new TurnState(firstDuelist.Id));

        private Duel(string id, Duelist firstDuelist, Duelist secondDuelist, Turn turn) {
            this.id = id;
            this.firstDuelist = firstDuelist;
            this.secondDuelist = secondDuelist;
            this.turn = turn;
        }

        public static Duel Start(string id, string firstDuelistId, string secondDuelistId) {
            var firstDuelist = Duelist.Create(firstDuelistId, Deck.Create());
            var secondDuelist = Duelist.Create(secondDuelistId, Deck.Create());
            var duel = new Duel(id, firstDuelist, secondDuelist, null);
            duel.DomainEvents.Add(
                new DuelStarted(id, 
                    DuelistState.From(firstDuelist), 
                    DuelistState.From(secondDuelist)));
            return duel;
        }

        public void StartNextTurn() {
            turn = new Turn(firstDuelist.Id);
            SetManaSlots();
            RefillMana();
            DrawInitialHand();
            DrawACard();
            DomainEvents.Add(new FirstDuelistTurnStarted(id, firstDuelist.Id));
        }

        private void DrawACard() {
            firstDuelist.DrawCard();
            var duelistState = DuelistState.From(firstDuelist);
            DomainEvents.Add(new CardDrawed(id, duelistState.Id, duelistState.Hand.Last()));
        }

        private void DrawInitialHand() {
            firstDuelist.DrawCard();
            firstDuelist.DrawCard();
            firstDuelist.DrawCard();
            var initialHand = DuelistState.From(firstDuelist).Hand;
            DomainEvents.Add(new InitialHandDrawed(id, firstDuelist.Id, initialHand));
        }

        private void RefillMana() {
            firstDuelist.RefillMana();
            DomainEvents.Add(new ManaRefilled(id, turn.DuelistId, 1));
        }

        private void SetManaSlots() {
            firstDuelist.IncrementManaSlot();
            DomainEvents.Add(new ManaSlotSet(id, turn.DuelistId, 1));
        }

        public static Duel Restore(DuelState duel) {
            return new Duel(duel.Id, duel.FirstDuelist.ToEntity(), duel.SecondDuelist.ToEntity(), duel.Turn.ToValueObject());
        }
    }
}