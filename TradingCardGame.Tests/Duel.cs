using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework.Constraints;

namespace TradingCardGame.Tests {
    public class Duel {
        private readonly string id;
        private readonly List<DomainEvent> events = new List<DomainEvent>();

        private Duel(string id) {
            this.id = id;
        }

        public ReadOnlyCollection<DomainEvent> Events => this.events.AsReadOnly();

        public static Duel Call(string id) {
            var duel = new Duel(id);
            duel.events.Add(new DuelCalled(id));
            return duel;
        }
    }
}