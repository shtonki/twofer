using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{
    public abstract class ManaOrb
    {
        public static ManaOrb Life => null;
        public static ManaOrb Order => null;
        public static ManaOrb Nature => null;
        public static ManaOrb Death => null;
        public static ManaOrb Chaos => null;
        public static ManaOrb Valor => null;
        public static ManaOrb Colourless => null;
    }                         

    public enum GameColours
    {
        Life,
        Order,
        Nature,
        Death,
        Chaos,
        Valor,
        Colourless,
    }
}
