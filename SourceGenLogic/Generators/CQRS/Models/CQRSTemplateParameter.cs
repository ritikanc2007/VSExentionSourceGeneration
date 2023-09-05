using Restarted.Generators.Definitions;
using Restarted.Generators.Processor.Interfaces;
using SourceGeneratorParser.Models.Metadata;
using SourceGeneratorParser.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restarted.Generators.Generators.Repositories.Service.Models
{
    public class CQRSTemplateParameter : ITemplateParameter
    {

       
        public CQRSTemplateParameter(string nameSpace, string sourceFileName, TypeDefinitionInfo typeDefinitionInfo, MethodItemInfo currentMethod,string cqrsRequestName,string pluralEntityName,string requestType, bool isMethodGeneration)
        {

            SourceFileName = sourceFileName;
            PreferredNameSpace = nameSpace;
            TypeDefinitionInfo = typeDefinitionInfo;
            CurrentMethod = currentMethod;
            CQRSRequestName=cqrsRequestName;
            PluralEntityName = pluralEntityName;
            IsMethodGeneration = isMethodGeneration;
        }

        public string PluralEntityName { get; set; }
        public string CQRSRequestName { get; set; }
        public string PreferredNameSpace { get; set; }
        public TypeDefinitionInfo TypeDefinitionInfo { get; set;}

        public string RequestType { get; set; }
        public MethodItemInfo CurrentMethod { get; set; }


        public bool IsMethodGeneration { get; set; }
        public string SourceFileName { get; set; }
    }

}
