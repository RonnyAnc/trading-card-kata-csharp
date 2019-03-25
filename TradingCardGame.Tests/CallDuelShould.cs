using System;
using NSubstitute;
using NUnit.Framework;

namespace TradingCardGame.Tests {
    public class CallDuelShould {
        [Test]
        public void publish_a_duel_called_event_when_new_duel_is_created() {
            var callDuel = new CallDuel();
            var duelCalledObserver = Substitute.For<IObserver<DuelCalled>>();
            var callDuelHandler = new CallDuelHandler(duelCalledObserver);

            callDuelHandler.Execute(callDuel);

            duelCalledObserver.Received(1).OnNext(Arg.Any<DuelCalled>());
        }
    }

    public class CallDuelHandler {
        public CallDuelHandler(IObserver<DuelCalled> eventPublisher) {
            
        }

        public void Execute(CallDuel callDuel) {
        }
    }

    public class CallDuel { }
}