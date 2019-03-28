using System.Collections.Generic;
using System.Collections.ObjectModel;
using LanguageExt;

namespace TradingCardGame {
    public class Duel {
        private readonly string id;
        private readonly List<DomainEvent> events = new List<DomainEvent>();
        private List<string> duelists = new List<string>();

        private Duel(string id) {
            this.id = id;
        }

        private Duel(string id, Option<DuelistState> first, Option<DuelistState> second) {
            this.id = id;
            first.IfSome(duelist => duelists.Add(duelist.Id));
            second.IfSome(duelist => duelists.Add(duelist.Id));
        }

        public ReadOnlyCollection<DomainEvent> Events => this.events.AsReadOnly();

        public static Duel Call(string id) {
            var duel = new Duel(id);
            duel.events.Add(new DuelCalled(id));
            return duel;
        }

        public static Duel Rebuild(string id, Option<DuelistState> firstDuelist, Option<DuelistState> secondDuelist) {
            return new Duel(id, firstDuelist, secondDuelist);
        }

        public void AddDuelist(string duelistId) {
            duelists.Add(duelistId);
            events.Add(new DuelistJoined(id, duelistId));
            if (duelists.Count == 2) 
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