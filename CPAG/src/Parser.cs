using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    class Parser
    {
        private ParseStream ParseStream;


        private enum Heads
        {
            EndOfStream,

            BaseStats,
        }

        public Parser(ParseStream parseStream)
        {
            ParseStream = parseStream;
        }

        private Generator Generator;

        public void Parse()
        {
            Generator = new Generator();

            while (true)
            {
                var head = ParseHead();
                if (head == Heads.EndOfStream) { break; }
                ParseBody(head);
            }

            var output = Generator.Generate();
            Console.WriteLine(output);
            File.WriteAllText("out.cs", output);
        }

        private Heads ParseHead()
        {
            var eatenString = ParseStream.EatWord();
            if (eatenString == null) { return Heads.EndOfStream; }

            Heads head;
            if (!Heads.TryParse(eatenString, out head))
            {
                throw new ParseError(ParseStream, "Expected block name.");
            }

            return head;
        }

        private void ParseBody(Heads head)
        {
            switch (head)
            {
                case Heads.BaseStats:
                {
                    Generator.EmitBaseStatsBlock(ParseBaseStatsBlock());
                } break;

                case Heads.EndOfStream:
                {
                } break;

                default:
                {
                    throw new Exception("Illegal fallthrough");
                } break;
            }
        }

        private Block ParseBaseStatsBlock()
        {
            var text = ParseStream.EatBlock();
            return new Block(text);
        }
    }
}
