using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twofer.src.GameState
{
    public abstract class GameObject
    {
        public int GameUniqueID { get; }

        public GameObject(GameState game)
        {

        }
    }
}
