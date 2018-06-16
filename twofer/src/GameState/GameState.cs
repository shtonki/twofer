using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{
    public class GameState
    {
        private IEnumerable<Card> AllCards;
        private IEnumerable<Card> FieldCards;

        private GameStack GameStack;

        private Player[] AllPlayers;

        public void CastAbility(AbilityActivation abilityActivation)
        {
            GameStack.CastAbility(abilityActivation);

            // MISSING generate GameEvent for triggered abilities ?
        }

        private void HandleGameTransaction(GameTransaction gameTransaction)
        {
            foreach (var card in FieldCards)
            {
                card.CheckTriggeredAbilities(gameTransaction.Events);
                // MISSING place them on stack
            }

            foreach (var gameEvent in gameTransaction.Events)
            {
                HandleGameEvent(gameEvent);
            }
        }

        private void HandleGameEvent(GameEvent gameEvent)
        {
            if (gameEvent is GameEventMoveToPile)
            {
                HandleGameEventMoveToPile(gameEvent as GameEventMoveToPile);
            }
        }

        private void HandleGameEventMoveToPile(GameEventMoveToPile gevent)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }
    }
}
