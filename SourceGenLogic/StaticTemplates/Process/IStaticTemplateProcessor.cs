using Restarted.Generators.Common.Context;
using Restarted.Generators.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restarted.Generators.FeatureProcessors.Process
{
    public interface IStaticTemplateProcessor
    {
        string Process(TypeOfTemplate codeType, ITemplateParameter parameter);
        string Transform(Dictionary<string, string> replacementParameters);
    }
}
