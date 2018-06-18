using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    class Program
    {
        public const string OutputFileName = "card.cs";

        static void Main(string[] args)
        {
            var parseStream = new ParseStream("../../exampleCards/basic.card");
            var parser = new Parser(parseStream);
            var text = parser.Parse();
            File.WriteAllText(OutputFileName, text);
        }
    }
}
