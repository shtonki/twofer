using System.Collections.Generic;
using System;

namespace twofer.src.GameState
{
    public abstract class Card : GameObject
    {
        public IntegerStat Power        { get; protected set; } = new IntegerStat(0);
        public IntegerStat Toughness    { get; protected set; } = new IntegerStat(0);
        public IntegerStat Movement     { get; protected set; } = new IntegerStat(0);

        public StringStat Name { get; protected set; } = new StringStat("noname");

        protected Card(GameState game) : base(game)
        {

        }

        public IEnumerable<AbilityActivation> CheckTriggeredAbilities(IEnumerable<GameEvent> gameEvents)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

        // CPAG Interface
        protected abstract void CPAGResetStats();
        //protected abstract IEnumerable<AbilityActivation> CPAGCheckTriggeredAbilities(GameEvent gevent);
        //protected abstract void CPAGUpdateStats();
    }
}

