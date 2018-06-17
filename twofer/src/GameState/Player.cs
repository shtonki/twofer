using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{
    public class Player : GameObject
    {
        public bool IsHero;

        public Player(GameState game) : base(game)
        {
        }
    }
}
