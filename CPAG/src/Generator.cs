using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPAG.src
{
    class Generator
    {
        private Block BaseStatsBlock { get; set; }
        private Emittor Emittor { get; set; }
        
        public void EmitBaseStatsBlock(Block body)
        {
            if (BaseStatsBlock != null)
            {
                throw new GenerationError("Reemission of BaseStatsBlock.");
            }

            BaseStatsBlock = body;
        }


        string NAMESPACE = @"twofer.src.GameState";
        string CLASSNAME = @"Shibaby";

        public string Generate()
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

            EmitFunctionResetStats();

            Emittor.DeindentAndEmitClosingBracket();
        }

        private void EmitFunctionResetStats()
        {
            Emittor.EmitLine("public override void ResetStats()");
            Emittor.EmitOpeningBracketAndIndent();
            Emittor.EmitLine("// BaseStatsBlock");
            Emittor.EmitLine(BaseStatsBlock.Content);
            Emittor.DeindentAndEmitClosingBracket();
        }
    }
}
