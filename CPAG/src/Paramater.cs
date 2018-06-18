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

        protected Member ToMemberBoilerplate(Member.MemberType type)
        {
            string memberType = type.ToString();
            string functionName = Name + "Script";
            string makeFunctionName = "Make" + memberType;
            string prototype = Generator.CPAGMemberPrototype;
            string arguments = Generator.CPAGEnvironmentFunctionArgument;


            Block declaration = new Block(
                "{0} {1} ({2})\n" +
                "{{\n" +
                "{3}" +
                "}}\n" +
                "private {4} {5};",
                prototype, functionName, arguments, 
                Body, 
                memberType, Name
                );

            Block definition = new Block(
                "{0} = {1}( {2} );",
                Name, makeFunctionName, functionName
                );

            return new Member(type, Name, declaration, definition);
        }
    }

    public class ParamaterTarget : Paramater
    {
        public ParamaterTarget(string name, string body) : base(name, body)
        {
        }

        public override Member ToMember()
        {
            return ToMemberBoilerplate(Member.MemberType.Target);
        }
    }

    public class ParamaterCost : Paramater
    {
        public ParamaterCost(string name, string body) : base(name, body)
        {

        }

        public override Member ToMember()
        {
            return ToMemberBoilerplate(Member.MemberType.Cost);
        }
    }

    public class ParamaterConstraint : Paramater
    {
        public ParamaterConstraint(string name, string body) : base(name, body)
        {
        }

        public override Member ToMember()
        {
            return ToMemberBoilerplate(Member.MemberType.Constraint);
        }
    }

    public class ParamaterEffect : Paramater
    {
        public ParamaterEffect(string name, string body) : base(name, body)
        {

        }

        public override Member ToMember()
        {
            return ToMemberBoilerplate(Member.MemberType.Effect);
        }
    }
}
