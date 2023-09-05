using Restarted.Generators.Common.Context;
using Restarted.Generators.Generators.BaseModels;
using Restarted.Generators.Processor.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Restarted.Generators.Generators.MapperProfiles
{
    public class MapperProfileTemplateParameter : ITemplateParameter
    {


        public MapperProfileTemplateParameter(string preferredNameSpace, string sourceFileName, Dictionary<string,string> targetSourceMap, bool isMethodGeneration)
        {
            PreferredNameSpace = preferredNameSpace;
            SourceFileName = sourceFileName;
            TargetSourceMap = targetSourceMap;

          IsMethodGeneration = isMethodGeneration;
        }
        public string SourceFileName { get; set; }
        public string PreferredNameSpace { get; set; }
        public Dictionary<string,string> TargetSourceMap { get; set; }

        public bool IsMethodGeneration { get; set; }


    }


}
