using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    class ParseError : Exception
    {
        public ParseStream ParseStream;

        public ParseError(ParseStream parseStream, string message) : base(CShartSyntaxIsBest(parseStream, message))
        {
            ParseStream = parseStream;
        }

        private static string CShartSyntaxIsBest(ParseStream parseStream, string message)
        {
            return String.Format("{0}\n{1}", message, parseStream.GenerateContextString(10));
        }
    }

    class GenerationError : Exception
    {
        public GenerationError(string message) : base(message)
        {
        }
    }
}
