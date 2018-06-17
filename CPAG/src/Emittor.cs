using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    class Emittor
    {
        private StringBuilder Collector = new StringBuilder();
        private int IndentationLevel = 0;

        public string Collect()
        {
            return Collector.ToString();
        }

        public void Indent()
        {
            IndentationLevel++;
        }

        public void Deindent()
        {
            IndentationLevel--;
        }

        public void EmitOpeningBracketAndIndent()
        {
            EmitLine("{");
            Indent();
        }

        public void DeindentAndEmitClosingBracket()
        {
            Deindent();
            EmitLine("}");
        }

        public void EmitLine(string emission)
        {
            for (int i = 0; i < IndentationLevel; i++)
            {
                Collector.Append("    ");
            }
            Collector.AppendLine(emission);
        }

        public void EmitBlock(Block block)
        {
            if (!string.IsNullOrEmpty(block.Comment))
            {
                EmitLine("// " + block.Comment);
            }
            EmitLine(block.Content);
            EmitLine("");
        }
    }
}
