using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{

    /// <summary>
    /// Represents a block of code
    /// </summary>
    class Block
    {
        public string Content { get; }

        public Block(string content)
        {
            Content = content;
        }
    }
}
