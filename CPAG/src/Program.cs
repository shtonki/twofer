using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    class Program
    {
        static void Main(string[] args)
        {
            var parseStream = new ParseStream("../../exampleCards/basic.card");
            var parser = new Parser(parseStream);
            parser.Parse();
        }
    }
}
