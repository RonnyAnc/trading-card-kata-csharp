using System.Collections.Generic;
using System.Collections.ObjectModel;
using LanguageExt;

namespace TradingCardGame {
    public abstract class AggregateRoot {
        protected List<DomainEvent> DomainEvents = new List<DomainEvent>();
        public ReadOnlyCollection<DomainEvent> Events => DomainEvents.AsReadOnly();
    }
}