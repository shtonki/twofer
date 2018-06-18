using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    class Generator
    {
        public const string CPAGEnvironmentType = "CPAGToolbox";
        public const string CPAGEnvironmentName = "CPL";
        public const string CPAGEnvironmentFunctionArgument = 
            CPAGEnvironmentType + " " + CPAGEnvironmentName;
        public const string CPAGMemberPrototype =
            "private void";

        public Member BaseStatsFunction { get; set; }
        public const string BaseStatsPrototype = 
            "protected override void " + BaseStatsFunctionHandle + 
            "(" + CPAGEnvironmentFunctionArgument + ")";
        public const string BaseStatsFunctionHandle = "CPAGResetStats";


        private MemberCollection Members { get; } = new MemberCollection();

        private Emittor Emittor { get; set; }

        private struct ParamaterStruct
        {
            public Parser.Heads Head { get; }
            public string Handle { get; }


            /// <summary>
            /// Takes a string of format e.g. "Effect CastEffect" and separates them
            /// </summary>
            /// <param name="raw"></param>
            public ParamaterStruct(string raw) : this()
            {
                var splits = raw.Split(' ');

                if (splits.Length == 2)
                {
                    Parser.Heads temp;
                    if (Parser.Heads.TryParse(splits[0], out temp))
                    {
                        Head = temp;
                        Handle = splits[1].Trim();
                        return;
                    }
                }

                throw new GenerationException("Bad paramater format '" + raw + "'");
            }
        }

        public void GenerateBaseStatsBlock(string body)
        {
            if (BaseStatsFunction != null)
            {
                throw new GenerationException(
                    "Found multiple BaseStats blocks.");
            }

            var baseStatsBlock = new Block("{0} {{ {1} }}", BaseStatsPrototype, body);
            baseStatsBlock.Comment = "BaseStats Declaration";
            BaseStatsFunction = new Member(
                Member.MemberType.Function, BaseStatsFunctionHandle, 
                baseStatsBlock, Block.Empty);
            Members.Add(BaseStatsFunction);
        }

        public void GenerateActivatedAbility(
            string              abilityHandle, 
            IEnumerable<string> paramaterList)
        {
            var paramArray = paramaterList.Select(raw => new ParamaterStruct(raw)).ToArray();

            string costHandle = null;
            string constraintHandle = null;
            string effectHandle = null;

            foreach (var paramater in paramArray)
            {
                switch (paramater.Head)
                {
                    case Parser.Heads.Cost:
                    {
                        if (costHandle != null) { throw new GenerationException("bad"); }
                        costHandle = paramater.Handle;
                    } break;

                    case Parser.Heads.Effect:
                    {
                        if (effectHandle != null) { throw new GenerationException("bad"); }
                        effectHandle = paramater.Handle;
                    } break;

                    case Parser.Heads.Constraint:
                    {
                        if (constraintHandle != null) { throw new GenerationException("bad"); }
                        constraintHandle = paramater.Handle;
                    } break;
                }
            }

            if (costHandle == null)
            {
                // todo support for multiple Costs
                throw new GenerationException(
                    "ActivatedAbilities requires exactly one Cost paramater.");
            }

            if (paramArray.Count(p => p.Head == Parser.Heads.Cost) != 1)
            {
                throw new GenerationException(
                    "ActivatedAbilities requires exactly one Constraint paramater.");
            }


            // build target array
            var targetParamaters = paramArray.Where(p => p.Head == Parser.Heads.Target);
            StringBuilder sb = new StringBuilder();
            sb.Append("new Target[] { ");
            foreach (var target in targetParamaters)
            {
                sb.Append(target.Handle);
                sb.Append(", ");
            }
            sb.Append("}");
            string targetList = sb.ToString();

            Block abilityDefinition = new Block(
                "{0} = MakeActivatedAbility({1}, {2}, {3}, {4});",
                abilityHandle, costHandle, effectHandle, constraintHandle, targetList
                );
            Block abilityDeclaration = new Block(
                "private ActivatedAbility {0};", abilityHandle);
            var abilityMember = new Member(
                Member.MemberType.Ability, abilityHandle, abilityDeclaration, abilityDefinition);

            // add members
            Members.Add(abilityMember);
        }

        public void GenerateParamater(Paramater paramater)
        {
            Members.Add(paramater.ToMember());
        }

        public string NAMESPACE      { get; } = @"twofer.src.GameState";
        public string CLASSNAME      { get; } = @"Shibaby";

        public string Collect()
        {
            Emittor = new Emittor();
            EmitFile();
            return Emittor.Collect();
        }

        private void EmitFile()
        {
            // emit usings

            EmitNamespace();
        }

        private void EmitNamespace()
        {
            Emittor.EmitLine("namespace " + NAMESPACE);
            Emittor.EmitOpeningBracketAndIndent();

            EmitClass();

            Emittor.DeindentAndEmitClosingBracket();
        }

        private void EmitClass()
        {
            Emittor.EmitLine("class " + CLASSNAME + " : Card");
            Emittor.EmitOpeningBracketAndIndent();

            EmitConstructor();

            foreach (var member in Members.All)
            {
                Emittor.EmitBlock(member.Declaration);
            }

            Emittor.DeindentAndEmitClosingBracket();
        }

        private void EmitConstructor()
        {
            //  prototype
            Emittor.EmitLine("public " + CLASSNAME + "(GameState gameState) : base(gameState)");
            Emittor.EmitOpeningBracketAndIndent();

            // member definitions
            foreach (var member in Members.All)
            {
                Emittor.EmitBlock(member.Definition);
            }

            // BaseAbilities array
            Emittor.EmitLine("BaseAbilities = new[]");
            Emittor.EmitOpeningBracketAndIndent();
            foreach (var abilityHandle in Members.AbilityNames)
            {
                Emittor.EmitLine(abilityHandle + ",");
            }
            Emittor.DeindentAndEmitClosingBracket();
            Emittor.EmitLine(";");

            Emittor.DeindentAndEmitClosingBracket();
        }
    }

    /// <summary>
    /// Represents a member in the class being generated
    /// </summary>
    public class Member
    {
        public enum MemberType
        {
            Cost,
            Constraint,
            Effect,
            Ability,
            Function,
            Target,
        }

        public MemberType Type { get; }

        /// <summary>
        /// The name by which the member can be refered
        /// </summary>
        public string Handle { get; }

        /// <summary>
        /// The code block containing the declaration of the member
        /// </summary>
        public Block Declaration { get; }

        /// <summary>
        /// The code block containing the definition of the member
        /// </summary>
        public Block Definition { get; }

        public Member(MemberType type, string handle, Block declaration, Block definition)
        {
            Type = type;
            Handle = handle;
            Declaration = declaration;
            Definition = definition;
        }
    }

    class MemberCollection
    {
        public IEnumerable<Member> All => Members;
        public IEnumerable<string> AbilityNames => AbilityHandles;

        private List<Member> Members { get; } = new List<Member>();
        private List<string> AbilityHandles { get; } = new List<string>();

        public void Add(Member member)
        {
            if (Members.Any(m => m.Handle == member.Handle))
            {
                throw new GenerationException("too lazy for real message");
            }

            if (member.Type == Member.MemberType.Ability)
            {
                AbilityHandles.Add(member.Handle);
            }

            Members.Add(member);
        }
    }

}
