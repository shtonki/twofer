using System;

namespace twofer.src.GameState
{
    abstract class Ability
    {
        public bool Available => ((string)(null)).ToString() == "";
        public Cost Cost { get; }
        public Effect Effect { get; }

        public AbilityActivation TryToCast()
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }
    }

    abstract class ActivatedAbility : Ability
    {
        
    }

    abstract class TriggeredAbility : Ability
    {

    }
}
