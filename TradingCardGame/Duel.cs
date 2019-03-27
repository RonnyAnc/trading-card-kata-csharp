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

        public ReadOnlyCollection<DomainEvent> Events => this.events.AsReadOnly();

        public static Duel Call(string id) {
            var duel = new Duel(id);
            duel.events.Add(new DuelCalled(id));
            return duel;
        }

        public static Duel Rebuild(string id) {
            return new Duel(id);
        }

        public void AddDuelist(string duelistId) {
            duelists.Add(duelistId);
            events.Add(new DuelistJoined(id, duelistId));
            if (duelists.Count == 2) 
                events.Add(new AllDuelistsJoined(id));
        }
    }
}