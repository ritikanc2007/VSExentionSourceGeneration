using Restarted.Generators.Common.Context;
using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Restarted.Generators.Generators.GlobalUsings
{
    public class GlobalUsingTemplateParameter : ITemplateParameter
    {


        public GlobalUsingTemplateParameter(string sourceFileName, HashSet<string> uniqueNamespaces, bool isMethodGeneration)
        {
            SourceFileName = sourceFileName;
            UniqueNamespaces = uniqueNamespaces;
            IsMethodGeneration = isMethodGeneration;
          
        }
        public string SourceFileName { get; set; }
        public string PreferredNameSpace { get; set; }
        public HashSet<string> UniqueNamespaces { get; set; }


        public bool IsMethodGeneration { get; set; }

    }


}
