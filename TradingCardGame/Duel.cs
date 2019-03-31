using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Transactions;
using LanguageExt;

namespace TradingCardGame {
    public class Duel : AggregateRoot {
        private readonly string id;
        private readonly Turn turn;
        private readonly Duelist firstDuelist;
        private readonly Duelist secondDuelist;

        public DuelState State => new DuelState(firstDuelist, secondDuelist, turn);

        private Duel(string id, string firstDuelist, string secondDuelist, string turn) {
            this.id = id;
            this.firstDuelist = new Duelist(firstDuelist);
            this.secondDuelist = new Duelist(secondDuelist);
            this.turn = new Turn(turn);
        }

        public static Duel Start(string id, string firstDuelist, string secondDuelist) {
            var duel = new Duel(id, firstDuelist, secondDuelist, firstDuelist);
            duel.Start();
            return duel;
        }

        private void Start() {
            DomainEvents.Add(new DuelStarted(id));
            DomainEvents.Add(new DuelistTurnStarted(id, firstDuelist.Id));
            DomainEvents.Add(new ManaSlotSet(id, turn.DuelistId, 1));
        }
    }

    internal class Turn : TurnState {
        public string DuelistId { get; }

        public Turn(string duelistId) {
            DuelistId = duelistId;
        }
    }

    internal class Duelist : DuelistState {
        public string Id { get; }
        public int ManaSlots { get; }

        public Duelist(string id) {
            Id = id;
        }
    }

    public class DuelState {
        public DuelistState FirstDuelist { get; }
        public DuelistState SecondDuelist { get; }
        public TurnState Turn { get; set; }

        public DuelState(DuelistState firstDuelist, DuelistState secondDuelist, TurnState turn) {
            FirstDuelist = firstDuelist;
            SecondDuelist = secondDuelist;
            Turn = turn;
        }
    }

    public interface DuelistState {
        string Id { get; }
        int ManaSlots { get; }
    }
}