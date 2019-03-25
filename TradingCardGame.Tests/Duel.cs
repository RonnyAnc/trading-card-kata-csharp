using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TradingCardGame.Tests {
    public class Duel {
        private readonly List<DomainEvent> events = new List<DomainEvent>();

        public Duel(string id) {
            
        }

        public ReadOnlyCollection<DomainEvent> Events => this.events.AsReadOnly();
    }
}