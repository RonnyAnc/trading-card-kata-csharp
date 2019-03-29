using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LanguageExt;

namespace TradingCardGame {
    public class Duel {
        private readonly string id;
        private readonly List<DomainEvent> events = new List<DomainEvent>();
        private Option<string> first = Option<string>.None;
        private Option<string> second = Option<string>.None;

        private Duel(string id, Option<DuelistState> first, Option<DuelistState> second) {
            this.id = id;
            this.first = GetDuelistFrom(first);
            this.second = GetDuelistFrom(second);
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
                Option<DuelistState>.None);
            duel.events.Add(new DuelCalled(id));
            return duel;
        }

        public static Duel Rebuild(string id, Option<DuelistState> firstDuelist, Option<DuelistState> secondDuelist) {
            return new Duel(id, firstDuelist, secondDuelist);
        }

        public void AddDuelist(string duelistId) {
            first.IfSome(_ => second = second.IfNone(duelistId));
            first = first.IfNone(duelistId);

            events.Add(new DuelistJoined(id, duelistId));

            if (first.IsSome && second.IsSome) 
                events.Add(new AllDuelistsJoined(id));
        }

        public void Start() {
            events.Add(new DuelStarted(id));
        }
    }

    public interface DuelistState {
        string Id { get; }
    }
}