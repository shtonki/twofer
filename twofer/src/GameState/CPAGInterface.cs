﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{

    public delegate void CPAGCodeBlock(CPAGToolbox cpagToolbox);
    public delegate bool CPAGConstraint(CPAGToolbox cpagToolbox);
    public delegate bool CPAGEffect(CPAGToolbox cpagToolbox);
    public delegate bool CPAGTarget(CPAGToolbox cpagToolbox);
    public delegate bool CPAGCost(CPAGToolbox cpagToolbox);

    public class CPAGToolbox
    {
        public void Constrain(bool constraint)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }
    }
    
    public partial class Card
    {
        // Functions that must be generated by CPAG
        protected abstract void CPAGResetStats(CPAGToolbox CPAG);


        // Member making functions exposed to CPAG

        protected ActivatedAbility  MakeActivatedAbility(Cost cost, Effect effect, Constraint castable, Target[] targets)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

        protected Effect            MakeEffect(CPAGCodeBlock block)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

        protected Cost              MakeCost(CPAGCodeBlock block)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

        protected Constraint        MakeConstraint(CPAGCodeBlock block)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }

        protected Target            MakeTarget(CPAGCodeBlock block)
        {
            throw new NotImplementedException("if you weren't expecting too see this you might be in some trouble son");
        }
    }
}
