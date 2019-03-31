namespace TradingCardGame.DuelAggregate.State {
    public class DuelState {
        public DuelistState FirstDuelist { get; }
        public DuelistState SecondDuelist { get; }
        public TurnState Turn { get; set; }

        public DuelState(DuelistState firstDuelist, DuelistState secondDuelist, TurnState turn) {
            FirstDuelist = firstDuelist;
            SecondDuelist = secondDuelist;
            Turn = turn;
        }
    }
}