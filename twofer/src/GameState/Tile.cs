using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{
    public class Tile : GameObject
    {
        public Pawn Pawn { get; }

        public Tile(GameState game) : base(game)
        {
        }
    }
}
