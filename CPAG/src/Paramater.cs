using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CPAG.src
{

    public abstract class Paramater
    {
        public string Name { get; }
        public string Body { get; }

        protected Paramater(string name, string body)
        {
            Name = name;
            Body = body;
        }

        public abstract Member ToMember();

        protected Member x(Member.MemberType type)
        {
            string memberType = type.ToString();
            string functionName = Name + "Script";
            string makeFunctionName = "Make" + memberType;

            Block declaration = new Block(
                "{0} {1} ({3})\n" +
                "{{\n" +
                "{2}" +
                "}}\n" +
                "private {5} {4};",
                Generator.CPAGMemberPrototype, functionName, 
                Body, Generator.CPAGEnvironmentFunctionArgument, Name, memberType);

            Block definition = new Block(
                "{0} = {2}( {1} );",
                Name, functionName, makeFunctionName);
            return new Member(type, Name, declaration, definition);
        }
    }

    public class ParamaterCost : Paramater
    {
        public ParamaterCost(string name, string body) : base(name, body)
        {

        }

        public override Member ToMember()
        {
            return x(Member.MemberType.Cost);
            Block declaration = new Block("private Cost {0};", Name);
            Block definition = new Block("{0} = {1}", Name, Body);
            return new Member(Member.MemberType.Cost, Name, declaration, definition);
        }
    }

    public class ParamaterConstraint : Paramater
    {
        public ParamaterConstraint(string name, string body) : base(name, body)
        {
        }

        public override Member ToMember()
        {
            return x(Member.MemberType.Constraint);
            string functionName = Name + "Script";
            Block declaration = new Block(
                "{0} {1} ({3})\n" +
                "{{\n" +
                "{2}" +
                "}}\n" +
                "private Constraint {4};",
                Generator.CPAGMemberPrototype, functionName, Body, Generator.CPAGEnvironmentFunctionArgument, Name);
            Block definition = new Block(
                "{0} = MakeContraint( {1} );",
                Name, functionName);
            return new Member(Member.MemberType.Constraint, Name, declaration, definition);
        }
    }

    public class ParamaterEffect : Paramater
    {
        public ParamaterEffect(string name, string body) : base(name, body)
        {

        }

        public override Member ToMember()
        {
            return x(Member.MemberType.Effect);
            Block declaration = new Block(
                "private Effect {0};", Name);

            Block definition = new Block(
                "{0} = MakeEffect( {2} =>\n" +
                "{{\n" +
                "{1}" +
                "}});", 
                Name, Body, Generator.CPAGEnvironmentName);

            return new Member(Member.MemberType.Effect, Name, declaration, definition);
        }
    }
}
