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

    }

    // doesn't belong here but i don\t want to make a file now
    public class Constraint
    {
        
    }

    public class Target
    {
        
    }
}

namespace twofer.src.GameState
{
    class Shibaby : Card
    {
        public Shibaby(GameState gameState) : base(gameState)
        {


            CastCost = MakeCost(CastCostScript);

            ThisPowerOverOne = MakeConstraint(ThisPowerOverOneScript);

            CastEffect = MakeEffect(CastEffectScript);

            CastTarget0 = MakeTarget(CastTarget0Script);

            CastTarget1 = MakeTarget(CastTarget1Script);

            CastAbility = MakeActivatedAbility(CastCost, CastEffect, ThisPowerOverOne, new Target[] { CastTarget0, CastTarget1, });

            BaseAbilities = new[]
            {
                CastAbility,
            }
            ;
        }
        // BaseStats Declaration
        protected override void CPAGResetStats(CPAGToolbox CPL)
        {
            Name = "Shibaby";
            Power = 4;
            Toughness = 4;
        }

        private void CastCostScript(CPAGToolbox CPL)
        {

            //CPL.ManaCost(ManaOrb.Order);
        }
        private Cost CastCost;

        private void ThisPowerOverOneScript(CPAGToolbox CPL)
        {

            //CPL.Constrain(this.Power > 1);
        }
        private Constraint ThisPowerOverOne;

        private void CastEffectScript(CPAGToolbox CPL)
        {

            //CPL.DrawCards(this.Owner, 2);
        }
        private Effect CastEffect;

        private void CastTarget0Script(CPAGToolbox CPL)
        {

            //CPL.Target.Creature(c => c.IsWhite);
        }
        private Target CastTarget0;

        private void CastTarget1Script(CPAGToolbox CPL)
        {

            //CPL.Target.Player();
        }
        private Target CastTarget1;

        private ActivatedAbility CastAbility;

    }
}
