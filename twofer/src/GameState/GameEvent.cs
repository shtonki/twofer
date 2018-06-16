using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{
    /// <summary>
    /// Represents a unit of happening in the game.
    /// E.g. a card moving from one pile to another, a card taking damage, a card 
    /// declaring an attack, a spell being cast, a mana orb being depleted...
    /// Anything that affects gamestate in basically.
    /// </summary>
    public abstract class GameEvent
    {
    }

    /// <summary>
    /// Represents a set of GameEvents that happen at the same time.
    /// </summary>
    public class GameTransaction
    {
        public IEnumerable<GameEvent> Events { get; }

        public GameTransaction(IEnumerable<GameEvent> events)
        {
            Events = events;
        }
    }

    public class GameEventMoveToPile : GameEvent
    {
        public Pile Pile { get; }
        public Card Card { get; }
    }
}
