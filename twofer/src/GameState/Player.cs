using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{
    class Player : GameObject
    {
        public bool IsHero;

        public Player(GameState game) : base(game)
        {
        }
    }
}
