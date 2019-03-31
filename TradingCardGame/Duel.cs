using System.Collections.Generic;
using System.Collections.ObjectModel;
using LanguageExt;

namespace TradingCardGame {
    public class Duel : AggregateRoot {
        private readonly string id;
        private readonly string turn;
        private readonly string firstDuelist;
        private readonly string secondDuelist;

        private Duel(string id, string firstDuelist, string secondDuelist, string turn) {
            this.id = id;
            this.firstDuelist = firstDuelist;
            this.secondDuelist = secondDuelist;
            this.turn = turn;
        }

        public static Duel Start(string id, string firstDuelist, string secondDuelist) {
            var duel = new Duel(id, firstDuelist, secondDuelist, firstDuelist);
            duel.Start();
            return duel;
        }

        private void Start() {
            DomainEvents.Add(new DuelStarted(id));
            DomainEvents.Add(new DuelistTurnStarted(id, firstDuelist));
            DomainEvents.Add(new ManaSlotSet(id, turn, 1));
        }
    }

    public interface DuelistState {
        string Id { get; }
        int ManaSlots { get; }
    }
}