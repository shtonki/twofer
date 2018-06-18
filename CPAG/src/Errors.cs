using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    // compiler valium
    [Serializable]
    class ParseException : Exception
    {
        public ParseStream ParseStream;

        public ParseException(ParseStream parseStream, string message) : base(CShartSyntaxIsBest(parseStream, message))
        {
            ParseStream = parseStream;
        }

        private static string CShartSyntaxIsBest(ParseStream parseStream, string message)
        {
            return String.Format("{0}\nat line:{2} col:{3}\n{1}", message, parseStream.GenerateContextString(10), parseStream.CurrentLine, parseStream.CurrentColumn);
        }
    }

    class GenerationException : Exception
    {
        public GenerationException(string message) : base(message)
        {
        }
    }
}
