using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TradingCardGame {
    public class Duel {
        private readonly string id;
        private readonly List<DomainEvent> events = new List<DomainEvent>();
        private List<string> duelists = new List<string>();

        private Duel(string id) {
            this.id = id;
        }

        private Duel(string id, DuelistPersistanceContract first, DuelistPersistanceContract second) {
            this.id = id;
            if (!first.IsNull)
                this.duelists.Add(first.Id);
            if (!second.IsNull)
                this.duelists.Add(second.Id);
        }

        public ReadOnlyCollection<DomainEvent> Events => this.events.AsReadOnly();

        public static Duel Call(string id) {
            var duel = new Duel(id);
            duel.events.Add(new DuelCalled(id));
            return duel;
        }

        public static Duel Rebuild(string id, DuelistPersistanceContract firstDuelist, DuelistPersistanceContract secondDuelist) {
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

    public interface DuelistPersistanceContract {
        string Id { get; }
        bool IsNull { get; }
    }
}