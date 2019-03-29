using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LanguageExt;

namespace TradingCardGame {
    public class Duel {
        private readonly string id;
        private readonly List<DomainEvent> events = new List<DomainEvent>();
        private Option<string> turn = Option<string>.None;
        private Option<string> first = Option<string>.None;
        private Option<string> second = Option<string>.None;

        private Duel(string id, Option<DuelistState> first, Option<DuelistState> second, Option<TurnState> turn) {
            this.id = id;
            this.first = GetDuelistFrom(first);
            this.second = GetDuelistFrom(second);
            turn.IfSome(turnState => this.turn = turnState.DuelistId);
        }

        private static Option<string> GetDuelistFrom(Option<DuelistState> first) {
            return first.BiBind(
                duelist => duelist.Id, 
                () => Option<string>.None);
        }

        public ReadOnlyCollection<DomainEvent> Events => this.events.AsReadOnly();

        public static Duel Call(string id) {
            var duel = new Duel(
                id, 
                Option<DuelistState>.None, 
                Option<DuelistState>.None,
                Option<TurnState>.None);
            duel.events.Add(new DuelCalled(id));
            return duel;
        }

        public static Duel Rebuild(string id, Option<DuelistState> firstDuelist, Option<DuelistState> secondDuelist, TurnState currentTurn = null) {
            var turnState = currentTurn == null ? Option<TurnState>.None : Option<TurnState>.Some(currentTurn);
            return new Duel(id, firstDuelist, secondDuelist, turnState);
        }

        public void AddDuelist(string duelistId) {
            first.BiIter(
                _ => second = second.IfNone(duelistId),
                () => first = first.IfNone(duelistId));           
            events.Add(new DuelistJoined(id, duelistId));
            if (first.IsSome && second.IsSome) 
                events.Add(new AllDuelistsJoined(id));
        }

        public void Start() {
            events.Add(new DuelStarted(id));
            first.IfSome(firstId => events.Add(new DuelistTurnStarted(id, firstId)));
        }

        public void SetManaSlots() {
            first.IfSome(
                firstId => 
                    turn.IfSome(currentDuelist => events.Add(new ManaSlotSet(id, currentDuelist, 1))
            ));
        }
    }

    public interface DuelistState {
        string Id { get; }
        int ManaSlots { get; }
    }
}