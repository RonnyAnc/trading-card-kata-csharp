namespace TradingCardGame.DuelAggregate.State {
    public class DuelState {
        public string Id { get; }
        public DuelistState FirstDuelist { get; }
        public DuelistState SecondDuelist { get; }
        public TurnState Turn { get; set; }

        public DuelState(string id, DuelistState firstDuelist, DuelistState secondDuelist, TurnState turn) {
            Id = id;
            FirstDuelist = firstDuelist;
            SecondDuelist = secondDuelist;
            Turn = turn;
        }
    }
}