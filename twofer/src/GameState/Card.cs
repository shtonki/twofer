using System.Collections.Generic;
using System;

namespace twofer.src.GameState
{
    public abstract partial class Card : GameObject
    {
        public IntegerStat Power        { get; protected set; } = new IntegerStat(0);
        public IntegerStat Toughness    { get; protected set; } = new IntegerStat(0);
        public IntegerStat Movement     { get; protected set; } = new IntegerStat(0);

        public StringStat Name          { get; protected set; } = new StringStat("noname");

        protected Ability[] BaseAbilities { get; set; }

        protected Card(GameState game) : base(game)
        {

        }

        public IEnumerable<AbilityActivation> CheckTriggeredAbilities(IEnumerable<GameEvent> gameEvents)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

    }

    // doesn't belong here but i don\t want to make a file now
    public class Constraint
    {
        
    }
}

