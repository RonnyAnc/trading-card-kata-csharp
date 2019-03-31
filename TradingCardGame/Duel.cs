using System.Collections.Generic;
using System.Collections.ObjectModel;
using LanguageExt;

namespace TradingCardGame {
    public class Duel : AggregateRoot {
        private readonly string id;
        private readonly string turn;
        private readonly string first;
        private readonly string second;

        private Duel(string id, string firstDuelist, string secondDuelist, string turn) {
            this.id = id;
            first = firstDuelist;
            second = secondDuelist;
            this.turn = turn;
        }

        public static Duel Rebuild(string id, DuelistState firstDuelist, DuelistState secondDuelist, TurnState currentTurn) {
            return new Duel(id, firstDuelist.Id, secondDuelist.Id, currentTurn.DuelistId);
        }

        public static Duel Start(string id, string firstDuelist, string secondDuelist) {
            var duel = new Duel(id, firstDuelist, secondDuelist, firstDuelist);
            duel.DomainEvents.Add(new DuelStarted(id));
            duel.DomainEvents.Add(new DuelistTurnStarted(id, firstDuelist));
            return duel;
        }

        public void SetManaSlots() {
            DomainEvents.Add(new ManaSlotSet(id, turn, 1));
        }
    }

    public interface DuelistState {
        string Id { get; }
        int ManaSlots { get; }
    }
}