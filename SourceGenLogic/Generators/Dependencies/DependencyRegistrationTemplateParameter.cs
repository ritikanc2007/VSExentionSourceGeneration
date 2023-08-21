
using Restarted.Generators.Common.Context;
using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Restarted.Generators.Generators.Dependencies
{
    internal class DependencyRegistrationTemplateParameter : ITemplateParameter
    {


        public DependencyRegistrationTemplateParameter(string preferredNameSpace, string sourceFileName, Dictionary<string, string> targetSourceMap)
        {
            PreferredNameSpace = preferredNameSpace;
            SourceFileName = sourceFileName;
            TargetSourceMap = targetSourceMap;


        }
        public string SourceFileName { get; set; }
        public string PreferredNameSpace { get; set; }
        public Dictionary<string, string> TargetSourceMap { get; set; }




    }


}
