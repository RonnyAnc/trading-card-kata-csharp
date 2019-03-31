using System.Collections.Generic;
using System.Collections.ObjectModel;
using LanguageExt;

namespace TradingCardGame {
    public class Duel : AggregateRoot {
        private readonly string id;
        private readonly string turn;
        private readonly Duelist firstDuelist;
        private readonly Duelist secondDuelist;

        public DuelState State => new DuelState(firstDuelist, secondDuelist);

        private Duel(string id, string firstDuelist, string secondDuelist, string turn) {
            this.id = id;
            this.firstDuelist = new Duelist(firstDuelist);
            this.secondDuelist = new Duelist(secondDuelist);
            this.turn = turn;
        }

        public static Duel Start(string id, string firstDuelist, string secondDuelist) {
            var duel = new Duel(id, firstDuelist, secondDuelist, firstDuelist);
            duel.Start();
            return duel;
        }

        private void Start() {
            DomainEvents.Add(new DuelStarted(id));
            DomainEvents.Add(new DuelistTurnStarted(id, firstDuelist.Id));
            DomainEvents.Add(new ManaSlotSet(id, turn, 1));
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

        public DuelState(DuelistState firstDuelist, DuelistState secondDuelist) {
            FirstDuelist = firstDuelist;
            SecondDuelist = secondDuelist;
        }
    }

    public interface DuelistState {
        string Id { get; }
        int ManaSlots { get; }
    }
}