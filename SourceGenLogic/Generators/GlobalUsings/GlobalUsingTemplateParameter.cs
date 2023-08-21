using Restarted.Generators.Common.Context;
using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Restarted.Generators.Generators.GlobalUsings
{
    internal class GlobalUsingTemplateParameter : ITemplateParameter
    {


        public GlobalUsingTemplateParameter(string sourceFileName, HashSet<string> uniqueNamespaces)
        {
            SourceFileName = sourceFileName;
            UniqueNamespaces = uniqueNamespaces;

          
        }
        public string SourceFileName { get; set; }
        public string PreferredNameSpace { get; set; }
        public HashSet<string> UniqueNamespaces { get; set; }

          


    }


}
