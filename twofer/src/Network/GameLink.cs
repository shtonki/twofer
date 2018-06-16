using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twofer.src.GameState;

namespace twofer.src.Network
{
    interface GameLink
    {
        void SendGameObject(GameObject gameObject);
        GameObject ReceiveGameObject();
    }
}
