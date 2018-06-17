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
    public class Block
    {
        public string Content { get; }
        public string Comment { get; set; }

        public static Block Empty => new Block("");

        public Block(string content, params object[] formatArgs)
        {
            if (formatArgs.Length > 0)
            {
                Content = String.Format(content, formatArgs);
            }
            else
            {
                Content = content;
            }
        }
    }
}
