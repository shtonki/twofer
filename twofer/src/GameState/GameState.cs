using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
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
