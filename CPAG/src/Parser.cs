using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    public class Parser
    {
        public const char EndOfStream = '\0';
        public const char StartOfBlock = '{';
        public const char EndOfBlock = '}';
        public const char StartOfParamaters = '(';
        public const char EndOfParamaters = ')';
        public const char ParamaterSeparator = ',';

        public enum Heads
        {
            EndOfStream,

            BaseStats,

            ActivatedAbility,

            Cost,
            Constraint,
            Effect,
            Target,
        }

        private Generator Generator;
        private ParseStream ParseStream;

        public Parser(ParseStream parseStream)
        {
            ParseStream = parseStream;
        }

        public string Parse()
        {
            Generator = new Generator();

            while (true)
            {
                var head = ParseHead();
                if (head == Heads.EndOfStream) { break; }
                ParseBody(head);
            }

            return Generator.Collect();
        }

        private Heads ParseHead()
        {
            var eatenString = ParseStream.EatWord();
            if (eatenString == null) { return Heads.EndOfStream; }

            Heads head;
            if (!Heads.TryParse(eatenString, out head))
            {
                throw new ParseException(ParseStream, "Expected block name.");
            }

            return head;
        }

        private void ParseBody(Heads head)
        {
            switch (head)
            {
                // paramaters
                case Heads.Cost:
                case Heads.Constraint:
                case Heads.Effect:
                case Heads.Target:
                {
                    ParseParamaterBlock(head);
                } break;

                case Heads.BaseStats:
                {
                    ParseBaseStatsBlock();
                } break;

                case Heads.ActivatedAbility:
                {
                    ParseActivatedAbilityBlock();
                } break;

                case Heads.EndOfStream:
                {
                } break;

                default:
                {
                    throw new Exception("Illegal fallthrough");
                }
            }
        }

        private void ParseBaseStatsBlock()
        {
            var text = ParseStream.EatBlock();
            Generator.GenerateBaseStatsBlock(text);
        }

        private void ParseActivatedAbilityBlock()
        {
            var abilityName = ParseStream.EatWord();
            var paramaterList = ParseStream.EatParamaterList();
            Generator.GenerateActivatedAbility(abilityName, paramaterList);
        }

        private void ParseParamaterBlock(Heads paramaterHead)
        {
            var paramaterName = ParseStream.EatWord();
            var paramaterBody = ParseStream.EatBlock();
            var param = ParseParamater(paramaterHead, paramaterName, paramaterBody);
            Generator.GenerateParamater(param);
        }

        private Paramater ParseParamater(
            Heads   paramaterHead, 
            string  paramaterName, 
            string  paramaterBody)
        {
            switch (paramaterHead)
            {
                case Heads.Cost:
                {
                    return new ParamaterCost(paramaterName, paramaterBody);
                } 
                case Heads.Constraint:
                {
                    return new ParamaterConstraint(paramaterName, paramaterBody);
                } 
                case Heads.Effect:
                {
                    return new ParamaterEffect(paramaterName, paramaterBody);
                }

                case Heads.Target:
                {
                    return new ParamaterTarget(paramaterName, paramaterBody);
                }

                default:
                {
                    throw new Exception("bad");
                }
            }
        }
    }
}
