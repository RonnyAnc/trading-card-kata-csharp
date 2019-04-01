﻿using System.Collections.Generic;
using System.Linq;
using TradingCardGame.DuelAggregate.Events;
using TradingCardGame.DuelAggregate.State;

namespace TradingCardGame.DuelAggregate {
    public class Duel : AggregateRoot {
        private readonly string id;
        private readonly Turn turn;
        private readonly Duelist firstDuelist;
        private readonly Duelist secondDuelist;

        public DuelState State => new DuelState(firstDuelist, secondDuelist, turn);

        private Duel(string id, string firstDuelist, string secondDuelist, string turn) {
            this.id = id;
            this.firstDuelist = new Duelist(firstDuelist);
            this.secondDuelist = new Duelist(secondDuelist);
            this.turn = new Turn(turn);
        }

        private Duel(string duelId, Duelist firstDuelist, Duelist secondDuelist, string turn) {
            this.id = duelId;
            this.firstDuelist = firstDuelist;
            this.secondDuelist = secondDuelist;
            this.turn = new Turn(turn);
        }

        public static Duel Start(string id, string firstDuelist, string secondDuelist) {
            var duel = new Duel(id, firstDuelist, secondDuelist, firstDuelist);
            duel.Start();
            return duel;
        }

        private void Start() {
            DomainEvents.Add(new DuelStarted(id));
            DomainEvents.Add(new DuelistTurnStarted(id, firstDuelist.Id));
            SetManaSlots();
            RefillMana();
        }

        private void RefillMana() {
            firstDuelist.RefillMana();
            DomainEvents.Add(new ManaRefilled(id, turn.DuelistId, 1));
        }

        private void SetManaSlots() {
            firstDuelist.IncrementManaSlot();
            DomainEvents.Add(new ManaSlotSet(id, turn.DuelistId, 1));
        }

        public static Duel Start(string duelId, DuelistState firstDuelist, DuelistState secondDuelist) {
            var duel = new Duel(duelId, new Duelist(firstDuelist.Id, firstDuelist.Deck), new Duelist(secondDuelist.Id, secondDuelist.Deck), firstDuelist.Id);
            duel.Start();
            duel.DrawInitialHand();
            return duel;
        }

        private void DrawInitialHand() {
            // TODO: feature envy
            firstDuelist.Deck.Cards.RemoveRange(0, 3);
            firstDuelist.Hand.AddRange(new List<CardState> {new Card(1,1), new Card(1, 1) , new Card(1, 1) });
        }
    }

    internal class Card : CardState {
        public int ManaCost { get; }
        public int Damage { get; }
        public Card(int manaCost, int damage) {
            ManaCost = manaCost;
            Damage = damage;
        }
    }
}